using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace stockapp
{
    public partial class ServerSettingForm : Form
    {
        public string ip_hangqing;
        public string ip_jiaoyi;
        public ServerSettingForm(string ip_hq,string ip_jy)
        {
            InitializeComponent();
            this.ip_hangqing = ip_hq;
            this.ip_jiaoyi = ip_jy;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = ip_hangqing;
            this.textBox2.Text = ip_jiaoyi;
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
            ip_hangqing = this.textBox1.Text;
            ip_jiaoyi = this.textBox2.Text;
        }
    }
}
