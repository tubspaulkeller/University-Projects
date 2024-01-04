using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x407 dokumentiert.

namespace oscilloscope_keller
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Oscilloscope myOscilloscope;

        public MainPage()
        {
            this.InitializeComponent();
            myOscilloscope = new Oscilloscope();    
            myOscilloscope.newData += newDataHandler;
           
        }
      
        private void newDataHandler(double[] data)
        {
            // Min-Value
            double minValue = data.Select(x => x).Min();
            Vmin.Text = "Min: " +minValue.ToString("0.00"); 

           
            // Max-Value
            double maxValue = data.Select(x => x).Max();
            Vmax.Text = "Max: " + maxValue.ToString("0.00");

            // Average-Value
            double avgValue= data.Select(x => x).Average();
            Vavg.Text = "AVG: " + avgValue.ToString("0.00");

            // Effective Value
            double effValue= Math.Sqrt(data.Select(x => x*x).Average()); 
            Veff.Text = "Eff: " + effValue.ToString("0.00");
            
            // GUI
            PointCollection clp = new PointCollection();
            for (int i = 0; i < data.Length; i++)
            {
                clp.Add(new Point(i,250-250*(data[i]/1.65)));  
            }
            poly.Points = clp;
        }
        

      

        private void SliderValueChange(object sender, RangeBaseValueChangedEventArgs e)
        {
            myOscilloscope.TriggerLevel = TriggerSlider.Value;
        }


        private void ChannelA_Click(object sender, RoutedEventArgs e)
        {
            if (A.IsChecked == true)
            {
                B.IsChecked = false;
                myOscilloscope.channelA = true;
                myOscilloscope.channelB = false;
            }


        }

        private void ChannelB_Click(object sender, RoutedEventArgs e)
        {
            if (B.IsChecked == true)
            {
                A.IsChecked = false;
                myOscilloscope.channelA = false;
                myOscilloscope.channelB = true;

            }
          

        }

        private void TriggerClick(object sender, RoutedEventArgs e)
        {
            if (Trigger.IsChecked == true)
            {
                myOscilloscope.triggerOn = true;
            }
            else myOscilloscope.triggerOn = false;



        }

        
    }
}
