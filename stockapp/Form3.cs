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
    public partial class Form3 : Form
    {
        public int m_StockNum=100;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void 报价ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1(m_StockNum);
            f1.Show();
        }

        private void 设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Form2 f2 = new Form2();
            //f2.ShowDialog();
            //m_StockNum = f2.m_DefaultNum;
        }
    }
}
