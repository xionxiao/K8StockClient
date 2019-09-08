using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
namespace stockapp
{
    public partial class Form3 : Form
    {
        public int m_StockNum = 100;
        public string ip_hq = "123.57.232.178:80";
        public string ip_jy = "10.10.135.92:8888";
        private string ordernum = "";
        public JObject json1 = new JObject();
        public JObject json2 = new JObject();
        delegate void Reflist(JArray ja);
        delegate void Reflist2(JObject jo);
       
        public Form3()
        {
            InitializeComponent();

        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void 报价ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Thread t = new Thread(ThreadFun);
            //t.IsBackground = true;
            //t.Start();
            ThreadFun();

        }
        public void ThreadFun()
        {
            Form1 f1 = new Form1(ip_hq, ip_jy, json1, this,json2);
            f1.TopMost = true;
            f1.Show();
        }

        private void 设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4(ip_hq, ip_jy);
            f4.ShowDialog();
            ip_hq = f4.ip_hangqing;
            ip_jy = f4.ip_jiaoyi;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.listView1.Columns.Add("时间", 80);
            this.listView1.Columns.Add("代码", 60);
            this.listView1.Columns.Add("名称", 80);
            this.listView1.Columns.Add("价格", 70);
            this.listView1.Columns.Add("股数", 60);
            this.listView1.Columns.Add("方向", 90);
            this.listView1.Columns.Add("委托编号", 80);
            this.listView1.Columns.Add("状态", 60);

            this.listView2.Columns.Add("证券代码", 80);
            this.listView2.Columns.Add("证券名称", 80);
            this.listView2.Columns.Add("仓位方向", 100);
            this.listView2.Columns.Add("证券数量", 80);
            //this.listView2.Columns.Add("持仓均价", 80);
            //this.listView2.Columns.Add("参考盈亏", 100);
            //this.listView2.Columns.Add("盈亏比例", 80);

            Control.CheckForIllegalCrossThreadCalls = false;

            listView1.DoubleBuffering(true);
            listView2.DoubleBuffering(true);
   
            Thread thread = new Thread(Run);
            thread.IsBackground = true;
            thread.Start();
            JObject jo = new JObject();
           
        }
        private void refresh_list1(JArray ja)
        {
            //是否为创建控件的线程，不是为true
            if (this.listView1.InvokeRequired)
            {
                //为当前控件指定委托
                this.listView1.Invoke(new Reflist(refresh_list1), ja);
            }
            else
            {
                int pos = -1;
                listView1.Items.Clear();
                for (int i = 0; i < ja.Count; i++)
                {
                    ListViewItem item1 = new ListViewItem();

                    item1.SubItems[0].Text = ja[i]["委托时间"].ToString();

                    item1.SubItems.Add(ja[i]["证券代码"].ToString());

                    item1.SubItems.Add(ja[i]["证券名称"].ToString());

                    item1.SubItems.Add(ja[i]["委托价格"].ToString());

                    item1.SubItems.Add(ja[i]["委托数量"].ToString());

                    item1.SubItems.Add(ja[i]["买卖标志"].ToString());
                    if (ja[i]["买卖标志"].ToString().Equals("买入担保品"))
                    {
                        item1.ForeColor = Color.Red;
                    }
                    else if (ja[i]["买卖标志"].ToString().Equals("卖出担保品"))
                    {
                        item1.ForeColor = RGB(0x65E339);
                    }

                    item1.SubItems.Add(ja[i]["合同编号"].ToString());
                    if (ordernum.Equals(ja[i]["合同编号"].ToString()))
                    {
                        item1.BackColor = Color.Blue;
                        pos = i;
                    }
                    item1.SubItems.Add(ja[i]["状态说明"].ToString());

                    listView1.Items.Add(item1);

                }
                //if (pos == -1)
                    listView1.EnsureVisible(listView1.Items.Count - 1);
                //else
                //    listView1.EnsureVisible(pos);
            }
        }
       
        
        private void refresh_list2(JObject ja)
        {
            //是否为创建控件的线程，不是为true
            if (this.listView2.InvokeRequired)
            {
                //为当前控件指定委托
                this.listView2.Invoke(new Reflist2(refresh_list2), ja);
            }
            else
            {
                listView2.Items.Clear();
               // JsonReader jr = new JsonReader();
                foreach (JProperty jp in ja.Properties())
                {
                    ListViewItem item1 = new ListViewItem();

                    item1.SubItems[0].Text = ja[jp.Name]["证券代码"].ToString() ;

                    item1.SubItems.Add(ja[jp.Name]["证券名称"].ToString());

                    item1.SubItems.Add(ja[jp.Name]["买卖标志"].ToString());

                    item1.SubItems.Add(ja[jp.Name]["成交数量"].ToString());

                    if (ja[jp.Name]["买卖标志"].ToString().Contains("买入"))
                    {
                        item1.ForeColor = Color.Red;
                    }
                    else if (ja[jp.Name]["买卖标志"].ToString().Contains("卖出"))
                    {
                        item1.ForeColor = RGB(0x65E339);;
                    }
                    //MessageBox.Show(jp.ToString());
                    listView2.Items.Add(item1);

                }

            }
        }
        public void Run()
        {
            while (true)
            {
                string str = "http://" + ip_jy + "/query?catalogues=orderlist";

                string str2 = GetRequestData(str);

                if (str2 == null)
                {
                    Thread.Sleep(1000);
                    goto l2;
                }
                JObject jo = (JObject)JsonConvert.DeserializeObject(str2);
                string[] values = jo.Properties().Select(item => item.Value.ToString()).ToArray();

                if (values[0] == "[]" || jo["error"] != null)
                {
                    Thread.Sleep(1000);
                    goto l2;
                }

                JArray ja = (JArray)JsonConvert.DeserializeObject(values[0]);

                refresh_list1(ja);

                //////////////////////////////////
                //////////////////////////////////
                //////////////////////////////////

            l2:
                str = "http://" + ip_jy + "/query?catalogues=deals";

                str2 = GetRequestData(str);

                if (str2 == null)
                {
                    Thread.Sleep(1000);
                    goto l3;
                }


                jo = (JObject)JsonConvert.DeserializeObject(str2);

                values = jo.Properties().Select(item => item.Value.ToString()).ToArray();

                if (values[0] == "[]" || jo["error"] != null)
                {
                    Thread.Sleep(1000);
                    goto l3;
                }



                ja = (JArray)JsonConvert.DeserializeObject(values[0]);



                refresh_list2(filter_data(ja));
            l3:
                /////////////////////////////////
                /////////////////////////////////
                /////////////////////////////////

                 str = "http://" + ip_jy + "/query?catalogues=position";

                str2 = GetRequestData(str);

                if (str2 == null)
                {
                    Thread.Sleep(1000);
                    goto l4;
                }

                jo = (JObject)JsonConvert.DeserializeObject(str2);

                values = jo.Properties().Select(item => item.Value.ToString()).ToArray();

                if (values[0] == "[]" || jo["error"] != null)
                {
                    Thread.Sleep(1000);
                    goto l4;
                }
                ja = (JArray)JsonConvert.DeserializeObject(values[0]);

                for (int i = 0; i < ja.Count; i++)
                {
                    json1[ja[i]["证券代码"].ToString()] = ja[i]["可卖数量"].ToString();
                }

                /////////////////////////////
                /////////////////////////////
                /////////////////////////////

            l4:
                str = "http://" + ip_jy + "/query?catalogues=stockpool";

                str2 = GetRequestData(str);

                if (str2 == null)
                {
                    Thread.Sleep(1000);
                    continue;
                }

                jo = (JObject)JsonConvert.DeserializeObject(str2);

                values = jo.Properties().Select(item => item.Value.ToString()).ToArray();

                if (values[0] == "[]" || jo["error"] != null)
                {
                    Thread.Sleep(1000);
                    continue;
                }
                JObject jo1= (JObject)JsonConvert.DeserializeObject(values[0]);
                json2.RemoveAll();
                foreach (JProperty jp in jo1.Properties())
                {
                    json2[jp.Name] = jo1[jp.Name]["融券数量"].ToString();
                }
                Thread.Sleep(3000);
            }
        }
        private JObject filter_data(JArray ja)
        {
            JObject jo= new JObject();
          //  int index = 0;
            int num = ja.Count;
            JArray jacopy = new JArray();
            for (int i = 0; i < num; i++)
            {
                if (ja[i]["状态说明"].ToString().Contains("普通成交"))
                {

                    jacopy.Add(ja[i]);
                  //  index++;
                }
            }
            for (int i = 0; i < jacopy.Count; i++)
                {
                    if (jo[jacopy[i]["证券代码"].ToString()] == null)
                        jo[jacopy[i]["证券代码"].ToString()] = jacopy[i];

                    else
                    {
                        int num1=0, num2=0;//买入+ 卖出-

                        //这个时候和原来的比较
                        if (jo[jacopy[i]["证券代码"].ToString()]["买卖标志"].ToString().Contains("买入"))
                        {

                            num1 = int.Parse(jo[jacopy[i]["证券代码"].ToString()]["成交数量"].ToString());
                        }
                        else if (jo[jacopy[i]["证券代码"].ToString()]["买卖标志"].ToString().Contains("卖出"))
                        {
                            num1 = -1 * int.Parse(jo[jacopy[i]["证券代码"].ToString()]["成交数量"].ToString());
                        }

                        if (jacopy[i]["买卖标志"].ToString().Contains("买入"))
                        {
                            num2 = int.Parse(jacopy[i]["成交数量"].ToString());
                        }
                        else if(jacopy[i]["买卖标志"].ToString().Contains("卖出"))
                        {
                            num2 = -1 * int.Parse(jacopy[i]["成交数量"].ToString());
                        }

                        if ( num1 + num2 > 0)
                        {
                            jo[jacopy[i]["证券代码"].ToString()]["买卖标志"] = "买入";
                        //    jo[jacopy[i]["证券代码"].ToString()]["买卖标志"].ToString().Replace("卖出", "买入");
                            jo[jacopy[i]["证券代码"].ToString()]["成交数量"] = (num1 + num2).ToString();

                        }
                        else if (num1 + num2 < 0)
                        {
                            jo[jacopy[i]["证券代码"].ToString()]["买卖标志"] = "卖出";
                             //   jo[jacopy[i]["证券代码"].ToString()]["买卖标志"].ToString().Replace("买入", "卖出");
                            jo[jacopy[i]["证券代码"].ToString()]["成交数量"] = (-1 * (num1 + num2)).ToString();
                        }
                        else
                        {

                            jo.Remove(jacopy[i]["证券代码"].ToString());

                        }
                    }
                }

           
            return jo;
        }
        //private void refresh_list2()
        //{
        //    string str1 = "http://" + ip_jy + "/query?catalogues=position";
        //    //string str2 = "http://" + ip_jy + "/query?catalogues=stockpool";
        //    string str3 = GetRequestData(str1);
        //    //string str4 = GetRequestData(str2);
        //    if (str3 == null)
        //        return;
        //    JObject jo = (JObject)JsonConvert.DeserializeObject(str3);
        //    string[] values = jo.Properties().Select(item => item.Value.ToString()).ToArray();

        //    if (values[0] == "[]" || jo["error"] != null)
        //    {
        //        return;
        //    }

        //    JArray ja = (JArray)JsonConvert.DeserializeObject(values[0]);

        // //   listView2.Items.Clear();

        //    for (int i = 0; i < ja.Count; i++)
        //    {
        //        //ListViewItem item1 = new ListViewItem();

        //        //item1.SubItems[0].Text = ja[i]["证券代码"].ToString();

        //        //item1.SubItems.Add(ja[i]["证券名称"].ToString());

        //        //item1.SubItems.Add("买入");

        //        //item1.SubItems.Add(ja[i]["证券数量"].ToString());

        //        //item1.SubItems.Add(ja[i]["买入成本价"].ToString());

        //        //item1.SubItems.Add(ja[i]["浮动盈亏"].ToString());

        //        //item1.SubItems.Add(ja[i]["盈亏比例(%)"].ToString());
        //        json[ja[i]["证券代码"].ToString()] = ja[i]["可卖数量"].ToString();
        //        //listView2.Items.Add(item1);

        //    }
        //    //listView2.EnsureVisible(listView2.Items.Count - 1);

        //    // jo = (JObject)JsonConvert.DeserializeObject(str4);
        //    // values = jo.Properties().Select(item => item.Value.ToString()).ToArray();

        //    //if (values[0] == "[]" || jo["error"] != null)
        //    //{
        //    //    return;
        //    //}

        //    //ja = (JArray)JsonConvert.DeserializeObject(values[0]);
        //}
        //private void refresh_list1()
        //{


        //    string str = "http://" + ip_jy + "/query?catalogues=orderlist";



        //    string str2 = GetRequestData(str);

        //    if (str2 == null)
        //        return;

        //    JObject jo = (JObject)JsonConvert.DeserializeObject(str2);
        //    string[] values = jo.Properties().Select(item => item.Value.ToString()).ToArray();

        //    if (values[0] == "[]" || jo["error"] != null)
        //    {
        //        return;
        //    }

        //    listView1.Items.Clear();

        //    JArray ja = (JArray)JsonConvert.DeserializeObject(values[0]);

        //    int pos = -1;
        //    for (int i = 0; i < ja.Count; i++)
        //    {
        //        ListViewItem item1 = new ListViewItem();

        //        item1.SubItems[0].Text = ja[i]["委托时间"].ToString();

        //        item1.SubItems.Add(ja[i]["证券代码"].ToString());

        //        item1.SubItems.Add(ja[i]["证券名称"].ToString());

        //        item1.SubItems.Add(ja[i]["委托价格"].ToString());

        //        item1.SubItems.Add(ja[i]["委托数量"].ToString());

        //        item1.SubItems.Add(ja[i]["买卖标志"].ToString());
        //        if (ja[i]["买卖标志"].ToString().Equals("买入担保品"))
        //        {
        //            item1.ForeColor = Color.Red;
        //        }
        //        else if (ja[i]["买卖标志"].ToString().Equals("卖出担保品"))
        //        {
        //            item1.ForeColor = RGB(0x65E339);
        //        }

        //        item1.SubItems.Add(ja[i]["合同编号"].ToString());
        //        if (ordernum.Equals(ja[i]["合同编号"].ToString()))
        //        {
        //            item1.BackColor = Color.Blue;
        //           // pos = i;
        //        }

        //        item1.SubItems.Add(ja[i]["状态说明"].ToString());

        //        if (ja[i]["状态说明"].ToString().Equals("未报") || ja[i]["状态说明"].ToString().Equals("已报"))
        //        {
        //            listView1.Items.Add(item1);
        //            pos++;
        //        }
        //    }
            
        //        listView1.EnsureVisible(pos);
            
           

        //    //    Thread.Sleep(2000);
        //    //}

        //}
        private Color RGB(int color)
        {
            int r = 0xFF & color;
            int g = 0xFF00 & color;
            g >>= 8;
            int b = 0xFF0000 & color;
            b >>= 16;
            return Color.FromArgb(r, g, b);
        }
        public static string GetRequestData(string sUrl)
        {
            //MessageBox.Show(sUrl);
            //使用HttpWebRequest类的Create方法创建一个请求到uri的对象。
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(sUrl);
            //指定请求的方式为Get方式
            request.Method = WebRequestMethods.Http.Get;
            request.Timeout = 1000;
            //获取该请求所响应回来的资源，并强转为HttpWebResponse响应对象
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());

                string str = reader.ReadToEnd();
                response.Close();
                return str;
            }
            catch
            {
                return null;
            }


        }

        //private void timer1_Tick(object sender, EventArgs e)
        //{

        //    refresh_list1();
        //    refresh_list2();

        //}

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (listView1.SelectedIndices.Count != 0)
                ordernum = listView1.SelectedItems[0].SubItems[6].Text;

        }

       

        private void Form3_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Escape)
            {
                string stock = "";
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    if (ordernum.Equals(listView1.Items[i].SubItems[6].Text))
                    {
                        stock = listView1.Items[i].SubItems[1].Text;
                    }
                }


                string str = "http://" + ip_jy + "/cancel?stock=" + stock + "&order=" + ordernum;
                string ret = GetRequestData_Post(str);

            }
        }
        public static string GetRequestData_Post(string sUrl)
        {
            //MessageBox.Show(sUrl);
            //使用HttpWebRequest类的Create方法创建一个请求到uri的对象。
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(sUrl);
            //指定请求的方式为Get方式
            request.Method = WebRequestMethods.Http.Post;
            request.Timeout = 1000;
            //获取该请求所响应回来的资源，并强转为HttpWebResponse响应对象
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());

                string str = reader.ReadToEnd();
                response.Close();
                return str;
            }
            catch
            {
                return null;
            }


        }
        protected void refresh_control()
        {
            string str = "http://" + ip_jy + "/query?catalogues=orderlist";

            string str2 = GetRequestData(str);

            if (str2 == null)
            {
                Thread.Sleep(1000);
                goto l2;
            }
            JObject jo = (JObject)JsonConvert.DeserializeObject(str2);
            string[] values = jo.Properties().Select(item => item.Value.ToString()).ToArray();

            if (values[0] == "[]" || jo["error"] != null)
            {
                Thread.Sleep(1000);
                goto l2;
            }

            JArray ja = (JArray)JsonConvert.DeserializeObject(values[0]);

            refresh_list1(ja);

                //////////////////////////////////
        //////////////////////////////////
        //////////////////////////////////

            l2:
            str = "http://" + ip_jy + "/query?catalogues=deals";

            str2 = GetRequestData(str);

            if (str2 == null)
            {
                Thread.Sleep(1000);
                goto l3;
            }


            jo = (JObject)JsonConvert.DeserializeObject(str2);

            values = jo.Properties().Select(item => item.Value.ToString()).ToArray();

            if (values[0] == "[]" || jo["error"] != null)
            {
                Thread.Sleep(1000);
                goto l3;
            }



            ja = (JArray)JsonConvert.DeserializeObject(values[0]);



            refresh_list2(filter_data(ja));
        l3:
            /////////////////////////////////
            /////////////////////////////////
            /////////////////////////////////

            str = "http://" + ip_jy + "/query?catalogues=position";

            str2 = GetRequestData(str);

            if (str2 == null)
            {
                Thread.Sleep(1000);
                return;
            }

            jo = (JObject)JsonConvert.DeserializeObject(str2);

            values = jo.Properties().Select(item => item.Value.ToString()).ToArray();

            if (values[0] == "[]" || jo["error"] != null)
            {
                Thread.Sleep(1000);
                return;
            }
            ja = (JArray)JsonConvert.DeserializeObject(values[0]);

            for (int i = 0; i < ja.Count; i++)
            {
                json1[ja[i]["证券代码"].ToString()] = ja[i]["可卖数量"].ToString();
            }

            /////////////////////////////
            /////////////////////////////
            /////////////////////////////

            str = "http://" + ip_jy + "/query?catalogues=stockpool";

            str2 = GetRequestData(str);

            if (str2 == null)
            {
                Thread.Sleep(1000);
                return;
            }

            jo = (JObject)JsonConvert.DeserializeObject(str2);

            values = jo.Properties().Select(item => item.Value.ToString()).ToArray();

            if (values[0] == "[]" || jo["error"] != null)
            {
                Thread.Sleep(1000);
                return;
            }
            JObject jo1 = (JObject)JsonConvert.DeserializeObject(values[0]);
            json2.RemoveAll();
            foreach (JProperty jp in jo1.Properties())
            {
                json2[jp.Name] = jo1[jp.Name]["融券数量"].ToString();
            }
        }
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x63:
                     Win32API.My_lParam ml = new Win32API.My_lParam();
                        Type t = ml.GetType();
                        ml = (Win32API.My_lParam)m.GetLParam(t);
                    if(ml.s!=null)
                        listBox1.Items.Add(ml.s);
                   refresh_control();
                    break;

            }
            base.WndProc(ref m);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
    public static class ControlExtensions
    {
        public static void DoubleBuffering(this Control control, bool enable)
        {
            var doubleBufferPropertyInfo
                = control.GetType().GetProperty("DoubleBuffered",
                                   BindingFlags.Instance | BindingFlags.NonPublic);
            doubleBufferPropertyInfo.SetValue(control, enable, null);
        }
    }
}
