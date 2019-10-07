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
using RestSharp;
using K8.Properties;

namespace K8
{
    public partial class MainForm : Form
    {
        private string mTradeServer = "http://" + Settings.Default.TradeServerIP;
        private string ordernum = "";
        private OrderListDataSet mOrderListDataSet = new OrderListDataSet();
        delegate void Reflist_JArray();
        delegate void Reflist_JObject(JObject jo);

        public MainForm()
        {
            InitializeComponent();
        }

        private void QouteMenuItem_Click(object sender, EventArgs e)
        {
            QuoteForm f1 = new QuoteForm(this);
            f1.Show();
        }

        private void SettingMenuItem_Click(object sender, EventArgs e)
        {
            ServerSettingForm f4 = new ServerSettingForm();
            f4.ShowDialog();
        }

        public class OrderListDataItem
        {
            public string mOrderTime;
            public string mStockCode;
            public string mStockName;
            public double mOrderPrice;
            public int mOrderSize;
            public string mOrderDirect;    // 买卖方向
            public string mOrderId;     // 委托编号
            public string mOrderStatus; // 订单状态

            private void init(JObject jobject)
            {
                this.mOrderTime = jobject["委托时间"].ToString();//Convert.ToDateTime(jobject["委托时间"].ToString());
                this.mStockCode = jobject["证券代码"].ToString();
                this.mStockName = jobject["证券名称"].ToString();
                this.mOrderPrice = Convert.ToDouble(jobject["委托价格"].ToString());
                this.mOrderSize = Convert.ToInt32(jobject["委托数量"].ToString());
                this.mOrderDirect = jobject["买卖标志"].ToString();
                this.mOrderId = jobject["合同编号"].ToString();
                this.mOrderStatus = jobject["状态说明"].ToString();
            }

            public OrderListDataItem(JObject jobject)
            {
                init(jobject);
            }

            public OrderListDataItem(String str)
            {
                JObject jobject = (JObject)JsonConvert.DeserializeObject(str);
                init(jobject);
            }
        }

        public class OrderListDataSet
        {
            public HashSet<String> checked_ids = new HashSet<string>();
            public String selected_id = null;
            private Dictionary<String, OrderListDataItem> mOrderListDataSet = new Dictionary<string,OrderListDataItem>();
            
            public void addItem(OrderListDataItem item)
            {
                if(!mOrderListDataSet.ContainsKey(item.mOrderId))
                {
                    mOrderListDataSet.Add(item.mOrderId, item);
                }
            }

            public OrderListDataItem getItem(String id)
            {
                return mOrderListDataSet[id];
            }

            public OrderListDataItem getItem(int index)
            {
                List<String> list = new List<String>(mOrderListDataSet.Keys);
                return mOrderListDataSet[list[index]];
            }

            public OrderListDataItem[] getItems()
            {
                var array = new OrderListDataItem[mOrderListDataSet.Count];
                int i = 0;
                foreach (var k in mOrderListDataSet.Keys)
                {
                    array[i] = mOrderListDataSet[k];
                    i++;
                }
                return array;
            }
        }

        private void MainFormInit(object sender, EventArgs e)
        {
            this.OrderList.Columns.Add("时间", 80);
            this.OrderList.Columns.Add("代码", 60);
            this.OrderList.Columns.Add("名称", 80);
            this.OrderList.Columns.Add("价格", 70);
            this.OrderList.Columns.Add("股数", 60);
            this.OrderList.Columns.Add("方向", 90);
            this.OrderList.Columns.Add("委托编号", 80);
            this.OrderList.Columns.Add("状态", 60);
            this.OrderList.Columns.Add("0", 60);

            this.PositionList.Columns.Add("证券代码", 80);
            this.PositionList.Columns.Add("证券名称", 80);
            this.PositionList.Columns.Add("仓位方向", 100);
            this.PositionList.Columns.Add("证券数量", 80);
            this.PositionList.Columns.Add("0", 80);
            //this.listView2.Columns.Add("持仓均价", 80);
            //this.listView2.Columns.Add("参考盈亏", 100);
            //this.listView2.Columns.Add("盈亏比例", 80);

            Control.CheckForIllegalCrossThreadCalls = false;

            OrderList.DoubleBuffering(true);
            PositionList.DoubleBuffering(true);

            Thread thread = new Thread(Run);
            thread.IsBackground = true;
            thread.Start();

        }

        private int refresh_ol_count = 0;
        private void refresh_OrderList()
        {
            try
            {
                //是否为创建控件的线程，不是为true
                if (this.OrderList.InvokeRequired)
                {
                    //为当前控件指定委托
                    this.OrderList.Invoke(new Reflist_JArray(refresh_OrderList));
                }
                else
                {
                    OrderList.BeginUpdate();
                    OrderList.Items.Clear();
                    refresh_ol_count++;
                    OrderList.Columns[8].Text = refresh_ol_count.ToString();
                    var data_item = mOrderListDataSet.getItems();
                    for (int i = 0; i < data_item.Length; i++)
                    {

                        ListViewItem view_item = new ListViewItem();
                        view_item.SubItems[0].Text = data_item[i].mOrderTime.ToString();
                        view_item.SubItems.Add(data_item[i].mStockCode);
                        view_item.SubItems.Add(data_item[i].mStockName);
                        view_item.SubItems.Add(data_item[i].mOrderPrice.ToString());
                        view_item.SubItems.Add(data_item[i].mOrderSize.ToString());
                        view_item.SubItems.Add(data_item[i].mOrderDirect);
                        if (data_item[i].mOrderDirect.Equals("买入担保品"))
                        {
                            view_item.ForeColor = Color.Red;
                        }
                        else if (data_item[i].mOrderDirect.Equals("卖出担保品")
                            || data_item[i].mOrderDirect.Equals("融券卖出"))
                        {
                            view_item.ForeColor = QuoteForm.RGB(0x65E339);
                        }

                        view_item.SubItems.Add(data_item[i].mOrderId);
                        view_item.SubItems.Add(data_item[i].mOrderStatus);
                        if (data_item[i].mOrderId.Equals(mOrderListDataSet.selected_id))
                        {
                            view_item.Selected = true;
                        }
                        if (mOrderListDataSet.checked_ids.Contains(data_item[i].mOrderId))
                        {
                            view_item.Checked = true;
                        }
                        OrderList.Items.Add(view_item);
                    }
                    //OrderList.EnsureVisible(OrderList.Items.Count - 1);
                    OrderList.EndUpdate();
                }
            }
            catch
            {
                return;
            }
        }

        private int refresh_pl_count = 0;
        private void refresh_PositionList(JObject ja)
        {
            if (ja == null || ja.Count == 0)
                return;
            //是否为创建控件的线程，不是为true
            if (this.PositionList.InvokeRequired)
            {
                //为当前控件指定委托
                this.PositionList.Invoke(new Reflist_JObject(refresh_PositionList), ja);
            }
            else
            {
                try
                {
                    PositionList.Items.Clear();
                    PositionList.BeginUpdate();
                    PositionList.Columns[4].Text = refresh_pl_count.ToString();
                    refresh_pl_count++;
                    foreach (JProperty jp in ja.Properties())
                    {
                        ListViewItem item1 = new ListViewItem();
                        item1.SubItems[0].Text = ja[jp.Name]["证券代码"].ToString();
                        item1.SubItems.Add(ja[jp.Name]["证券名称"].ToString());
                        item1.SubItems.Add(ja[jp.Name]["买卖标志"].ToString());
                        item1.SubItems.Add(ja[jp.Name]["成交数量"].ToString());

                        if (ja[jp.Name]["买卖标志"].ToString().Contains("买入"))
                        {
                            item1.ForeColor = Color.Red;
                        }
                        else if (ja[jp.Name]["买卖标志"].ToString().Contains("卖出"))
                        {
                            item1.ForeColor = QuoteForm.RGB(0x65E339); ;
                        }
                        PositionList.Items.Add(item1);
                    }
                    PositionList.EndUpdate();
                }
                catch
                {
                    return;
                }
            } 
        }

        private JArray str_to_jarray(string field, string source_str)
        {
            if (source_str == null)
                return null;
            try
            {
                JObject jo = (JObject)JsonConvert.DeserializeObject(source_str);
                if (jo.Count == 0 || jo[field] == null)
                    return null;
                JArray ja = (JArray)JsonConvert.DeserializeObject(jo[field].ToString());
                return ja;
            }
            catch
            {
                return null;
            }

        }

        private JObject str_to_jobject(string field, string str)
        {
            if (str == null)
                return null;
            try
            {
                JObject jo = (JObject)JsonConvert.DeserializeObject(str);
                if (jo.Count == 0 || jo[field] == null)
                    return null;
                JObject jo1 = (JObject)JsonConvert.DeserializeObject(jo[field].ToString());
                return jo1;
            }
            catch
            {
                return null;
            }
        }

        public void Run()
        {
            FetchOrdersList(false);
            FetchDeals(false);
        }

        public void FetchPosition(bool once=true)
        {
            var client = new RestClient(mTradeServer);
            var request = new RestRequest("query", Method.GET);
            request.AddParameter("catalogues", "position");

            client.ExecuteAsync(request, response =>
            {
                var temp = str_to_jarray("position", response.Content);
                DataSet.gPositionList = temp;
                if (once == false)
                {
                    Thread.Sleep(Settings.Default.PositionRefreshDelay);
                    FetchPosition(false);
                }
            });
        }

        public void FetchStockPool(bool once = true)
        {
            var client = new RestClient(mTradeServer);
            var request = new RestRequest("query", Method.GET);
            request.AddParameter("catalogues", "stockpool");

            client.ExecuteAsync(request, response =>
            {
                var temp = str_to_jobject("stockpool", response.Content);
                DataSet.gStockPool = temp;
                if (once == false)
                {
                    Thread.Sleep(Settings.Default.StockPoolRefreshDelay);
                    FetchStockPool(false);
                }
            });
        }

        public void FetchOrdersList(bool once = true)
        {
            var client = new RestClient(mTradeServer);
            var request = new RestRequest("query", Method.GET);
            request.AddParameter("catalogues", "orderlist");

            client.ExecuteAsync(request, response =>
            {
                var temp = str_to_jarray("orderlist", response.Content);
                for (int i=0; i<temp.Count; i++) {
                    mOrderListDataSet.addItem(new OrderListDataItem((JObject)temp[i]));
                }
                refresh_OrderList();
                if (once == false)
                {
                    Thread.Sleep(Settings.Default.OrdersListRefreshDelay);
                    FetchOrdersList(false);
                }
            });
        }

        public void FetchDeals(bool once = true)
        {
            var client = new RestClient(mTradeServer);
            var request = new RestRequest("query", Method.GET);
            request.AddParameter("catalogues", "deals");
            client.ExecuteAsync(request, res =>
            {
                var temp = str_to_jarray("deals", res.Content);
                refresh_PositionList(filter_data(temp));
                DataSet.gDeals = temp;
                if (once == false)
                {
                    Thread.Sleep(Settings.Default.DealsRefreshDelay);
                    FetchDeals(false);
                }
            });
        }

        private JObject filter_data(JArray ja)
        {
            if (ja == null)
                return null;
            JObject jo = new JObject();
            int num = ja.Count;
            JArray jacopy = new JArray();
            for (int i = 0; i < num; i++)
            {
                if (ja[i]["状态说明"].ToString().Contains("普通成交"))
                {
                    jacopy.Add(ja[i]);
                }
            }
            for (int i = 0; i < jacopy.Count; i++)
            {
                if (jo[jacopy[i]["证券代码"].ToString()] == null)
                    jo[jacopy[i]["证券代码"].ToString()] = jacopy[i];

                else
                {
                    int num1 = 0, num2 = 0;//买入+ 卖出-

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
                    else if (jacopy[i]["买卖标志"].ToString().Contains("卖出"))
                    {
                        num2 = -1 * int.Parse(jacopy[i]["成交数量"].ToString());
                    }

                    if (num1 + num2 > 0)
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

        private void OrderList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                OrderList.FocusedItem.Checked = !OrderList.FocusedItem.Checked;
                e.Handled = e.SuppressKeyPress = true;
            }
            if (e.KeyCode == Keys.Escape)
            {
                string stock = "";
                for (int i = 0; i < OrderList.Items.Count; i++)
                {
                    if (ordernum.Equals(OrderList.Items[i].SubItems[6].Text))
                    {
                        stock = OrderList.Items[i].SubItems[1].Text;
                    }
                }
                string str = mTradeServer + "/cancel?stock=" + stock + "&order=" + ordernum;
                string ret = QuoteForm.GetRequestData_Post(str);
            }
        }

        protected void refresh_control()
        {
            string temp = mTradeServer + "/query?catalogues=orderlist";
            string str = QuoteForm.GetRequestData(temp);
            JArray ja = str_to_jarray("orderlist", str);
            refresh_OrderList();

            temp = mTradeServer + "/query?catalogues=deals";
            string str1 = QuoteForm.GetRequestData(temp);
            ja = str_to_jarray("deals", str1);
            refresh_PositionList(filter_data(ja));
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x63:
                    Win32API.My_lParam ml = new Win32API.My_lParam();
                    Type t = ml.GetType();
                    ml = (Win32API.My_lParam)m.GetLParam(t);
                    if (ml.s != null)
                        OutPutBox.Items.Add(ml.s);
                    refresh_control();
                    break;
            }
            base.WndProc(ref m);
        }

        private void OrderList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OrderList.SelectedItems.Count != 0)
            {
                OrderList.SelectedItems[0].Focused = true;
                mOrderListDataSet.selected_id = OrderList.SelectedItems[0].SubItems[6].Text;
            }
        }

        private void OrderList_CheckedIndexChanged(object sender, EventArgs e)
        {
            /*
            for (int i = 0; i < OrderList.CheckedItems.Count; i++)
            {
                mOrderListDataSet.checked_ids.Add(OrderList.CheckedItems[i].SubItems[6].Text);
            }
             * */
        }

        private void OrderList_Check(object sender, ItemCheckEventArgs e)
        {
            if (e.CurrentValue == CheckState.Checked)
            {
                mOrderListDataSet.checked_ids.Remove(OrderList.Items[e.Index].SubItems[6].Text);
            }
            else if (e.CurrentValue == CheckState.Unchecked)
            {
                mOrderListDataSet.checked_ids.Add(OrderList.Items[e.Index].SubItems[6].Text);
            }
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
