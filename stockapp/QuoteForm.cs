using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Threading;
using System.Timers;
using System.Runtime.Serialization;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

using System.Reflection;
using System.Reflection.Emit;
using RestSharp;
using K8.Properties;

namespace K8
{
    public partial class QuoteForm : Form
    {
        private int stocknum = 100;        /* 增减股数 */
        private string mStockCode = null;
        private int choice_f = 0;           /*区别f1 f2 f3*/
        private MainForm mMainForm;
        delegate void Reflist(JObject ja);   /*声明委托*/
        private string[] CH_NUM = { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九", "十" };
        private RestClient mMarketClient;
        private RestClient mTradeClient;
        private System.Timers.Timer mTimer;

        public QuoteForm(Form fm)
        {
            String mMarketIP = Properties.Settings.Default.MarketServerIP;
            String mTradeIP = Properties.Settings.Default.TradeServerIP;
            this.mMarketClient = new RestClient("http://" + mMarketIP);
            this.mTradeClient = new RestClient("http://" + mTradeIP);

            mMainForm = (MainForm)fm;

            InitializeComponent();

            /*开启双缓冲*/
            QuoteList.DoubleBuffering(true);
            TransactionDetailList.DoubleBuffering(true);
            TransactionList.DoubleBuffering(true);

            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.QuoteList.Columns.Add("", 40);
            this.QuoteList.Columns.Add("价格", 50);
            this.QuoteList.Columns.Add("数量", 50);
            this.QuoteList.Columns.Add("0", 70);
            ListViewItem item = new ListViewItem();
            for (int i = 0; i < 10; i++)
            {
                item = new ListViewItem("卖"+CH_NUM[10-i]);
                item.ForeColor = Color.White;
                item.UseItemStyleForSubItems = false;
                item.SubItems.Add(new ListViewItem.ListViewSubItem());
                item.SubItems.Add(new ListViewItem.ListViewSubItem());
                this.QuoteList.Items.Add(item);
            }
            item = new ListViewItem();
            item.BackColor = Color.Gray;
            this.QuoteList.Items.Add(item);
            for (int i = 1; i <= 10; i++)
            {
                item = new ListViewItem("买" + CH_NUM[i]);
                item.ForeColor = Color.White;
                item.UseItemStyleForSubItems = false;
                item.SubItems.Add(new ListViewItem.ListViewSubItem());
                item.SubItems.Add(new ListViewItem.ListViewSubItem());
                this.QuoteList.Items.Add(item);
            }

            this.TransactionDetailList.Columns.Add("价格", 40);
            this.TransactionDetailList.Columns.Add("数量", 35);
            this.TransactionDetailList.Columns.Add("D", 24);
            this.TransactionDetailList.Columns.Add("时间", 65);
            //this.TransactionDetailList.Columns.Add("", 10);
            for (int i = 0; i <30; ++i)
            {
                item = new ListViewItem();
                item.UseItemStyleForSubItems = false;
                item.SubItems.Add("");
                item.SubItems.Add("");
                item.SubItems.Add("");
                item.SubItems.Add("");
                TransactionDetailList.Items.Add(item);
            }

            this.TransactionList.Columns.Add("价格", 48);
            this.TransactionList.Columns.Add("数量", 35);
            this.TransactionList.Columns.Add("时间", 48);
            this.TransactionList.Columns.Add("C", 24);
            //this.TransactionList.Columns.Add("", 10);
            for (int i = 0; i < 30; ++i)
            {
                item = new ListViewItem();
                item.UseItemStyleForSubItems = false;
                item.SubItems.Add("");
                item.SubItems.Add("");
                item.SubItems.Add("");
                item.SubItems.Add("");
                TransactionList.Items.Add(item);
            }

            /* 初始化定时器，定时刷新F2、F3池 */
            mTimer = new System.Timers.Timer(1000);
            mTimer.Elapsed += new ElapsedEventHandler(timer_event_handler);
            mTimer.AutoReset = true;
            mTimer.Enabled = true;
        }

        private void timer_event_handler(object sender, ElapsedEventArgs e)
        {
            if (mStockCode != null)
            {
                get_f2_pool();
                get_f3_pool();
            }
        }

        private JObject str_to_jobject(string str)
        {
            try
            {
                JObject jo = (JObject)JsonConvert.DeserializeObject(str);
                return jo;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return null;
            }
        }

        /*更新第三个列表框控件*/
        //private int ref_list3_count = 0;
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
                    //TransactionList.Columns[4].Text = (++ref_list3_count).ToString();
                    //TransactionList.Items.Clear();
                    for (int i = 0; i < ja.Count; i++)
                    {
                        ListViewItem item = TransactionList.Items[i];
                        item.SubItems[0].Text = ja[i]["价格"].ToString().Substring(0,
                            ja[i]["价格"].ToString().IndexOf(".") + 3);
                        int nq = int.Parse(ja[i]["现量"].ToString());
                        item.SubItems[1].Text = (nq.ToString());
                        item.SubItems[2].Text = (ja[i]["时间"].ToString());

                        int num1 = int.Parse(ja[i]["笔数"].ToString());
                        if (num1 == 0)
                        {
                            num1 = 0;
                        }
                        else
                            num1 = nq / num1;
                        item.SubItems[3].Text = (num1.ToString());

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
                    }
                }
                TransactionList.EndUpdate();
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString());
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
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString());
                return;
            }
        }

        //private int ref_list2_count = 0;
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
                    //TransactionDetailList.Columns[4].Text = (++ref_list2_count).ToString();
                    //TransactionDetailList.Items.Clear();

                    for (int i = 0; i < ja.Count; i++)
                    {
                        ListViewItem item = TransactionDetailList.Items[i];
                        item.UseItemStyleForSubItems = false;
                        item.SubItems[0].Text = ja[i]["价格"].ToString().Substring(0,
                            ja[i]["价格"].ToString().IndexOf(".") + 3);
                        item.SubItems[1].Text = (ja[i]["成交量"].ToString().Substring(0,
                           ja[i]["成交量"].ToString().IndexOf(".")));
                        item.SubItems[2].Text = (ja[i]["性质"].ToString());
                        item.SubItems[3].Text = (ja[i]["成交时间"].ToString());
                        if (ja[i]["性质"].ToString() == "S")
                        {
                            item.SubItems[0].ForeColor = RGB(0x65E339); //green
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
                    }
                    TransactionDetailList.EndUpdate();
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString());
                return;
            }
        }

        private void refresh_quote_controls(JObject jo)
        {
            try
            {
                var ja = jo["quote10"];
                double price = double.Parse(ja[0]["昨收"].ToString());
                this.Text = ja[0]["名称"].ToString();
                txb_current_price.Text = ja[0]["现价"].ToString().Substring(0,
                            ja[0]["现价"].ToString().IndexOf(".") + 3);
                double now_price = Double.Parse(txb_current_price.Text);
                double rate = (now_price - price) / price;
                if (rate > 0)
                    txb_rise_rate.ForeColor = RGB(0x5C5CFF);
                else
                    txb_rise_rate.ForeColor = Color.Green;
                txb_rise_rate.Text = string.Format("{0:0.00%}", rate);
                double amount = double.Parse(ja[0]["总金额"].ToString());
                amount /= 10000;
                txb_money.Text = string.Format("{0:N0}", amount) + " W";
                txb_openning_price.Text = ja[0]["开盘"].ToString().Substring(0,
                    ja[0]["开盘"].ToString().IndexOf(".") + 3);
                txb_closing_price.Text = ja[0]["昨收"].ToString().Substring(0,
                            ja[0]["昨收"].ToString().IndexOf(".") + 3);
                double temp1 = price * 1.1;
                this.txb_rising_price.Text = string.Format("{0:F2}", temp1);
                temp1 = price * 0.9;
                this.txb_limit_price.Text = string.Format("{0:F2}", temp1);
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString());
                return;
            }
        }

        public void FetchQuote(bool once = true)
        {
            if (mMarketClient != null)
            {
                var request = new RestRequest("query", Method.GET);
                request.AddParameter("catalogues", "quote10");
                request.AddParameter("stocks", mStockCode);

                // AsyncRequest is MultiThread
                mMarketClient.ExecuteAsync(request, response =>
                {
                    var temp = str_to_jobject(response.Content);
                    RefreshQuoteList(temp);
                    refresh_quote_controls(temp);
                    if (!once)
                    {
                        Thread.Sleep(Settings.Default.QuoteRefreshDelay);
                        FetchQuote(false);
                    }
                });
            }
        }

        public void FetchTransactionDetail(bool once = true)
        {
            if (mMarketClient != null)
            {
                var request = new RestRequest("query", Method.GET);
                request.AddParameter("catalogues", "transaction_detail");
                request.AddParameter("stock", mStockCode);

                mMarketClient.ExecuteAsync(request, response =>
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
        }

        public void FetchTransaction(bool once = true)
        {
            if (mMarketClient != null)
            {
                var request = new RestRequest("query", Method.GET);
                request.AddParameter("catalogues", "transaction");
                request.AddParameter("stock", mStockCode);
            
                mMarketClient.ExecuteAsync(request, response =>
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
        }

        private void handle_shiftup_msg()
        {
            if (txb_price.Focused)
            {
                try
                {
                    float price = float.Parse(txb_price.Text);
                    price += 0.01F;
                    txb_price.Text = price.ToString();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString()); 
                    return;
                }
                txb_price.Focus();
                txb_price.SelectAll();
            }
            if (txb_amount.Focused)
            {
                try
                {
                    int num = int.Parse(txb_amount.Text);
                    num += 100;
                    txb_amount.Text = num.ToString();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                    return;
                }
                txb_amount.Focus();
                txb_amount.SelectAll();
            }
        }

        private void handle_shiftdown_msg()
        {
            if (txb_price.Focused)
            {
                try
                {
                    double price = double.Parse(txb_price.Text);
                    if (price < 0.01)
                        return;
                    price -= 0.01;
                    txb_price.Text = price.ToString();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                    return;
                }
            }
            if (txb_amount.Focused)
            {
                try
                {
                    int num = int.Parse(txb_amount.Text);
                    if (num < 100)
                        return;
                    num -= 100;
                    txb_amount.Text = num.ToString();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                    return;
                }
            }
        }

        private void clear_form()
        {
            choice_f = 0;
            label_price.Visible = true;
            txb_amount.Text = "";
            label_amount.Visible = true;
            txb_price.Text = "";
            label_f2.Visible = true;
            txb_f2_pool.Visible = true;
            txb_f2_pool.Text = "";
            label_f3.Visible = true;
            txb_f3_pool.Visible = true;
            txb_f3_pool.Text = "";
        }

        private void start_recv_data()
        {
            txb_stockcode.Focus();
            txb_stockcode.SelectAll();
            txb_current_price.Text = "";
            txb_closing_price.Text = "";
            txb_money.Text = "";
            txb_closing_price.Text = "";
            txb_rising_price.Text = "";
            txb_limit_price.Text = "";
            txb_rise_rate.Text = "";

            clear_form();

            mStockCode = txb_stockcode.Text;
            if (!Regex.IsMatch(mStockCode, @"^\d{6}$"))
            {
                MessageBox.Show("请输入正确的股票代码");
                return;
            }

            get_f2_pool();
            get_f3_pool();
            TransactionDetailList.EnsureVisible(TransactionDetailList.Items.Count - 1);
            TransactionList.EnsureVisible(TransactionDetailList.Items.Count - 1);
            FetchQuote(false);
            FetchTransactionDetail(false);
            FetchTransaction(false);
        }

        private void post_order()
        {
            if (mStockCode == null)
                return;
            try
            {

                float price = float.Parse(txb_price.Text);
                int num = int.Parse(txb_amount.Text);

                if (choice_f == 1)
                {
                    var request = new RestRequest("buy", Method.POST);
                    request.AddParameter("stock", mStockCode);
                    request.AddParameter("price", price);
                    request.AddParameter("share", num);

                    mTradeClient.ExecuteAsync(request, response =>
                    {
                        var res = str_to_jobject(response.Content);
                        if (res != null)
                        {
                            if (res["result"] != null)
                            {
                                mMainForm.PrintMessage(res["result"].ToString());
                            }
                            if (res["error"] != null)
                            {
                                mMainForm.PrintMessage(res["error"].ToString());
                            }
                        }
                    });
                }
                if (choice_f == 2)
                {
                    var request = new RestRequest("sell", Method.POST);
                    request.AddParameter("stock", mStockCode);
                    request.AddParameter("price", price);
                    request.AddParameter("share", num);

                    mTradeClient.ExecuteAsync(request, response =>
                    {
                        var res = str_to_jobject(response.Content);
                        if (res != null)
                        {
                            if (res["result"] != null)
                            {
                                mMainForm.PrintMessage(res["result"].ToString());
                            }
                            if (res["error"] != null)
                            {
                                mMainForm.PrintMessage(res["error"].ToString());
                            }
                        }
                    });
                }
                if (choice_f == 3)
                {
                    var request = new RestRequest("short", Method.POST);
                    request.AddParameter("type", "frompool");
                    request.AddParameter("stock", mStockCode);
                    request.AddParameter("price", price);
                    request.AddParameter("share", num);

                    mTradeClient.ExecuteAsync(request, response =>
                    {
                        var res = str_to_jobject(response.Content);
                        if (res != null)
                        {
                            if (res["result"] != null)
                            {
                                mMainForm.PrintMessage(res["result"].ToString());
                            }
                            if (res["error"] != null)
                            {
                                mMainForm.PrintMessage(res["error"].ToString());
                            }
                        }
                    });
                }
                if (choice_f == 4)
                {
                    var request = new RestRequest("short", Method.POST);
                    request.AddParameter("stock", mStockCode);
                    request.AddParameter("price", price);
                    request.AddParameter("share", num);

                    mTradeClient.ExecuteAsync(request, response =>
                    {
                        var res = str_to_jobject(response.Content);
                        if (res != null)
                        {
                            if (res["result"] != null)
                            {
                                mMainForm.PrintMessage(res["result"].ToString());
                            }
                            if (res["error"] != null)
                            {
                                mMainForm.PrintMessage(res["error"].ToString());
                            }
                        }
                    });
                }
                clear_form();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void handle_f1_msg()
        {
            label_price.Visible = true;
            txb_amount.Visible = true;
            label_price.Text = "买入";
            label_price.ForeColor = RGB(0x5C5CFF);

            label_amount.Visible = true;
            txb_price.Visible = true;
            label_amount.Text = "股数";
            label_amount.ForeColor = RGB(0x5C5CFF); ;

            label_f2.Visible = true;
            txb_f2_pool.Visible = true;
            get_f2_pool();

            label_f3.Visible = false;
            txb_f3_pool.Visible = false;

            if (QuoteList.Items.Count > 0)
            {
                if (QuoteList.Items[11].SubItems[1].Text == "0.00")
                    txb_price.Text = QuoteList.Items[9].SubItems[1].Text;
                else
                    txb_price.Text = QuoteList.Items[11].SubItems[1].Text;

                txb_amount.Text = stocknum.ToString();
                txb_price.SelectAll();
            }
            else
            {
                txb_price.Text = "";
            }
            txb_price.Focus();
            choice_f = 1;
        }

        private void get_f2_pool()
        {
            if (mStockCode != null && DataSet.gPositionList.ContainsKey(mStockCode))
            {
                txb_f2_pool.Text = DataSet.gPositionList[mStockCode].available_quantitiy.ToString();
            }
            else
            {
                txb_f2_pool.Text = "0";
            }
        }

        private void handle_f2_msg()
        {
            label_price.Visible = true;
            txb_amount.Visible = true;
            label_price.Text = "卖出";
            label_price.ForeColor = RGB(0x65E339);

            label_amount.Text = "股数";
            label_amount.ForeColor = RGB(0x65E339);

            label_amount.Visible = true;
            txb_price.Visible = true;

            label_f2.Visible = true;
            txb_f2_pool.Visible = true;
            get_f2_pool();

            label_f3.Visible = false;
            txb_f3_pool.Visible = false;

            if (QuoteList.Items.Count > 0)
            {
                if (QuoteList.Items[9].SubItems[1].Text == "0.00")
                    txb_price.Text = QuoteList.Items[11].SubItems[1].Text;
                else
                    txb_price.Text = QuoteList.Items[9].SubItems[1].Text;
                txb_amount.Text = stocknum.ToString();
                txb_price.SelectAll();
            }
            else
            {
                txb_price.Text = "";
            }
            txb_price.Focus();
            txb_price.Focus();
            choice_f = 2;
        }

        private void handle_f3_msg()
        {
            label_price.Visible = true;
            label_price.Text = "卖出";
            label_price.ForeColor = Color.Blue;
            txb_price.Visible = true;

            label_amount.Visible = true;
            label_amount.Text = "股数";
            label_amount.ForeColor = Color.Blue;
            txb_amount.Visible = true;

            label_f2.Visible = false;
            txb_f2_pool.Visible = false;

            label_f3.Visible = true;
            txb_f3_pool.Visible = true;
            get_f3_pool();

            if (QuoteList.Items.Count > 0)
            {
                if (QuoteList.Items[9].SubItems[1].Text == "0.00")
                    txb_price.Text = QuoteList.Items[11].SubItems[1].Text;
                else
                    txb_price.Text = QuoteList.Items[9].SubItems[1].Text;
                txb_amount.Text = stocknum.ToString();
                txb_price.SelectAll();
            }
            else
            {
                txb_price.Text = "";
            }
            txb_price.Focus();
            choice_f = 3;
        }

        private void get_f3_pool()
        {
            if (mStockCode != null && DataSet.gStockPool.ContainsKey(mStockCode))
            {
                txb_f3_pool.Text = DataSet.gStockPool[mStockCode].ToString();
            }
            else
            {
                txb_f3_pool.Text = "0";
            }
        }

        private void handle_f4_msg()
        {
            label_price.Visible = true;
            txb_amount.Visible = true;

            label_amount.Visible = true;
            txb_price.Visible = true;

            label_f3.Visible = false;
            txb_f3_pool.Visible = false;
            
            label_price.Text = "卖出";
            label_price.ForeColor = Color.Blue;

            label_amount.Text = "股数";
            label_amount.ForeColor = Color.Blue;

            if (QuoteList.Items.Count > 0)
            {
                txb_price.Text = txb_rising_price.Text;
                txb_amount.Text = stocknum.ToString();
                txb_price.SelectAll();
            }
            else
            {
                txb_price.Text = "";
            }
            txb_price.Focus();
            choice_f = 4;
        }

        private void handle_f5_msg()
        {
            label_price.Visible = true;
            txb_amount.Visible = true;

            label_amount.Visible = true;
            txb_price.Visible = true;

            label_f3.Visible = false;
            txb_f3_pool.Visible = false;

            label_price.Text = "卖出";
            label_price.ForeColor = Color.Blue;

            label_amount.Text = "股数";
            label_amount.ForeColor = Color.Blue;

            if (QuoteList.Items.Count > 0)
            {
                txb_price.Text = QuoteList.Items[9].SubItems[1].Text;
                txb_amount.Text = stocknum.ToString();
                txb_price.SelectAll();
            }
            else
            {
                txb_price.Text = "";
            }
            txb_price.Focus();
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
                clear_form();
            }
            if (e.KeyCode == Keys.Enter && txb_stockcode.Focused)
            {
                start_recv_data();
            }
            if (e.KeyCode == Keys.Enter && txb_amount.Focused)
            {
                post_order();
            }
            if (e.KeyCode == Keys.Enter && txb_price.Focused)
            {
                post_order();
            }
            if (mStockCode == null)
                return;
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

        private void OnFromLoad(object sender, EventArgs e)
        {
            //Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void stockCodeTextBox_OnMouseClick(object sender, MouseEventArgs e)
        {
            txb_stockcode.Focus();
            txb_stockcode.SelectAll();
        }

        //实现tab键的切换
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Tab && txb_amount.Focused)
            {
                txb_price.Focus();
                return true;
            }
            return false;
        }

        private void priceTextBox_OnMouseClick(object sender, MouseEventArgs e)
        {
            txb_price.Focus();
            txb_price.SelectAll();
        }

        private void quantityTextBox_OnMouseClick(object sender, MouseEventArgs e)
        {
            txb_amount.Focus();
            txb_amount.SelectAll();
        }

        private void OnClick(object sender, EventArgs e)
        {
            txb_stockcode.Focus();
        }

        /*实现在客户区点击时候自动聚焦到StocCodetextBox上面*/
        public const int WM_NCLBUTTONDOWN = 0x00A1;   /* 单击标题栏消息 */
        public const int WN_NCLBUTTONDBLCLK = 0x00A3;  /* 双击标题栏消息 */
        public const int WN_NCRBUTTONDOWN = 0x00A4;    /* 右键单击 */
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_NCLBUTTONDOWN)
            {
                txb_stockcode.Focus();
            }
            if (m.Msg == WN_NCLBUTTONDBLCLK)
            {
                this.TopMost = !this.TopMost;
            }
            if (m.Msg == WN_NCRBUTTONDOWN)
            {
                m.WParam = IntPtr.Zero;
                QuantitySettingForm f2 = new QuantitySettingForm(stocknum);
                DialogResult res = f2.ShowDialog();
                if (res == DialogResult.Cancel)
                {
                    stocknum = f2.m_DefaultNum;
                }
                return;
            }
            base.WndProc(ref m);
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            mMarketClient = null;
        }
    }
}
