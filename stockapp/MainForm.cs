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
        private RestClient mTradeClient = new RestClient("http://" + Settings.Default.TradeServerIP);
        private OrderListDataSet mOrderListDataSet = new OrderListDataSet();

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

            public void clear()
            {
                mOrderListDataSet.Clear();
            }

            public OrderListDataItem getItem(String id)
            {
                try
                {
                    return mOrderListDataSet[id];
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                    return null;
                }
            }

            public OrderListDataItem getItem(int index)
            {
                try
                {
                    List<String> list = new List<String>(mOrderListDataSet.Keys);
                    return mOrderListDataSet[list[index]];
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                    return null;
                }
            }

            public OrderListDataItem[] getItems()
            {
                try
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
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                    return null;
                }
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

            this.DayPositionList.Columns.Add("证券代码", 80);
            this.DayPositionList.Columns.Add("证券名称", 80);
            this.DayPositionList.Columns.Add("仓位方向", 100);
            this.DayPositionList.Columns.Add("证券数量", 80);
            this.DayPositionList.Columns.Add("0", 80);
            //this.listView2.Columns.Add("持仓均价", 80);
            //this.listView2.Columns.Add("参考盈亏", 100);
            //this.listView2.Columns.Add("盈亏比例", 80);

            Control.CheckForIllegalCrossThreadCalls = false;

            OrderList.DoubleBuffering(true);
            DayPositionList.DoubleBuffering(true);

            StartFetchData();
        }

        private int refresh_ol_count = 0;
        delegate void Refresh_Delegate();
        private void refresh_OrderList()
        {
            try
            {
                //是否为创建控件的线程，不是为true
                if (this.OrderList.InvokeRequired)
                {
                    //为当前控件指定委托
                    this.OrderList.Invoke(new Refresh_Delegate(refresh_OrderList));
                }
                else
                {
                    refresh_ol_count++;
                    OrderList.Columns[8].Text = refresh_ol_count.ToString();
                    var data_item = mOrderListDataSet.getItems();
                    if (data_item != null)
                    {
                        int top_index = (OrderList.TopItem != null) ? OrderList.TopItem.Index : -1;
                        OrderList.BeginUpdate();
                        OrderList.Items.Clear();
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
                        OrderList.EndUpdate();
                        if (top_index > 0 && OrderList.Items.Count > top_index)
                        {
                            
                            int H = OrderList.Height;
                            int h = OrderList.TopItem.GetBounds(ItemBoundsPortion.Entire).Height;
                            int n = H / h - 1;
                            if ( n + top_index >= OrderList.Items.Count-1)
                                OrderList.TopItem = OrderList.Items[top_index+1];
                            else
                                OrderList.TopItem = OrderList.Items[top_index];
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return;
            }
        }

        private int refresh_pl_count = 0;
        delegate void Reflist_JObject(JObject jo);
        private void refresh_DayPositionList(JObject ja)
        {
            //是否为创建控件的线程，不是为true
            if (this.DayPositionList.InvokeRequired)
            {
                //为当前控件指定委托
                this.DayPositionList.Invoke(new Reflist_JObject(refresh_DayPositionList), ja);
            }
            else
            {
                try
                {
                    DayPositionList.BeginUpdate();
                    DayPositionList.Items.Clear();
                    DayPositionList.Columns[4].Text = refresh_pl_count.ToString();
                    refresh_pl_count++;
                    if (ja != null)
                    {
                        foreach (JProperty jp in ja.Properties())
                        {
                            ListViewItem item = new ListViewItem();
                            item.SubItems[0].Text = ja[jp.Name]["证券代码"].ToString();
                            item.SubItems.Add(ja[jp.Name]["证券名称"].ToString());
                            item.SubItems.Add(ja[jp.Name]["买卖标志"].ToString());
                            item.SubItems.Add(ja[jp.Name]["成交数量"].ToString());

                            if (ja[jp.Name]["买卖标志"].ToString().Contains("买入"))
                            {
                                item.ForeColor = Color.Red;
                            }
                            else if (ja[jp.Name]["买卖标志"].ToString().Contains("卖出"))
                            {
                                item.ForeColor = QuoteForm.RGB(0x65E339); ;
                            }
                            DayPositionList.Items.Add(item);
                        }
                    }
                    DayPositionList.EndUpdate();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
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
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
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
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return null;
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

        private void StartFetchData()
        {
            FetchOrdersList(false);
            FetchDeals(false);
            FetchStockPool(false);
            FetchPosition(false);
        }

        public void FetchPosition(bool once=true)
        {
            var request = new RestRequest("query", Method.GET);
            request.AddParameter("catalogues", "position");

            mTradeClient.ExecuteAsync(request, response =>
            {
                var temp = str_to_jarray("position", response.Content);
                if (temp != null)
                {
                    DataSet.gPositionList.Clear();
                    try
                    {
                        foreach (var i in temp)
                        {
                            JObject jo = (JObject)JsonConvert.DeserializeObject(i.ToString());
                            var obj = new PositionDataItem(jo);
                            DataSet.gPositionList.Add(obj.stock_code, obj);
                        }
                    }
                    catch (Exception e)
                    {
                        PrintMessage(e.ToString());
                    }
                }
                if (once == false)
                {
                    Thread.Sleep(Settings.Default.PositionRefreshDelay);
                    FetchPosition(false);
                }
            });
        }

        public void FetchStockPool(bool once = true)
        {
            var request = new RestRequest("query", Method.GET);
            request.AddParameter("catalogues", "stockpool");

            mTradeClient.ExecuteAsync(request, response =>
            {
                var temp = str_to_jobject("stockpool", response.Content);
                if (temp != null)
                {
                    foreach (var i in temp)
                    {
                        JObject jo = (JObject)JsonConvert.DeserializeObject(i.Value.ToString());
                        if (!DataSet.gStockPool.ContainsKey(i.Key))
                        {
                            DataSet.gStockPool.Add(i.Key, Convert.ToInt32(jo["融券数量"]));
                        }
                    }
                }
                if (once == false)
                {
                    Thread.Sleep(Settings.Default.StockPoolRefreshDelay);
                    FetchStockPool(false);
                }
            });
        }

        public void FetchOrdersList(bool once = true)
        {
            var request = new RestRequest("query", Method.GET);
            request.AddParameter("catalogues", "orderlist");

            mTradeClient.ExecuteAsync(request, response =>
            {
                var temp = str_to_jarray("orderlist", response.Content);
                if (temp != null)
                {
                    mOrderListDataSet.clear();
                    for (int i = 0; i < temp.Count; i++)
                    {
                        mOrderListDataSet.addItem(new OrderListDataItem((JObject)temp[i]));
                    }
                    refresh_OrderList();
                }
                if (once == false)
                {
                    Thread.Sleep(Settings.Default.OrdersListRefreshDelay);
                    FetchOrdersList(false);
                }
            });
        }

        public void FetchDeals(bool once = true)
        {
            var request = new RestRequest("query", Method.GET);
            request.AddParameter("catalogues", "deals");
            mTradeClient.ExecuteAsync(request, res =>
            {
                var temp = str_to_jarray("deals", res.Content);
                var temp1 = filter_data(temp);
                refresh_DayPositionList(temp1);
                DataSet.gDeals = temp1;
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
                foreach(var i in mOrderListDataSet.checked_ids)
                {
                    var order = mOrderListDataSet.getItem(i);
                    if (order != null)
                    {
                        var request = new RestRequest("cancel", Method.POST);
                        request.AddParameter("stock", order.mStockCode);
                        request.AddParameter("order", order.mOrderId);

                        mTradeClient.ExecuteAsync(request, response =>
                        {
                            var res = str_to_jobject(response.Content);
                            if (res != null)
                            {
                                if (res["result"] != null)
                                {
                                    this.PrintMessage(res["result"].ToString());
                                }
                                if (res["error"] != null)
                                {
                                    this.PrintMessage(res["error"].ToString());
                                }
                            }
                
                        });
                    }
                }
            }
        }

        delegate void PrintMessageDelegate(String msg);
        public void PrintMessage(string msg)
        {
            if (this.OutPutBox.InvokeRequired)
            {
                //为当前控件指定委托
                this.OutPutBox.Invoke(new PrintMessageDelegate(PrintMessage), msg);
            }
            else
            {
                int current_index = OutPutBox.TopIndex;
                OutPutBox.Items.Add(msg);
                int visibleItems = OutPutBox.ClientSize.Height / OutPutBox.ItemHeight;
                var index = Math.Max(OutPutBox.Items.Count - visibleItems + 1, 0);
                if (index == 0 || index - current_index < 4)
                {
                    OutPutBox.TopIndex = index;
                }
            }
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

        private void QuitMenu_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
