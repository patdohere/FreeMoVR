using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fleck;

namespace FreeMoVR_Simple_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            vJoyFeeder vjf = new vJoyFeeder();
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
                    allSockets.ToList().ForEach(s => s.Send("Return: " + vjf.parseInstructionString(message)));
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
