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
            this.HSpliter = new System.Windows.Forms.SplitContainer();
            this.VSpliter = new System.Windows.Forms.SplitContainer();
            this.label4 = new System.Windows.Forms.Label();
            this.riseRateTextBox = new System.Windows.Forms.TextBox();
            this.dropRateTextBox = new System.Windows.Forms.TextBox();
            this.risingPriceTextBox = new System.Windows.Forms.TextBox();
            this.closingTextBox = new System.Windows.Forms.TextBox();
            this.openningTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.moneyTextBox = new System.Windows.Forms.TextBox();
            this.currentPriceTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.stockCodeTextBox = new System.Windows.Forms.TextBox();
            this.QuoteList = new System.Windows.Forms.ListView();
            this.SubVSpliter = new System.Windows.Forms.SplitContainer();
            this.TransactionDetailList = new System.Windows.Forms.ListView();
            this.TransactionList = new System.Windows.Forms.ListView();
            this.quantityTextBox = new System.Windows.Forms.TextBox();
            this.priceTextBox = new System.Windows.Forms.TextBox();
            this.label_amount = new System.Windows.Forms.Label();
            this.lable_buy = new System.Windows.Forms.Label();
            this.F3PoolTextBox = new System.Windows.Forms.TextBox();
            this.F2PoolTextBox = new System.Windows.Forms.TextBox();
            this.label_F3 = new System.Windows.Forms.Label();
            this.label_F2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.HSpliter)).BeginInit();
            this.HSpliter.Panel1.SuspendLayout();
            this.HSpliter.Panel2.SuspendLayout();
            this.HSpliter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VSpliter)).BeginInit();
            this.VSpliter.Panel1.SuspendLayout();
            this.VSpliter.Panel2.SuspendLayout();
            this.VSpliter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SubVSpliter)).BeginInit();
            this.SubVSpliter.Panel1.SuspendLayout();
            this.SubVSpliter.Panel2.SuspendLayout();
            this.SubVSpliter.SuspendLayout();
            this.SuspendLayout();
            // 
            // HSpliter
            // 
            this.HSpliter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HSpliter.IsSplitterFixed = true;
            this.HSpliter.Location = new System.Drawing.Point(0, 0);
            this.HSpliter.Margin = new System.Windows.Forms.Padding(0);
            this.HSpliter.Name = "HSpliter";
            this.HSpliter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // HSpliter.Panel1
            // 
            this.HSpliter.Panel1.Controls.Add(this.VSpliter);
            // 
            // HSpliter.Panel2
            // 
            this.HSpliter.Panel2.Controls.Add(this.F3PoolTextBox);
            this.HSpliter.Panel2.Controls.Add(this.F2PoolTextBox);
            this.HSpliter.Panel2.Controls.Add(this.label_F3);
            this.HSpliter.Panel2.Controls.Add(this.label_F2);
            this.HSpliter.Panel2.Controls.Add(this.quantityTextBox);
            this.HSpliter.Panel2.Controls.Add(this.priceTextBox);
            this.HSpliter.Panel2.Controls.Add(this.label_amount);
            this.HSpliter.Panel2.Controls.Add(this.lable_buy);
            this.HSpliter.Size = new System.Drawing.Size(380, 506);
            this.HSpliter.SplitterDistance = 480;
            this.HSpliter.SplitterWidth = 1;
            this.HSpliter.TabIndex = 0;
            // 
            // VSpliter
            // 
            this.VSpliter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VSpliter.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.VSpliter.IsSplitterFixed = true;
            this.VSpliter.Location = new System.Drawing.Point(0, 0);
            this.VSpliter.Name = "VSpliter";
            // 
            // VSpliter.Panel1
            // 
            this.VSpliter.Panel1.Controls.Add(this.label4);
            this.VSpliter.Panel1.Controls.Add(this.riseRateTextBox);
            this.VSpliter.Panel1.Controls.Add(this.dropRateTextBox);
            this.VSpliter.Panel1.Controls.Add(this.risingPriceTextBox);
            this.VSpliter.Panel1.Controls.Add(this.closingTextBox);
            this.VSpliter.Panel1.Controls.Add(this.openningTextBox);
            this.VSpliter.Panel1.Controls.Add(this.label6);
            this.VSpliter.Panel1.Controls.Add(this.label7);
            this.VSpliter.Panel1.Controls.Add(this.moneyTextBox);
            this.VSpliter.Panel1.Controls.Add(this.currentPriceTextBox);
            this.VSpliter.Panel1.Controls.Add(this.label1);
            this.VSpliter.Panel1.Controls.Add(this.label5);
            this.VSpliter.Panel1.Controls.Add(this.label2);
            this.VSpliter.Panel1.Controls.Add(this.label3);
            this.VSpliter.Panel1.Controls.Add(this.stockCodeTextBox);
            this.VSpliter.Panel1.Controls.Add(this.QuoteList);
            // 
            // VSpliter.Panel2
            // 
            this.VSpliter.Panel2.Controls.Add(this.SubVSpliter);
            this.VSpliter.Size = new System.Drawing.Size(380, 480);
            this.VSpliter.SplitterDistance = 185;
            this.VSpliter.SplitterWidth = 1;
            this.VSpliter.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(1, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 12);
            this.label4.TabIndex = 87;
            this.label4.Text = "涨幅";
            // 
            // riseRateTextBox
            // 
            this.riseRateTextBox.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.riseRateTextBox.Location = new System.Drawing.Point(36, 53);
            this.riseRateTextBox.Name = "riseRateTextBox";
            this.riseRateTextBox.ReadOnly = true;
            this.riseRateTextBox.Size = new System.Drawing.Size(52, 21);
            this.riseRateTextBox.TabIndex = 86;
            // 
            // dropRateTextBox
            // 
            this.dropRateTextBox.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dropRateTextBox.ForeColor = System.Drawing.Color.Green;
            this.dropRateTextBox.Location = new System.Drawing.Point(128, 53);
            this.dropRateTextBox.Name = "dropRateTextBox";
            this.dropRateTextBox.ReadOnly = true;
            this.dropRateTextBox.Size = new System.Drawing.Size(52, 21);
            this.dropRateTextBox.TabIndex = 85;
            // 
            // risingPriceTextBox
            // 
            this.risingPriceTextBox.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.risingPriceTextBox.ForeColor = System.Drawing.Color.Red;
            this.risingPriceTextBox.Location = new System.Drawing.Point(128, 30);
            this.risingPriceTextBox.Name = "risingPriceTextBox";
            this.risingPriceTextBox.ReadOnly = true;
            this.risingPriceTextBox.Size = new System.Drawing.Size(52, 21);
            this.risingPriceTextBox.TabIndex = 84;
            // 
            // closingTextBox
            // 
            this.closingTextBox.Location = new System.Drawing.Point(128, 76);
            this.closingTextBox.Name = "closingTextBox";
            this.closingTextBox.ReadOnly = true;
            this.closingTextBox.Size = new System.Drawing.Size(52, 21);
            this.closingTextBox.TabIndex = 83;
            // 
            // openningTextBox
            // 
            this.openningTextBox.Location = new System.Drawing.Point(36, 76);
            this.openningTextBox.Name = "openningTextBox";
            this.openningTextBox.ReadOnly = true;
            this.openningTextBox.Size = new System.Drawing.Size(52, 21);
            this.openningTextBox.TabIndex = 82;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(1, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 12);
            this.label6.TabIndex = 81;
            this.label6.Text = "今开";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(94, 80);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 12);
            this.label7.TabIndex = 80;
            this.label7.Text = "昨收";
            // 
            // moneyTextBox
            // 
            this.moneyTextBox.Location = new System.Drawing.Point(97, 5);
            this.moneyTextBox.Name = "moneyTextBox";
            this.moneyTextBox.ReadOnly = true;
            this.moneyTextBox.Size = new System.Drawing.Size(83, 21);
            this.moneyTextBox.TabIndex = 79;
            // 
            // currentPriceTextBox
            // 
            this.currentPriceTextBox.Location = new System.Drawing.Point(36, 30);
            this.currentPriceTextBox.MaxLength = 5;
            this.currentPriceTextBox.Name = "currentPriceTextBox";
            this.currentPriceTextBox.ReadOnly = true;
            this.currentPriceTextBox.Size = new System.Drawing.Size(52, 21);
            this.currentPriceTextBox.TabIndex = 78;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(60, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 12);
            this.label1.TabIndex = 77;
            this.label1.Text = "金额";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(94, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 12);
            this.label5.TabIndex = 76;
            this.label5.Text = "跌停";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(1, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 12);
            this.label2.TabIndex = 75;
            this.label2.Text = "现价";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(94, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 12);
            this.label3.TabIndex = 74;
            this.label3.Text = "涨停";
            // 
            // stockCodeTextBox
            // 
            this.stockCodeTextBox.Location = new System.Drawing.Point(1, 5);
            this.stockCodeTextBox.Name = "stockCodeTextBox";
            this.stockCodeTextBox.Size = new System.Drawing.Size(55, 21);
            this.stockCodeTextBox.TabIndex = 72;
            // 
            // QuoteList
            // 
            this.QuoteList.BackColor = System.Drawing.Color.Black;
            this.QuoteList.ForeColor = System.Drawing.Color.Gray;
            this.QuoteList.Location = new System.Drawing.Point(0, 100);
            this.QuoteList.MinimumSize = new System.Drawing.Size(185, 380);
            this.QuoteList.Name = "QuoteList";
            this.QuoteList.Scrollable = false;
            this.QuoteList.Size = new System.Drawing.Size(185, 380);
            this.QuoteList.TabIndex = 73;
            this.QuoteList.UseCompatibleStateImageBehavior = false;
            this.QuoteList.View = System.Windows.Forms.View.Details;
            // 
            // SubVSpliter
            // 
            this.SubVSpliter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SubVSpliter.Location = new System.Drawing.Point(0, 0);
            this.SubVSpliter.Name = "SubVSpliter";
            this.SubVSpliter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SubVSpliter.Panel1
            // 
            this.SubVSpliter.Panel1.Controls.Add(this.TransactionDetailList);
            // 
            // SubVSpliter.Panel2
            // 
            this.SubVSpliter.Panel2.Controls.Add(this.TransactionList);
            this.SubVSpliter.Size = new System.Drawing.Size(194, 480);
            this.SubVSpliter.SplitterDistance = 240;
            this.SubVSpliter.TabIndex = 0;
            // 
            // TransactionDetailList
            // 
            this.TransactionDetailList.BackColor = System.Drawing.Color.Black;
            this.TransactionDetailList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TransactionDetailList.Location = new System.Drawing.Point(0, 0);
            this.TransactionDetailList.Name = "TransactionDetailList";
            this.TransactionDetailList.Size = new System.Drawing.Size(194, 240);
            this.TransactionDetailList.TabIndex = 1;
            this.TransactionDetailList.UseCompatibleStateImageBehavior = false;
            this.TransactionDetailList.View = System.Windows.Forms.View.Details;
            // 
            // TransactionList
            // 
            this.TransactionList.BackColor = System.Drawing.Color.Black;
            this.TransactionList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TransactionList.Location = new System.Drawing.Point(0, 0);
            this.TransactionList.Name = "TransactionList";
            this.TransactionList.Size = new System.Drawing.Size(194, 236);
            this.TransactionList.TabIndex = 1;
            this.TransactionList.UseCompatibleStateImageBehavior = false;
            this.TransactionList.View = System.Windows.Forms.View.Details;
            // 
            // quantityTextBox
            // 
            this.quantityTextBox.Location = new System.Drawing.Point(124, 2);
            this.quantityTextBox.Name = "quantityTextBox";
            this.quantityTextBox.Size = new System.Drawing.Size(52, 21);
            this.quantityTextBox.TabIndex = 60;
            // 
            // priceTextBox
            // 
            this.priceTextBox.Location = new System.Drawing.Point(34, 2);
            this.priceTextBox.Name = "priceTextBox";
            this.priceTextBox.Size = new System.Drawing.Size(52, 21);
            this.priceTextBox.TabIndex = 59;
            // 
            // label_amount
            // 
            this.label_amount.AutoSize = true;
            this.label_amount.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_amount.Location = new System.Drawing.Point(92, 6);
            this.label_amount.Name = "label_amount";
            this.label_amount.Size = new System.Drawing.Size(31, 12);
            this.label_amount.TabIndex = 58;
            this.label_amount.Text = "数量";
            // 
            // lable_buy
            // 
            this.lable_buy.AutoSize = true;
            this.lable_buy.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lable_buy.Location = new System.Drawing.Point(2, 6);
            this.lable_buy.Name = "lable_buy";
            this.lable_buy.Size = new System.Drawing.Size(31, 12);
            this.lable_buy.TabIndex = 57;
            this.lable_buy.Text = "买入";
            // 
            // F3PoolTextBox
            // 
            this.F3PoolTextBox.Location = new System.Drawing.Point(319, 2);
            this.F3PoolTextBox.Name = "F3PoolTextBox";
            this.F3PoolTextBox.ReadOnly = true;
            this.F3PoolTextBox.Size = new System.Drawing.Size(52, 21);
            this.F3PoolTextBox.TabIndex = 64;
            // 
            // F2PoolTextBox
            // 
            this.F2PoolTextBox.Location = new System.Drawing.Point(229, 2);
            this.F2PoolTextBox.Name = "F2PoolTextBox";
            this.F2PoolTextBox.ReadOnly = true;
            this.F2PoolTextBox.Size = new System.Drawing.Size(52, 21);
            this.F2PoolTextBox.TabIndex = 63;
            // 
            // label_F3
            // 
            this.label_F3.AutoSize = true;
            this.label_F3.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_F3.Location = new System.Drawing.Point(287, 6);
            this.label_F3.Name = "label_F3";
            this.label_F3.Size = new System.Drawing.Size(32, 12);
            this.label_F3.TabIndex = 62;
            this.label_F3.Text = "F3池";
            // 
            // label_F2
            // 
            this.label_F2.AutoSize = true;
            this.label_F2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_F2.Location = new System.Drawing.Point(189, 6);
            this.label_F2.Name = "label_F2";
            this.label_F2.Size = new System.Drawing.Size(32, 12);
            this.label_F2.TabIndex = 61;
            this.label_F2.Text = "F2池";
            // 
            // QuoteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 506);
            this.Controls.Add(this.HSpliter);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(388, 540);
            this.MinimumSize = new System.Drawing.Size(195, 540);
            this.Name = "QuoteForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
            this.Load += new System.EventHandler(this.OnFromLoad);
            this.Click += new System.EventHandler(this.OnClick);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            this.HSpliter.Panel1.ResumeLayout(false);
            this.HSpliter.Panel2.ResumeLayout(false);
            this.HSpliter.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HSpliter)).EndInit();
            this.HSpliter.ResumeLayout(false);
            this.VSpliter.Panel1.ResumeLayout(false);
            this.VSpliter.Panel1.PerformLayout();
            this.VSpliter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.VSpliter)).EndInit();
            this.VSpliter.ResumeLayout(false);
            this.SubVSpliter.Panel1.ResumeLayout(false);
            this.SubVSpliter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SubVSpliter)).EndInit();
            this.SubVSpliter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer HSpliter;
        private System.Windows.Forms.SplitContainer VSpliter;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox riseRateTextBox;
        private System.Windows.Forms.TextBox dropRateTextBox;
        private System.Windows.Forms.TextBox risingPriceTextBox;
        private System.Windows.Forms.TextBox closingTextBox;
        private System.Windows.Forms.TextBox openningTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox moneyTextBox;
        private System.Windows.Forms.TextBox currentPriceTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox stockCodeTextBox;
        private System.Windows.Forms.ListView QuoteList;
        private System.Windows.Forms.SplitContainer SubVSpliter;
        private System.Windows.Forms.ListView TransactionDetailList;
        private System.Windows.Forms.ListView TransactionList;
        private System.Windows.Forms.TextBox F3PoolTextBox;
        private System.Windows.Forms.TextBox F2PoolTextBox;
        private System.Windows.Forms.Label label_F3;
        private System.Windows.Forms.Label label_F2;
        private System.Windows.Forms.TextBox quantityTextBox;
        private System.Windows.Forms.TextBox priceTextBox;
        private System.Windows.Forms.Label label_amount;
        private System.Windows.Forms.Label lable_buy;


    }
}

