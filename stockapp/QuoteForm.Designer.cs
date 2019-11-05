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
            this.lab_rising_rate = new System.Windows.Forms.Label();
            this.txb_rise_rate = new System.Windows.Forms.RichTextBox();
            this.txb_limit_price = new System.Windows.Forms.RichTextBox();
            this.txb_rising_price = new System.Windows.Forms.RichTextBox();
            this.txb_closing_price = new System.Windows.Forms.RichTextBox();
            this.txb_openning_price = new System.Windows.Forms.RichTextBox();
            this.lab_openning_price = new System.Windows.Forms.Label();
            this.lab_closing_price = new System.Windows.Forms.Label();
            this.txb_money = new System.Windows.Forms.TextBox();
            this.txb_current_price = new System.Windows.Forms.RichTextBox();
            this.lab_money = new System.Windows.Forms.Label();
            this.lab_limit_price = new System.Windows.Forms.Label();
            this.lab_current_price = new System.Windows.Forms.Label();
            this.lab_rising_price = new System.Windows.Forms.Label();
            this.txb_stockcode = new System.Windows.Forms.TextBox();
            this.QuoteList = new System.Windows.Forms.ListView();
            this.SubVSpliter = new System.Windows.Forms.SplitContainer();
            this.TransactionDetailList = new System.Windows.Forms.ListView();
            this.TransactionList = new System.Windows.Forms.ListView();
            this.txb_f3_pool = new System.Windows.Forms.TextBox();
            this.txb_f2_pool = new System.Windows.Forms.TextBox();
            this.label_f3 = new System.Windows.Forms.Label();
            this.label_f2 = new System.Windows.Forms.Label();
            this.txb_amount = new System.Windows.Forms.TextBox();
            this.txb_price = new System.Windows.Forms.TextBox();
            this.label_amount = new System.Windows.Forms.Label();
            this.label_price = new System.Windows.Forms.Label();
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
            this.HSpliter.Panel2.Controls.Add(this.txb_f3_pool);
            this.HSpliter.Panel2.Controls.Add(this.txb_f2_pool);
            this.HSpliter.Panel2.Controls.Add(this.label_f3);
            this.HSpliter.Panel2.Controls.Add(this.label_f2);
            this.HSpliter.Panel2.Controls.Add(this.txb_amount);
            this.HSpliter.Panel2.Controls.Add(this.txb_price);
            this.HSpliter.Panel2.Controls.Add(this.label_amount);
            this.HSpliter.Panel2.Controls.Add(this.label_price);
            this.HSpliter.Size = new System.Drawing.Size(372, 491);
            this.HSpliter.SplitterDistance = 465;
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
            this.VSpliter.Panel1.Controls.Add(this.lab_rising_rate);
            this.VSpliter.Panel1.Controls.Add(this.txb_rise_rate);
            this.VSpliter.Panel1.Controls.Add(this.txb_limit_price);
            this.VSpliter.Panel1.Controls.Add(this.txb_rising_price);
            this.VSpliter.Panel1.Controls.Add(this.txb_closing_price);
            this.VSpliter.Panel1.Controls.Add(this.txb_openning_price);
            this.VSpliter.Panel1.Controls.Add(this.lab_openning_price);
            this.VSpliter.Panel1.Controls.Add(this.lab_closing_price);
            this.VSpliter.Panel1.Controls.Add(this.txb_money);
            this.VSpliter.Panel1.Controls.Add(this.txb_current_price);
            this.VSpliter.Panel1.Controls.Add(this.lab_money);
            this.VSpliter.Panel1.Controls.Add(this.lab_limit_price);
            this.VSpliter.Panel1.Controls.Add(this.lab_current_price);
            this.VSpliter.Panel1.Controls.Add(this.lab_rising_price);
            this.VSpliter.Panel1.Controls.Add(this.txb_stockcode);
            this.VSpliter.Panel1.Controls.Add(this.QuoteList);
            // 
            // VSpliter.Panel2
            // 
            this.VSpliter.Panel2.Controls.Add(this.SubVSpliter);
            this.VSpliter.Size = new System.Drawing.Size(372, 465);
            this.VSpliter.SplitterDistance = 180;
            this.VSpliter.SplitterWidth = 1;
            this.VSpliter.TabIndex = 0;
            // 
            // lab_rising_rate
            // 
            this.lab_rising_rate.AutoSize = true;
            this.lab_rising_rate.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_rising_rate.Location = new System.Drawing.Point(1, 57);
            this.lab_rising_rate.Name = "lab_rising_rate";
            this.lab_rising_rate.Size = new System.Drawing.Size(31, 12);
            this.lab_rising_rate.TabIndex = 87;
            this.lab_rising_rate.Text = "涨幅";
            // 
            // txb_rise_rate
            // 
            this.txb_rise_rate.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txb_rise_rate.Location = new System.Drawing.Point(36, 53);
            this.txb_rise_rate.Multiline = false;
            this.txb_rise_rate.Name = "txb_rise_rate";
            this.txb_rise_rate.ReadOnly = true;
            this.txb_rise_rate.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.txb_rise_rate.Size = new System.Drawing.Size(52, 21);
            this.txb_rise_rate.TabIndex = 86;
            this.txb_rise_rate.TabStop = false;
            this.txb_rise_rate.Text = "";
            this.txb_rise_rate.WordWrap = false;
            // 
            // txb_limit_price
            // 
            this.txb_limit_price.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txb_limit_price.ForeColor = System.Drawing.Color.Green;
            this.txb_limit_price.Location = new System.Drawing.Point(126, 53);
            this.txb_limit_price.Multiline = false;
            this.txb_limit_price.Name = "txb_limit_price";
            this.txb_limit_price.ReadOnly = true;
            this.txb_limit_price.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.txb_limit_price.Size = new System.Drawing.Size(52, 21);
            this.txb_limit_price.TabIndex = 85;
            this.txb_limit_price.TabStop = false;
            this.txb_limit_price.Text = "";
            this.txb_limit_price.WordWrap = false;
            // 
            // txb_rising_price
            // 
            this.txb_rising_price.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txb_rising_price.ForeColor = System.Drawing.Color.Red;
            this.txb_rising_price.Location = new System.Drawing.Point(126, 30);
            this.txb_rising_price.Multiline = false;
            this.txb_rising_price.Name = "txb_rising_price";
            this.txb_rising_price.ReadOnly = true;
            this.txb_rising_price.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.txb_rising_price.Size = new System.Drawing.Size(52, 21);
            this.txb_rising_price.TabIndex = 84;
            this.txb_rising_price.TabStop = false;
            this.txb_rising_price.Text = "";
            this.txb_rising_price.WordWrap = false;
            // 
            // txb_closing_price
            // 
            this.txb_closing_price.Location = new System.Drawing.Point(126, 76);
            this.txb_closing_price.Multiline = false;
            this.txb_closing_price.Name = "txb_closing_price";
            this.txb_closing_price.ReadOnly = true;
            this.txb_closing_price.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.txb_closing_price.Size = new System.Drawing.Size(52, 21);
            this.txb_closing_price.TabIndex = 83;
            this.txb_closing_price.TabStop = false;
            this.txb_closing_price.Text = "";
            this.txb_closing_price.WordWrap = false;
            // 
            // txb_openning_price
            // 
            this.txb_openning_price.Location = new System.Drawing.Point(36, 76);
            this.txb_openning_price.Multiline = false;
            this.txb_openning_price.Name = "txb_openning_price";
            this.txb_openning_price.ReadOnly = true;
            this.txb_openning_price.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.txb_openning_price.Size = new System.Drawing.Size(52, 21);
            this.txb_openning_price.TabIndex = 82;
            this.txb_openning_price.TabStop = false;
            this.txb_openning_price.Text = "";
            this.txb_openning_price.WordWrap = false;
            // 
            // lab_openning_price
            // 
            this.lab_openning_price.AutoSize = true;
            this.lab_openning_price.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_openning_price.Location = new System.Drawing.Point(1, 80);
            this.lab_openning_price.Name = "lab_openning_price";
            this.lab_openning_price.Size = new System.Drawing.Size(31, 12);
            this.lab_openning_price.TabIndex = 81;
            this.lab_openning_price.Text = "今开";
            // 
            // lab_closing_price
            // 
            this.lab_closing_price.AutoSize = true;
            this.lab_closing_price.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_closing_price.Location = new System.Drawing.Point(93, 80);
            this.lab_closing_price.Name = "lab_closing_price";
            this.lab_closing_price.Size = new System.Drawing.Size(31, 12);
            this.lab_closing_price.TabIndex = 80;
            this.lab_closing_price.Text = "昨收";
            // 
            // txb_money
            // 
            this.txb_money.Location = new System.Drawing.Point(95, 5);
            this.txb_money.Name = "txb_money";
            this.txb_money.ReadOnly = true;
            this.txb_money.Size = new System.Drawing.Size(83, 21);
            this.txb_money.TabIndex = 79;
            this.txb_money.WordWrap = false;
            // 
            // txb_current_price
            // 
            this.txb_current_price.Location = new System.Drawing.Point(36, 30);
            this.txb_current_price.MaxLength = 5;
            this.txb_current_price.Multiline = false;
            this.txb_current_price.Name = "txb_current_price";
            this.txb_current_price.ReadOnly = true;
            this.txb_current_price.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.txb_current_price.Size = new System.Drawing.Size(52, 21);
            this.txb_current_price.TabIndex = 78;
            this.txb_current_price.TabStop = false;
            this.txb_current_price.Text = "";
            this.txb_current_price.WordWrap = false;
            // 
            // lab_money
            // 
            this.lab_money.AutoSize = true;
            this.lab_money.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_money.Location = new System.Drawing.Point(59, 9);
            this.lab_money.Name = "lab_money";
            this.lab_money.Size = new System.Drawing.Size(31, 12);
            this.lab_money.TabIndex = 77;
            this.lab_money.Text = "金额";
            // 
            // lab_limit_price
            // 
            this.lab_limit_price.AutoSize = true;
            this.lab_limit_price.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_limit_price.Location = new System.Drawing.Point(93, 57);
            this.lab_limit_price.Name = "lab_limit_price";
            this.lab_limit_price.Size = new System.Drawing.Size(31, 12);
            this.lab_limit_price.TabIndex = 76;
            this.lab_limit_price.Text = "跌停";
            // 
            // lab_current_price
            // 
            this.lab_current_price.AutoSize = true;
            this.lab_current_price.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_current_price.Location = new System.Drawing.Point(1, 34);
            this.lab_current_price.Name = "lab_current_price";
            this.lab_current_price.Size = new System.Drawing.Size(31, 12);
            this.lab_current_price.TabIndex = 75;
            this.lab_current_price.Text = "现价";
            // 
            // lab_rising_price
            // 
            this.lab_rising_price.AutoSize = true;
            this.lab_rising_price.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_rising_price.Location = new System.Drawing.Point(93, 34);
            this.lab_rising_price.Name = "lab_rising_price";
            this.lab_rising_price.Size = new System.Drawing.Size(31, 12);
            this.lab_rising_price.TabIndex = 74;
            this.lab_rising_price.Text = "涨停";
            // 
            // txb_stockcode
            // 
            this.txb_stockcode.Location = new System.Drawing.Point(1, 5);
            this.txb_stockcode.Name = "txb_stockcode";
            this.txb_stockcode.Size = new System.Drawing.Size(55, 21);
            this.txb_stockcode.TabIndex = 1;
            this.txb_stockcode.WordWrap = false;
            // 
            // QuoteList
            // 
            this.QuoteList.BackColor = System.Drawing.Color.Black;
            this.QuoteList.ForeColor = System.Drawing.Color.Gray;
            this.QuoteList.Location = new System.Drawing.Point(0, 100);
            this.QuoteList.MinimumSize = new System.Drawing.Size(180, 365);
            this.QuoteList.Name = "QuoteList";
            this.QuoteList.Scrollable = false;
            this.QuoteList.Size = new System.Drawing.Size(180, 365);
            this.QuoteList.TabIndex = 73;
            this.QuoteList.TabStop = false;
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
            this.SubVSpliter.Size = new System.Drawing.Size(191, 465);
            this.SubVSpliter.SplitterDistance = 239;
            this.SubVSpliter.TabIndex = 0;
            // 
            // TransactionDetailList
            // 
            this.TransactionDetailList.BackColor = System.Drawing.Color.Black;
            this.TransactionDetailList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TransactionDetailList.Location = new System.Drawing.Point(0, 0);
            this.TransactionDetailList.Name = "TransactionDetailList";
            this.TransactionDetailList.Size = new System.Drawing.Size(191, 239);
            this.TransactionDetailList.TabIndex = 1;
            this.TransactionDetailList.TabStop = false;
            this.TransactionDetailList.UseCompatibleStateImageBehavior = false;
            this.TransactionDetailList.View = System.Windows.Forms.View.Details;
            // 
            // TransactionList
            // 
            this.TransactionList.BackColor = System.Drawing.Color.Black;
            this.TransactionList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TransactionList.Location = new System.Drawing.Point(0, 0);
            this.TransactionList.Name = "TransactionList";
            this.TransactionList.Size = new System.Drawing.Size(191, 222);
            this.TransactionList.TabIndex = 1;
            this.TransactionList.TabStop = false;
            this.TransactionList.UseCompatibleStateImageBehavior = false;
            this.TransactionList.View = System.Windows.Forms.View.Details;
            // 
            // txb_f3_pool
            // 
            this.txb_f3_pool.Location = new System.Drawing.Point(313, 2);
            this.txb_f3_pool.Name = "txb_f3_pool";
            this.txb_f3_pool.ReadOnly = true;
            this.txb_f3_pool.Size = new System.Drawing.Size(52, 21);
            this.txb_f3_pool.TabIndex = 64;
            this.txb_f3_pool.WordWrap = false;
            // 
            // txb_f2_pool
            // 
            this.txb_f2_pool.Location = new System.Drawing.Point(218, 2);
            this.txb_f2_pool.Name = "txb_f2_pool";
            this.txb_f2_pool.ReadOnly = true;
            this.txb_f2_pool.Size = new System.Drawing.Size(52, 21);
            this.txb_f2_pool.TabIndex = 64;
            this.txb_f2_pool.WordWrap = false;
            // 
            // label_f3
            // 
            this.label_f3.AutoSize = true;
            this.label_f3.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_f3.Location = new System.Drawing.Point(277, 6);
            this.label_f3.Name = "label_f3";
            this.label_f3.Size = new System.Drawing.Size(31, 12);
            this.label_f3.TabIndex = 62;
            this.label_f3.Text = "可融";
            // 
            // label_f2
            // 
            this.label_f2.AutoSize = true;
            this.label_f2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_f2.Location = new System.Drawing.Point(183, 6);
            this.label_f2.Name = "label_f2";
            this.label_f2.Size = new System.Drawing.Size(31, 12);
            this.label_f2.TabIndex = 61;
            this.label_f2.Text = "可卖";
            // 
            // txb_amount
            // 
            this.txb_amount.Location = new System.Drawing.Point(124, 2);
            this.txb_amount.Name = "txb_amount";
            this.txb_amount.Size = new System.Drawing.Size(52, 21);
            this.txb_amount.TabIndex = 3;
            this.txb_amount.WordWrap = false;
            // 
            // txb_price
            // 
            this.txb_price.Location = new System.Drawing.Point(34, 2);
            this.txb_price.Name = "txb_price";
            this.txb_price.Size = new System.Drawing.Size(52, 21);
            this.txb_price.TabIndex = 2;
            this.txb_price.WordWrap = false;
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
            // label_price
            // 
            this.label_price.AutoSize = true;
            this.label_price.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_price.Location = new System.Drawing.Point(2, 6);
            this.label_price.Name = "label_price";
            this.label_price.Size = new System.Drawing.Size(31, 12);
            this.label_price.TabIndex = 57;
            this.label_price.Text = "买入";
            // 
            // QuoteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 491);
            this.Controls.Add(this.HSpliter);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(380, 525);
            this.MinimumSize = new System.Drawing.Size(190, 525);
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
        private System.Windows.Forms.Label lab_rising_rate;
        private System.Windows.Forms.RichTextBox txb_rise_rate;
        private System.Windows.Forms.RichTextBox txb_limit_price;
        private System.Windows.Forms.RichTextBox txb_rising_price;
        private System.Windows.Forms.RichTextBox txb_closing_price;
        private System.Windows.Forms.RichTextBox txb_openning_price;
        private System.Windows.Forms.RichTextBox txb_current_price;
        private System.Windows.Forms.Label lab_openning_price;
        private System.Windows.Forms.Label lab_closing_price;
        private System.Windows.Forms.TextBox txb_money;
        private System.Windows.Forms.Label lab_money;
        private System.Windows.Forms.Label lab_limit_price;
        private System.Windows.Forms.Label lab_current_price;
        private System.Windows.Forms.Label lab_rising_price;
        private System.Windows.Forms.TextBox txb_stockcode;
        private System.Windows.Forms.ListView QuoteList;
        private System.Windows.Forms.SplitContainer SubVSpliter;
        private System.Windows.Forms.ListView TransactionDetailList;
        private System.Windows.Forms.ListView TransactionList;
        private System.Windows.Forms.TextBox txb_f3_pool;
        private System.Windows.Forms.TextBox txb_f2_pool;
        private System.Windows.Forms.Label label_f3;
        private System.Windows.Forms.Label label_f2;
        private System.Windows.Forms.TextBox txb_amount;
        private System.Windows.Forms.TextBox txb_price;
        private System.Windows.Forms.Label label_amount;
        private System.Windows.Forms.Label label_price;


    }
}

