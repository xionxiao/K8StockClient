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
    public partial class Form_main : Form
    {
        public int m_StockNum = 100;
        public string ip_hq = "123.57.232.178:80";
        public string ip_jy = "10.10.135.92:8888";
        private string ordernum = "";
        public JObject json1 = new JObject();
        public JObject json2 = new JObject();
        delegate void Reflist(JArray ja);
        delegate void Reflist2(JObject jo);
        public Form_main()
        {
            InitializeComponent();

        }
        private void BaojiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_hangqi f1 = new Form_hangqi(ip_hq, ip_jy, json1, this, json2);
            f1.TopMost = true;
            f1.Show();

        }
        private void SetToolStripMenuItem_Click(object sender, EventArgs e)
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
            if (ja == null || ja.Count == 0)
                return;
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
                        item1.ForeColor = Form_hangqi.RGB(0x65E339);
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
            if (ja == null || ja.Count == 0)
                return;
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
                        item1.ForeColor = Form_hangqi.RGB(0x65E339);;
                    }
                    //MessageBox.Show(jp.ToString());
                    listView2.Items.Add(item1);

                }

            }
        }
        private JArray str_to_jarray(string str)
        {
            if (str == null)
                return null;
            try
            {
                JObject jo = (JObject)JsonConvert.DeserializeObject(str);
                if (jo.Count == 0 || jo["orderlist"] == null)
                    return null;
                JArray ja = (JArray)JsonConvert.DeserializeObject(jo["orderlist"].ToString());
                return ja;
            }
            catch
            {
                return null;
            }
         }
        private JArray str1_to_jarray(string str)
        {
            if (str == null)
                return null;
            try
            {
                JObject jo = (JObject)JsonConvert.DeserializeObject(str);
                if (jo.Count == 0 || jo["deals"] == null)
                    return null;
                JArray ja = (JArray)JsonConvert.DeserializeObject(jo["deals"].ToString());
                return ja;
            }
            catch
            {
                return null;
            }
        }
        private JArray str2_to_jarray(string str)
        {
            if (str == null)
                return null;
            try
            {
                JObject jo = (JObject)JsonConvert.DeserializeObject(str);
                if (jo.Count == 0 || jo["position"] == null)
                    return null;
                JArray ja = (JArray)JsonConvert.DeserializeObject(jo["position"].ToString());
                return ja;
            }
            catch
            {
                return null;
            }
        }
        private void get_sell_nums(JArray ja)
        {
            json1.RemoveAll();
            for (int i = 0; i < ja.Count; i++)
            {
                json1[ja[i]["证券代码"].ToString()] = ja[i]["可卖数量"].ToString();
            }
        }
        private JObject str3_to_jobject(string str)
        {
            if (str == null)
                return null;
            try
            {
                JObject jo = (JObject)JsonConvert.DeserializeObject(str);
                if (jo.Count == 0 || jo["stockpool"] == null)
                    return null;
                JObject jo1 = (JObject)JsonConvert.DeserializeObject(jo["stockpool"].ToString());
                return jo1;
            }
            catch
            {
                return null;
            }
        }
        private void get_rongquan_nums(JObject jo)
        {
            json2.RemoveAll();
            foreach (JProperty jp in jo.Properties())
            {
                json2[jp.Name] = jo[jp.Name]["融券数量"].ToString();
            }
        }
        public void Run()
        {
            while (true)
            {
                string temp = "http://" + ip_jy + "/query?catalogues=orderlist";
                string str = Form_hangqi.GetRequestData(temp);
                JArray ja= str_to_jarray(str);
                refresh_list1(ja);

                temp = "http://" + ip_jy + "/query?catalogues=deals";
                string str1 =Form_hangqi.GetRequestData(temp);
                ja = str1_to_jarray(str1);
                refresh_list2(filter_data(ja));
                   
                temp = "http://" + ip_jy + "/query?catalogues=position";
                string str2 =Form_hangqi.GetRequestData(temp);
                ja = str2_to_jarray(str2);
                get_sell_nums(ja);

                str = "http://" + ip_jy + "/query?catalogues=stockpool";
                string str3 = Form_hangqi.GetRequestData(str);
                JObject jo = str3_to_jobject(str3);
                get_rongquan_nums(jo); 
               
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
                string ret =Form_hangqi.GetRequestData_Post(str);

            }
        }    
        protected void refresh_control()
        {
            string temp = "http://" + ip_jy + "/query?catalogues=orderlist";
            string str = Form_hangqi.GetRequestData(temp);
            JArray ja = str_to_jarray(str);
            refresh_list1(ja);

            temp = "http://" + ip_jy + "/query?catalogues=deals";
            string str1 = Form_hangqi.GetRequestData(temp);
            ja = str1_to_jarray(str1);
            refresh_list2(filter_data(ja));
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
