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

namespace FreeMoVR_Server
{
    /// <summary>
    /// Interaction logic for vJoy.xaml
    /// </summary>
    public partial class vJoy : Page
    {
        // vJoyFeeder instance
        static public vJoyFeeder vjfeed;
       
        public vJoy()
        {
            InitializeComponent();
            vjfeed = new vJoyFeeder();
            // Initialize to the center
            vjfeed.inputRawCoords(0.0, 0.0);
        }

        /* Instructions for parsing
         * Instruction string will be parsed as Command / Attribute / Value, with each token separated by whitespace
         * Current valid commands:
         * SET (input x y coords to vJoy) | GET (retrieve current x y coords from vJoy) | STATUS (is joystick active?) | ACQUIRE (acquire vJoy driver)
         * 
         * Current valid attributes:
         * RAW_INPUT (x y coords without conversion)| SCALED_INPUT(x y coords with conversion) 
         * 
         * Current valid values (both x and y values must be inputted together):
         * X_Y (The format must be X,Y)
         * 
         * IMPORTANT!!!
         * 1. YOU MUST DETERMINE IF DRIVER IS ENABLED OR NOT FIRST.
         * 2. YOU MUST ACQUIRE THE DRIVER ( ACQUIRE is currently automatically done, i have the methods written if
         * we want to manually acquire from the server later, remember to remove auto acquire from vJoyFeeder class)
         * Get functions do not need to set a value, values included with GET token will be ignored.
         * SCALED_INPUT may only be used with the GET command, SCALED_INPUT used with SET command will be ignored.
         * STATUS and ACQUIRE do not need attributes or values, if they are included they will be ignored.
         * 
         * Valid instruction Examples:
         * e.g. GET RAW_INPUT 
         * e.g. SET RAW_INPUT 0.1,-0.45
         * e.g. GET SCALED_INPUT
        */
        public String parseInstructionString(String command_attribute_value)
        {
            string[] tokenlist = command_attribute_value.Split(' '); // split by whitespace

            //put each token into temp variable
            string command = tokenlist[0];
            string attribute = tokenlist[1];
            string value = tokenlist[2];

            //parse logic
            if (command.Equals("SET"))
            {
                if(attribute.Equals("RAW_INPUT"))
                {
                    //X must precede Y!
                    string[] coords = value.Split(',');
                    double X = double.Parse(coords[0]);
                    double Y = double.Parse(coords[1]);
                    if(X < 1.0  || Y < 1.0 || X > -1.0 || Y > -1.0)
                    {
                        return setXYcoord(X, Y);
                    }
                    else {return "BAD instruction: X or Y is out of bounds!";}
                }
                else { return "BAD instruction: RAW_INPUT ONLY!"; }
            }
            else if (command.Equals("GET"))
            {
                if(attribute.Equals("RAW_INPUT"))
                {
                    return getXYraw();
                }
                else if(attribute.Equals("SCALED_INPUT"))
                {
                    return getXYscaled();
                }
                else { return "Bad instruction: RAW_INPUT or SCALED_INPUT only!"; }
            }
            else if(command.Equals("STATUS"))
            {
                return isVJOYcontrollerEnabled();
            }
            else if(command.Equals("ACQUIRE"))
            {
                //only if we want to input this command
                //vjfeed.acquireVJOYdriver();
                //output acquire success text
                return "ACQUIRE has not been implemented yet";
            }
            else { return "Bad instruction: SET, GET, STATUS, ACQUIRE only!"; }
        }

        //Input XY raw coords, abtracted up a layer
        private string setXYcoord(double x, double y)
        {
           return vjfeed.inputRawCoords(x, y); 
        }

        //Get XY raw coords, abtracted up a layer
        private String getXYraw()
        {
            return vjfeed.retrieveRawCoords();
        }

        //Get XY scaled coords, abstracted up a layer
        private String getXYscaled()
        {
            return vjfeed.retrieveScaledCoords();
        }

        //Get status of vJoy controller, is it enabled?
        private String isVJOYcontrollerEnabled()
        {
            if (vjfeed.isVJOYenabled())
            { return "vJoy driver is enabled!"; }
            else
            { return "vJoy driver is not enabled"; }
        }

    }
}
