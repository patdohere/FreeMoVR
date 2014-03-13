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

using Fleck;

namespace FreeMoVR_Server2
{
    /// <summary>
    /// Interaction logic for RunServer.xaml
    /// </summary>
    public partial class RunServer : UserControl
    {
        private vJoyFeeder vj;

        public RunServer()
        {
            InitializeComponent();
            this.vj = new vJoyFeeder();
        }

        public void runFleck()
        {

            FleckLog.Level = LogLevel.Debug;
            var allSockets = new List<IWebSocketConnection>();
            var server = new WebSocketServer("localhost:8181");
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    Console.WriteLine("Open!");
                    allSockets.Add(socket);
                };
                socket.OnClose = () =>
                {
                    Console.WriteLine("Close!");
                    allSockets.Remove(socket);
                };
                socket.OnMessage = message =>
                {
                    // This is the only line of code that outputs to console. Each instruction given to parser will return a string, which
                    // can then be used in the windows APP UI.

                    //Console.WriteLine(vj.parseInstructionString(message));
                    allSockets.ToList().ForEach(s => s.Send("Echo: " + message));
                    allSockets.ToList().ForEach(s => s.Send("Return: " + vj.parseInstructionString(message)));
                };
            });


            var input = Console.ReadLine();
            while (input != "exit")
            {
                foreach (var socket in allSockets.ToList())
                {
                    socket.Send(input);
                }

                input = Console.ReadLine();
            }
        }
    }
}
