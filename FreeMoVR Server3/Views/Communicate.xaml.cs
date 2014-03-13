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

namespace FreeMoVR_Server3.Views
{
    /// <summary>
    /// Interaction logic for Communicate.xaml
    /// </summary>
    public partial class Communicate : UserControl
    {
        public Communicate()
        {
            InitializeComponent();
        }

        // To be used for the console transitions
        //void Tick(object sender, EventArgs e)
        //{
        //    var dateTime = DateTime.Now;
        //    transitioning.Content = new TextBlock { Text = "Transitioning Content! WHAT WHAT. CONSOLE SHOWS UP HERE." + dateTime, SnapsToDevicePixels = true };
        //}
    }
}
