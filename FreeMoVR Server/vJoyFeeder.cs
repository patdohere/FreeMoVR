using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vJoyInterfaceWrap;

namespace FreeMoVR_Server
{
    public class vJoyFeeder
    {
        static public vJoyInterfaceWrap.vJoy joystick;
        static public uint id;
        static public double xRaw;
        static public double yRaw;

        //feeder application constructor, setup joystick instance and acquire the joystick driver installed on machine.
        public vJoyFeeder()
        {
            // Console.WriteLine(displayJoyDriverInfo());
            joystick = new vJoyInterfaceWrap.vJoy();
            id = 1;
            //Console.WriteLine("vJoy instance created, id set as " + id);

            //if(joystick.vJoyEnabled())
            //{
            //    Console.WriteLine("vJoy driver is enabled");
            //}
            //else
            //{
            //    console.writeline("vjoy driver is not enabled");
            //}
            
            joystick.AcquireVJD(id); // this is the code that actually acquires the driver
            //Console.WriteLine("vJoy instance id = " + id + " has acquired driver");
            //Console.WriteLine("vJoy input is live!");
        }

        //Returns whether the driver is enabled
        public bool isVJOYenabled()
        {
            return joystick.vJoyEnabled();
        }

        //Acquire vJoy driver
        public bool acquireVJOYdriver()
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
        private bool setConvertedJoystickAxis()
        {
            //no motion in joystick is X = 16383, Y = 16383
            // y  = 16384x + 16384, calculation is done in this method
            int xScaled = (int)Math.Floor(16384 * xRaw) + 16384;
            int yScaled = (int)Math.Floor(16384 * yRaw) + 16384;
            joystick.SetAxis(xScaled, id, HID_USAGES.HID_USAGE_X);
            joystick.SetAxis(yScaled, id, HID_USAGES.HID_USAGE_Y);
            // return "Values set as X: " + xScaled + " Y: " + yScaled;
            return true;
        }

        //input joystick axis, call conversion and set function
        public string inputRawCoords(double inputX, double inputY)
        {
            xRaw = inputX;
            yRaw = inputY;
            setConvertedJoystickAxis();
            return "Raw coords inputted X: " + xRaw + " Y: " + yRaw;
        }

        //retrieve raw coords, used for debugging.
        public string retrieveRawCoords()
        {
            return "xRaw: " + xRaw + " yRaw: " + yRaw;
        }

        //retrieve scaled coords, used for debugging.
        public string retrieveScaledCoords()
        {
            int xScaled = (int)Math.Floor(16384 * xRaw) + 16384;
            int yScaled = (int)Math.Floor(16384 * yRaw) + 16384;
            return "xScaled: " + xScaled + " yScaled " + yScaled;
        }

    }
}
