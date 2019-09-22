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

        private void Form4_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = Settings.Default.MarketServerIP;
            this.textBox2.Text = Settings.Default.TradeServerIP;
        }

        private void Form4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {  
                this.Close();
            }
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.MarketServerIP = this.textBox1.Text;
            Settings.Default.TradeServerIP = this.textBox2.Text;
            Settings.Default.Save();
        }
    }
}
