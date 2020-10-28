using System;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace ASample.NetCore.SerialPortLib
{
    public class SendSerialPortServices
    {
        static SerialPort _serialPort;
        public void Wirte(string content)
        {
            try
            {
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
                var byteContent = Encoding.Unicode.GetBytes(content);
                _serialPort.Write(byteContent,0, byteContent.Length);
            
                _serialPort.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
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
