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

    class DataSet
    {
        public static JArray gOrderList = null;
        public static Dictionary<String, int>gPositionList = new Dictionary<string,int>();
        public static JObject gDeals = null;
        public static Dictionary<String, int> gStockPool = new Dictionary<string, int>();
    }
}
