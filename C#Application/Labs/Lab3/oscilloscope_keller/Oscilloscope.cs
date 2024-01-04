using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.Gpio;
using Windows.Devices.Spi;
using Windows.UI.Xaml;

namespace oscilloscope_keller
{
    public delegate void newDataType(double[] buf);
    public class Oscilloscope
    {
        GpioPin pin35;
        GpioPin pin28;
        GpioPin pin12;
        SpiDevice SPI_ADC;
        Task ta;
        public event newDataType newData;
        double[] values;
        DispatcherTimer timer;
        GpioController gpioctrl = GpioController.GetDefault();
        public bool triggerOn;
        public double TriggerLevel;
        public bool channelA;
        public bool channelB;
        public Oscilloscope()
        {
            GPIOInit();
            ta = SPI_Init();
            timer = new DispatcherTimer();  
            timer.Interval = new TimeSpan(0, 0, 0,0,100);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            if (!ta.IsCompleted) {
                return;
            }
            double[] voltages = new double[2*960];
            double voltage = 0.0;
            byte[] send = new byte[2*1920];
            byte[] receive = new byte[2*1920];
            int val = 0;
            pin35.Write(GpioPinValue.Low);
            //send welcher Channel 
            //Channel 000 
            //ChannelB Bit 808080808080 jeden zweiten wert auf 8 800800800800800

            if (channelB)
            {
                for (int i = 0; i < 3840; i+= 2)
                {
                    send[i] = 8;
                }
            }




            SPI_ADC.TransferFullDuplex(send, receive);
            pin35.Write(GpioPinValue.High);

           // List<double> voltagesList = new List<double>();    
            for (int i = 0; i < voltages.Length; i++)
            {

                val = receive[i*2] << 8;
                val += receive[i*2 + 1];
                val = val >> 2;
                voltages[i] = (val - 522.0) / (772.0 - 273.0) * 3.3;
                


            }

            


            if (triggerOn == true)
            {
                double [] results = triggerIsActivated(voltages);
                if (newData != null) { newData(results); };
            }
            else
            if (newData != null) { newData(voltages.Where((i, x) => (i < 960)).ToArray());  };


            // System.Diagnostics.Debug.WriteLine("byte0: {0}  Byte1: {1} value:{2}", receive[0], receive[1], val);
            //System.Diagnostics.Debug.WriteLine("Voltage {0}", voltage);
        }

        private void GPIOInit()
        {

            pin28 = gpioctrl.OpenPin(28);
            pin28.SetDriveMode(GpioPinDriveMode.Output);
            pin28.Write(GpioPinValue.High);

            pin35 = gpioctrl.OpenPin(35);
            pin35.SetDriveMode(GpioPinDriveMode.Output);
            pin35.Write(GpioPinValue.High);

            pin12 = gpioctrl.OpenPin(12);
            pin12.SetDriveMode(GpioPinDriveMode.Output);
            pin12.Write(GpioPinValue.High);


        }

        private async Task SPI_Init()
        {
            String spiDeviceSelector = SpiDevice.GetDeviceSelector();
            IReadOnlyList<DeviceInformation> devices = await DeviceInformation.FindAllAsync(spiDeviceSelector);

            SpiConnectionSettings SPI_Settings = new SpiConnectionSettings(0);

            SPI_Settings.ClockFrequency = 4800000;
            SPI_Settings.Mode = SpiMode.Mode3;

            SPI_ADC = await SpiDevice.FromIdAsync(devices[0].Id, SPI_Settings);
        }
    

        public double [] triggerIsActivated(double [] voltages)
        {
            double[] results = new double[960];
            int trig = 0;
            for (int i = 0; i < 960; i += 1)
            {
                if ((voltages[i] < TriggerLevel) && (voltages[i + 3] > TriggerLevel))
                {
                    trig = i;
                    break;
                }
            }

            for (int i = trig; i < trig+960 ; i++)
            {
                results[i - trig] = voltages[i];
            }
            return results; 
        }
}
}
