// Decompiled with JetBrains decompiler
// Type: DT_3391.Form2
// Assembly: DT-9961T, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 681D06A8-439A-4E73-B916-52A7EBFBD7ED
// Assembly location: C:\Program Files (x86)\Multimeter\Multimeter.exe

using Get_COMX;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Management;
using System.Text;
using System.Threading;

namespace QM1571
{
    public class Core
    {
        private AnalyzeData andata = new AnalyzeData();
        private string csvLine = ""; //the current row of data converted to CSV line
        private string comPortNumber = ""; //Com port number to be used for the device
        private bool closing = false; //A flag to tell the port reader to stop reading while the port is being closed.
        private bool isRecieving = false; //A flag to report data coming in.

        private SerialPort serialPort1 = new SerialPort();
        private StreamWriter csvFile = null; // A CSV file with the current measurements.



        /// <summary>
        /// Open the COM port and start listening to incoming data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void connectToMultimeter(object sender, EventArgs e)
        {
            if (this.serialPort1.IsOpen) this.serialPort1.Close(); //re-open

            this.serialPort1.PortName = this.comPortNumber; //get the port number
            this.serialPort1.Open();
            Program.LogIt("Port open: " + this.comPortNumber);
        }


        /// <summary>
        /// Close open ports on exit asynchronously
        /// </summary>
        public void StopLogging()
        {
            if (this.serialPort1.IsOpen) new Thread(new ThreadStart(this.closeSer)).Start();
        }

        /// <summary>
        /// Safely close the open COM ports and files
        /// </summary>
        public void closeSer()
        {
            this.closing = true;
            if (this.serialPort1.IsOpen) this.serialPort1.Close();
            csvFile.Close();
        }

        /// <summary>
        /// Initialize and start listening to the COM port for data
        /// </summary>
        public void StartLogging()
        {

            Program.LogIt("Initialising the COM port");
            //initialise the COM port
            this.serialPort1.BaudRate = 2400;
            this.serialPort1.ReadTimeout = 1000;
            this.serialPort1.ReceivedBytesThreshold = 19;
            this.serialPort1.DataReceived += new SerialDataReceivedEventHandler(this.serialPort1_DataReceived);

            //Find out the COM port number assuming there is only 1 device connected
            ArrayList USB_com = new ArrayList();
            USB_com = Get_COM.GetSerialPort("Silicon Labs CP210x USB to UART Bridge");
            Program.LogIt("Found connected recievers: " + USB_com.Count.ToString());
            this.comPortNumber = USB_com[0].ToString();

            //Prepare the output file
            string scvPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\logger-" + DateTime.Now.ToString("yyyyMMdd-HHmm") + ".txt";
            Program.LogIt("Saving to: " + scvPath);
            // Write the string array to a new file named "WriteLines.txt".
            csvFile = new StreamWriter(scvPath);

            //Try to connect to the device
            connectToMultimeter(null, null);

        }

        /// <summary>
        /// Process and save the data in the COM-port buffer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (this.closing) return; //don't log while it's closing

            if (!this.isRecieving)
            {
                Program.LogIt("The data started coming in.");
                this.isRecieving = true;
            }

            int bytesToRead = this.serialPort1.BytesToRead;
            if (bytesToRead < 15) return;
            byte[] numArray = new byte[bytesToRead];
            this.serialPort1.Read(numArray, 0, bytesToRead);
            this.serialPort1.DiscardInBuffer();
            if (!this.andata.listdata(ref this.csvLine, numArray))
            {
                //discard current data and try again - not 100% sure it will work
                this.serialPort1.DiscardInBuffer();
                this.serialPort1.Close();
                this.serialPort1.Open();
                csvLine = DateTime.Now.ToString("o") + ",Buffer error";
            }

            //save the data in a file if there is any
            if (this.closing || csvLine == "") return;

            csvFile.WriteLine(this.csvLine);
            csvFile.Flush();

        }

    }
}
