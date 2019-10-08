using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace K8
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }

    class PositionDataItem
    {
        public PositionDataItem(JObject jo)
        {
            stock_code = jo["证券代码"].ToString();
            stock_name = jo["证券名称"].ToString();
            stock_quantity = Convert.ToInt32(jo["证券数量"].ToString());
            available_quantitiy = Convert.ToInt32(jo["可卖数量"].ToString());
            reference_value = Convert.ToDouble(jo["参考市值"].ToString());
            reference_cost = Convert.ToDouble(jo["参考成本"].ToString());
            reference_price = Convert.ToDouble(jo["参考市值价格"].ToString());
            profit_and_loss_rate = Convert.ToDouble(jo["盈亏比例(%)"].ToString());
            profit_and_lose_value = Convert.ToDouble(jo["浮动盈亏"].ToString());
            purchase_cost_price = Convert.ToDouble(jo["买入成本价"].ToString());
        }

        public string stock_code;                   //证券代码
        public string stock_name;                   //证券名称
        public int stock_quantity;                  //证券数量
        public int available_quantitiy;             //可卖数量
        public double reference_value;              //参考市值
        public double reference_cost;               //参考成本
        public double reference_price;              //参考市值价格
        public double profit_and_loss_rate;         //浮动盈亏（%）
        public double profit_and_lose_value;        //浮动盈亏
        public double purchase_cost_price;          //买入成本价
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

    static class DataSet
    {
        public static JArray gOrderList = null;
        public static Dictionary<String, PositionDataItem> gPositionList = new Dictionary<String, PositionDataItem>();
        public static JObject gDeals = null;
        public static Dictionary<String, int> gStockPool = new Dictionary<string, int>();
    }
}
