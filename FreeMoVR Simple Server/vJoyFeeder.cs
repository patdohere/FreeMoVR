using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using vJoyInterfaceWrap;

namespace FreeMoVR_Simple_Server
{
    public class vJoyFeeder
    {
        static private vJoyInterfaceWrap.vJoy joystick;
        static private uint id = 1;
        static private double xRaw;
        static private double yRaw;

        //feeder application constructor, setup joystick instance and acquire the joystick driver installed on machine.
        public vJoyFeeder()
        {  
            joystick = new vJoyInterfaceWrap.vJoy();
         
            // if there is a missing .dll error here, make sure both vJoyInterface.dll and vJoyInterfaceWrap.dll is added to the project and also added 
            // in the same folder as the debug or release .exe, often times only the vJoyInterfaceWrap.dll is there.
            joystick.AcquireVJD(id); // this is the code that actually acquires the driver
            inputRawCoords(0.0, 0.0);// set default coordinates upon construction
        }


        /* Instructions for parsing
         * Instruction string will be parsed as COMMAND / ATTRIBUTE / VALUE, with each token separated by whitespace
         * Current valid COMMANDS:
         * SET (input x y coords to vJoy) | GET (retrieve current x y coords from vJoy) | STATUS (is joystick active?) 
         * ACQUIRE (get the vjoy driver (id is hardcoded to 1) | RELEASE (release the vjoy driver)
         * 
         * Current valid ATTRIBUTES:
         * RAW_INPUT (x y coords without conversion)| SCALED_INPUT(x y coords with conversion)  | BUTTON_PRESS (simulates a button press
         * 
         * Current valid VALUES (both x and y values must be inputted together):
         * X_Y (The format must be X,Y) | N (Number below 10)
         * 
         * IMPORTANT!!!
         * 1. YOU MUST DETERMINE IF DRIVER IS ENABLED OR NOT FIRST.
         * 2. YOU MUST ACQUIRE THE DRIVER ( ACQUIRE is currently automatically done, i have the methods written if
         * we want to manually acquire from the server later, remember to remove auto acquire from vJoyFeeder class)
         * 3. BUTTON_PRESS functionality has been removed, it will not be used with sensortag input. Instead, set functionality to keyboard instead
         * if testing is desired.
         * 4. Get functions do not need to set a value, values included with GET token will be ignored.
         * 5. SCALED_INPUT may only be used with the GET command, SCALED_INPUT used with SET command will be ignored.
         * 6. STATUS, ACQUIRE, RLEASE do not need attributes or values, if they are included they will be ignored.
         * 7. ACCQUIRE/RELEASE are not actually called because acquire is called automatically when class is constructed. If joystick driver
         * is released, there is no way of acquiring it again unless the entire vJoyFeeder instance is created again. However, ACQUIRE/RELEASE
         * functionality can be implemented if desired.
         * 
         * I did my best to make the parser as robust as possible, but there may still be errors!
         * 
         * Valid instruction Examples:
         * e.g. GET RAW_INPUT 
         * e.g. SET RAW_INPUT 0.1,-0.45
         * e.g. GET SCALED_INPUT
         * e.g. STATUS
        */
        public String parseInstructionString(String command_attribute_value)
        {
            //Console.WriteLine("INStRUCTION HAS BEEN ENTERED");
            //Console.WriteLine("FROM INSIDE PARSE " + command_attribute_value);

            // The "formatted" gets rid of any new lines and linebreaks that may be inserted when text is inputted from websocket
            string formatted = Regex.Replace(command_attribute_value, @"\r\n?|\n", ""); 
            string[] tokenlist = formatted.Split(' '); // split by whitespace

            //put each token into temp variable
            
            
            string command = tokenlist[0];
            string attribute;
            string value;

           // Console.WriteLine("current command: " + command);
            //parse logic

            if (command.Equals("SET"))
            {
                
               
               try { attribute = tokenlist[1]; }
               catch(IndexOutOfRangeException e)
               {
                   return "ERROR: You didn't set an attribute!";
               }
                if (attribute.Equals("RAW_INPUT"))
                {
                   // Console.WriteLine("Made it into raw input\n");
                    //X must precede Y!

                    try { value = tokenlist[2]; ; }
                    catch (IndexOutOfRangeException e)
                    {
                        
                        return "ERROR: You didn't set the values!";
                    }
                   
                    string[] coords = value.Split(',');

                    double X, Y;
                    try
                    {
                        X = double.Parse(coords[0]);
                        Y = double.Parse(coords[1]);
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        return "ERROR: You didn't set values properly!";
                    }
                    
                    if (X <= 1.0 && Y <= 1.0 && X >= -1.0 && Y >= -1.0)
                    {   
                        return inputRawCoords(X,Y);
                    }
                    else { return "BAD instruction: X or Y is out of bounds!"; }
                }
                //else if(attribute.Equals("BUTTON_PRESS"))
                //{
                //    Console.WriteLine("Made it into button press\n");
                //    value = tokenlist[2];
                //    int n = int.Parse(value);
                //    return pushButton((uint)n);
                //}
                else { return "BAD instruction: RAW_INPUT ONLY!"; }
            }
            else if (command.Equals("GET"))
            {
                try { attribute = tokenlist[1]; }
                catch (IndexOutOfRangeException e)
                {
                    return "ERROR: You didn't set an attribute!";
                }

                attribute = tokenlist[1];
               // Console.WriteLine("made it into GET");
                if (attribute.Equals("RAW_INPUT"))
                {
                    return retrieveRawCoords();
                }
                else if (attribute.Equals("SCALED_INPUT"))
                {
                    return retrieveScaledCoords();
                }
                else { return "BAD instruction: RAW_INPUT or SCALED_INPUT only!"; }
            }
            else if (command.Equals("STATUS"))
            {
                return isVJOYenabled();
            }
            else if (command.Equals("ACQUIRE"))
            {
                //only if we want to input this command
                //acquireVJOYdriver();
                //output acquire success text
                
                return "ACQUIRE Command is automatically done";
            }
            else if (command.Equals("RELEASE"))
            {
                //releaseJoystick();
                return "RELEASE Command is not available, since ACQUIRE is automatically done. Just close the application and re-run.";
            }
            
            else { return "BAD instruction: SET, GET, STATUS, ACQUIRE, RELEASE only!"; }
        }

        //Returns whether the driver is enabled
        private String isVJOYenabled()
        {
            if (joystick.vJoyEnabled())
            { return "vJoy driver is enabled!"; }
            else
            { return "vJoy driver is not enabled"; }
        }

        //Acquire vJoy driver
        private bool acquireVJOYdriver()
        {
            joystick.AcquireVJD(id);
            return true;
        }

        //retrieve driver info
        private string displayJoyDriverInfo()
        {
            String a = joystick.GetvJoyManufacturerString();
            String b = joystick.GetvJoyProductString();
            String c = joystick.GetvJoySerialNumberString();

            return a + " | " + b + " | " + c;
        }

        //set joystick axis, private class to be called
        private bool convertAndSetJoystickAxis()
        {
            //no motion in joystick is X = 16383, Y = 16383
            // y  = 16384x + 16384, calculation is done in this method
            int xScaled = (int)Math.Floor(16384 * xRaw) + 16384;
            int yScaled = (int)Math.Floor(16384 * yRaw) + 16384;
            //Console.WriteLine("raw value in convert function X: " + xRaw + " Y: " + yRaw);
            joystick.SetAxis(xScaled, id, HID_USAGES.HID_USAGE_X);
            joystick.SetAxis(yScaled, id, HID_USAGES.HID_USAGE_Y);
            // return "Values set as X: " + xScaled + " Y: " + yScaled;
            return true;
        }

        //input joystick axis, call conversion and set function
        private string inputRawCoords(double inputX, double inputY)
        {
            xRaw = inputX;
            yRaw = inputY;
            //Console.WriteLine("raw value in input function X: " + xRaw + " Y: " + yRaw);
            convertAndSetJoystickAxis();
            return "Raw coords inputted X: " + xRaw + " Y: " + yRaw;
        }

        //retrieve raw coords, used for debugging.
        private string retrieveRawCoords()
        {
            return "xRaw: " + xRaw + " yRaw: " + yRaw;
        }

        //retrieve scaled coords, used for debugging.
        private string retrieveScaledCoords()
        {
            int xScaled = (int)Math.Floor(16384 * xRaw) + 16384;
            int yScaled = (int)Math.Floor(16384 * yRaw) + 16384;
            return "xScaled: " + xScaled + " yScaled: " + yScaled;
        }

        //used for button pushes
        private string pushButton(uint n)
        {
            joystick.SetBtn(true, id, n);
            joystick.SetBtn(false, id, n); //release button right after it is pushed
            return "Button " + n + " has been pushed!";
        }

        //function may or may not be used
        private string releaseButton(uint n)
        {
            joystick.SetBtn(false, id, n);
            return "Button " + n + " has been released!";
        }

        //Release joystick
        private string releaseJoystick()
        {
            joystick.RelinquishVJD(id);
            return "Joystick has been released";
        }

    }
}
