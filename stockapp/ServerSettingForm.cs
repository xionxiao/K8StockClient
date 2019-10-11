using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using K8.Properties;

namespace K8
{
    public partial class ServerSettingForm : Form
    {
        public ServerSettingForm()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            this.textbox_market_ip.Text = Settings.Default.MarketServerIP;
            this.textbox_trade_ip.Text = Settings.Default.TradeServerIP;
        }

        private void Form_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_ok_Click(sender, e);
            }
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            Settings.Default.MarketServerIP = this.textbox_market_ip.Text;
            Settings.Default.TradeServerIP = this.textbox_trade_ip.Text;
            Settings.Default.Save();
            Settings.Default.Upgrade();
            this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
