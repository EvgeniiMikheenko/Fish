namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ConnectMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.отключитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.COM = new System.IO.Ports.SerialPort(this.components);
            this.Filtr_lbl = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.fopen = new System.Windows.Forms.Button();
            this.send_btn = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.richTextBox4 = new System.Windows.Forms.RichTextBox();
            this.richTextBox5 = new System.Windows.Forms.RichTextBox();
            this.richTextBox6 = new System.Windows.Forms.RichTextBox();
            this.richTextBox7 = new System.Windows.Forms.RichTextBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConnectMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1479, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ConnectMenu
            // 
            this.ConnectMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.отключитьToolStripMenuItem,
            this.настройкаToolStripMenuItem});
            this.ConnectMenu.Name = "ConnectMenu";
            this.ConnectMenu.Size = new System.Drawing.Size(97, 20);
            this.ConnectMenu.Text = "Подключение";
            this.ConnectMenu.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ConnectMenu_DropDownItemClicked);
            this.ConnectMenu.Click += new System.EventHandler(this.ConnectMenu_Click);
            // 
            // отключитьToolStripMenuItem
            // 
            this.отключитьToolStripMenuItem.Name = "отключитьToolStripMenuItem";
            this.отключитьToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.отключитьToolStripMenuItem.Text = "Отключить";
            // 
            // настройкаToolStripMenuItem
            // 
            this.настройкаToolStripMenuItem.Name = "настройкаToolStripMenuItem";
            this.настройкаToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.настройкаToolStripMenuItem.Text = "Настройка";
            // 
            // COM
            // 
            this.COM.BaudRate = 57600;
            this.COM.WriteBufferSize = 4096;
            this.COM.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.COM_DataReceived);
            // 
            // Filtr_lbl
            // 
            this.Filtr_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Filtr_lbl.AutoSize = true;
            this.Filtr_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Filtr_lbl.Location = new System.Drawing.Point(12, 53);
            this.Filtr_lbl.Name = "Filtr_lbl";
            this.Filtr_lbl.Size = new System.Drawing.Size(245, 39);
            this.Filtr_lbl.TabIndex = 2;
            this.Filtr_lbl.Text = "Не подключен";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 27);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "hello";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog";
            // 
            // fopen
            // 
            this.fopen.Location = new System.Drawing.Point(11, 194);
            this.fopen.Name = "fopen";
            this.fopen.Size = new System.Drawing.Size(75, 23);
            this.fopen.TabIndex = 4;
            this.fopen.Text = "Open file";
            this.fopen.UseVisualStyleBackColor = true;
            this.fopen.Click += new System.EventHandler(this.fopen_Click);
            // 
            // send_btn
            // 
            this.send_btn.Location = new System.Drawing.Point(12, 223);
            this.send_btn.Name = "send_btn";
            this.send_btn.Size = new System.Drawing.Size(75, 23);
            this.send_btn.TabIndex = 6;
            this.send_btn.Text = "Send FW";
            this.send_btn.UseVisualStyleBackColor = true;
            this.send_btn.Click += new System.EventHandler(this.send_btn_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(92, 95);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(252, 448);
            this.richTextBox1.TabIndex = 7;
            this.richTextBox1.Text = "";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(350, 95);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(242, 448);
            this.richTextBox2.TabIndex = 8;
            this.richTextBox2.Text = "";
            // 
            // richTextBox3
            // 
            this.richTextBox3.Location = new System.Drawing.Point(598, 95);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.Size = new System.Drawing.Size(229, 448);
            this.richTextBox3.TabIndex = 9;
            this.richTextBox3.Text = "";
            // 
            // richTextBox4
            // 
            this.richTextBox4.Location = new System.Drawing.Point(833, 95);
            this.richTextBox4.Name = "richTextBox4";
            this.richTextBox4.Size = new System.Drawing.Size(243, 448);
            this.richTextBox4.TabIndex = 10;
            this.richTextBox4.Text = "";
            // 
            // richTextBox5
            // 
            this.richTextBox5.Location = new System.Drawing.Point(1117, 27);
            this.richTextBox5.Name = "richTextBox5";
            this.richTextBox5.Size = new System.Drawing.Size(297, 621);
            this.richTextBox5.TabIndex = 11;
            this.richTextBox5.Text = "";
            // 
            // richTextBox6
            // 
            this.richTextBox6.Location = new System.Drawing.Point(395, 550);
            this.richTextBox6.Name = "richTextBox6";
            this.richTextBox6.Size = new System.Drawing.Size(297, 98);
            this.richTextBox6.TabIndex = 12;
            this.richTextBox6.Text = "";
            // 
            // richTextBox7
            // 
            this.richTextBox7.Location = new System.Drawing.Point(92, 550);
            this.richTextBox7.Name = "richTextBox7";
            this.richTextBox7.Size = new System.Drawing.Size(297, 98);
            this.richTextBox7.TabIndex = 13;
            this.richTextBox7.Text = "";
            // 
            // timer
            // 
            this.timer.Interval = 4;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(368, 25);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 14;
            this.button2.Text = "hello";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(547, 27);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 15;
            this.button3.Text = "hello";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1479, 660);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.richTextBox7);
            this.Controls.Add(this.richTextBox6);
            this.Controls.Add(this.richTextBox5);
            this.Controls.Add(this.richTextBox4);
            this.Controls.Add(this.richTextBox3);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.send_btn);
            this.Controls.Add(this.fopen);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Filtr_lbl);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Проверка фильтра";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ConnectMenu;
        private System.Windows.Forms.ToolStripMenuItem отключитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem настройкаToolStripMenuItem;
        private System.IO.Ports.SerialPort COM;
        private System.Windows.Forms.Label Filtr_lbl;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button fopen;
        private System.Windows.Forms.Button send_btn;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.RichTextBox richTextBox3;
        private System.Windows.Forms.RichTextBox richTextBox4;
        private System.Windows.Forms.RichTextBox richTextBox5;
        private System.Windows.Forms.RichTextBox richTextBox6;
        private System.Windows.Forms.RichTextBox richTextBox7;
        public System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}

