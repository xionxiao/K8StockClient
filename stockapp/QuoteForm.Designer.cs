namespace K8
{
    partial class QuoteForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.QuoteList = new System.Windows.Forms.ListView();
            this.stockCodeInputBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.transaction_list = new System.Windows.Forms.ListView();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.TextBox();
            this.richTextBox2 = new System.Windows.Forms.TextBox();
            this.richTextBox3 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.transaction_detail_list = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // QuoteList
            // 
            this.QuoteList.BackColor = System.Drawing.Color.Black;
            this.QuoteList.ForeColor = System.Drawing.Color.Gray;
            this.QuoteList.Location = new System.Drawing.Point(-1, 100);
            this.QuoteList.Name = "QuoteList";
            this.QuoteList.Size = new System.Drawing.Size(182, 410);
            this.QuoteList.TabIndex = 0;
            this.QuoteList.UseCompatibleStateImageBehavior = false;
            this.QuoteList.View = System.Windows.Forms.View.Details;
            // 
            // stockCodeInputBox
            // 
            this.stockCodeInputBox.Location = new System.Drawing.Point(4, 1);
            this.stockCodeInputBox.Name = "stockCodeInputBox";
            this.stockCodeInputBox.Size = new System.Drawing.Size(53, 21);
            this.stockCodeInputBox.TabIndex = 0;
            this.stockCodeInputBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBox1_MouseClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(185, 520);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 12);
            this.label3.TabIndex = 19;
            this.label3.Text = "F2池";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(-1, 520);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "买入";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(286, 520);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 12);
            this.label5.TabIndex = 21;
            this.label5.Text = "F3池";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(89, 520);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 12);
            this.label6.TabIndex = 22;
            this.label6.Text = "数量";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(217, 516);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(57, 21);
            this.textBox3.TabIndex = 23;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(318, 516);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(57, 21);
            this.textBox4.TabIndex = 24;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(31, 516);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(57, 21);
            this.textBox5.TabIndex = 25;
            this.textBox5.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBox5_MouseClick);
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(121, 516);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(57, 21);
            this.textBox6.TabIndex = 26;
            this.textBox6.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBox6_MouseClick);
            this.textBox6.TextChanged += new System.EventHandler(this.textBox6_TextChanged);
            // 
            // transaction_list
            // 
            this.transaction_list.BackColor = System.Drawing.Color.Black;
            this.transaction_list.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.transaction_list.ForeColor = System.Drawing.Color.Gray;
            this.transaction_list.Location = new System.Drawing.Point(183, 249);
            this.transaction_list.Name = "transaction_list";
            this.transaction_list.Size = new System.Drawing.Size(196, 261);
            this.transaction_list.TabIndex = 27;
            this.transaction_list.UseCompatibleStateImageBehavior = false;
            this.transaction_list.View = System.Windows.Forms.View.Details;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(91, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 12);
            this.label1.TabIndex = 28;
            this.label1.Text = "涨停";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(2, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 12);
            this.label2.TabIndex = 29;
            this.label2.Text = "现价";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(91, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 12);
            this.label7.TabIndex = 30;
            this.label7.Text = "跌停";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(60, 5);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 12);
            this.label8.TabIndex = 31;
            this.label8.Text = "金额";
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(33, 27);
            this.textBox7.MaxLength = 5;
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(52, 21);
            this.textBox7.TabIndex = 34;
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(94, 1);
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            this.textBox9.Size = new System.Drawing.Size(83, 21);
            this.textBox9.TabIndex = 36;
            // 
            // textBox11
            // 
            this.textBox11.Location = new System.Drawing.Point(125, 73);
            this.textBox11.Name = "textBox11";
            this.textBox11.ReadOnly = true;
            this.textBox11.Size = new System.Drawing.Size(52, 21);
            this.textBox11.TabIndex = 41;
            // 
            // textBox12
            // 
            this.textBox12.Location = new System.Drawing.Point(33, 73);
            this.textBox12.Name = "textBox12";
            this.textBox12.ReadOnly = true;
            this.textBox12.Size = new System.Drawing.Size(52, 21);
            this.textBox12.TabIndex = 40;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(2, 77);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 12);
            this.label10.TabIndex = 39;
            this.label10.Text = "今开";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(91, 77);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(31, 12);
            this.label11.TabIndex = 38;
            this.label11.Text = "昨收";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.richTextBox1.ForeColor = System.Drawing.Color.Red;
            this.richTextBox1.Location = new System.Drawing.Point(125, 26);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(52, 21);
            this.richTextBox1.TabIndex = 42;
            // 
            // richTextBox2
            // 
            this.richTextBox2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.richTextBox2.ForeColor = System.Drawing.Color.Green;
            this.richTextBox2.Location = new System.Drawing.Point(125, 49);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.Size = new System.Drawing.Size(52, 21);
            this.richTextBox2.TabIndex = 43;
            // 
            // richTextBox3
            // 
            this.richTextBox3.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.richTextBox3.Location = new System.Drawing.Point(33, 50);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.ReadOnly = true;
            this.richTextBox3.Size = new System.Drawing.Size(52, 21);
            this.richTextBox3.TabIndex = 44;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(2, 54);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 12);
            this.label9.TabIndex = 45;
            this.label9.Text = "涨幅";
            // 
            // transaction_detail_list
            // 
            this.transaction_detail_list.BackColor = System.Drawing.Color.Black;
            this.transaction_detail_list.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.transaction_detail_list.ForeColor = System.Drawing.Color.Gray;
            this.transaction_detail_list.Location = new System.Drawing.Point(184, 1);
            this.transaction_detail_list.Name = "transaction_detail_list";
            this.transaction_detail_list.Size = new System.Drawing.Size(195, 251);
            this.transaction_detail_list.TabIndex = 46;
            this.transaction_detail_list.UseCompatibleStateImageBehavior = false;
            this.transaction_detail_list.View = System.Windows.Forms.View.Details;
            // 
            // QuoteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 540);
            this.Controls.Add(this.transaction_detail_list);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.richTextBox3);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.textBox11);
            this.Controls.Add(this.textBox12);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textBox9);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.transaction_list);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.stockCodeInputBox);
            this.Controls.Add(this.QuoteList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "QuoteForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Click += new System.EventHandler(this.Form1_Click);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView QuoteList;
        private System.Windows.Forms.TextBox stockCodeInputBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.ListView transaction_list;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox richTextBox1;
        private System.Windows.Forms.TextBox richTextBox2;
        private System.Windows.Forms.TextBox richTextBox3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ListView transaction_detail_list;
    }
}

