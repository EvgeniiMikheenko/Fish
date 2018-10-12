using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class COM_settings : Form 
    {

        //private bool is_open;
        public COM_settings()
        {
            InitializeComponent();
            //is_open = false;
            Baudrate.SelectedIndex = 1;
            Parity.SelectedIndex = 0;
            DataBits.SelectedIndex = 1;
            StopBit.SelectedIndex = 0;
            Adress.Text = "1";

        }


        //public bool Csisopen
        //{
        //    get
        //    { return is_open; }
        //    set
        //    { is_open = value; }
        //}

       
        Form1 Mform = new Form1();
        
        private void COM_settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            Mform.COM_set(Baudrate.SelectedIndex, DataBits.SelectedIndex, Parity.SelectedIndex, StopBit.SelectedIndex, Convert.ToInt32(Adress.Text));
        }

        private void Ok_btn_Click(object sender, EventArgs e)
        {
            int br = Baudrate.SelectedIndex;
            int db = DataBits.SelectedIndex;
            int par = Parity.SelectedIndex;
            int sb = StopBit.SelectedIndex;
            Int32 ad = Convert.ToInt32(Adress.Text);
            Mform.COM_set(br, db, par, sb, ad);



            Mform.Csisopen = false;
            this.Close();
           // COM_settings_FormClosing(sender, null);

        }

        private void maskedTextBox1_Leave(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Adress.Text) > 250)
                Adress.Text = "250";
            else if (Convert.ToInt32(Adress.Text) < 0)
                Adress.Text = "0";
       
        }
    }
}
