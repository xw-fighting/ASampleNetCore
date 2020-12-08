using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;
using SATOPrinterAPI;

namespace ASample.NetCore.SerialPortLib
{
    public class SendSerialPortServices
    {
        static SerialPort _serialPort;

        public void Wirte(string content)
        {
            try
            {
                Console.WriteLine("1:"+content);
                // Create a new SerialPort object with default settings.
                _serialPort = new SerialPort();

                // Allow the user to set the appropriate properties.
                var portName = ConfigurationReader.GetValue("SerialPort:PortName");
                var baudRate = ConfigurationReader.GetValue("SerialPort:BaudRate");
                //var parity = ConfigurationReader.GetValue("SerialPort:Parity");
                var dataBits = ConfigurationReader.GetValue("SerialPort:DataBits");
                //var stopBits = ConfigurationReader.GetValue("SerialPort:StopBits");
                var handshake = ConfigurationReader.GetValue("SerialPort:Handshake");
                var readTimeout = ConfigurationReader.GetValue("SerialPort:ReadTimeout");
                var writeTimeout = ConfigurationReader.GetValue("SerialPort:WriteTimeout");

                _serialPort.PortName = portName ?? _serialPort.PortName;
                _serialPort.BaudRate = string.IsNullOrEmpty(baudRate) ?_serialPort.BaudRate:Convert.ToInt32(baudRate);
                _serialPort.Parity = _serialPort.Parity;
                _serialPort.DataBits = string.IsNullOrEmpty(dataBits) ? _serialPort.DataBits : Convert.ToInt32(dataBits);
                _serialPort.StopBits = _serialPort.StopBits;
                //_serialPort.Handshake = SetPortHandshake(_serialPort.Handshake);

                // Set the read/write timeouts
                _serialPort.ReadTimeout = Convert.ToInt32(readTimeout);
                _serialPort.WriteTimeout = Convert.ToInt32(writeTimeout);
                _serialPort.Open();

                //写入SerialPort
                Console.WriteLine(content);
                //var byteContent = Encoding.UTF8.GetBytes(content);

                

                var buffer = new List<byte>();
                //byte[] tmp = { 10 }; //这里的10是厂家说明书里的命令 16进制是0x0A,10进制是10，表示打印并换行

                var data = StringToByteArray('\u0002' + content + '\u0003');
                Printer SATOPrinter = new Printer();
                SATOPrinter.Interface = Printer.InterfaceType.COM;
                SATOPrinter.COMPort = "COM1";
                //_serialPort.Write(data, 0, data.Length);
                SATOPrinter.Send(data);


                //_serialPort.Close();
                //Printer SATOPrinter = new Printer();
                //SATOPrinter.Send(data);
            }
            catch (Exception ex)
            {
                _serialPort.Close();
                Console.WriteLine(ex.Message);
            }
        }

        public static byte[] StringToByteArray(string data, string encoding = "ansi")
        {
            if (encoding == null)
                return Encoding.Default.GetBytes(data);
            var lower = encoding.ToLower();
            return lower == "ascii" ? Encoding.ASCII.GetBytes(data) : (lower == "utf7" ? Encoding.UTF7.GetBytes(data) : (lower == "utf8" ? Encoding.UTF8.GetBytes(data) : (lower == "ansi" ? Encoding.Default.GetBytes(data) : (lower == "utf16" ? Encoding.Unicode.GetBytes(data) : (lower == "utf32" ? Encoding.UTF32.GetBytes(data) : Encoding.Default.GetBytes(data))))));
        }


        /// <summary>
        /// 字符串转16进制字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        private static byte[] StrToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            var returnBytes = new byte[hexString.Length / 2];
            for (var i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        public void Read()
        {
            try
            {
                // Begin communications
                _serialPort.Open();
                _serialPort.DataReceived += new
                    SerialDataReceivedEventHandler(PortDataReceived);

            }
            catch (TimeoutException)
            {

            }
        }

        private void PortDataReceived(object sender,
            SerialDataReceivedEventArgs e)
        {
            // Show all the incoming data in the port's buffer
            var readContent = _serialPort.ReadExisting();
            //处理读取到的信息
        }

        public static Handshake SetPortHandshake(Handshake defaultPortHandshake)
        {
            string handshake;

            Console.WriteLine("Available Handshake options:");
            foreach (string s in Enum.GetNames(typeof(Handshake)))
            {
                Console.WriteLine("   {0}", s);
            }

            Console.Write("Enter Handshake value (Default: {0}):", defaultPortHandshake.ToString());
            handshake = Console.ReadLine();

            if (handshake == "")
            {
                handshake = defaultPortHandshake.ToString();
            }

            return (Handshake)Enum.Parse(typeof(Handshake), handshake, true);
        }
    }
}
