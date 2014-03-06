using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Alchemy;
using Alchemy.Classes;
using System.Net;


namespace FreeMoVR_Server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

              // instantiate a new server - acceptable port and IP range,
              // and set up your methods.

              var aServer = new WebSocketServer(81, IPAddress.Any) {
                OnReceive = OnReceive,
                OnSend = OnSend,
                OnConnect = OnConnect,
                OnConnected = OnConnected,
                OnDisconnect = OnDisconnect,
                TimeOut = new TimeSpan(0, 5, 0)
              };

              aServer.Start();
        }


        static void OnDisconnect(UserContext context)
        {
            throw new NotImplementedException();
        }

        static void OnConnect(UserContext context)
        {
            throw new NotImplementedException();
        }

        static void OnSend(UserContext context)
        {
            throw new NotImplementedException();
        }

        static void OnReceive(UserContext context)
        {
            throw new NotImplementedException();
        }

        static void OnConnected(UserContext context)
        {
            Console.WriteLine("Client Connection From : " +
            context.ClientAddress.ToString());

            //BLAH HOW TO DATA BIND THIS THANG.
        }


    }
}
