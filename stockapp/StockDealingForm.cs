﻿using System;
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
using System.Diagnostics;

using System.Reflection;
using System.Reflection.Emit;
using System.Threading;

namespace stockapp
{
    public partial class Form_hangqi : Form
    {
        private int stockcode = 0;
        private int stocknum = 100;
        private string ip_hangqi;
        private string ip_jiaoyi;
        private int choice_f = 0;           /*区别f1 f2 f3*/
        private Thread thread;
        private JObject js_sell_nums,js_rongquan_nums;
        private IntPtr main_wnd_handle;     /*主窗口句柄*/
        delegate void Reflist(JArray ja);   /*声明委托*/

        public Form_hangqi(string ip_hq, string ip_jy,JObject json1,Form_main fm,JObject json2)
        {
            InitializeComponent();

            this.ip_hangqi = ip_hq;
            this.ip_jiaoyi = ip_jy;
            js_sell_nums = json1;
            js_rongquan_nums = json2;
            main_wnd_handle = fm.Handle;

            /*开启双缓冲*/
            listView1.DoubleBuffering(true);
            listView2.DoubleBuffering(true);
            listView3.DoubleBuffering(true);            
        }
     
        /*更新第三个列表框控件*/
        private void refresh_list3(JArray ja)
        {
            if (ja == null)
                return;
            //是否为创建控件的线程，不是为true
            if (this.listView3.InvokeRequired)
            {
                //为当前控件指定委托
                this.listView1.Invoke(new Reflist(refresh_list3), ja);
            }
            else
            {
                for (int i = 0; i < ja.Count; i++)
                {
                    this.listView3.Items[i].SubItems[0].Text = ja[i]["价格"].ToString().Substring(0,
                        ja[i]["价格"].ToString().IndexOf(".") + 3);
                    int nq = int.Parse(ja[i]["现量"].ToString());
                    this.listView3.Items[i].SubItems[1].Text = (nq.ToString());

                    this.listView3.Items[i].SubItems[2].Text = (ja[i]["时间"].ToString());

                    int num1 = int.Parse(ja[i]["笔数"].ToString());
                    if (num1 == 0)
                    {
                        num1 = 0;
                    }
                    else
                        num1 = nq / num1;
                    this.listView3.Items[i].SubItems[3].Text = (num1.ToString());

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
                listView3.EnsureVisible(listView3.Items.Count - 1);
            }
        }

        private JArray str_to_jarray(string str)
        {
            if (str == null)
                return null;
            try
            {
                JObject jo1 = (JObject)JsonConvert.DeserializeObject(str);
                if (jo1["data"] == null)
                    return null;
                JArray ja = (JArray)JsonConvert.DeserializeObject(jo1["data"].ToString());
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
                JObject jo1 = (JObject)JsonConvert.DeserializeObject(str);
                if (jo1["data"] == null)
                    return null;
                JArray ja = (JArray)JsonConvert.DeserializeObject(jo1["data"].ToString());
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
                JObject jo1 = (JObject)JsonConvert.DeserializeObject(str);
                if (jo1["stocks"] == null)
                    return null;
                JArray ja = (JArray)JsonConvert.DeserializeObject(jo1["stocks"].ToString());
                return ja;
            }
            catch
            {
                return null;
            }
        }

        private void refresh_list1(JArray ja)
        {
            if (ja == null || ja.Count ==0)
                return;
            if (this.listView1.InvokeRequired)
            {
                //为当前控件指定委托
                this.listView1.Invoke(new Reflist(refresh_list1), ja);
            }
            else
            {
                 string [] str_price_arr ={"","一价","二价","三价","四价","五价","六价","七价",
                                        "八价","九价","十价"};
                 string[] str_num_arr = { "","一量","二量","三量","四量","五量","六量","七量","八量"
                                        ,"九量","十量"};
                 double price = double.Parse(ja[0]["昨收"].ToString());
                 string s = "";
                 for (int i =10 ;i>=1;i--)
                 {
 	                s = ja[0]["卖"+str_price_arr[i]].ToString();
 	                listView1.Items[10-i].SubItems[1].Text = s.Substring(0, s.IndexOf(".") + 3);
 	                listView1.Items[10-i].SubItems[2].Text = ja[0]["卖"+str_num_arr[i]].ToString();
 	                if (double.Parse(s) > price)
    	                listView1.Items[10-i].SubItems[1].ForeColor = RGB(0x5C5CFF);//blue
 	                else if (double.Parse(s) < price)
    	                listView1.Items[10-i].SubItems[1].ForeColor = RGB(0x65E339); //green
 	                else
    	                listView1.Items[10-i].SubItems[1].ForeColor = Color.White;
                 }
                 for (int i = 1; i <= 10; i++)
                 {
                     s = ja[0]["买" + str_price_arr[i]].ToString();
                     listView1.Items[10 + i].SubItems[1].Text = s.Substring(0, s.IndexOf(".") + 3);
                     listView1.Items[10 + i].SubItems[2].Text = ja[0]["买" + str_num_arr[i]].ToString();
                     if (double.Parse(s) > price)
                         listView1.Items[10 + i].SubItems[1].ForeColor = RGB(0x5C5CFF);//blue
                     else if (double.Parse(s) < price)
                         listView1.Items[10 + i].SubItems[1].ForeColor = RGB(0x65E339);//green
                     else
                         listView1.Items[10 + i].SubItems[1].ForeColor = Color.White;
                 }
            }
        }

        private void refresh_list2(JArray ja)
        {
            if (ja == null  || ja.Count ==0)
                return;
            if (this.listView2.InvokeRequired)
            {
                //为当前控件指定委托
                this.listView2.Invoke(new Reflist(refresh_list2), ja);
            }
            else
            {
                for (int i = 0; i < ja.Count; i++)
                {
                    listView2.Items[i].SubItems[0].Text = ja[i]["价格"].ToString().Substring(0,
                        ja[i]["价格"].ToString().IndexOf(".") + 3);
                    listView2.Items[i].SubItems[1].Text = ja[i]["成交量"].ToString().Substring(0,
                       ja[i]["成交量"].ToString().IndexOf("."));
                    listView2.Items[i].SubItems[2].Text = ja[i]["性质"].ToString();

                    if (ja[i]["性质"].ToString() == "S")
                    {
                        listView2.Items[i].SubItems[0].ForeColor = RGB(0x65E339);//green
                        listView2.Items[i].SubItems[1].ForeColor = RGB(0x65E339);
                        listView2.Items[i].SubItems[2].ForeColor = RGB(0x65E339);
                        listView2.Items[i].SubItems[3].ForeColor = RGB(0x65E339);
                    }
                    else
                    {
                        listView2.Items[i].SubItems[0].ForeColor = RGB(0x5C5CFF); //blue
                        listView2.Items[i].SubItems[1].ForeColor = RGB(0x5C5CFF); 
                        listView2.Items[i].SubItems[2].ForeColor = RGB(0x5C5CFF); 
                        listView2.Items[i].SubItems[3].ForeColor = RGB(0x5C5CFF); 
                    }
                    listView2.Items[i].SubItems[3].Text = ja[i]["成交时间"].ToString();
                    listView2.EnsureVisible(listView2.Items.Count - 1);
                }
            }
        }

        private void refresh_lefttop_controls(JArray ja1)
        {
            if (ja1 == null || ja1.Count == 0)
                return;
            
            double price = double.Parse(ja1[0]["昨收"].ToString());
            this.Text = ja1[0]["名称"].ToString();
            textBox7.Text = ja1[0]["现价"].ToString().Substring(0,
                        ja1[0]["现价"].ToString().IndexOf(".") + 3);
            double now_price = Double.Parse(textBox7.Text);
            double rate = (now_price - price) / price;
            if (rate > 0)
                richTextBox3.ForeColor = RGB(0x5C5CFF);
            else
                richTextBox3.ForeColor = Color.Green;
            richTextBox3.Text = string.Format("{0:0.00%}", rate);
            double amount = double.Parse(ja1[0]["总金额"].ToString());
            amount /= 10000;
            textBox9.Text = string.Format("{0:N0}", amount) + " W";
            textBox12.Text = ja1[0]["开盘"].ToString().Substring(0,
                ja1[0]["开盘"].ToString().IndexOf(".") + 3);
            textBox11.Text = ja1[0]["昨收"].ToString().Substring(0,
                        ja1[0]["昨收"].ToString().IndexOf(".") + 3);
            double temp1 = price * 1.1;
            this.richTextBox1.Text = string.Format("{0:F2}", temp1);
            temp1 = price * 0.9;
            this.richTextBox2.Text = string.Format("{0:F2}", temp1);           
        }

        private void add_list2(JArray ja)
        {
            if (ja == null || ja.Count == 0)
                return;
            if (this.listView2.InvokeRequired)
            {
                //为当前控件指定委托
                this.listView2.Invoke(new Reflist(add_list2), ja);
            }
            else
            {
                for (int i = 0; i < ja.Count; i++)
                {
                    ListViewItem item1 = new ListViewItem();
                    item1.UseItemStyleForSubItems = false;
                    item1.SubItems[0].Text = ja[i]["价格"].ToString().Substring(0,
                        ja[i]["价格"].ToString().IndexOf(".") + 3);
                    item1.SubItems.Add(ja[i]["成交量"].ToString().Substring(0,
                        ja[i]["成交量"].ToString().IndexOf(".")));
                    item1.SubItems.Add(ja[i]["性质"].ToString());
                    item1.SubItems.Add(ja[i]["成交时间"].ToString());
                    if (ja[i]["性质"].ToString() == "S")
                    {
                        item1.SubItems[0].ForeColor = RGB(0x65E339);
                        item1.SubItems[1].ForeColor = RGB(0x65E339);
                        item1.SubItems[2].ForeColor = RGB(0x65E339);//green
                        item1.SubItems[3].ForeColor = RGB(0x65E339);
                    }
                    else
                    {
                        item1.SubItems[0].ForeColor = RGB(0x5C5CFF);
                        item1.SubItems[1].ForeColor = RGB(0x5C5CFF);
                        item1.SubItems[2].ForeColor = RGB(0x5C5CFF);//blue
                        item1.SubItems[3].ForeColor = RGB(0x5C5CFF);
                    }
                    //3、将数据集item1添加到ListView控件  
                    listView2.Items.Add(item1);
                }
            }
        }

        private void add_list1(JArray ja)
        {
            if (ja == null || ja.Count == 0)
                return;
            if (this.listView1.InvokeRequired)
            {
                //为当前控件指定委托
                this.listView1.Invoke(new Reflist(add_list1), ja);
            }
            else
            {
                string[] str_price_arr ={"","一价","二价","三价","四价","五价","六价","七价",
                                        "八价","九价","十价"};
                string[] str_num_arr = { "","一量","二量","三量","四量","五量","六量","七量","八量"
                                        ,"九量","十量"};
                ListViewItem item=null;
                double price = double.Parse(ja[0]["昨收"].ToString());
                string s = "";
                for (int i = 10; i >= 1; i--)
                {
                    item = new ListViewItem();
                    item.UseItemStyleForSubItems = false;
                    item.SubItems[0].Text = "卖" + str_price_arr[i].Substring(0,1);
                    s = ja[0]["卖"+str_price_arr[i]].ToString();
                    item.SubItems.Add(s.Substring(0, s.IndexOf(".") + 3));
                    if (double.Parse(s) > price)
                        item.SubItems[1].ForeColor = RGB(0x5C5CFF);
                    else if (double.Parse(s) < price)
                        item.SubItems[1].ForeColor = RGB(0x65E339);
                    else
                        item.SubItems[1].ForeColor = Color.White;
                    item.SubItems.Add(ja[0]["卖"+str_num_arr[i]].ToString());
                    item.SubItems[2].ForeColor = Color.White;
                    listView1.Items.Add(item);
                }
                item = new ListViewItem();
                item.BackColor = Color.Gray;
                listView1.Items.Add(item);
                for (int i = 1; i <= 10; i++)
                {
                    item = new ListViewItem();
                    item.UseItemStyleForSubItems = false;
                    item.SubItems[0].Text = "买"+str_price_arr[i].Substring(0,1);
                    s = ja[0]["买"+str_price_arr[i]].ToString();
                    item.SubItems.Add(s.Substring(0, s.IndexOf(".") + 3));
                    if (double.Parse(s) > price)
                        item.SubItems[1].ForeColor = RGB(0x5C5CFF);
                    else if (double.Parse(s) < price)
                        item.SubItems[1].ForeColor = RGB(0x65E339);
                    else
                        item.SubItems[1].ForeColor = Color.White;
                    item.SubItems.Add(ja[0]["买"+str_price_arr[i]].ToString());
                    item.SubItems[2].ForeColor = Color.White;
                    listView1.Items.Add(item);
                }
            }
        }

        private void add_list3(JArray ja)
        {

            if (ja == null || ja.Count == 0)
                return;

            if (this.listView3.InvokeRequired)
            {
                //为当前控件指定委托
                this.listView3.Invoke(new Reflist(add_list3), ja);
            }
            else
            {
                for (int i = 0; i < ja.Count; i++)
                {
                    ListViewItem item1 = new ListViewItem();

                    item1.UseItemStyleForSubItems = false;

                    item1.SubItems[0].Text = ja[i]["价格"].ToString().Substring(0,
                        ja[i]["价格"].ToString().IndexOf(".") + 3);
                    int nq = int.Parse(ja[i]["现量"].ToString());

                    item1.SubItems.Add(nq.ToString());

                    item1.SubItems.Add(ja[i]["时间"].ToString());

                    item1.SubItems.Add(ja[i]["笔数"].ToString());

                    if (ja[i]["买卖"].ToString() == "1")
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
            }
        }

        private void Run(object num)
        {
            string Num = num.ToString();
            while (true)
            {
                if (listView2.Items.Count == 0)
                {
                    string str = GetRequestData("http://" + ip_hangqi + 
                        "/query?catalogues=transaction_detail&stock=" + Num);
                    JArray ja1 = str_to_jarray(str);
                    add_list2(ja1);
                }
           
                if(listView3.Items.Count == 0)
                {
                    string str2 = GetRequestData("http://" + ip_hangqi +
                        "/query?catalogues=transaction&stock=" + Num);
                    JArray ja1 = str2_to_jarray(str2);
                    add_list3(ja1);                
                }
                if (listView1.Items.Count == 0 )
                {
                    string str1 = GetRequestData("http://" + ip_hangqi +
                        "/query?catalogues=quote10&stocks=" + Num);
                    JArray ja1 = str1_to_jarray(str1);
                    add_list1(ja1);
                    refresh_lefttop_controls(ja1);   
                   
                }           
          
                 /*如果listview1不是首次显示的数据*/
                if (listView1.Items.Count != 0)
                {
                    string str1 = GetRequestData("http://" + ip_hangqi +
                       "/query?catalogues=quote10&stocks=" + Num);
                    JArray ja1=str1_to_jarray(str1);
                    refresh_list1(ja1);
                    refresh_lefttop_controls(ja1);
                }

                if (listView2.Items.Count != 0)
                {
                    string str = GetRequestData("http://" + ip_hangqi +
                      "/query?catalogues=transaction_detail&stock=" + Num);
                    JArray ja1 = str_to_jarray(str);
                    refresh_list2(ja1);
                }

                if (listView3.Items.Count != 0)
                {
                    string str2 = GetRequestData("http://" + ip_hangqi +
                       "/query?catalogues=transaction&stock=" + Num);
                    JArray ja1 = str2_to_jarray(str2);
                    refresh_list3(ja1);
                }
            }
        }

        private void handle_shiftup_msg()
        {
            if (textBox5.Focused)
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
        private void handle_shiftdown_msg()
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

        private void handle_escape_msg()
        {
            choice_f = 0;
            label4.Visible = false;
            textBox6.Visible = false;
            textBox6.Text = "";
            label6.Visible = false;
            textBox5.Visible = false;
            textBox5.Text = "";
            textBox2.Visible = false;
        }

        private void start_recv_data()
        {
            textBox1.Focus();
            textBox1.SelectAll();
            listView1.Clear();
            listView2.Clear();
            listView3.Clear();
            textBox7.Text = "";
            textBox11.Text = "";
            textBox9.Text = "";
            textBox11.Text = "";
            richTextBox1.Text = "";
            richTextBox2.Text = "";
            richTextBox3.Text = "";
            textBox3.Text = "";

            this.listView2.Columns.Add("Price", 45);
            this.listView2.Columns.Add("Size", 40);
            this.listView2.Columns.Add("D", 20);
            this.listView2.Columns.Add("Time", 60);

            this.listView1.Columns.Add("ID", 45);
            this.listView1.Columns.Add("Price", 60);
            this.listView1.Columns.Add("Q", 72);

            this.listView3.Columns.Add("Price", 45);
            this.listView3.Columns.Add("Size", 45);
            this.listView3.Columns.Add("Time", 50);
            this.listView3.Columns.Add("C", 30);


            if (thread != null && thread.IsAlive == true)
            {
                thread.Abort();
            }
            string temp;
            stockcode = int.Parse(textBox1.Text);
            temp = stockcode.ToString();
            if (stockcode > 999999)
            {
                stockcode = 0;
                MessageBox.Show("请输入六位数!");
                return;

            }
            if (stockcode < 100000)
            {
                temp = string.Format("{0:D6}", stockcode);
            }
            //创建一个线程
            thread = new Thread(new ParameterizedThreadStart(Run));
            thread.IsBackground = true;
            thread.Start(temp);
        }

        private void post_order()
        {
            float price = float.Parse(textBox5.Text);
            int num = int.Parse(textBox6.Text);
            string temp = "";
            if (stockcode < 100000)
            {
                temp = string.Format("{0:D6}", stockcode);
            }
            else
            {
                temp = stockcode.ToString();
            }
            if (choice_f == 1)
            {
                string str = "http://" + ip_jiaoyi + "/buy?stock=" + temp + "&price=" + price +
                 "&share=" + num;

                Thread thread = new Thread(new ParameterizedThreadStart(post_msg_to_main_wnd));
                thread.IsBackground = true;
                thread.Start(str);
            }
            if (choice_f == 2)
            {
                string str = "http://" + ip_jiaoyi + "/sell?stock=" + temp + "&price=" + price +
                 "&share=" + num;
                Thread thread = new Thread(new ParameterizedThreadStart(post_msg_to_main_wnd));
                thread.IsBackground = true;
                thread.Start(str);
            }
            if (choice_f == 3)
            {
                string str = "http://" + ip_jiaoyi + "/short?type=frompool&stock=" + temp + "&price=" + price +
                     "&share=" + num;
                Thread thread = new Thread(new ParameterizedThreadStart(post_msg_to_main_wnd));
                thread.IsBackground = true;
                thread.Start(str);       
            }
            if (choice_f == 4)
            {
                string str = "http://" + ip_jiaoyi + "/short?type=direct&stock=" + temp +
                    "&share=" + num + "&price=" + price;
                Thread thread = new Thread(new ParameterizedThreadStart(post_msg_to_main_wnd));
                thread.IsBackground = true;
                thread.Start(str);
               
            }
            choice_f = 0;
            label4.Visible = false;
            textBox6.Visible = false;
            textBox6.Text = "";
            label6.Visible = false;
            textBox5.Visible = false;
            textBox5.Text = "";
            textBox2.Visible = false;
        }

        private void handle_f1_msg()
        {
            string temp = "";
            if (stockcode < 100000)
            {
                temp = string.Format("{0:D6}", stockcode);
            }
            else
            {
                temp = stockcode.ToString();
            }
            label4.Visible = true;
            textBox6.Visible = true;

            label6.Visible = true;
            textBox5.Visible = true;
            textBox2.Visible = true;

            label3.Text = "F2池";
            label5.Text = "F3池";

            label4.Text = "买入";
            label4.ForeColor = RGB(0x5C5CFF); ;

            label6.Text = "股数";
            label6.ForeColor = RGB(0x5C5CFF); ;

            if (listView1.Items.Count > 0)
            {
                if (listView1.Items[11].SubItems[1].Text == "0.00")
                    textBox5.Text = listView1.Items[9].SubItems[1].Text;
                else
                    textBox5.Text = listView1.Items[11].SubItems[1].Text;

                textBox6.Text = stocknum.ToString();
                textBox5.SelectAll();
            }
            else
            {
                textBox5.Text = "";
            }
            textBox5.Focus();
            if (js_sell_nums[temp] != null)
                textBox3.Text = js_sell_nums[temp].ToString();
            choice_f = 1;
        }

        private void handle_f2_msg()
        {
            string temp = "";
            if (stockcode < 100000)
            {
                temp = string.Format("{0:D6}", stockcode);
            }
            else
            {
                temp = stockcode.ToString();
            }
            label4.Visible = true;
            textBox6.Visible = true;

            label6.Visible = true;
            textBox5.Visible = true;
            textBox2.Visible = true;

            label3.Text = "F2池";
            label5.Text = "";
            textBox4.Visible = false;

            label4.Text = "卖出";
            label4.ForeColor = RGB(0x65E339);

            label6.Text = "股数";
            label6.ForeColor = RGB(0x65E339);

            if (listView1.Items.Count > 0)
            {
                if (listView1.Items[9].SubItems[1].Text == "0.00")
                    textBox5.Text = listView1.Items[11].SubItems[1].Text;
                else
                    textBox5.Text = listView1.Items[9].SubItems[1].Text;
                textBox6.Text = stocknum.ToString();
                textBox5.SelectAll();
            }
            else
            {
                textBox5.Text = "";
            }
            if (js_sell_nums[temp] != null)
                textBox3.Text = js_sell_nums[temp].ToString();
            else
                textBox3.Text = (0).ToString();
            textBox5.Focus();
            textBox5.Focus();
            choice_f = 2;
        }

        private void handle_f3_msg()
        {
            string temp = "";
            if (stockcode < 100000)
            {
                temp = string.Format("{0:D6}", stockcode);
            }
            else
            {
                temp = stockcode.ToString();
            }
            label4.Visible = true;
            textBox6.Visible = true;

            label6.Visible = true;
            textBox5.Visible = true;
            textBox2.Visible = true;

            label3.Text = "F3池：";
            label5.Text = "";
            textBox4.Visible = false;

            label4.Text = "卖出";
            label4.ForeColor = Color.Blue;

            label6.Text = "股数";
            label6.ForeColor = Color.Blue;

            if (listView1.Items.Count > 0)
            {
                if (listView1.Items[9].SubItems[1].Text == "0.00")
                    textBox5.Text = listView1.Items[11].SubItems[1].Text;
                else
                    textBox5.Text = listView1.Items[9].SubItems[1].Text;
                textBox6.Text = stocknum.ToString();
                textBox5.SelectAll();
            }
            else
            {
                textBox5.Text = "";
            }
            textBox5.Focus();
            if (js_rongquan_nums[temp] != null)
                textBox3.Text = js_rongquan_nums[temp].ToString();
            else
                textBox3.Text = (0).ToString();
            choice_f = 3;
        }

        private void handle_f4_msg()
        {
            label4.Visible = true;
            textBox6.Visible = true;

            label6.Visible = true;
            textBox5.Visible = true;
            textBox2.Visible = true;

            label3.Text = "公共池";
            label5.Text = "";
            textBox4.Visible = false;

            label4.Text = "卖出";
            label4.ForeColor = Color.Blue;

            label6.Text = "股数";
            label6.ForeColor = Color.Blue;

            if (listView1.Items.Count > 0)
            {
                textBox5.Text = richTextBox1.Text;
                textBox6.Text = stocknum.ToString();
                textBox5.SelectAll();
            }
            else
            {
                textBox5.Text = "";
            }
            textBox5.Focus();
            choice_f = 4;
        }

        private void handle_f5_msg()
        {
            label4.Visible = true;
                textBox6.Visible = true;

                label6.Visible = true;
                textBox5.Visible = true;
                textBox2.Visible = true;

                label3.Text = "公共池";
                label5.Text = "";
                textBox4.Visible = false;


                label4.Text = "卖出";
                label4.ForeColor = Color.Blue;

                label6.Text = "股数";
                label6.ForeColor = Color.Blue;

                if (listView1.Items.Count > 0)
                {
                    textBox5.Text = listView1.Items[9].SubItems[1].Text;
                    textBox6.Text = stocknum.ToString();
                    textBox5.SelectAll();
                }
                else
                {
                    textBox5.Text = "";
                }
                textBox5.Focus();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                handle_shiftup_msg();
            }
            if (e.KeyCode == Keys.Down)
            {
                handle_shiftdown_msg();
            }
            if (e.KeyCode == Keys.Escape)
            {
                handle_escape_msg();
            }
            if (e.KeyCode == Keys.Enter && textBox1.Focused)
            {
                start_recv_data();
            }
            if (e.KeyCode == Keys.Enter && textBox6.Focused)
            {
                post_order();
            }
            if (e.KeyCode == Keys.Enter && textBox5.Focused)
            {
                post_order();
            }
            if (e.KeyCode == Keys.F1)
            {
                handle_f1_msg();
            }
            if (e.KeyCode == Keys.F2)
            {
                handle_f2_msg();
            }
            if (e.KeyCode == Keys.F3)
            {
                handle_f3_msg();  
            }
            if (e.KeyCode == Keys.F4)
            {
                handle_f4_msg();               
            }
            if (e.KeyCode == Keys.F5)
            {
                handle_f5_msg();                
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                QuantitySettingForm f2 = new QuantitySettingForm(stocknum);
                DialogResult res = f2.ShowDialog();
                if (res == DialogResult.Cancel)
                {
                    stocknum = f2.m_DefaultNum;
                    return;
                }

                //   MessageBox.Show(f2.m_DefaultNum.ToString());
            }
        }

        public static Color RGB(int color)
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

        private void Form1_Load(object sender, EventArgs e)
        {
            this.listView2.Columns.Add("Price", 45);
            this.listView2.Columns.Add("Size", 40);
            this.listView2.Columns.Add("D", 20);
            this.listView2.Columns.Add("Time", 60);

            this.listView1.Columns.Add("ID", 50);
            this.listView1.Columns.Add("Price", 60);
            this.listView1.Columns.Add("Size", 65);

            this.listView3.Columns.Add("Price", 45);
            this.listView3.Columns.Add("Size", 45);
            this.listView3.Columns.Add("Time", 50);
            this.listView3.Columns.Add("N", 30);
            //Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Focus();
            textBox1.SelectAll();
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

        /*实现在客户区点击时候自动聚焦到textBox1上面*/
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
                return;
            }
        }

        public void post_msg_to_main_wnd(object str)
        {
            string str1 = (string)str;
            string str2 = GetRequestData_Post(str1);
            Win32API.My_lParam lp = new Win32API.My_lParam();
            lp.i = 3;
            lp.s = str2;
            Win32API.SendMessage(main_wnd_handle, 0x63, 0, ref lp);
        }

        /*将数字以 万 千 百 进行分割*/
        private void show_num(int num)
        {
            string temp = "";
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
            if (num >= 100)
            {
                temp = temp + (num / 100) + "百";
                num = num - (num / 100) * 100;
            }
            textBox2.Text = temp;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (thread != null && thread.IsAlive == true)
            {
                thread.Abort();
            }
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
