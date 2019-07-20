using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Runtime.Serialization;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace stockapp
{
    public partial class Form1 : Form
    {
        public int m_No=0;
        public int m_Num = 100;
        public Form1(int num )
        {
            InitializeComponent();
            m_Num = num;
        }

        public static string UnicodeToGB(string text)
        {
            try
            {
                System.Text.RegularExpressions.MatchCollection mc = System.Text.RegularExpressions.Regex.Matches(text, "\\\\u([\\w]{4})");
                if (mc != null && mc.Count > 0)
                {
                    foreach (System.Text.RegularExpressions.Match m2 in mc)
                    {
                        string v = m2.Value;
                        string word = v.Substring(2);
                        byte[] codes = new byte[2];
                        int code = Convert.ToInt32(word.Substring(0, 2), 16);
                        int code2 = Convert.ToInt32(word.Substring(2), 16);
                        codes[0] = (byte)code2;
                        codes[1] = (byte)code;
                        text = text.Replace(v, Encoding.Unicode.GetString(codes));
                    }
                }
                else
                {

                }
            }
            catch {
                return text;
            }
            return text;
        }
       
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            { 
                if(textBox5.Focused)
                {
                    try
                    {
                        float price = float.Parse(textBox5.Text);
                        price += 0.01F;
                        textBox5.Text = price.ToString();
                    }
                    catch
                    {
                        return;
                    }
                    textBox5.Focus();
                    textBox5.SelectAll();
                }
                if (textBox6.Focused)
                {
                    try
                    {
                        int num = int.Parse(textBox6.Text);
                        num += 100;
                        textBox6.Text = num.ToString();
                    }
                    catch
                    {
                        return;
                    }
                    textBox6.Focus();
                    textBox6.SelectAll();
                }
            }
            if (e.KeyCode == Keys.Down)
            {
                if (textBox5.Focused)
                {
                    try
                    {
                        double price = double.Parse(textBox5.Text);
                        if (price < 0.01)
                            return;
                        price -= 0.01;
                        textBox5.Text = price.ToString();
                    }
                    catch
                    {
                        return;
                    }
                }
                if (textBox6.Focused)
                {
                    try
                    {
                        int num = int.Parse(textBox6.Text);
                        if (num < 100)
                            return;
                        num -= 100;
                        textBox6.Text = num.ToString();
                    }
                    catch
                    {
                        return;
                    }
                }

            }
            if (e.KeyCode == Keys.Enter  && textBox1.Focused)
            {
                timer1.Enabled = false;
                timer2.Enabled = false;
                timer3.Enabled = false;

                textBox1.Focus();
                textBox1.SelectAll();
                listView1.Clear();
                listView2.Clear();
                listView3.Clear();


                this.listView2.Columns.Add("Price", 45);
                this.listView2.Columns.Add("Q", 30);
                this.listView2.Columns.Add("D",20);
                this.listView2.Columns.Add("Time", 60);

                this.listView1.Columns.Add("ID", 45);
                this.listView1.Columns.Add("Price", 60);
                this.listView1.Columns.Add("Q", 72);

                this.listView3.Columns.Add("Price", 45);
                this.listView3.Columns.Add("Q", 45);
                this.listView3.Columns.Add("Time", 50);
                this.listView3.Columns.Add("C", 30);

                string temp;

                try
                {
                    m_No = int.Parse(textBox1.Text);
                    temp = m_No.ToString();
                    if ( m_No > 999999)
                    {
                        m_No = 0;
                        MessageBox.Show("请输入六位数!");
                        return;

                    }
                    if (m_No < 100000)
                    {
                        temp = string.Format("{0:D6}",m_No);
                    }

                    String str = GetRequestData("http://123.57.232.178/query?catalogues=transaction_detail&stock=" + temp);

                    String str1 = GetRequestData("http://123.57.232.178/query?catalogues=quote10&stocks=" + temp);

                    string str2 = GetRequestData("http://123.57.232.178/query?catalogues=transaction&stock=" + temp);
                    
                    
                    string str3 = UnicodeToGB(str);

                    string str4 = UnicodeToGB(str1);

                    string str5 = UnicodeToGB(str2);

                    //MessageBox.Show(str3);
                
                    JObject jo=(JObject)JsonConvert.DeserializeObject(str3);
                  
                    
                    string[] values =jo.Properties().Select(item => item.Value.ToString()).ToArray();

                    if (values[0] == "[]")
                    {
                        return;
                    }
                    JArray ja = (JArray)JsonConvert.DeserializeObject(values[0]);
                    for (int i = 0; i < ja.Count; i++)          
                    { 
                        ListViewItem item1 = new ListViewItem();
                        item1.UseItemStyleForSubItems = false;
                   

                        item1.SubItems[0].Text = ja[i]["价格"].ToString().Substring(0,
                            ja[i]["价格"].ToString().IndexOf(".")+3);
                        item1.SubItems.Add(ja[i]["成交量"].ToString().Substring(0,
                            ja[i]["成交量"].ToString().IndexOf(".")));
                        item1.SubItems.Add(ja[i]["性质"].ToString());

                        
                        item1.SubItems.Add(ja[i]["成交时间"].ToString());
                        if (ja[i]["性质"].ToString() == "S")
                        {
                            item1.SubItems[0].ForeColor = RGB(0x65E339);
                            item1.SubItems[1].ForeColor = RGB(0x65E339);
                            item1.SubItems[2].ForeColor = RGB(0x65E339);
                            item1.SubItems[3].ForeColor = RGB(0x65E339);
                        }
                        else
                        {
                            item1.SubItems[0].ForeColor = RGB(0x5C5CFF);
                            item1.SubItems[1].ForeColor = RGB(0x5C5CFF);
                            item1.SubItems[2].ForeColor = RGB(0x5C5CFF);
                            item1.SubItems[3].ForeColor = RGB(0x5C5CFF);
                        }
                        //3、将数据集item1添加到ListView控件  
                        listView2.Items.Add(item1);
                    }
                    //MessageBox.Show(str5);
                    JObject jo2 = (JObject)JsonConvert.DeserializeObject(str5);


                    string[] values2 = jo2.Properties().Select(item => item.Value.ToString()).ToArray();

                    if (values2[0] == "[]")
                    {
                        return;
                    }
                    JArray ja2 = (JArray)JsonConvert.DeserializeObject(values2[0]);
                    for (int i = 0; i < ja2.Count; i++)
                    {
                        ListViewItem item1 = new ListViewItem();

                        item1.UseItemStyleForSubItems = false;

                       


                        item1.SubItems[0].Text = ja2[i]["价格"].ToString().Substring(0,
                            ja2[i]["价格"].ToString().IndexOf(".") + 3);
                        int nq = int.Parse(ja2[i]["现量"].ToString());

                        item1.SubItems.Add(nq.ToString());
                       
                        item1.SubItems.Add(ja2[i]["时间"].ToString());

                        item1.SubItems.Add(ja2[i]["笔数"].ToString());

                        if (ja2[i]["买卖"].ToString() == "1")
                        {
                            item1.SubItems[0].ForeColor = RGB(0x65E339);
                            item1.SubItems[1].ForeColor = RGB(0x65E339);
                            item1.SubItems[2].ForeColor = RGB(0x65E339);
                            item1.SubItems[3].ForeColor = Color.White;
                        }
                        else
                        {
                            item1.SubItems[0].ForeColor = RGB(0x5C5CFF);
                            item1.SubItems[1].ForeColor = RGB(0x5C5CFF);
                            item1.SubItems[2].ForeColor = RGB(0x5C5CFF);
                            item1.SubItems[3].ForeColor = Color.White;
                        }
                        if (nq >= 500)
                        {
                            item1.SubItems[1].ForeColor = RGB(0xc000c0);
                        }
                        //3、将数据集item1添加到ListView控件  
                        listView3.Items.Add(item1);
                    }

                    JObject jo1 = (JObject)JsonConvert.DeserializeObject(str4);

                    string[] values1 = jo1.Properties().Select(item => item.Value.ToString()).ToArray();


                    if (values1[0] == "[]")
                    {
                        return;
                    }

                    JArray ja1 = (JArray)JsonConvert.DeserializeObject(values1[0]);
                 
                   
                    ListViewItem item2 ;
                    string s;

                    item2 = new ListViewItem();

                    double price = double.Parse(ja1[0]["昨收"].ToString());


                    item2.UseItemStyleForSubItems = false;
                    item2.SubItems[0].Text = "卖十";
                    s = ja1[0]["卖十价"].ToString();
                    item2.SubItems.Add(s.Substring(0, s.IndexOf(".") + 3));
                    if (double.Parse(s) > price)
                        item2.SubItems[1].ForeColor = RGB(0x5C5CFF);
                    else if (double.Parse(s) < price)
                        item2.SubItems[1].ForeColor = RGB(0x65E339);
                    else
                        item2.SubItems[1].ForeColor = Color.White;

                        item2.SubItems.Add(ja1[0]["卖十量"].ToString());
                    item2.SubItems[2].ForeColor = Color.White;
                    listView1.Items.Add(item2);
                
                      

                    item2 = new ListViewItem();
                    item2.UseItemStyleForSubItems = false;
                    item2.SubItems[0].Text = "卖九";
                    s = ja1[0]["卖九价"].ToString();
                    item2.SubItems.Add(s.Substring(0, s.IndexOf(".") + 3));
                    if (double.Parse(s) > price)
                        item2.SubItems[1].ForeColor = RGB(0x5C5CFF);
                    else if (double.Parse(s) < price)
                        item2.SubItems[1].ForeColor = RGB(0x65E339);
                    else
                        item2.SubItems[1].ForeColor = Color.White;
                    item2.SubItems.Add(ja1[0]["卖九量"].ToString());
                    item2.SubItems[2].ForeColor = Color.White;
                    listView1.Items.Add(item2);


                    item2 = new ListViewItem();
                    item2.UseItemStyleForSubItems = false;
                    item2.SubItems[0].Text = "卖八";
                    s = ja1[0]["卖八价"].ToString();
                    item2.SubItems.Add(s.Substring(0, s.IndexOf(".") + 3));
                    if (double.Parse(s) > price)
                        item2.SubItems[1].ForeColor = RGB(0x5C5CFF);
                    else if (double.Parse(s) < price)
                        item2.SubItems[1].ForeColor = RGB(0x65E339);
                    else
                        item2.SubItems[1].ForeColor = Color.White;
                    item2.SubItems.Add(ja1[0]["卖八量"].ToString());
                    item2.SubItems[2].ForeColor = Color.White;
                    listView1.Items.Add(item2);

                    item2 = new ListViewItem();
                    item2.UseItemStyleForSubItems = false;
                    s = ja1[0]["卖七价"].ToString();
                    item2.SubItems[0].Text = "卖七";
                    item2.SubItems.Add(s.Substring(0, s.IndexOf(".") + 3));
                    if (double.Parse(s) > price)
                        item2.SubItems[1].ForeColor = RGB(0x5C5CFF);
                    else if (double.Parse(s) < price)
                        item2.SubItems[1].ForeColor = RGB(0x65E339);
                    else
                        item2.SubItems[1].ForeColor = Color.White;
                    item2.SubItems.Add(ja1[0]["卖七量"].ToString());
                    item2.SubItems[2].ForeColor = Color.White;
                    listView1.Items.Add(item2);



                    item2 = new ListViewItem();
                    item2.UseItemStyleForSubItems = false;
                    item2.SubItems[0].Text = "卖六";
                    s = ja1[0]["卖六价"].ToString();
                    item2.SubItems.Add(s.Substring(0, s.IndexOf(".") + 3));
                    if (double.Parse(s) > price)
                        item2.SubItems[1].ForeColor = RGB(0x5C5CFF);
                    else if (double.Parse(s) < price)
                        item2.SubItems[1].ForeColor = RGB(0x65E339);
                    else
                        item2.SubItems[1].ForeColor = Color.White;
                    item2.SubItems.Add(ja1[0]["卖六量"].ToString());
                    item2.SubItems[2].ForeColor = Color.White;
                    listView1.Items.Add(item2);


                    item2 = new ListViewItem();
                    item2.UseItemStyleForSubItems = false;
                    item2.SubItems[0].Text = "卖五";
                    s = ja1[0]["卖五价"].ToString();
                    item2.SubItems.Add(s.Substring(0, s.IndexOf(".") + 3));
                    if (double.Parse(s) > price)
                        item2.SubItems[1].ForeColor = RGB(0x5C5CFF);
                    else if (double.Parse(s) < price)
                        item2.SubItems[1].ForeColor = RGB(0x65E339);
                    else
                        item2.SubItems[1].ForeColor = Color.White;
                    item2.SubItems.Add(ja1[0]["卖五量"].ToString());
                    item2.SubItems[2].ForeColor = Color.White;
                    listView1.Items.Add(item2);


                    item2 = new ListViewItem();
                    item2.UseItemStyleForSubItems = false;
                    item2.SubItems[0].Text = "卖四";
                    s = ja1[0]["卖四价"].ToString();
                    item2.SubItems.Add(s.Substring(0, s.IndexOf(".") + 3));
                    if (double.Parse(s) > price)
                        item2.SubItems[1].ForeColor = RGB(0x5C5CFF);
                    else if (double.Parse(s) < price)
                        item2.SubItems[1].ForeColor = RGB(0x65E339);
                    else
                        item2.SubItems[1].ForeColor = Color.White;
                    item2.SubItems.Add(ja1[0]["卖四量"].ToString());
                    item2.SubItems[2].ForeColor = Color.White;
                    listView1.Items.Add(item2);


                    item2 = new ListViewItem();
                    item2.UseItemStyleForSubItems = false;
                    item2.SubItems[0].Text = "卖三";
                    s = ja1[0]["卖三价"].ToString();
                    item2.SubItems.Add(s.Substring(0, s.IndexOf(".") + 3));
                    if (double.Parse(s) > price)
                        item2.SubItems[1].ForeColor = RGB(0x5C5CFF);
                    else if (double.Parse(s) < price)
                        item2.SubItems[1].ForeColor = RGB(0x65E339);
                    else
                        item2.SubItems[1].ForeColor = Color.White;
                    s = ja1[0]["卖三量"].ToString();
                    item2.SubItems.Add(s);
                    item2.SubItems[2].ForeColor = Color.White;
                    listView1.Items.Add(item2);

                    item2 = new ListViewItem();
                    item2.UseItemStyleForSubItems = false;
                    item2.SubItems[0].Text = "卖二";
                     s = ja1[0]["卖二价"].ToString();
                     item2.SubItems.Add(s.Substring(0, s.IndexOf(".") + 3));
                     if (double.Parse(s) > price)
                         item2.SubItems[1].ForeColor = RGB(0x5C5CFF);
                     else if (double.Parse(s) < price)
                         item2.SubItems[1].ForeColor = RGB(0x65E339);
                     else
                         item2.SubItems[1].ForeColor = Color.White;
                    s = ja1[0]["卖二量"].ToString();
                    item2.SubItems.Add(s);
                    item2.SubItems[2].ForeColor = Color.White;
                    listView1.Items.Add(item2);


                    item2 = new ListViewItem();
                    item2.UseItemStyleForSubItems = false;
                    item2.SubItems[0].Text = "卖一";
                    s=ja1[0]["卖一价"].ToString();
                    item2.SubItems.Add(s.Substring(0, s.IndexOf(".") + 3));
                    if (double.Parse(s) > price)
                        item2.SubItems[1].ForeColor = RGB(0x5C5CFF);
                    else if (double.Parse(s) < price)
                        item2.SubItems[1].ForeColor = RGB(0x65E339);
                    else
                        item2.SubItems[1].ForeColor = Color.White;
                    s= ja1[0]["卖一量"].ToString();
                    item2.SubItems.Add(s);
                    item2.SubItems[2].ForeColor = Color.White;
                    listView1.Items.Add(item2);







                    item2 = new ListViewItem();
                    item2.BackColor = Color.Gray;
                    listView1.Items.Add(item2);









                    item2 = new ListViewItem();
                    item2.UseItemStyleForSubItems = false;
                    item2.SubItems[0].Text = "买一";
                    s = ja1[0]["买一价"].ToString();
                    item2.SubItems.Add(s.Substring(0, s.IndexOf(".") + 3));
                    if (double.Parse(s) > price)
                        item2.SubItems[1].ForeColor = RGB(0x5C5CFF);
                    else if (double.Parse(s) < price)
                        item2.SubItems[1].ForeColor = RGB(0x65E339);
                    else
                        item2.SubItems[1].ForeColor = Color.White;
                    item2.SubItems.Add(ja1[0]["买一量"].ToString());
                    item2.SubItems[2].ForeColor = Color.White;
                    listView1.Items.Add(item2);

                    item2 = new ListViewItem();
                    item2.UseItemStyleForSubItems = false;
                    item2.SubItems[0].Text = "买二";
                    s = ja1[0]["买二价"].ToString();
                    item2.SubItems.Add(s.Substring(0, s.IndexOf(".") + 3));
                    if (double.Parse(s) > price)
                        item2.SubItems[1].ForeColor = RGB(0x5C5CFF);
                    else if (double.Parse(s) < price)
                        item2.SubItems[1].ForeColor = RGB(0x65E339);
                    else
                        item2.SubItems[1].ForeColor = Color.White;
                    item2.SubItems.Add(ja1[0]["买二量"].ToString());
                    item2.SubItems[2].ForeColor = Color.White;
                    listView1.Items.Add(item2);

                    item2 = new ListViewItem();
                    item2.UseItemStyleForSubItems = false;
                    item2.SubItems[0].Text = "买三";
                    s = ja1[0]["买三价"].ToString();
                    item2.SubItems.Add(s.Substring(0, s.IndexOf(".") + 3));
                    if (double.Parse(s) > price)
                        item2.SubItems[1].ForeColor = RGB(0x5C5CFF);
                    else if (double.Parse(s) < price)
                        item2.SubItems[1].ForeColor = RGB(0x65E339);
                    else
                        item2.SubItems[1].ForeColor = Color.White;
                    item2.SubItems.Add(ja1[0]["买三量"].ToString());
                    item2.SubItems[2].ForeColor = Color.White;
                    listView1.Items.Add(item2);


                    item2 = new ListViewItem();
                    item2.UseItemStyleForSubItems = false;
                    item2.SubItems[0].Text = "买四";
                    s = ja1[0]["买四价"].ToString();
                    item2.SubItems.Add(s.Substring(0, s.IndexOf(".") + 3));
                    if (double.Parse(s) > price)
                        item2.SubItems[1].ForeColor = RGB(0x5C5CFF);
                    else if (double.Parse(s) < price)
                        item2.SubItems[1].ForeColor = RGB(0x65E339);
                    else
                        item2.SubItems[1].ForeColor = Color.White;
                    item2.SubItems.Add(ja1[0]["买四量"].ToString());
                    item2.SubItems[2].ForeColor = Color.White;
                    listView1.Items.Add(item2);

                    item2 = new ListViewItem();
                    item2.UseItemStyleForSubItems = false;
                    item2.SubItems[0].Text = "买五";
                    s = ja1[0]["买五价"].ToString();
                    item2.SubItems.Add(s.Substring(0, s.IndexOf(".") + 3));
                    if (double.Parse(s) > price)
                        item2.SubItems[1].ForeColor = RGB(0x5C5CFF);
                    else if (double.Parse(s) < price)
                        item2.SubItems[1].ForeColor = RGB(0x65E339);
                    else
                        item2.SubItems[1].ForeColor = Color.White;
                    item2.SubItems.Add(ja1[0]["买五量"].ToString());
                    item2.SubItems[2].ForeColor = Color.White;
                    listView1.Items.Add(item2);

                    item2 = new ListViewItem();
                    item2.UseItemStyleForSubItems = false;
                    item2.SubItems[0].Text = "买六";
                    s = ja1[0]["买六价"].ToString();
                    item2.SubItems.Add(s.Substring(0, s.IndexOf(".") + 3));
                    if (double.Parse(s) > price)
                        item2.SubItems[1].ForeColor = RGB(0x5C5CFF);
                    else if (double.Parse(s) < price)
                        item2.SubItems[1].ForeColor = RGB(0x65E339);
                    else
                        item2.SubItems[1].ForeColor = Color.White;
                    item2.SubItems.Add(ja1[0]["买六量"].ToString());
                    item2.SubItems[2].ForeColor = Color.White;
                    listView1.Items.Add(item2);

                    item2 = new ListViewItem();
                    item2.UseItemStyleForSubItems = false;
                    item2.SubItems[0].Text = "买七";
                    s = ja1[0]["买七价"].ToString();
                    item2.SubItems.Add(s.Substring(0, s.IndexOf(".") + 3));
                    if (double.Parse(s) > price)
                        item2.SubItems[1].ForeColor = RGB(0x5C5CFF);
                    else if (double.Parse(s) < price)
                        item2.SubItems[1].ForeColor = RGB(0x65E339);
                    else
                        item2.SubItems[1].ForeColor = Color.White;
                    item2.SubItems.Add(ja1[0]["买七量"].ToString());
                    item2.SubItems[2].ForeColor = Color.White;
                    listView1.Items.Add(item2);

                    item2 = new ListViewItem();
                    item2.UseItemStyleForSubItems = false;
                    item2.SubItems[0].Text = "买八";
                    s = ja1[0]["买八价"].ToString();
                    item2.SubItems.Add(s.Substring(0, s.IndexOf(".") + 3));
                    if (double.Parse(s) > price)
                        item2.SubItems[1].ForeColor = RGB(0x5C5CFF);
                    else if (double.Parse(s) < price)
                        item2.SubItems[1].ForeColor = RGB(0x65E339);
                    else
                        item2.SubItems[1].ForeColor = Color.White;
                    item2.SubItems.Add(ja1[0]["买八量"].ToString());
                    item2.SubItems[2].ForeColor = Color.White;
                    listView1.Items.Add(item2);

                    item2 = new ListViewItem();
                    item2.UseItemStyleForSubItems = false;
                    item2.SubItems[0].Text = "买九";
                    s = ja1[0]["买九价"].ToString();
                    item2.SubItems.Add(s.Substring(0, s.IndexOf(".") + 3));
                    if (double.Parse(s) > price)
                        item2.SubItems[1].ForeColor = RGB(0x5C5CFF);
                    else if (double.Parse(s) < price)
                        item2.SubItems[1].ForeColor = RGB(0x65E339);
                    else
                        item2.SubItems[1].ForeColor = Color.White;
                    item2.SubItems.Add(ja1[0]["买九量"].ToString());
                    item2.SubItems[2].ForeColor = Color.White;
                    listView1.Items.Add(item2);

                    item2 = new ListViewItem();
                    item2.UseItemStyleForSubItems = false;
                    item2.SubItems[0].Text = "买十";
                    s = ja1[0]["买十价"].ToString();
                    item2.SubItems.Add(s.Substring(0, s.IndexOf(".") + 3));
                    if (double.Parse(s) > price)
                        item2.SubItems[1].ForeColor = RGB(0x5C5CFF);
                    else if (double.Parse(s) < price)
                        item2.SubItems[1].ForeColor = RGB(0x65E339);
                    else
                        item2.SubItems[1].ForeColor = Color.White;
                    item2.SubItems.Add(ja1[0]["买十量"].ToString());
                    item2.SubItems[2].ForeColor = Color.White;
                    listView1.Items.Add(item2);
                   
               

                    this.Text = ja1[0]["名称"].ToString();
                    
                    textBox7.Text = ja1[0]["现价"].ToString().Substring(0, 
                        ja1[0]["现价"].ToString().IndexOf(".")+3);

                    double now_price = Double.Parse(textBox7.Text);

                    double rate = (now_price - price) / price;
                    if (rate > 0)
                        richTextBox3.ForeColor = RGB(0x5C5CFF);
                    else
                        richTextBox3.ForeColor = Color.Green;

                    richTextBox3.Text = string.Format("{0:0.00%}", rate);


                    double amount = double.Parse(ja1[0]["总金额"].ToString());
                    amount /=10000;
                    textBox9.Text = string.Format("{0:N0}", amount)+" W";

                    textBox12.Text = ja1[0]["开盘"].ToString().Substring(0,
                        ja1[0]["开盘"].ToString().IndexOf(".") + 3);

                    textBox11.Text = ja1[0]["昨收"].ToString().Substring(0,
                        ja1[0]["昨收"].ToString().IndexOf(".") + 3);

                    double temp1 = price * 1.1;
                    this.richTextBox1.Text = string.Format("{0:F2}", temp1);
                  //  this.richTextBox1.SelectionBackColor = Color.RGB(0x5C5CFF);;
                    temp1 = price * 0.9;
                    this.richTextBox2.Text = string.Format("{0:F2}", temp1);
                    //this.richTextBox2.SelectionBackColor = RGB(0x65E339);

                    timer1.Enabled = true;
                    timer2.Enabled = true;
                    timer3.Enabled = true;
                }
                catch
                {
                    return;
                }
            }
          
            if (e.KeyCode == Keys.Enter && textBox6.Focused)
            {
                MessageBox.Show("提交订单........,这里是个demo");
                label4.Visible = false;
                textBox6.Visible = false;
                textBox6.Text = "";
                label6.Visible = false;
                textBox5.Visible = false;
                textBox5.Text = "";

            }
            if (e.KeyCode == Keys.Enter && textBox5.Focused)
            {
                MessageBox.Show("提交订单........,这里是个demo");
                label4.Visible = false;
                textBox6.Visible = false;
                textBox6.Text = "";
                label6.Visible = false;
                textBox5.Visible = false;
                textBox5.Text = "";

            }
       

            if (e.KeyCode == Keys.F1)
            {
                label4.Visible = true;
                textBox6.Visible = true;
            
                label6.Visible = true;
                textBox5.Visible = true;
            


                label3.Text = "F2池";
                label5.Text = "F3池";

                label4.Text = "买入价格";
                label4.BackColor = RGB(0x5C5CFF);;

                label6.Text = "股数";
                label6.BackColor = RGB(0x5C5CFF);;

                if (listView1.Items.Count > 0)
                {


                    if (listView1.Items[11].SubItems[1].Text == "0.00")
                        textBox5.Text = listView1.Items[9].SubItems[1].Text;
                    else
                        textBox5.Text = listView1.Items[11].SubItems[1].Text;
                    
                    textBox6.Text = m_Num.ToString();
                    textBox5.SelectAll();
                }
                else
                {
                    textBox5.Text = "";
                }
                textBox5.Focus();

               
            }

            if (e.KeyCode == Keys.F2)
            {


                label4.Visible = true;
                textBox6.Visible = true;

                label6.Visible = true;
                textBox5.Visible = true;


                label3.Text = "F2池";
                label5.Text = "";
                textBox4.Visible = false;


                label4.Text = "卖出价格";
                label4.BackColor = RGB(0x65E339);

                label6.Text = "股数";
                label6.BackColor = RGB(0x65E339);


                if (listView1.Items.Count > 0)
                {
                    if (listView1.Items[9].SubItems[1].Text == "0.00")
                        textBox5.Text = listView1.Items[11].SubItems[1].Text;
                    else
                        textBox5.Text = listView1.Items[9].SubItems[1].Text;
                    textBox6.Text = m_Num.ToString();
                    textBox5.SelectAll();
                }
                else
                {
                    textBox5.Text = "";
                }

                textBox5.Focus();
                textBox5.Focus();
            }

            if (e.KeyCode == Keys.F3)
            {

                label4.Visible = true;
                textBox6.Visible = true;

                label6.Visible = true;
                textBox5.Visible = true;


                label3.Text = "F3池：";
                label5.Text = "";
                textBox4.Visible = false;


                label4.Text = "卖出价格";
                label4.BackColor = Color.Blue;

                label6.Text = "股数";
                label6.BackColor = Color.Blue;


                if (listView1.Items.Count > 0)
                {
                    if (listView1.Items[9].SubItems[1].Text == "0.00")
                        textBox5.Text = listView1.Items[11].SubItems[1].Text;
                    else
                        textBox5.Text = listView1.Items[9].SubItems[1].Text;
                    textBox6.Text = m_Num.ToString();
                    textBox5.SelectAll();
                }
                else
                {
                    textBox5.Text = "";
                }
                textBox5.Focus();
            }

            if (e.KeyCode == Keys.F4)
            {
                label4.Visible = true;
                textBox6.Visible = true;

                label6.Visible = true;
                textBox5.Visible = true;


                label3.Text = "公共池";
                label5.Text = "";
                textBox4.Visible = false;


                label4.Text = "公共池";
                label4.BackColor = Color.Blue;

                label6.Text = "股数";
                label6.BackColor = Color.Blue;


                if (listView1.Items.Count > 0)
                {
                    textBox5.Text = richTextBox1.Text;
                    textBox6.Text = m_Num.ToString();
                    textBox5.SelectAll();
                }
                else
                {
                    textBox5.Text = "";
                }
                textBox5.Focus();
            }

            if (e.KeyCode == Keys.F5)
            {
                label3.Text = "公共池";
                label5.Text = "";
                textBox4.Visible = false;


                label4.Text = "卖出价格";
                label4.BackColor = Color.Blue;

                label6.Text = "股数";
                label6.BackColor = Color.Blue;


                if (listView1.Items.Count > 0)
                {
                    textBox5.Text = listView1.Items[9].SubItems[1].Text;
                    textBox6.Text = m_Num.ToString();
                    textBox5.SelectAll();
                }
                else
                {
                    textBox5.Text = "";
                }
                textBox5.Focus();
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Form2 f2 = new Form2(m_Num);
                DialogResult res =  f2.ShowDialog();
                if (res == DialogResult.Cancel)
                {
                    m_Num = f2.m_DefaultNum;
                    return;
                }
               
             //   MessageBox.Show(f2.m_DefaultNum.ToString());
            }
        }

          private  Color RGB(int color)
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
            //使用HttpWebRequest类的Create方法创建一个请求到uri的对象。
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(sUrl);
            //指定请求的方式为Get方式
            request.Method = WebRequestMethods.Http.Get;
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

        private void Form1_Load(object sender, EventArgs e)
        {
            this.listView2.Columns.Add("Price", 45);
            this.listView2.Columns.Add("Q", 30);
            this.listView2.Columns.Add("D",20);
            this.listView2.Columns.Add("Time", 60);

            this.listView1.Columns.Add("ID", 50);
            this.listView1.Columns.Add("Price", 60);
            this.listView1.Columns.Add("Q", 65);


            this.listView3.Columns.Add("Price", 45);
            this.listView3.Columns.Add("Q", 45);
            this.listView3.Columns.Add("Time", 50);
            this.listView3.Columns.Add("N", 30);

        


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string temp;

            if (m_No != 0)
            {

                temp = m_No.ToString();

                if (m_No > 999999)
                {
                    m_No = 0;
                    MessageBox.Show("请输入六位数!");
                    return;

                }
                if (m_No < 100000)
                {
                    temp = string.Format("{0:D6}", m_No);
                }

               


                String str1 = GetRequestData("http://123.57.232.178/query?catalogues=quote10&stocks=" + temp);
    
                string str3 = UnicodeToGB(str1);




                JObject jo1;

                try
                {
                     jo1= (JObject)JsonConvert.DeserializeObject(str3);
                }
                catch {
                    return;
                }
                    //if (jo1["error"].ToString() != "")
                //{
                //    return;
                //}
                string[] values1 = jo1.Properties().Select(item => item.Value.ToString()).ToArray();

                if (values1[0] == "[]")
                {
                    return;
                }
               
                JArray ja1 = (JArray)JsonConvert.DeserializeObject(values1[0]);
                string s;



                s = ja1[0]["卖十价"].ToString();
                listView1.Items[0].SubItems[1].Text = s.Substring(0, s.IndexOf(".") + 3);
                listView1.Items[0].SubItems[2].Text = ja1[0]["卖十量"].ToString();

                s = ja1[0]["卖九价"].ToString();
                listView1.Items[1].SubItems[1].Text = s.Substring(0, s.IndexOf(".") + 3);
                listView1.Items[1].SubItems[2].Text = ja1[0]["卖九量"].ToString();

                s = ja1[0]["卖八价"].ToString();
                listView1.Items[2].SubItems[1].Text = s.Substring(0, s.IndexOf(".") + 3);
                listView1.Items[2].SubItems[2].Text = ja1[0]["卖八量"].ToString();

                s = ja1[0]["卖七价"].ToString();
                listView1.Items[3].SubItems[1].Text = s.Substring(0, s.IndexOf(".") + 3);
                listView1.Items[3].SubItems[2].Text = ja1[0]["卖七量"].ToString();

                s = ja1[0]["卖六价"].ToString();
                listView1.Items[4].SubItems[1].Text = s.Substring(0, s.IndexOf(".") + 3);
                listView1.Items[4].SubItems[2].Text = ja1[0]["卖六量"].ToString();


                s = ja1[0]["卖五价"].ToString();
                listView1.Items[5].SubItems[1].Text = s.Substring(0, s.IndexOf(".") + 3);
                listView1.Items[5].SubItems[2].Text = ja1[0]["卖五量"].ToString();

                s = ja1[0]["卖四价"].ToString();
                listView1.Items[6].SubItems[1].Text = s.Substring(0, s.IndexOf(".") + 3);
                listView1.Items[6].SubItems[2].Text = ja1[0]["卖四量"].ToString();


                s = ja1[0]["卖三价"].ToString();
                listView1.Items[7].SubItems[1].Text = s.Substring(0, s.IndexOf(".") + 3);
                listView1.Items[7].SubItems[2].Text = ja1[0]["卖三量"].ToString();

                s = ja1[0]["卖二价"].ToString();
                listView1.Items[8].SubItems[1].Text = s.Substring(0, s.IndexOf(".") + 3);
                listView1.Items[8].SubItems[2].Text = ja1[0]["卖二量"].ToString();

                s = ja1[0]["卖一价"].ToString();
                listView1.Items[9].SubItems[1].Text = s.Substring(0, s.IndexOf(".") + 3);
                listView1.Items[9].SubItems[2].Text = ja1[0]["卖一量"].ToString();

                s = ja1[0]["买一价"].ToString();
                listView1.Items[11].SubItems[1].Text = s.Substring(0, s.IndexOf(".") + 3);
                listView1.Items[11].SubItems[2].Text = ja1[0]["买一量"].ToString();

                s = ja1[0]["买二价"].ToString();
                listView1.Items[12].SubItems[1].Text = s.Substring(0, s.IndexOf(".") + 3);
                listView1.Items[12].SubItems[2].Text = ja1[0]["买二量"].ToString();

                s = ja1[0]["买三价"].ToString();
                listView1.Items[13].SubItems[1].Text = s.Substring(0, s.IndexOf(".") + 3);
                listView1.Items[13].SubItems[2].Text = ja1[0]["买三量"].ToString();


                s = ja1[0]["买四价"].ToString();
                listView1.Items[14].SubItems[1].Text = s.Substring(0, s.IndexOf(".") + 3);
                listView1.Items[14].SubItems[2].Text = ja1[0]["买四量"].ToString();

               

                s = ja1[0]["买五价"].ToString();
                listView1.Items[15].SubItems[1].Text = s.Substring(0, s.IndexOf(".") + 3);
                listView1.Items[15].SubItems[2].Text = ja1[0]["买六量"].ToString();


                s = ja1[0]["买六价"].ToString();
                listView1.Items[16].SubItems[1].Text = s.Substring(0, s.IndexOf(".") + 3);
                listView1.Items[16].SubItems[2].Text = ja1[0]["买六量"].ToString();

                s = ja1[0]["买七价"].ToString();
                listView1.Items[17].SubItems[1].Text = s.Substring(0, s.IndexOf(".") + 3);
                listView1.Items[17].SubItems[2].Text = ja1[0]["买七量"].ToString();

                s = ja1[0]["买八价"].ToString();
                listView1.Items[18].SubItems[1].Text = s.Substring(0, s.IndexOf(".") + 3);
                listView1.Items[18].SubItems[2].Text = ja1[0]["买八量"].ToString();

                s = ja1[0]["买九价"].ToString();
                listView1.Items[19].SubItems[1].Text = s.Substring(0, s.IndexOf(".") + 3);
                listView1.Items[19].SubItems[2].Text = ja1[0]["买九量"].ToString();

                s = ja1[0]["买十价"].ToString();
                listView1.Items[20].SubItems[1].Text = s.Substring(0, s.IndexOf(".") + 3);
                listView1.Items[20].SubItems[2].Text = ja1[0]["买十量"].ToString();


            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            string temp;

            if (m_No != 0)
            {
    
                temp = m_No.ToString();

                if (m_No > 999999)
                {
                    m_No = 0;
                    MessageBox.Show("请输入六位数!");
                    return;

                }
                if (m_No < 100000)
                {
                    temp = string.Format("{0:D6}", m_No);
                }
                
                
                String str = GetRequestData("http://123.57.232.178/query?catalogues=transaction_detail&stock=" + temp);

                string str1 = UnicodeToGB(str);

                JObject jo;
                try
                {
                     jo = (JObject)JsonConvert.DeserializeObject(str1);
                }
                catch
                {
                    return;
                }

                string[] values = jo.Properties().Select(item => item.Value.ToString()).ToArray();

                if (values[0] == "[]")
                {
                    return;
                }
                JArray ja = (JArray)JsonConvert.DeserializeObject(values[0]);
                for (int i = 0; i < ja.Count; i++)
                {
                   

                    listView2.Items[i].SubItems[0].Text = ja[i]["价格"].ToString().Substring(0, 
                        ja[i]["价格"].ToString().IndexOf(".") + 3) ;
                    listView2.Items[i].SubItems[1].Text = ja[i]["成交量"].ToString().Substring(0,
                       ja[i]["成交量"].ToString().IndexOf(".") );
                    listView2.Items[i].SubItems[2].Text = ja[i]["性质"].ToString();


                    if (ja[i]["性质"].ToString() == "S")
                    {
                        listView2.Items[i].SubItems[0].ForeColor = RGB(0x65E339);
                        listView2.Items[i].SubItems[1].ForeColor = RGB(0x65E339);
                        listView2.Items[i].SubItems[2].ForeColor = RGB(0x65E339);
                        listView2.Items[i].SubItems[3].ForeColor = RGB(0x65E339);
                    }

                    else
                    {
                        listView2.Items[i].SubItems[0].ForeColor = RGB(0x5C5CFF);;
                        listView2.Items[i].SubItems[1].ForeColor = RGB(0x5C5CFF);;
                        listView2.Items[i].SubItems[2].ForeColor = RGB(0x5C5CFF);;
                        listView2.Items[i].SubItems[3].ForeColor = RGB(0x5C5CFF);;
                    }
                    listView2.Items[i].SubItems[3].Text = ja[i]["成交时间"].ToString();
                    listView2.EnsureVisible(listView2.Items.Count-1);
                }
            }
           
            
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Focus();
            textBox1.SelectAll();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            string temp;

            if (m_No != 0)
            {

                temp = m_No.ToString();

                if (m_No > 999999)
                {
                    m_No = 0;
                    MessageBox.Show("请输入六位数!");
                    return;

                }
                if (m_No < 100000)
                {
                    temp = string.Format("{0:D6}", m_No);
                }


                String str = GetRequestData("http://123.57.232.178/query?catalogues=transaction&stock=" + temp);

                string str1 = UnicodeToGB(str);

                 JObject jo;
                try
                {
                    jo= (JObject)JsonConvert.DeserializeObject(str1);
                }
                catch
                {
                    return;
                }

                string[] values = jo.Properties().Select(item => item.Value.ToString()).ToArray();

                if (values[0] == "[]")
                {
                    return;
                }
                JArray ja = (JArray)JsonConvert.DeserializeObject(values[0]);
                for (int i = 0; i < ja.Count; i++)
                {
                    


                    this.listView3.Items[i].SubItems[0].Text = ja[i]["价格"].ToString().Substring(0,
                        ja[i]["价格"].ToString().IndexOf(".") + 3);
                    int nq = int.Parse(ja[i]["现量"].ToString());
                    this.listView3.Items[i].SubItems[1].Text = (nq.ToString());

                    this.listView3.Items[i].SubItems[2].Text = (ja[i]["时间"].ToString());

                    int num = int.Parse(ja[i]["笔数"].ToString());
                    if (num == 0)
                    {
                        num = 0;
                    }
                    else
                        num = nq / num;
                    this.listView3.Items[i].SubItems[3].Text=(num.ToString());

                    if (ja[i]["买卖"].ToString() == "1")
                    {
                        this.listView3.Items[i].SubItems[0].ForeColor = RGB(0x65E339);
                        this.listView3.Items[i].SubItems[1].ForeColor = RGB(0x65E339);
                        this.listView3.Items[i].SubItems[2].ForeColor = RGB(0x65E339);
                        this.listView3.Items[i].SubItems[3].ForeColor = Color.White;
                    }
                    else
                    {
                        this.listView3.Items[i].SubItems[0].ForeColor = RGB(0x5C5CFF);
                        this.listView3.Items[i].SubItems[1].ForeColor = RGB(0x5C5CFF);
                        this.listView3.Items[i].SubItems[2].ForeColor = RGB(0x5C5CFF);
                        this.listView3.Items[i].SubItems[3].ForeColor = Color.White;
                    }
                    if (nq >= 500)
                    {
                        this.listView3.Items[i].SubItems[1].ForeColor = RGB(0xc000c0);
                    }
                   
                }
            }
            listView3.EnsureVisible(listView3.Items.Count-1);
        }

        //实现tab键的切换
        protected override bool ProcessCmdKey(ref   Message msg, Keys keyData)
        {
            if (keyData == Keys.Tab && textBox6.Focused)
            {

                textBox5.Focus();
                return true;
            }

            return false;
        }

        private void textBox5_MouseClick(object sender, MouseEventArgs e)
        {
            textBox5.Focus();
            textBox5.SelectAll();
        }

        private void textBox6_MouseClick(object sender, MouseEventArgs e)
        {
            textBox6.Focus();
            textBox6.SelectAll();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        public const int WM_NCLBUTTONDOWN = 0x00A1;

        protected override void WndProc(ref Message m) 
        {
            if (m.Msg == WM_NCLBUTTONDOWN)     
            {
                textBox1.Focus();
            }        
            base.WndProc(ref m); 
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int num = int.Parse(textBox6.Text);
                show_num(num);
            }
            catch
            {
 
            }
        }
        private void show_num(int num)
        {
            string temp="";
            if (num >= 10000)
            {
                temp = temp + (num / 10000) + "万";
                num = num - (num / 10000) * 10000;
            }
           if (num >= 1000)
            {
                temp = temp + (num / 1000) + "千";
                num = num - (num / 1000) * 1000;
            }
             if(num>=100)
            {
                temp = temp + (num / 100) + "百";
                num = num - (num / 100) * 100;
            }
             textBox2.Text = temp;
        }
    }
}
