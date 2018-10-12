namespace WindowsFormsApplication1
{
    partial class COM_settings
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
            this.Baudrate = new System.Windows.Forms.ComboBox();
            this.DataBits = new System.Windows.Forms.ComboBox();
            this.Parity = new System.Windows.Forms.ComboBox();
            this.StopBit = new System.Windows.Forms.ComboBox();
            this.BaudrateLbl = new System.Windows.Forms.Label();
            this.DataBitsLbl = new System.Windows.Forms.Label();
            this.ParityLbl = new System.Windows.Forms.Label();
            this.StopBitLbl = new System.Windows.Forms.Label();
            this.Ok_btn = new System.Windows.Forms.Button();
            this.AdressLbl = new System.Windows.Forms.Label();
            Adress = new System.Windows.Forms.MaskedTextBox();
            this.SuspendLayout();
            // 
            // Baudrate
            // 
            this.Baudrate.FormattingEnabled = true;
            this.Baudrate.Items.AddRange(new object[] {
            "9600",
            "115200"});
            this.Baudrate.Location = new System.Drawing.Point(59, 12);
            this.Baudrate.Name = "Baudrate";
            this.Baudrate.Size = new System.Drawing.Size(121, 21);
            this.Baudrate.TabIndex = 2;
            // 
            // DataBits
            // 
            this.DataBits.FormattingEnabled = true;
            this.DataBits.Items.AddRange(new object[] {
            "7",
            "8"});
            this.DataBits.Location = new System.Drawing.Point(59, 39);
            this.DataBits.Name = "DataBits";
            this.DataBits.Size = new System.Drawing.Size(121, 21);
            this.DataBits.TabIndex = 3;
            // 
            // Parity
            // 
            this.Parity.FormattingEnabled = true;
            this.Parity.Items.AddRange(new object[] {
            "None",
            "Even",
            "Odd"});
            this.Parity.Location = new System.Drawing.Point(59, 66);
            this.Parity.Name = "Parity";
            this.Parity.Size = new System.Drawing.Size(121, 21);
            this.Parity.TabIndex = 4;
            // 
            // StopBit
            // 
            this.StopBit.FormattingEnabled = true;
            this.StopBit.Items.AddRange(new object[] {
            "None",
            "One"});
            this.StopBit.Location = new System.Drawing.Point(59, 93);
            this.StopBit.Name = "StopBit";
            this.StopBit.Size = new System.Drawing.Size(121, 21);
            this.StopBit.TabIndex = 5;
            // 
            // BaudrateLbl
            // 
            this.BaudrateLbl.AutoSize = true;
            this.BaudrateLbl.Location = new System.Drawing.Point(3, 15);
            this.BaudrateLbl.Name = "BaudrateLbl";
            this.BaudrateLbl.Size = new System.Drawing.Size(50, 13);
            this.BaudrateLbl.TabIndex = 6;
            this.BaudrateLbl.Text = "Baudrate";
            // 
            // DataBitsLbl
            // 
            this.DataBitsLbl.AutoSize = true;
            this.DataBitsLbl.Location = new System.Drawing.Point(3, 42);
            this.DataBitsLbl.Name = "DataBitsLbl";
            this.DataBitsLbl.Size = new System.Drawing.Size(47, 13);
            this.DataBitsLbl.TabIndex = 7;
            this.DataBitsLbl.Text = "DataBits";
            // 
            // ParityLbl
            // 
            this.ParityLbl.AutoSize = true;
            this.ParityLbl.Location = new System.Drawing.Point(3, 69);
            this.ParityLbl.Name = "ParityLbl";
            this.ParityLbl.Size = new System.Drawing.Size(33, 13);
            this.ParityLbl.TabIndex = 8;
            this.ParityLbl.Text = "Parity";
            // 
            // StopBitLbl
            // 
            this.StopBitLbl.AutoSize = true;
            this.StopBitLbl.Location = new System.Drawing.Point(3, 96);
            this.StopBitLbl.Name = "StopBitLbl";
            this.StopBitLbl.Size = new System.Drawing.Size(41, 13);
            this.StopBitLbl.TabIndex = 9;
            this.StopBitLbl.Text = "StopBit";
            // 
            // Ok_btn
            // 
            this.Ok_btn.Location = new System.Drawing.Point(59, 155);
            this.Ok_btn.Name = "Ok_btn";
            this.Ok_btn.Size = new System.Drawing.Size(121, 23);
            this.Ok_btn.TabIndex = 10;
            this.Ok_btn.Text = "Ok";
            this.Ok_btn.UseVisualStyleBackColor = true;
            this.Ok_btn.Click += new System.EventHandler(this.Ok_btn_Click);
            // 
            // AdressLbl
            // 
            this.AdressLbl.AutoSize = true;
            this.AdressLbl.Location = new System.Drawing.Point(3, 126);
            this.AdressLbl.Name = "AdressLbl";
            this.AdressLbl.Size = new System.Drawing.Size(39, 13);
            this.AdressLbl.TabIndex = 11;
            this.AdressLbl.Text = "Adress";
            // 
            // Adress
            // 
            Adress.Location = new System.Drawing.Point(59, 123);
            Adress.Mask = "000";
            Adress.Name = "Adress";
            Adress.Size = new System.Drawing.Size(100, 20);
            Adress.TabIndex = 12;
            Adress.Leave += new System.EventHandler(this.maskedTextBox1_Leave);
            // 
            // COM_settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(196, 190);
            this.Controls.Add(Adress);
            this.Controls.Add(this.AdressLbl);
            this.Controls.Add(this.Ok_btn);
            this.Controls.Add(this.StopBitLbl);
            this.Controls.Add(this.ParityLbl);
            this.Controls.Add(this.DataBitsLbl);
            this.Controls.Add(this.BaudrateLbl);
            this.Controls.Add(this.StopBit);
            this.Controls.Add(this.Parity);
            this.Controls.Add(this.DataBits);
            this.Controls.Add(this.Baudrate);
            this.Name = "COM_settings";
            this.Text = "COM_settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.COM_settings_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox Baudrate;
        private System.Windows.Forms.ComboBox DataBits;
        private System.Windows.Forms.ComboBox Parity;
        private System.Windows.Forms.ComboBox StopBit;
        private System.Windows.Forms.Label BaudrateLbl;
        private System.Windows.Forms.Label DataBitsLbl;
        private System.Windows.Forms.Label ParityLbl;
        private System.Windows.Forms.Label StopBitLbl;
        private System.Windows.Forms.Button Ok_btn;
        private System.Windows.Forms.Label AdressLbl;
        public static System.Windows.Forms.MaskedTextBox Adress;
    }
}