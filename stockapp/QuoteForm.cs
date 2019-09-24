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
using System.Diagnostics;

using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using RestSharp;
using K8.Properties;

namespace K8
{
    public partial class QuoteForm : Form
    {
        private int stockcode = 0;
        private int stocknum = 100;
        private string mStockCode = null;
        private string ip_hangqi;
        private string ip_jiaoyi;
        private int choice_f = 0;           /*区别f1 f2 f3*/
        private Thread thread = null;
        private IntPtr main_wnd_handle;     /*主窗口句柄*/
        private Form parent;
        delegate void Reflist(JObject ja);   /*声明委托*/
        private string[] CH_NUM = { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九", "十" };

        public QuoteForm(Form fm)
        {
            InitializeComponent();

            this.ip_hangqi = Properties.Settings.Default.MarketServerIP;
            this.ip_jiaoyi = Properties.Settings.Default.TradeServerIP;
            parent = fm;
            main_wnd_handle = fm.Handle;

            /*开启双缓冲*/
            QuoteList.DoubleBuffering(true);
            TransactionDetailList.DoubleBuffering(true);
            TransactionList.DoubleBuffering(true);
        }

        private JObject str_to_jobject(string str)
        {
            try
            {
                JObject jo = (JObject)JsonConvert.DeserializeObject(str);
                return jo;
            }
            catch
            {
                return null;
            }
        }

        /*更新第三个列表框控件*/
        private int ref_list3_count = 0;
        private void RefreshTransactionList(JObject jo)
        {
            try
            {
                JArray ja = (JArray)(jo["transaction"]);
                //是否为创建控件的线程，不是为true
                if (this.TransactionList.InvokeRequired)
                {
                    //为当前控件指定委托
                    this.TransactionList.Invoke(new Reflist(RefreshTransactionList), jo);
                }
                else
                {
                    TransactionList.BeginUpdate();
                    TransactionList.Columns[4].Text = (++ref_list3_count).ToString();
                    TransactionList.Items.Clear();
                    for (int i = 0; i < ja.Count; i++)
                    {
                        ListViewItem item = new ListViewItem();
                        item.SubItems[0].Text = ja[i]["价格"].ToString().Substring(0,
                            ja[i]["价格"].ToString().IndexOf(".") + 3);
                        int nq = int.Parse(ja[i]["现量"].ToString());
                        item.SubItems.Add(nq.ToString());
                        item.SubItems.Add(ja[i]["时间"].ToString());

                        int num1 = int.Parse(ja[i]["笔数"].ToString());
                        if (num1 == 0)
                        {
                            num1 = 0;
                        }
                        else
                            num1 = nq / num1;
                        item.SubItems.Add(num1.ToString());

                        if (ja[i]["买卖"].ToString() == "1")
                        {
                            item.SubItems[0].ForeColor = RGB(0x65E339);
                            item.SubItems[1].ForeColor = RGB(0x65E339);
                            item.SubItems[2].ForeColor = RGB(0x65E339);
                            item.SubItems[3].ForeColor = Color.White;
                        }
                        else
                        {
                            item.SubItems[0].ForeColor = RGB(0x5C5CFF);
                            item.SubItems[1].ForeColor = RGB(0x5C5CFF);
                            item.SubItems[2].ForeColor = RGB(0x5C5CFF);
                            item.SubItems[3].ForeColor = Color.White;
                        }
                        if (nq >= 500)
                        {
                            item.SubItems[1].ForeColor = RGB(0xc000c0);
                        }
                        TransactionList.Items.Add(item);
                    }

                }
                TransactionList.EnsureVisible(TransactionList.Items.Count-1);
                TransactionList.EndUpdate();
            }
            catch
            {
                return;
            }
        }

        private int ref_list1_count = 0;
        private void RefreshQuoteList(JObject jo)
        {
            try
            {
                var ja = jo["quote10"];
                if (this.QuoteList.InvokeRequired)
                {
                    //为当前控件指定委托
                    this.QuoteList.Invoke(new Reflist(RefreshQuoteList), jo);
                }
                else
                {
                    QuoteList.BeginUpdate();
                    QuoteList.Columns[3].Text = (++ref_list1_count).ToString();
                    double price = double.Parse(ja[0]["昨收"].ToString());
                    string s = "";
                    for (int i = 10; i >= 1; i--)
                    {
                        s = ja[0]["卖" + CH_NUM[i] + "价"].ToString();
                        QuoteList.Items[10 - i].SubItems[1].Text = s.Substring(0, s.IndexOf(".") + 3);
                        QuoteList.Items[10 - i].SubItems[2].Text = ja[0]["卖" + CH_NUM[i] + "量"].ToString();
                        if (double.Parse(s) > price)
                            QuoteList.Items[10 - i].SubItems[1].ForeColor = RGB(0x5C5CFF);//blue
                        else if (double.Parse(s) < price)
                            QuoteList.Items[10 - i].SubItems[1].ForeColor = RGB(0x65E339); //green
                        else
                            QuoteList.Items[10 - i].SubItems[1].ForeColor = Color.White;
                        QuoteList.Items[10 - i].SubItems[2].ForeColor = Color.White;
                    }
                    for (int i = 1; i <= 10; i++)
                    {
                        s = ja[0]["买" + CH_NUM[i] + "价"].ToString();
                        QuoteList.Items[10 + i].SubItems[1].Text = s.Substring(0, s.IndexOf(".") + 3);
                        QuoteList.Items[10 + i].SubItems[2].Text = ja[0]["买" + CH_NUM[i] + "量"].ToString();
                        if (double.Parse(s) > price)
                            QuoteList.Items[10 + i].SubItems[1].ForeColor = RGB(0x5C5CFF);//blue
                        else if (double.Parse(s) < price)
                            QuoteList.Items[10 + i].SubItems[1].ForeColor = RGB(0x65E339);//green
                        else
                            QuoteList.Items[10 + i].SubItems[1].ForeColor = Color.White;
                        QuoteList.Items[10 + i].SubItems[2].ForeColor = Color.White;
                    }
                }
                QuoteList.EndUpdate();
            }
            catch
            {
                return;
            }
        }

        private int ref_list2_count = 0;
        private void RefreshTransactionDetailList(JObject jo)
        {
            try
            {
                JArray ja = (JArray)jo["transaction_detail"];
                if (this.TransactionDetailList.InvokeRequired)
                {
                    //为当前控件指定委托
                    this.TransactionDetailList.Invoke(new Reflist(RefreshTransactionDetailList), jo);
                }
                else
                {
                    TransactionDetailList.BeginUpdate();
                    TransactionDetailList.Columns[4].Text = (++ref_list2_count).ToString();
                    TransactionDetailList.Items.Clear();

                    for (int i = 0; i < ja.Count; i++)
                    {
                        ListViewItem item = new ListViewItem();
                        item.UseItemStyleForSubItems = false;
                        item.SubItems[0].Text = ja[i]["价格"].ToString().Substring(0,
                            ja[i]["价格"].ToString().IndexOf(".") + 3);
                        item.SubItems.Add(ja[i]["成交量"].ToString().Substring(0,
                           ja[i]["成交量"].ToString().IndexOf(".")));
                        item.SubItems.Add(ja[i]["性质"].ToString());
                        item.SubItems.Add(ja[i]["成交时间"].ToString());
                        if (ja[i]["性质"].ToString() == "S")
                        {
                            item.SubItems[0].ForeColor = RGB(0x65E339);//green
                            item.SubItems[1].ForeColor = RGB(0x65E339);
                            item.SubItems[2].ForeColor = RGB(0x65E339);
                            item.SubItems[3].ForeColor = RGB(0x65E339);
                        }
                        else
                        {
                            item.SubItems[0].ForeColor = RGB(0x5C5CFF); //blue
                            item.SubItems[1].ForeColor = RGB(0x5C5CFF);
                            item.SubItems[2].ForeColor = RGB(0x5C5CFF);
                            item.SubItems[3].ForeColor = RGB(0x5C5CFF);
                        }
                        TransactionDetailList.Items.Add(item);
                    }
                    TransactionDetailList.EnsureVisible(TransactionDetailList.Items.Count - 1);
                    TransactionDetailList.EndUpdate();
                }
            }
            catch
            {
                return;
            }
        }

        private void refresh_lefttop_controls(JObject jo)
        {
            try
            {
                var ja = jo["quote10"];

                double price = double.Parse(ja[0]["昨收"].ToString());
                this.Text = ja[0]["名称"].ToString();
                currentPriceTextBox.Text = ja[0]["现价"].ToString().Substring(0,
                            ja[0]["现价"].ToString().IndexOf(".") + 3);
                double now_price = Double.Parse(currentPriceTextBox.Text);
                double rate = (now_price - price) / price;
                if (rate > 0)
                    riseRateTextBox.ForeColor = RGB(0x5C5CFF);
                else
                    riseRateTextBox.ForeColor = Color.Green;
                riseRateTextBox.Text = string.Format("{0:0.00%}", rate);
                double amount = double.Parse(ja[0]["总金额"].ToString());
                amount /= 10000;
                textBox9.Text = string.Format("{0:N0}", amount) + " W";
                openningTextBox.Text = ja[0]["开盘"].ToString().Substring(0,
                    ja[0]["开盘"].ToString().IndexOf(".") + 3);
                closingTextBox.Text = ja[0]["昨收"].ToString().Substring(0,
                            ja[0]["昨收"].ToString().IndexOf(".") + 3);
                double temp1 = price * 1.1;
                this.risingPriceTextBox.Text = string.Format("{0:F2}", temp1);
                temp1 = price * 0.9;
                this.dropRateTextBox.Text = string.Format("{0:F2}", temp1);
            }
            catch
            {
                return;
            }
        }

        public void FetchQuote(bool once = true)
        {
            var client = new RestClient("http://" + ip_hangqi);
            var request = new RestRequest("query", Method.GET);
            request.AddParameter("catalogues", "quote10");
            request.AddParameter("stocks", mStockCode);

            client.ExecuteAsync(request, response =>
            {
                var temp = str_to_jobject(response.Content);
                RefreshQuoteList(temp);
                refresh_lefttop_controls(temp);
                if (!once)
                {
                    Thread.Sleep(Settings.Default.QuoteRefreshDelay);
                    FetchQuote(false);
                }
            });
        }

        public void FetchTransactionDetail(bool once = true)
        {
            var client = new RestClient("http://" + ip_hangqi);
            var request = new RestRequest("query", Method.GET);
            request.AddParameter("catalogues", "transaction_detail");
            request.AddParameter("stock", mStockCode);

            client.ExecuteAsync(request, response =>
            {
                JObject ja1 = str_to_jobject(response.Content);
                RefreshTransactionDetailList(ja1);
                if (!once)
                {
                    Thread.Sleep(Settings.Default.TransactionDetailRefreshDelay);
                    FetchTransactionDetail(false);
                }
            });
        }

        public void FetchTransaction(bool once = true)
        {
            var client = new RestClient("http://" + ip_hangqi);
            var request = new RestRequest("query", Method.GET);
            request.AddParameter("catalogues", "transaction");
            request.AddParameter("stock", mStockCode);

            client.ExecuteAsync(request, response =>
            {
                JObject ja1 = str_to_jobject(response.Content);
                RefreshTransactionList(ja1);
                if (!once)
                {
                    Thread.Sleep(Settings.Default.TransactionRefreshDelay);
                    FetchTransaction(false);
                }
            });
        }

        private void Run(object num)
        {
            FetchQuote(false);
            FetchTransactionDetail(false);
            FetchTransaction(false);
        }

        private void handle_shiftup_msg()
        {
            if (priceTextBox.Focused)
            {
                try
                {
                    float price = float.Parse(priceTextBox.Text);
                    price += 0.01F;
                    priceTextBox.Text = price.ToString();
                }
                catch
                {
                    return;
                }
                priceTextBox.Focus();
                priceTextBox.SelectAll();
            }
            if (quantityTextBox.Focused)
            {
                try
                {
                    int num = int.Parse(quantityTextBox.Text);
                    num += 100;
                    quantityTextBox.Text = num.ToString();
                }
                catch
                {
                    return;
                }
                quantityTextBox.Focus();
                quantityTextBox.SelectAll();
            }
        }

        private void handle_shiftdown_msg()
        {
            if (priceTextBox.Focused)
            {
                try
                {
                    double price = double.Parse(priceTextBox.Text);
                    if (price < 0.01)
                        return;
                    price -= 0.01;
                    priceTextBox.Text = price.ToString();
                }
                catch
                {
                    return;
                }
            }
            if (quantityTextBox.Focused)
            {
                try
                {
                    int num = int.Parse(quantityTextBox.Text);
                    if (num < 100)
                        return;
                    num -= 100;
                    quantityTextBox.Text = num.ToString();
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
            buy_lable.Visible = false;
            quantityTextBox.Visible = false;
            quantityTextBox.Text = "";
            quantity_label.Visible = false;
            priceTextBox.Visible = false;
            priceTextBox.Text = "";
        }

        private void start_recv_data()
        {
            stockCodeTextBox.Focus();
            stockCodeTextBox.SelectAll();
            currentPriceTextBox.Text = "";
            closingTextBox.Text = "";
            textBox9.Text = "";
            closingTextBox.Text = "";
            risingPriceTextBox.Text = "";
            dropRateTextBox.Text = "";
            riseRateTextBox.Text = "";
            F2PoolTextBox.Text = "";

            string temp;
            stockcode = int.Parse(stockCodeTextBox.Text);
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
            mStockCode = temp;

            if (thread == null)
            {
                //创建一个线程
                thread = new Thread(Run);
                thread.IsBackground = true;
                thread.Start();
            }
        }

        private void post_order()
        {
            float price = float.Parse(priceTextBox.Text);
            int num = int.Parse(quantityTextBox.Text);
            if (mStockCode == null)
                return;
            if (choice_f == 1)
            {
                string str = "http://" + ip_jiaoyi + "/buy?stock=" + mStockCode + "&price=" + price +
                 "&share=" + num;

                Thread thread = new Thread(new ParameterizedThreadStart(post_msg_to_main_wnd));
                thread.IsBackground = true;
                thread.Start(str);
            }
            if (choice_f == 2)
            {
                string str = "http://" + ip_jiaoyi + "/sell?stock=" + mStockCode + "&price=" + price +
                 "&share=" + num;
                Thread thread = new Thread(new ParameterizedThreadStart(post_msg_to_main_wnd));
                thread.IsBackground = true;
                thread.Start(str);
            }
            if (choice_f == 3)
            {
                string str = "http://" + ip_jiaoyi + "/short?type=frompool&stock=" + mStockCode + "&price=" + price +
                     "&share=" + num;
                Thread thread = new Thread(new ParameterizedThreadStart(post_msg_to_main_wnd));
                thread.IsBackground = true;
                thread.Start(str);
            }
            if (choice_f == 4)
            {
                string str = "http://" + ip_jiaoyi + "/short?type=direct&stock=" + mStockCode +
                    "&share=" + num + "&price=" + price;
                Thread thread = new Thread(new ParameterizedThreadStart(post_msg_to_main_wnd));
                thread.IsBackground = true;
                thread.Start(str);
            }
            choice_f = 0;
            buy_lable.Visible = false;
            quantityTextBox.Visible = false;
            quantityTextBox.Text = "";
            quantity_label.Visible = false;
            priceTextBox.Visible = false;
            priceTextBox.Text = "";
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
            buy_lable.Visible = true;
            quantityTextBox.Visible = true;

            quantity_label.Visible = true;
            priceTextBox.Visible = true;

            label3.Text = "F2池";

            buy_lable.Text = "买入";
            buy_lable.ForeColor = RGB(0x5C5CFF); ;

            quantity_label.Text = "股数";
            quantity_label.ForeColor = RGB(0x5C5CFF); ;

            if (QuoteList.Items.Count > 0)
            {
                if (QuoteList.Items[11].SubItems[1].Text == "0.00")
                    priceTextBox.Text = QuoteList.Items[9].SubItems[1].Text;
                else
                    priceTextBox.Text = QuoteList.Items[11].SubItems[1].Text;

                quantityTextBox.Text = stocknum.ToString();
                priceTextBox.SelectAll();
            }
            else
            {
                priceTextBox.Text = "";
            }
            priceTextBox.Focus();
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
            buy_lable.Visible = true;
            quantityTextBox.Visible = true;

            quantity_label.Visible = true;
            priceTextBox.Visible = true;

            label3.Text = "F2池";
            label5.Text = "";
            F3PoolTextBox.Visible = false;

            buy_lable.Text = "卖出";
            buy_lable.ForeColor = RGB(0x65E339);

            quantity_label.Text = "股数";
            quantity_label.ForeColor = RGB(0x65E339);

            if (QuoteList.Items.Count > 0)
            {
                if (QuoteList.Items[9].SubItems[1].Text == "0.00")
                    priceTextBox.Text = QuoteList.Items[11].SubItems[1].Text;
                else
                    priceTextBox.Text = QuoteList.Items[9].SubItems[1].Text;
                quantityTextBox.Text = stocknum.ToString();
                priceTextBox.SelectAll();
            }
            else
            {
                priceTextBox.Text = "";
            } 
            priceTextBox.Focus();
            priceTextBox.Focus();
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
            buy_lable.Visible = true;
            quantityTextBox.Visible = true;

            quantity_label.Visible = true;
            priceTextBox.Visible = true;

            label3.Text = "F3池：";
            label5.Text = "";
            F3PoolTextBox.Visible = false;

            buy_lable.Text = "卖出";
            buy_lable.ForeColor = Color.Blue;

            quantity_label.Text = "股数";
            quantity_label.ForeColor = Color.Blue;

            if (QuoteList.Items.Count > 0)
            {
                if (QuoteList.Items[9].SubItems[1].Text == "0.00")
                    priceTextBox.Text = QuoteList.Items[11].SubItems[1].Text;
                else
                    priceTextBox.Text = QuoteList.Items[9].SubItems[1].Text;
                quantityTextBox.Text = stocknum.ToString();
                priceTextBox.SelectAll();
            }
            else
            {
                priceTextBox.Text = "";
            }
            priceTextBox.Focus();
            choice_f = 3;
        }

        private void handle_f4_msg()
        {
            buy_lable.Visible = true;
            quantityTextBox.Visible = true;

            quantity_label.Visible = true;
            priceTextBox.Visible = true;

            label3.Text = "公共池";
            label5.Text = "";
            F3PoolTextBox.Visible = false;

            buy_lable.Text = "卖出";
            buy_lable.ForeColor = Color.Blue;

            quantity_label.Text = "股数";
            quantity_label.ForeColor = Color.Blue;

            if (QuoteList.Items.Count > 0)
            {
                priceTextBox.Text = risingPriceTextBox.Text;
                quantityTextBox.Text = stocknum.ToString();
                priceTextBox.SelectAll();
            }
            else
            {
                priceTextBox.Text = "";
            }
            priceTextBox.Focus();
            choice_f = 4;
        }

        private void handle_f5_msg()
        {
            buy_lable.Visible = true;
            quantityTextBox.Visible = true;

            quantity_label.Visible = true;
            priceTextBox.Visible = true;

            label3.Text = "公共池";
            label5.Text = "";
            F3PoolTextBox.Visible = false;


            buy_lable.Text = "卖出";
            buy_lable.ForeColor = Color.Blue;

            quantity_label.Text = "股数";
            quantity_label.ForeColor = Color.Blue;

            if (QuoteList.Items.Count > 0)
            {
                priceTextBox.Text = QuoteList.Items[9].SubItems[1].Text;
                quantityTextBox.Text = stocknum.ToString();
                priceTextBox.SelectAll();
            }
            else
            {
                priceTextBox.Text = "";
            }
            priceTextBox.Focus();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
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
            if (e.KeyCode == Keys.Enter && stockCodeTextBox.Focused)
            {
                start_recv_data();
            }
            if (e.KeyCode == Keys.Enter && quantityTextBox.Focused)
            {
                post_order();
            }
            if (e.KeyCode == Keys.Enter && priceTextBox.Focused)
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

        private void OnMouseDown(object sender, MouseEventArgs e)
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

        private void OnFromLoad(object sender, EventArgs e)
        {
            //Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void stockCodeTextBox_OnMouseClick(object sender, MouseEventArgs e)
        {
            stockCodeTextBox.Focus();
            stockCodeTextBox.SelectAll();
        }

        //实现tab键的切换
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Tab && quantityTextBox.Focused)
            {
                priceTextBox.Focus();
                return true;
            }
            return false;
        }

        private void priceTextBox_OnMouseClick(object sender, MouseEventArgs e)
        {
            priceTextBox.Focus();
            priceTextBox.SelectAll();
        }

        private void quantityTextBox_OnMouseClick(object sender, MouseEventArgs e)
        {
            quantityTextBox.Focus();
            quantityTextBox.SelectAll();
        }

        private void OnClick(object sender, EventArgs e)
        {
            stockCodeTextBox.Focus();
        }

        /*实现在客户区点击时候自动聚焦到StocCodetextBox上面*/
        public const int WM_NCLBUTTONDOWN = 0x00A1;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_NCLBUTTONDOWN)
            {
                stockCodeTextBox.Focus();
            }
            base.WndProc(ref m);
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

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            if (thread != null && thread.IsAlive == true)
            {
                thread.Abort();
            }
        }
    }
}
