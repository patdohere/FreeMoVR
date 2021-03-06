﻿using System;
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
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Windows.Storage.Pickers;
using Windows.Storage;
using System.Threading;
using System.Threading.Tasks;
using Windows.Networking.BackgroundTransfer;
using Windows.Networking.Sockets;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238
namespace FreeMoVR_App
{
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MessageWebSocket messageWebSocket;
        private DataWriter messageWriter;

        public MainPage()
        {
            this.InitializeComponent();
            //Button_Click(null, null);
        }


        // WebSockets Button
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //creates a new MessageWebSocket and connects to WebSocket server and sends data to server
                
                //Make a local copy
                MessageWebSocket webSocket = messageWebSocket;

                //Have we connected yet?
                if (webSocket == null)
                {

                    Uri server = new Uri(ServerAddressField.Text.Trim());
                    webSocket = new MessageWebSocket();
                    webSocket.Control.MessageType = SocketMessageType.Utf8;
                    webSocket.MessageReceived += MessageReceived;
                    webSocket.Closed += Closed;
                    await webSocket.ConnectAsync(server);
                    messageWebSocket = webSocket;
                    messageWriter = new DataWriter(webSocket.OutputStream);
                }

                //InputField is a textbox in the xaml
                string message = InputField.Text;
                messageWriter.WriteString(message);
                await messageWriter.StoreAsync();
            }
            catch (Exception ex)
            {
                String.Format("There is an error in connection"); 
            }
        }

        //Sends the data
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
                String.Format("There is an error sending the data"); 
            }
        }


        // Close connection
        private void Closed(IWebSocket sender, WebSocketClosedEventArgs args)
        {
            MessageWebSocket webSocket = Interlocked.Exchange(ref messageWebSocket, null);
            if (webSocket != null)
            {
                webSocket.Dispose();
            }
        }

        

        private void Websockets_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Websockets));
        }

        private void BLE_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(BLE));
        }

        /* Pages */
   
    }
}