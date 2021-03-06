﻿using System;
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
            //hardcoded vJoyFeeder class for testing, is this where we want to call the class?
            vJoyFeeder vj = new vJoyFeeder();
            
            InitializeComponent();

            //var server = new WebSocketServer("http://localhost:8080");

            //server.Start(socket =>
            //{
            //    socket.OnOpen = () => Console.WriteLine("Open!");
            //    socket.OnClose = () => Console.WriteLine("Close!");
            //    //socket.OnMessage = message => socket.Send(message);

            //});

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


        ///////

        public void Navigate(UserControl nextPage)
        {
            this.Content = nextPage;
        }

        public void Navigate(UserControl nextPage, object state)
        {
            this.Content = nextPage;
            ISwitchable s = nextPage as ISwitchable;

            if (s != null)
                s.UtilizeState(state);
            else
                throw new ArgumentException("NextPage is not ISwitchable! "
                  + nextPage.Name.ToString());
        }

        /*
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Switcher.pageSwitcher = this;
            Switcher.Switch(new vJoy());    //Navigate to the vJoy User Control
        }
        */
       
    }



    // Needed for navigation
    public interface ISwitchable
    {
        void UtilizeState(object state);
    }

    public static class Switcher
    {
        public static MainWindow pageSwitcher;

        public static void Switch(UserControl newPage)
        {
            pageSwitcher.Navigate(newPage);
        }

        public static void Switch(UserControl newPage, object state)
        {
            pageSwitcher.Navigate(newPage, state);
        }
    }
        

    }

