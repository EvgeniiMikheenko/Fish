using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using Modbus.Device;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


            is_connected = false;
            stateForm = WorkState.Idle;


            Form_Update = new BackgroundWorker();
            Form_Update.WorkerSupportsCancellation = true;
            Form_Update.WorkerReportsProgress = true;
            Form_Update.DoWork += Form_Update_DoWork;
            Form_Update.ProgressChanged += Form_Update_ProgressChanged;
            Form_Update.RunWorkerAsync();




            COM_Update = new BackgroundWorker();
            COM_Update.WorkerSupportsCancellation = true;
            COM_Update.WorkerReportsProgress = true;
            COM_Update.DoWork += COM_Update_DoWork;
            COM_Update.ProgressChanged += COM_Update_ProgressChanged;
            COM_Update.RunWorkerAsync();

            stateCOM = COMState.Idle;
            stateForm = WorkState.Idle;
   
        }




        PacketData Recv_Packet = new PacketData();
        private void COM_Update_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
        int fw_send_ind = 0;
        int fw_num = 0;
        int num_of_send = 0;
        int buff_size = 2840;
        int page = 0;
        char[] send_buff = new char[2767];
        int pg_count = 0;
        static int sended_page = 0;
        private void COM_Update_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                switch (stateCOM)
                {
                    case COMState.Idle:
                        break;
                    case COMState.Write:
                        //  COM.Write("hello");
                        Write_FW(fileText);
                        stateCOM = COMState.Idle;

                        break;
                    case COMState.Read:
                        
                        break;
                    case COMState.Start_Upload:
                        pg_count = Get_PGCount();



                        break;
                    case COMState.Wait:
                        if (new_msg == RECV_MSG.New)
                        {
                            if (Parce_msg(rbuff, recv_msg_length, Recv_Packet))
                            {
                                if (Recv_Packet.CMD == WAIT_FW_CMD)
                                {

                                    stateCOM = COMState.Info_read;
                                    
                                    Write_Message(ADDR, DOWNLOAD_CMD, Test_data);
                                    stateForm = WorkState.Update;
                                }
                            }
                        }
                        break;
                    case COMState.Info_read:
                        if (new_msg == RECV_MSG.New)
                        {
                            if (Parce_msg(rbuff, recv_msg_length, Recv_Packet))
                            {
                                if (Recv_Packet.CMD == DOWNLOAD_CMD)
                                {
                                    if (Get_RECV_BUF_LNG(Recv_Packet))
                                    {
                                        if ((fileText != null) && (Recv_Packet.buf_lng > 0))
                                        {
                                            stateCOM = COMState.Send_data_page;
                                            Write_Message(ADDR, RECV_FW_CMD, List_of_pages[sended_page]);
                                            if (List_of_pages.Count > (sended_page + 1))
                                                sended_page++;
                                            stateForm = WorkState.Update;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case COMState.Send_data_page:
                        if (new_msg == RECV_MSG.New)
                        {
                            if (Parce_msg(rbuff, recv_msg_length, Recv_Packet))
                            {
                                if (Recv_Packet.CMD == RECV_FW_CMD)
                                {
                                    if (Recv_Packet.Data[3] == DATA_OK)
                                    {
                                        Write_Message(ADDR, RECV_FW_CMD, List_of_pages[sended_page]);
                                        if (List_of_pages.Count > (sended_page + 1))
                                            sended_page++;
                                        stateForm = WorkState.Update;
                                    }
                                    else if (Recv_Packet.Data[3] == DATA_NOK)
                                    {
                                        sended_page = Recv_Packet.Data[4];     //неудачная страница
                                        stateCOM = COMState.Send_data_page;
                                        Write_Message(ADDR, RECV_FW_CMD, List_of_pages[sended_page]);
                                        if (List_of_pages.Count > (sended_page + 1))
                                            sended_page++;
                                        stateForm = WorkState.Update;
                                    }
                                    else if (Recv_Packet.Data[3] == DATA_END)    //конец записи
                                    {
                                        stateCOM = COMState.Info_write;
                                        Write_Message(ADDR, NEW_INFO_CMD, Test_data);
                                        stateForm = WorkState.Update;
                                    }
                                }
                            }
                        }
                
                     break;
                    case COMState.Info_write:

                        break;
                }
               Thread.Sleep(200);
            }

            


                
        }
        const byte ADDR = 0x01;
        const byte DATA_OK = 0x55;
        const byte DATA_NOK = 0xFF;
        const byte DATA_END = 0xAA;
        byte[] Test_data = { 1, 5, 7 };
        enum Status{
            wait,
            reset,
            reset_error,
            download,
            download_error,
            recv_fw,
            recv_error,
            new_info,
            new_info_errors
        }
        bool Get_RECV_BUF_LNG(PacketData rPack)
        {
            if (Recv_Packet.Data[0] != DATA_OK)
                return false;
            else {

                Recv_Packet.buf_lng = (ushort)(Recv_Packet.Data[1] + (Recv_Packet.Data[2]<<8));
                if (Recv_Packet.buf_lng != 0)
                    return true;
                else
                    return false;
            }
        }
        Status status = Status.wait;
        UInt16 MCU_BUF_lng;
        const byte WAIT_FW_CMD = 0x01;
        const byte RESET_CMD = 0x02;
        const byte DOWNLOAD_CMD = 0x03;
        const byte RECV_FW_CMD = 0x04;
        const byte NEW_INFO_CMD = 0x05;
        ushort Get_PGCount()
        {
            int ans = fileText.Length;
            ushort gg = 0;
            while (ans > 0)
            {
                ans -= buff_size;
                gg++;
            }
            return gg;
        }
        void Write_FW(string str)
        {
            while (fw_num < buff_size)
            {
                if (fw_num != 0 && fileText[fw_num] == ':')
                {
                    if ((fw_send_ind + 45) > buff_size)
                    {
                        COM.Write(send_buff, 0, fw_send_ind);
                        char[] strt = string.Format("!{0}@{1}#", page++, fw_send_ind).ToCharArray();
                        int cha = strt.Length;
                        COM.Write(strt, 0, cha);
                       // send_buff.SetValue()
                        fw_send_ind = 0;
                        if ((fileText[fw_num] != 0x0D) && (fileText[fw_num] != 0x0A))
                        {
                            send_buff[fw_send_ind++] = fileText[fw_num++];
                        }
                        else
                        {
                            fw_num++;
                        }
                        // send_buff[fw_send_ind++] = fileText[fw_num++];

                    }
                    else
                    {
                        send_buff[fw_send_ind++] = fileText[fw_num++];
                    }
                }
                else
                {
                    if ((fileText[fw_num] != 0x0D) && (fileText[fw_num] != 0x0A))
                    {
                        send_buff[fw_send_ind++] = fileText[fw_num++];
                    }
                    else
                    {
                        fw_num++;
                    }
                }
            }
                
        }
        string ex_message;
        void Write_Message(byte addr, byte cmd, byte[] data)
        {
            byte[] tmp = new byte[data.Length + 2];
            tmp[0] = addr;
            tmp[1] = cmd;
            Array.Copy(data, 0, tmp, 2, data.Length);
            Write_String(tmp, tmp.Length);

        }
        void Write_Message(byte addr, byte cmd, string data)
        {
         //   byte[] tmp = Encoding.ASCII.GetBytes(data);
            byte[] tmp = new byte[data.Length + 2];
            tmp[0] = addr;
            tmp[1] = cmd;

            Array.Copy(Encoding.ASCII.GetBytes(data), 0, tmp, 2, data.Length);
      //      string tmp = string.Format("{0}{1}", addr, cmd);
       //     tmp += data;
            Write_String(tmp, tmp.Length);

        }

        void Write_String(byte[]  str, int lng)
        {
            int crc16 = Calc_CRC(str, lng);
            byte[] tmp = new byte[str.Length + 2];
            Array.Copy(str, 0, tmp, 0, lng);
            tmp[tmp.Length - 2] = (byte)(crc16 & 0xFF);
            tmp[tmp.Length - 1] = (byte)((crc16 >> 8) & 0xFF);
            try
            {
                COM.Write(tmp, 0, tmp.Length);
            }
            catch(Exception ex)
            {
                ex_message = ex.Message;
            }

        }
        void Write_String(string str, int lng)
        {
            byte[] tmp_S = Encoding.ASCII.GetBytes(str);
            int crc16 = Calc_CRC(tmp_S, lng);
            byte[] tmp = new byte[str.Length + 2];
            Array.Copy(tmp_S, 0, tmp, 0, lng);
            tmp[tmp.Length - 2] = (byte)(crc16 & 0xFF);
            tmp[tmp.Length - 1] = (byte)((crc16 >> 8) & 0xFF);
            try
            {
                COM.Write(tmp, 0, tmp.Length);
            }
            catch (Exception ex)
            {
                ex_message = ex.Message;
            }

        }
        private void Form_Update_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                switch (stateForm)
                {
                    case WorkState.Idle:
                        break;
                    case WorkState.Update:
                        //if (new_msg)
                        //{
                        //    //  Filtr_lbl.Text = rbuff.ToString();

                        //    Filtr_lbl.Invoke((MethodInvoker)delegate
                        //    {
                        //        string msg = new string(rbuff);
                        //        Filtr_lbl.Text = msg;
                        //    });
                        //    Parce_msg(rbuff, recv_msg_length);

                        //    //                          stateForm = WorkState.Idle;
                        //    new_msg = false;
                        //    recv_msg_length = 0;
                        //}
                        break;


                }
                Thread.Sleep(500);
            }
        }
        UInt16 param_crc16_update(UInt16 crc, byte b)
        {
            return param_crc_update_crc16(crc, b, 0x8005, true);
        }
        byte reverse8(byte value)

        {
            value = (byte)((value & 0x55) << 1 | (value & 0xAA) >> 1);
            value = (byte)((value & 0x33) << 2 | (value & 0xCC) >> 2);
            value = (byte)((value & 0x0F) << 4 | (value & 0xF0) >> 4);

            return value;
        }
        UInt16 param_crc_update_crc16(UInt16 crc, byte b, UInt16 poly, bool reflectIn)
        {
            UInt16 j;

            if (reflectIn)
            {
                b = reverse8(b);
            }

            for (j = 0x80; j != 0; j >>= 1)
            {
                UInt16 bit = (UInt16)(crc & 0x8000);
                crc <<= 1;
                if ((b & j) != 0)
                {
                    bit ^= 0x8000;
                }
                if (bit != 0)
                {
                    crc ^= poly;
                }
            }

            return crc;
        }
        int Calc_CRC(byte[] buf, int lng)
        {
            UInt16 crc16 = 0xFFFF;
            for (int i = 0; i < lng; i++)
            {
                crc16 = param_crc16_update(crc16, buf[i]);
            }

            crc16 = param_crc16_finit(crc16);
            return crc16;
        }
        public struct PacketData
        {
           public byte CMD;
           public byte[] Data;
           public ushort buf_lng;
        };

        UInt16 param_crc16_finit(UInt16 crc)
        {
            crc = Reverse16(crc);
            return crc ^= 0x0000;
        }
        UInt16 Reverse16(UInt16 value)
        {
            value = (UInt16)((value & 0x5555) << 1 | (value & 0xAAAA) >> 1);
            value = (UInt16)((value & 0x3333) << 2 | (value & 0xCCCC) >> 2);
            value = (UInt16)((value & 0x0F0F) << 4 | (value & 0xF0F0) >> 4);
            value = (UInt16)((value & 0x00FF) << 8 | (value & 0xFF00) >> 8);

            return value;
        }

        bool Parce_msg(byte[] buf, int lng, PacketData rPack )
        {
            if (lng > 0)
            {
                if (Calc_CRC(buf, lng) != 0)
                {
                    new_msg = RECV_MSG.Idle;
             //       recv_msg_length = 0;
                    return false;
                }
                if (buf[0] != ADDR)
                {
                    new_msg = RECV_MSG.Idle;
              //      recv_msg_length = 0;
                    return false;
                }
                if ((buf[1] != RESET_CMD || buf[1] != DOWNLOAD_CMD || buf[1] != RECV_FW_CMD || buf[1] != NEW_INFO_CMD) == false )
                {
                    new_msg = RECV_MSG.Idle;
             //       recv_msg_length = 0;
                    return false;
                }

                Recv_Packet.CMD = (byte)buf[1];
                try
                {
                    byte[] tmp = new byte[lng - 4];
                    Array.Copy(buf, 2, tmp, 0, lng - 4);
                    Recv_Packet.Data = tmp;
                }
                catch(Exception ex)
                {
                    ex_message = ex.Message;
                }

                new_msg = RECV_MSG.Idle;
          //      recv_msg_length = 0;
                return true;
                //Parce_cmd(buf, lng, rPack);
                
            }

            return false;


        }

        void Parce_cmd(PacketData rPack)
        {
            switch (rPack.Data[1])
            {
                case RESET_CMD:
                    if (rPack.Data[2] == DATA_NOK)
                    {
                        //не перезапустился
                    }
                    else if (rPack.Data[2] == DATA_OK)
                    {
                        //перезапустился
                        status = Status.reset;
                    }
                 break;
                case WAIT_FW_CMD:
                    string st = string.Format("{0}{1}", ADDR, DOWNLOAD_CMD);
                 //   Write_String(st, st.Length);
                    status = Status.download;
                break;
                case DOWNLOAD_CMD:
                    if (rPack.Data[3] == DATA_OK)
                    {
                        char[] tmp = new char[rPack.Data.Length];
                        for (int i = 2; i < rPack.Data.Length; i++)
                        {
                            tmp[i - 2] = (char)rPack.Data[i];
                        }
                        ulong data_buf_lng = UInt64.Parse(tmp.ToString());


                        //начало перепрошивки
                    }
                    else
                    {
                        status = Status.wait;
                    }
                break;

            }
        }

        public int sleeptime = 100;
      

        private void Form_Update_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
          //  throw new NotImplementedException();
        }


        readonly BackgroundWorker Form_Update;
        readonly BackgroundWorker COM_Update;
        private void ConnectMenu_Click(object sender, EventArgs e)
        {
            string[] av_port = System.IO.Ports.SerialPort.GetPortNames();
            ConnectMenu.DropDownItems.Clear();

            ConnectMenu.DropDownItems.Add("Отключить");
            ConnectMenu.DropDownItems.Add("Настройка");
            foreach (string port in av_port)
                ConnectMenu.DropDownItems.Add(port);
        }
        object m_syncObject = new object();
        public enum State
        {
            Read,
            Write,
            New,
            Set
        }
        public State state { get; set; }
        ushort[] rdBuf;
       // private bool ModbusUpdate()
        //{
        //    try
        //    {
        //        lock (m_syncObject)
        //        {
        //            switch (state)
        //            {
        //                case State.Read:
        //                    try
        //                    {
        //                        rdBuf = m_mbMaster.ReadHoldingRegisters(0x01, 0x00, 1);
        //                        state = State.New;
        //                    }
        //                    catch
        //                    {
        //                        m_lastError = "Ошибка чтения";
        //                        return false;
        //                    }
        //                    break;
        //                case State.Write:
        //                    try
        //                    {
        //                        state = State.Set;
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        return false;
        //                    }
        //                    break;
        //                case State.New:
        //                    break;
        //                case State.Set:
        //                    state = State.Read;
        //                    break;
        //            }


        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        m_lastError = ex.Message;
        //        return false;
        //    }
        //}




        public enum WorkState
        {
            Idle,
            Update
        }
        public enum COMState
        {
            Read,
            Write,
            Idle,
            Start_Upload,
            Wait, 
            Reset,
            Info_read,
            Send_data_page,
            Resend_data_page,
            Info_write
        }
        bool is_connected = false;
        bool is_open = false;
        public bool Csisopen { get; set; }
        private WorkState stateForm = new WorkState();
        private COMState stateCOM = new COMState();
        public WorkState stateRegs { get; set; }
        string m_lastError;
        public void COM_set(int br, int db, int par, int sb, Int32 ad)
        {

            switch (br)
            {
                case 0:
                    COM.BaudRate = 9600;
                    break;
                case 1:
                    COM.BaudRate = 115200;
                    break;
            }

            switch (par)
            {
                case 0:
                    COM.Parity = System.IO.Ports.Parity.None;
                    break;
                case 1:
                    COM.Parity = System.IO.Ports.Parity.Even;
                    break;
                case 2:
                    COM.Parity = System.IO.Ports.Parity.Odd;
                    break;
            }

            switch (sb)
            {
                case 0:
                    COM.StopBits = System.IO.Ports.StopBits.One;
                    break;
                case 1:
                    COM.StopBits = System.IO.Ports.StopBits.One;
                    break;

            }


            //m_slaveAddr = (byte)ad;

            COM.DataBits = db + 7;
            //COM.Parity = par;
            //COM.StopBits = sb;


        }
        private void ConnectMenu_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string item = e.ClickedItem.ToString(); //ToolStripItemClickedEventArgs.ClickedItem;
            if (item == "Отключить" && COM.IsOpen)
            {
                COM.Close();
                is_connected = false;
                stateForm = WorkState.Idle;
                // Connectbtn.Enabled = false;
                stateRegs = WorkState.Idle;

                //return;
            }
            else if (item == "Настройка")
            {
                if (!is_open)
                {
                    if (COM.IsOpen)
                        COM.Close();

                    stateRegs = WorkState.Idle;
                    stateForm = WorkState.Idle;



                    Form Cs = new WindowsFormsApplication1.COM_settings();

                    Cs.Show();
                    is_open = true;
                    Csisopen = true;


                }



            }
            else
            {

                if (COM.IsOpen)
                    COM.Close();

                COM.PortName = item;
                try
                {
                    COM.Open();

                    is_connected = true;

                    stateForm = WorkState.Update;
                    Filtr_lbl.Invoke((MethodInvoker)delegate
                    {
                        Filtr_lbl.Text = "Подключен";
                        stateCOM = COMState.Wait;
                    });
                }
                catch (Exception ex)
                {
                    m_lastError = ex.Message;
                }


            }
        }
        byte[] rbuff = new byte[10000];
        enum RECV_MSG
        {
            New,
            InProgress,
            Idle
        }
        RECV_MSG new_msg = RECV_MSG.Idle;
        int recv_msg_length = 0;
        private void COM_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            if (new_msg != RECV_MSG.New)
            { 
                recv_msg_length = COM.Read(rbuff, 0, 100);
                new_msg = RECV_MSG.InProgress;
            }
            timer.Interval = 1000;
            timer.Enabled = true;
            timer.Start();
            new_msg = RECV_MSG.New;
          //  recv_msg_length = 0;
            timer.Enabled = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            stateCOM = COMState.Write;
        }
        bool is_file = false;
        string fileText;
        string[] pages;
        List<string> List_of_pages = new List<string>();

        private void fopen_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;


            string filename = openFileDialog1.FileName;
            // читаем файл в строку
            fileText = System.IO.File.ReadAllText(filename);
            richTextBox1.Text = "";
            richTextBox2.Text = "";
            richTextBox3.Text = "";
            richTextBox4.Text = "";
            richTextBox5.Text = "";
            richTextBox6.Text = "";
            richTextBox7.Text = "";
            richTextBox5.Invoke((MethodInvoker)delegate
            {
                richTextBox5.Text = fileText;
            });

            MessageBox.Show("Файл открыт");


            is_file = true;


            

            while (fw_num < fileText.Length)
            {
                if (fw_num != 0 && fileText[fw_num] == ':')
                {
                    if ((fw_send_ind + 45) > 2770)
                    {
                        //   COM.Write(send_buff, 0, fw_send_ind);
                        if (page == 0)
                        {

                            richTextBox1.Invoke((MethodInvoker)delegate
                            {
                                string s = new string(send_buff);
                                richTextBox1.Text += string.Format("!{0}@", page);
                                richTextBox1.Text += s;
                            });
                        }
                        if (page == 1)
                        {
                            richTextBox2.Invoke((MethodInvoker)delegate
                            {
                                string s = new string(send_buff);
                                richTextBox2.Text += string.Format("!{0}@", page);
                                richTextBox2.Text += s;
                            });
                        }
                        if (page == 2)
                        {
                            richTextBox3.Invoke((MethodInvoker)delegate
                            {
                                string s = new string(send_buff);
                                richTextBox3.Text += string.Format("!{0}@", page);
                                richTextBox3.Text += s;
                            });
                        }
                        if (page == 3)
                        {
                            richTextBox4.Invoke((MethodInvoker)delegate
                            {
                                string s = new string(send_buff);
                                richTextBox4.Text += string.Format("!{0}@", page);
                                richTextBox4.Text += s;
                            });
                        }
                        if (page >= 4)
                        {
                            richTextBox7.Invoke((MethodInvoker)delegate
                            {
                                string s = new string(send_buff);
                                richTextBox7.Text += string.Format("!{0}@", page);
                                richTextBox7.Text += s;
                                richTextBox7.Text += "|||";
                            });
                        }


                        char[] strt = string.Format("!{0:D}@{1:D}#", page++, fw_send_ind).ToCharArray();
                        char[] stt = new char[fw_send_ind + strt.Length];
                        Array.Copy(send_buff, 0, stt, 0, fw_send_ind);
                        Array.Copy(strt, 0, stt, fw_send_ind, strt.Length);
                        string f = new string(stt);
                        List_of_pages.Add(f);


                        //char[] strt = string.Format("!{0}@{1}#", page++, fw_send_ind).ToCharArray();
                        //string f = new string(send_buff);
                        //f += strt;
                        //LS.Add(f);

                        Array.Clear(send_buff, 0, 2767);
                        

                        // int cha = strt.Length;
                        // COM.Write(strt, 0, cha);
                        // send_buff.SetValue()
                        fw_send_ind = 0;
                        if ((fileText[fw_num] != 0x0D) && (fileText[fw_num] != 0x0A))
                        {
                            send_buff[fw_send_ind++] = fileText[fw_num++];
                        }
                        else
                        {
                            fw_num++;
                        }
                        // send_buff[fw_send_ind++] = fileText[fw_num++];

                    }
                    else
                    {
                        send_buff[fw_send_ind++] = fileText[fw_num++];
                    }

                }
                else
                {
                    if ((fileText[fw_num] != 0x0D) && (fileText[fw_num] != 0x0A))
                    {
                        send_buff[fw_send_ind++] = fileText[fw_num++];
                    }
                    else
                    {
                        fw_num++;
                    }

                }
                
            }
            if (fw_num == fileText.Length)
            {
                richTextBox6.Invoke((MethodInvoker)delegate
                {
                    string s = new string(send_buff);
                    richTextBox6.Text += string.Format("!{0}@", page);
                    richTextBox6.Text += s;



                });

                char[] strt = string.Format("!{0}@{1}#", page++, fw_send_ind).ToCharArray();
                char[] stt = new char[fw_send_ind + strt.Length];
                Array.Copy(send_buff, 0, stt, 0, fw_send_ind);
                Array.Copy(strt, 0, stt, fw_send_ind, strt.Length);
                string f = new string(stt);
                List_of_pages.Add(f);

                int dsdfsd = List_of_pages.Capacity;


            }



        }

        private void send_btn_Click(object sender, EventArgs e)
        {
            if (!is_file)
            {
                MessageBox.Show("Файл не открыт");
                    return;
            }

            stateCOM = COMState.Start_Upload;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //timer.Enabled = false;
            //timer.Stop();

            if (recv_msg_length != 0)
            {
                new_msg = RECV_MSG.New;
      //          recv_msg_length = 0;
                timer.Enabled = false;
                timer.Stop();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Write_Message(ADDR, DOWNLOAD_CMD, Test_data);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Write_Message(ADDR, RECV_FW_CMD, List_of_pages[sended_page]);

            string rrtsdf = "14";

            rrtsdf += List_of_pages[0];

            byte[] sdfsdf = Encoding.ASCII.GetBytes(rrtsdf);

            int dsfgdfg = Calc_CRC(sdfsdf, sdfsdf.Length);

            int fgfgfg = List_of_pages[0].Length;
            Write_Message(ADDR, RECV_FW_CMD, List_of_pages[0]);
          //  COM.Write(List_of_pages[0]);
          //  COM.Write(List_of_pages[0].ToCharArray(), 0, 500);

        }
    }
}
