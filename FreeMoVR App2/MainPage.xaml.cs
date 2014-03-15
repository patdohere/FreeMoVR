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
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Windows.Web;
using System.Threading;
using System.Threading.Tasks;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace FreeMoVR_App2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Accelerometer acc;

        private MessageWebSocket messageWebSocket;
        private DataWriter messageWriter;
        public MainPage()
        {
            this.InitializeComponent();
            info.Text = "Press Start";
        }

        private async void TheButton_Click(object sender, RoutedEventArgs e)
        {
            initialize.IsEnabled = false;
            stopButton.IsEnabled = true;
            acc = new Accelerometer();
            acc.SensorValueChanged += SensorValueChanged;
            await acc.Initialize();
            await acc.EnableSensor();
            await acc.SetReadPeriod(50);
            await acc.EnableNotifications();
        }

        private async void stop_Click(object sender, RoutedEventArgs e)
        {
            stopButton.IsEnabled = false;
            initialize.IsEnabled = true;
            await acc.DisableNotifications();
            await acc.DisableSensor();
            info.Text = "Press Start";
        }

        async void SensorValueChanged(object sender, X2CodingLab.SensorTag.SensorValueChangedEventArgs e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
            {
                switch (e.Origin)
                {
                    case SensorName.Accelerometer:
                        // WebSockets
                        MessageWebSocket webSocket = messageWebSocket;

                        if (webSocket == null)
                        {
                            Uri server = new Uri("ws://localhost:8181");
                            webSocket = new MessageWebSocket();
                            webSocket.Control.MessageType = SocketMessageType.Utf8;
                            webSocket.MessageReceived += MessageReceived;
                            webSocket.Closed += Closed;

                            await webSocket.ConnectAsync(server);
                            messageWebSocket = webSocket;
                            messageWriter = new DataWriter(webSocket.OutputStream);
                        }

                            string message = null;
                            byte[] accValue = await acc.ReadValue();
                            double[] accAxis = Accelerometer.CalculateCoordinates(e.RawData, 1 / 64.0);

                                double xRaw = accAxis[0] / 4;
                                double yRaw = accAxis[1] / 4;

                                message = "SET RAW_INPUT " + xRaw.ToString() + "," + yRaw.ToString();
                                info.Text = message;


                            messageWriter.WriteString(message);
                            await messageWriter.StoreAsync();
                        break;
                }
            });
        }

  

        private void MessageReceived(MessageWebSocket sender, MessageWebSocketMessageReceivedEventArgs args)
        {
            try
            {
                using (DataReader reader = args.GetDataReader())
                {
                    reader.UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf8;
                    string read = reader.ReadString(reader.UnconsumedBufferLength);
                }
            }
            catch (Exception ex)
            {
                WebErrorStatus status = WebSocketError.GetStatus(ex.GetBaseException().HResult);
            }
        }

        private void Closed(IWebSocket sender, WebSocketClosedEventArgs args)
        {
            MessageWebSocket webSocket = Interlocked.Exchange(ref messageWebSocket, null);
            if (webSocket != null)
            {
                webSocket.Dispose();
            }
        }
    }
}
