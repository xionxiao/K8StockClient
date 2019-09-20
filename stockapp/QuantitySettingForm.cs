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
    public partial class QuantitySettingForm : Form
    {
        public int m_DefaultNum;
        public QuantitySettingForm(int num)
        {
            m_DefaultNum = num;
            InitializeComponent();
        }

        private void Form2_KeyDown(object  sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && textBox1.Focused)
            {
                try
                {
                    m_DefaultNum = int.Parse(textBox1.Text);
                }
                catch
                {
                    MessageBox.Show("输入格式不对，请重新输入!");
                    return;
                }
                this.Close();
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.Text = m_DefaultNum.ToString();
        }
    }
}
