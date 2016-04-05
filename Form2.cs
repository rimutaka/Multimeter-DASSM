// Decompiled with JetBrains decompiler
// Type: DT_3391.Form2
// Assembly: DT-9961T, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 681D06A8-439A-4E73-B916-52A7EBFBD7ED
// Assembly location: C:\Program Files (x86)\Multimeter\Multimeter.exe

using DT_3391.Properties;
using Get_COMX;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Management;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using Yaowi.Common.Collections;
using Yaowi.Common.Windows.Controls;
using ZedGraph;

namespace DT_3391
{
    public class Form2 : Form
    {
        public static Form Add_list = new Form();
        private ListViewSorter listviewsorter = new ListViewSorter();
        private listViewAddItem addlist = new listViewAddItem();
        private AnalyzeData andata = new AnalyzeData();
        private string csvLine = ""; //the current row of data converted to CSV line
        private string comPortNumber = ""; //Com port number to be used for the device
        private bool closing = false;
        private ArrayList USB_com = new ArrayList();
        private int Err = 0;
        private int diu = 0;
        private IContainer components = (IContainer)null;
        private string sys;

        private bool isConnected = false;
 
        private SerialPort serialPort1;
 


        // Write the string array to a new file named "WriteLines.txt".
        private StreamWriter csvFile = null; // A CSV file with the current measurements.


        public Form2()
        {
            this.InitializeComponent();
        }


        /// <summary>
        /// Open the COM port and start listening to incoming data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void connectToMultimeter(object sender, EventArgs e)
        {
            if (!this.isConnected)
            {
                try
                {
                    if (this.serialPort1.IsOpen)
                    {
                        int num2 = (int)MessageBox.Show("Port is open", "Hint");
                    }
                    else
                    {
                        this.serialPort1.PortName = this.comPortNumber;
                        this.serialPort1.Open();

                        this.closing = false;
                    }
                }
                catch (Exception ex)
                {
                    int num2 = (int)MessageBox.Show(ex.Message, "Hint");
                }
            }
            else
            {
                this.serialPort1.Close();
            }
        }



        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.serialPort1.IsOpen)
                new Thread(new ThreadStart(this.closeSer)).Start();
        }

        public void closeSer()
        {
            this.closing = true;
            this.serialPort1.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.sys = Form2.Detect3264();
            this.USB_com = Get_COM.GetSerialPort("Silicon Labs CP210x USB to UART Bridge");
            for (int index = 0; index < this.USB_com.Count; ++index)
            {
                this.comPortNumber = this.USB_com[index].ToString();
                //this.com.Text = this.USB_com[0].ToString();
            }

            //Prepare the output file
            string scvPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\logger-" + DateTime.Now.ToString("yyyyMMdd-HHmm") + ".txt";

            // Write the string array to a new file named "WriteLines.txt".
            csvFile = new StreamWriter(scvPath);

            //Try to connect to the device
            connectToMultimeter(null, null);

        }

        private void newButton_Click(object sender, EventArgs e)
        {
            if (this.serialPort1.IsOpen)
                new Thread(new ThreadStart(this.closeSer)).Start();
            this.new_co();
        }

        private void new_co()
        {
            this.isConnected = true;
        }


         private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (this.closing) return;
            int bytesToRead = this.serialPort1.BytesToRead;
            if (bytesToRead < 15) return;
            byte[] numArray = new byte[bytesToRead];
            this.serialPort1.Read(numArray, 0, bytesToRead);
            this.serialPort1.DiscardInBuffer();
            if (!this.andata.listdata(ref this.csvLine, numArray))
            {
                this.serialPort1.DiscardInBuffer();
                ++this.Err;
                ++this.diu;
                if (this.Err >= 20)
                {
                    this.Err = 0;
                    this.serialPort1.Close();
                    this.isConnected = true;
                }
                else
                {
                    this.serialPort1.Close();
                    this.serialPort1.Open();
                }
            }
            else
            {
                //save the data in a file if there is any
                if (csvLine == "") return;

                    csvFile.WriteLine(this.csvLine);
                    csvFile.Flush();

                //this.AddData();
                this.Err = 0;
            }
        }

        /// <summary>
        /// Convert "data" into a CSV string
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        //private static string ArrayToCSV(ArrayList data) 
        //{
        //    foreach (List<string> line in data) { 

        //    }


        //}



        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new About().Show();
        }


        protected override void DefWndProc(ref Message m)
        {
            if (m.Msg == 537)
            {
                this.USB_com.Clear();
                switch (m.WParam.ToInt32())
                {
                    case 32768:
                        if (this.sys == "32")
                        {
                            this.portcom();
                            break;
                        }

                        break;
                    case 32772:

                        break;
                }
            }
            base.DefWndProc(ref m);
        }


        private void helpTopicsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Application.StartupPath + "\\help.chm");
        }

        private void zedGraphControl1_ContextMenuBuilder(ZedGraphControl sender, ContextMenuStrip menuStrip, Point mousePt, ZedGraphControl.ContextMenuObjectState objState)
        {
            foreach (ToolStripMenuItem toolStripMenuItem in (ArrangedElementCollection)menuStrip.Items)
            {
                if ((string)toolStripMenuItem.Tag == "unzoom")
                {
                    menuStrip.Items.Remove((ToolStripItem)toolStripMenuItem);
                    toolStripMenuItem.Visible = false;
                    break;
                }
            }
        }

        private void initializeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string str1 = "6F8F1698";
                byte[] buffer1 = new byte[str1.Length / 2];
                int startIndex1 = 0;
                while (startIndex1 < str1.Length)
                {
                    buffer1[startIndex1 / 2] = Convert.ToByte(str1.Substring(startIndex1, 2), 16);
                    startIndex1 += 2;
                }
                string str2 = "7F";
                byte[] buffer2 = new byte[str2.Length / 2];
                int startIndex2 = 0;
                while (startIndex2 < str2.Length)
                {
                    buffer2[startIndex2 / 2] = Convert.ToByte(str2.Substring(startIndex2, 2), 16);
                    startIndex2 += 2;
                }
                if (!this.serialPort1.IsOpen)
                {
                    this.serialPort1.ReceivedBytesThreshold = 3;
                    this.serialPort1.PortName = this.comPortNumber;
                    this.serialPort1.Open();
                    this.serialPort1.DiscardOutBuffer();
                    this.serialPort1.DiscardInBuffer();
                    this.serialPort1.Write(buffer1, 0, buffer1.Length);
                    Thread.Sleep(2000);
                    this.serialPort1.DiscardOutBuffer();
                    this.serialPort1.DiscardInBuffer();
                    this.serialPort1.Write(buffer2, 0, buffer2.Length);
                    Thread.Sleep(1000);
                    this.serialPort1.DiscardOutBuffer();
                    this.serialPort1.DiscardInBuffer();
                    this.serialPort1.Close();
                    int num = (int)MessageBox.Show("Initialize Fulfill!");
                    this.serialPort1.ReceivedBytesThreshold = 19;
                }
                else
                {
                    this.serialPort1.Close();
                    this.isConnected = true;
                    this.serialPort1.ReceivedBytesThreshold = 3;
                    this.serialPort1.PortName = this.comPortNumber;
                    this.serialPort1.Open();
                    this.serialPort1.DiscardOutBuffer();
                    this.serialPort1.DiscardInBuffer();
                    this.serialPort1.Write(buffer1, 0, buffer1.Length);
                    Thread.Sleep(2000);
                    this.serialPort1.DiscardOutBuffer();
                    this.serialPort1.DiscardInBuffer();
                    this.serialPort1.Write(buffer2, 0, buffer2.Length);
                    Thread.Sleep(1000);
                    this.serialPort1.DiscardOutBuffer();
                    this.serialPort1.DiscardInBuffer();
                    this.serialPort1.Close();
                    int num = (int)MessageBox.Show("Initialize Fulfill!");
                    this.serialPort1.ReceivedBytesThreshold = 19;
                }
            }
            catch
            {
                int num = (int)MessageBox.Show("Initialize Err!", "Err");
            }
        }

        public void portcom()
        {
            this.USB_com = !(this.sys == "32") ? Get_COM.GetSerialPort("Silicon Labs CP210x USB to UART Bridge") : Get_COM.GetPortNameFormVidPid("Vid_10c4", "Pid_ea60");
            for (int index = 0; index < this.USB_com.Count; ++index)
            {
                this.comPortNumber = this.USB_com[index].ToString();
            }


            if (this.comPortNumber == "")
            {
                this.isConnected = true;
            }

        }

        /// <summary>
        /// Check if this is a 32 or 64 bit system
        /// </summary>
        /// <returns></returns>
        public static string Detect3264()
        {
            ManagementObjectCollection objectCollection = new ManagementObjectSearcher(new ManagementScope("\\\\localhost", new ConnectionOptions()), new ObjectQuery("select AddressWidth from Win32_Processor")).Get();
            string str = (string)null;
            foreach (ManagementBaseObject managementBaseObject in objectCollection)
                str = managementBaseObject["AddressWidth"].ToString();
            return str;
        }

 
         protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = (IContainer)new Container();
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(Form2));

            this.serialPort1 = new SerialPort(this.components);
            this.SuspendLayout();
 
            this.serialPort1.BaudRate = 2400;
            this.serialPort1.ReadTimeout = 1000;
            this.serialPort1.ReceivedBytesThreshold = 19;
            this.serialPort1.DataReceived += new SerialDataReceivedEventHandler(this.serialPort1_DataReceived);

            this.Font = new Font("Arial", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            //      this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
            this.Margin = new Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Form2";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Multimeter V1.0 ";
            this.Load += new EventHandler(this.Form2_Load);
            this.FormClosed += new FormClosedEventHandler(this.Form2_FormClosed);
            this.FormClosing += new FormClosingEventHandler(this.Form2_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
