namespace K8
{
    partial class ServerSettingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_ok = new System.Windows.Forms.Button();
            this.label_note = new System.Windows.Forms.Label();
            this.textbox_trade_ip = new System.Windows.Forms.TextBox();
            this.textbox_market_ip = new System.Windows.Forms.TextBox();
            this.label_trade_ip = new System.Windows.Forms.Label();
            this.label_market_ip = new System.Windows.Forms.Label();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.btn_cancel);
            this.groupBox.Controls.Add(this.btn_ok);
            this.groupBox.Controls.Add(this.label_note);
            this.groupBox.Controls.Add(this.textbox_trade_ip);
            this.groupBox.Controls.Add(this.textbox_market_ip);
            this.groupBox.Controls.Add(this.label_trade_ip);
            this.groupBox.Controls.Add(this.label_market_ip);
            this.groupBox.Location = new System.Drawing.Point(25, 12);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(326, 150);
            this.groupBox.TabIndex = 0;
            this.groupBox.TabStop = false;
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(188, 117);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 4;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(59, 117);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(75, 23);
            this.btn_ok.TabIndex = 3;
            this.btn_ok.Text = "确定";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // label_note
            // 
            this.label_note.AutoSize = true;
            this.label_note.Location = new System.Drawing.Point(68, 94);
            this.label_note.Name = "label_note";
            this.label_note.Size = new System.Drawing.Size(179, 12);
            this.label_note.TabIndex = 4;
            this.label_note.Text = "地址的格式:例子(127.0.0.1:80)";
            // 
            // textbox_trade_ip
            // 
            this.textbox_trade_ip.Location = new System.Drawing.Point(125, 57);
            this.textbox_trade_ip.Name = "textbox_trade_ip";
            this.textbox_trade_ip.Size = new System.Drawing.Size(117, 21);
            this.textbox_trade_ip.TabIndex = 2;
            // 
            // textbox_market_ip
            // 
            this.textbox_market_ip.Location = new System.Drawing.Point(125, 18);
            this.textbox_market_ip.Name = "textbox_market_ip";
            this.textbox_market_ip.Size = new System.Drawing.Size(117, 21);
            this.textbox_market_ip.TabIndex = 1;
            // 
            // label_trade_ip
            // 
            this.label_trade_ip.AutoSize = true;
            this.label_trade_ip.Location = new System.Drawing.Point(66, 61);
            this.label_trade_ip.Name = "label_trade_ip";
            this.label_trade_ip.Size = new System.Drawing.Size(53, 12);
            this.label_trade_ip.TabIndex = 1;
            this.label_trade_ip.Text = "交易地址";
            // 
            // label_market_ip
            // 
            this.label_market_ip.AutoSize = true;
            this.label_market_ip.Location = new System.Drawing.Point(66, 22);
            this.label_market_ip.Name = "label_market_ip";
            this.label_market_ip.Size = new System.Drawing.Size(53, 12);
            this.label_market_ip.TabIndex = 0;
            this.label_market_ip.Text = "行情地址";
            // 
            // ServerSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 187);
            this.Controls.Add(this.groupBox);
            this.KeyPreview = true;
            this.Name = "ServerSettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "服务器地址设置";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(Form_OnKeyDown);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.Label label_trade_ip;
        private System.Windows.Forms.Label label_market_ip;
        private System.Windows.Forms.TextBox textbox_trade_ip;
        private System.Windows.Forms.TextBox textbox_market_ip;
        private System.Windows.Forms.Label label_note;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_ok;
    }
}