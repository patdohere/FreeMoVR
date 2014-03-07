using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vJoyInterfaceWrap;

namespace FreeMoVR_Server
{
    class vJoyFeeder
    {
        static public vJoyInterfaceWrap.vJoy joystick;
        static public uint id;
        static public double xRaw;
        static public double yRaw;
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

        private string displayJoyDriverInfo()
        {
            String a = joystick.GetvJoyManufacturerString();
            String b = joystick.GetvJoyProductString();
            String c = joystick.GetvJoySerialNumberString();

            return a + " | " + b + " | " + c;
        }


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

        public string inputRawCoords(double inputX, double inputY)
        {
            xRaw = inputX;
            yRaw = inputY;
            setConvertedJoystickAxis();
            return "Raw coords inputted X: " + xRaw + " Y: " + yRaw;
        }

        public string retrieveRawCoords()
        {
            return "xRaw: " + xRaw + " yRaw: " + yRaw;
        }

        public string retrieveScaledCoords()
        {
            int xScaled = (int)Math.Floor(16384 * xRaw) + 16384;
            int yScaled = (int)Math.Floor(16384 * yRaw) + 16384;
            return "xScaled: " + xScaled + " yScaled " + yScaled;
        }

    }
}
