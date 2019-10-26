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
            private Dictionary<String, OrderListDataItem> mOrderListDataSet = new Dictionary<string, OrderListDataItem>();

            public void addItem(OrderListDataItem item)
            {
                if (!mOrderListDataSet.ContainsKey(item.mOrderId))
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
                catch
                {
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
                catch
                {
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
                catch
                {
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

            Control.CheckForIllegalCrossThreadCalls = false;

            OrderList.DoubleBuffering(true);
            DayPositionList.DoubleBuffering(true);

            StartFetchData();
        }

        private int refresh_ol_count = 0;
        delegate void Refresh_Delegate();
        private void refresh_OrderList()
        {
            if (this.OrderList.InvokeRequired)
            {
                //委托到UI线程执行
                this.OrderList.Invoke(new Refresh_Delegate(refresh_OrderList));
            }
            else
            {
                UpdateOrderList();
            }
        }

        private void UpdateOrderList()
        {
            try
            {
                OrderList.Columns[8].Text = refresh_ol_count++.ToString();
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
                        if (n + top_index >= OrderList.Items.Count - 1)
                            OrderList.TopItem = OrderList.Items[top_index + 1];
                        else
                            OrderList.TopItem = OrderList.Items[top_index];
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private int refresh_pl_count = 0;
        private void refresh_PositionList()
        {
            if (this.DayPositionList.InvokeRequired)
            {
                // 委托到UI线程执行
                this.DayPositionList.Invoke(new Refresh_Delegate(refresh_PositionList));
            }
            else
            {
                UpdatePostionList();
            }
        }

        private void UpdatePostionList()
        {
            try
            {
                DayPositionList.BeginUpdate();
                DayPositionList.Items.Clear();
                DayPositionList.Columns[4].Text = refresh_pl_count++.ToString();

                foreach (var jp in DataSet.gPositionList)
                {
                    var ja = jp.Value;
                    int quantity = ja.inventory_quantitiy - ja.stock_quantity;
                    if (quantity != 0)
                    {
                        ListViewItem item = new ListViewItem();
                        item.SubItems[0].Text = ja.stock_code;
                        item.SubItems.Add(ja.stock_name);
                        string direction = (quantity > 0) ? "买入" : "卖出";
                        item.SubItems.Add(direction);
                        item.SubItems.Add(quantity.ToString());

                        if (direction.Equals("买入"))
                        {
                            item.ForeColor = Color.Red;
                        }
                        else if (direction.Equals("卖出"))
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

        private void StartFetchData()
        {
            FetchOrdersList(false);
            FetchStockPool(false);
            FetchPosition(false);
        }

        public void FetchPosition(bool once = true)
        {
            var client = new RestClient("http://" + Properties.Settings.Default.TradeServerIP);
            var request = new RestRequest("query", Method.GET);
            request.AddParameter("catalogues", "position");

            client.ExecuteAsync(request, response =>
            {
                try
                {
                    var temp = str_to_jarray("position", response.Content);
                    if (temp != null)
                    {
                        try
                        {
                            DataSet.gPositionList.Clear();
                            foreach (var i in temp)
                            {
                                JObject jo = (JObject)JsonConvert.DeserializeObject(i.ToString());
                                var obj = new PositionDataItem(jo);
                                DataSet.gPositionList.TryAdd(obj.stock_code, obj);
                            }
                            refresh_PositionList();
                        }
                        catch (Exception e)
                        {
                            PrintMessage(e.ToString());
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
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
            var client = new RestClient("http://" + Properties.Settings.Default.TradeServerIP);
            var request = new RestRequest("query", Method.GET);
            request.AddParameter("catalogues", "stockpool");

            client.ExecuteAsync(request, response =>
            {
                var temp = str_to_jobject("stockpool", response.Content);
                if (temp != null)
                {
                    try
                    {
                        DataSet.gStockPool.Clear();
                        foreach (var i in temp)
                        {
                            JObject jo = (JObject)JsonConvert.DeserializeObject(i.Value.ToString());
                            DataSet.gStockPool.TryAdd(i.Key, Convert.ToInt32(jo["融券数量"]));
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.ToString());
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
            var client = new RestClient("http://" + Properties.Settings.Default.TradeServerIP);
            var request = new RestRequest("query", Method.GET);
            request.AddParameter("catalogues", "orderlist");
            request.Timeout = 10 * 1000; // 10s

            client.ExecuteAsync(request, response =>
            {
                var temp = str_to_jarray("orderlist", response.Content);
                if (temp != null)
                {
                    try
                    {
                        mOrderListDataSet.clear();
                        for (int i = 0; i < temp.Count; i++)
                        {
                            mOrderListDataSet.addItem(new OrderListDataItem((JObject)temp[i]));
                        }
                        refresh_OrderList();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.ToString());
                    }
                }
                if (once == false)
                {
                    Thread.Sleep(Settings.Default.OrdersListRefreshDelay);
                    FetchOrdersList(false);
                }
            });
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
                foreach (var i in mOrderListDataSet.checked_ids)
                {
                    var order = mOrderListDataSet.getItem(i);
                    if (order != null)
                    {
                        var client = new RestClient("http://" + Properties.Settings.Default.TradeServerIP);
                        var request = new RestRequest("cancel", Method.POST);
                        request.AddParameter("stock", order.mStockCode);
                        request.AddParameter("order", order.mOrderId);

                        client.ExecuteAsync(request, response =>
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
                //委托到UI线程执行
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

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            const string sPath = @"output.log.txt";

            using (StreamWriter sw = File.AppendText(sPath))
            {
                sw.WriteLine(DateTime.Now.ToString());
                foreach (var i in this.OutPutBox.Items)
                {
                    sw.WriteLine(i.ToString());
                }
            }
            base.OnFormClosing(e);
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
