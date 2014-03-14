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
using X2CodingLab.SensorTag;
using X2CodingLab.SensorTag.Sensors;
using X2CodingLab.SensorTag.Exceptions;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace FreeMoVR_App2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Accelerometer acc;
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void TheButton_Click(object sender, RoutedEventArgs e)
        {
            acc = new Accelerometer();
            await acc.Initialize();
            await acc.EnableSensor();
            string success = "sensor has been enabled!";
            initialize.Content = success;
        }

        private async void getInfo(object sender, RoutedEventArgs e)
        {
            byte[] accValue = await acc.ReadValue();
            double[] accAxis = Accelerometer.CalculateCoordinates(accValue, 1 / 64.0);
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                string acceloutput = "X: " + accAxis[0].ToString("0.00") + " Y: " + accAxis[1].ToString("0.00") + " Z: " + accAxis[2].ToString("0.00");
                pull.Content = acceloutput;
            });
        }
            
    }
}
