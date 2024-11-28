/////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// This project demonstrates how to write a simple vJoy feeder in C#
//
// You can compile it with either #define ROBUST OR #define EFFICIENT
// The fuctionality is similar - 
// The ROBUST section demonstrate the usage of functions that are easy and safe to use but are less efficient
// The EFFICIENT ection demonstrate the usage of functions that are more efficient
//
// Functionality:
//	The program starts with creating one joystick object. 
//	Then it petches the device id from the command-line and makes sure that it is within range
//	After testing that the driver is enabled it gets information about the driver
//	Gets information about the specified virtual device
//	This feeder uses only a few axes. It checks their existence and 
//	checks the number of buttons and POV Hat switches.
//	Then the feeder acquires the virtual device
//	Here starts and endless loop that feedes data into the virtual device
//
/////////////////////////////////////////////////////////////////////////////////////////////////////////
#define ROBUST
//#define EFFICIENT

using Nefarius.ViGEm.Client;
using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
// Don't forget to add this


namespace TouchGrass
{
    class Program
    {
        // Declaring one joystick (Device id 1) and a position structure. 
        static bool stopServer = false;
        static void Main(string[] args)
        {

            // Handle signals for graceful shutdown
            Console.CancelKeyPress += (sender, e) =>
            {
                stopServer = true;
                Console.WriteLine("Signal received, shutting down server...");
            };

            UdpServer server = new UdpServer();
            server.StartServer(stopServer);

            return;
            var client = new ViGEmClient();


            var xboxC = client.CreateXbox360Controller();

            xboxC.Connect();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            // recommended: run this in its own thread
            Thread.Sleep(5000);
            /*for (var i = 0; i < 5; i++)
             {
                 xboxC.SetButtonState(Nefarius.ViGEm.Client.Targets.Xbox360.Xbox360Button.A, true);
                 Thread.Sleep(1000);
                 xboxC.SetButtonState(Nefarius.ViGEm.Client.Targets.Xbox360.Xbox360Button.A, false);
                 Thread.Sleep(1000);
             }*/

            /*
            xboxC.SetButtonState(Nefarius.ViGEm.Client.Targets.Xbox360.Xbox360Button.A, true); //jump
            Thread.Sleep(1000);
            xboxC.SetButtonState(Nefarius.ViGEm.Client.Targets.Xbox360.Xbox360Button.A, false);
            Thread.Sleep(1000);
            xboxC.SetButtonState(Nefarius.ViGEm.Client.Targets.Xbox360.Xbox360Button.B, true); //sprint
            Thread.Sleep(1000);
            xboxC.SetButtonState(Nefarius.ViGEm.Client.Targets.Xbox360.Xbox360Button.B, false);
            Thread.Sleep(1000);
            xboxC.SetButtonState(Nefarius.ViGEm.Client.Targets.Xbox360.Xbox360Button.X, true); //flask
            Thread.Sleep(1000);
            xboxC.SetButtonState(Nefarius.ViGEm.Client.Targets.Xbox360.Xbox360Button.X, false);
            Thread.Sleep(1000);
            xboxC.SetButtonState(Nefarius.ViGEm.Client.Targets.Xbox360.Xbox360Button.Y, true); //interact
            Thread.Sleep(1000);
            xboxC.SetButtonState(Nefarius.ViGEm.Client.Targets.Xbox360.Xbox360Button.Y, false);
            Thread.Sleep(1000);
            xboxC.SetButtonState(Nefarius.ViGEm.Client.Targets.Xbox360.Xbox360Button.Right, true); // arrow right
            Thread.Sleep(1000);
            xboxC.SetButtonState(Nefarius.ViGEm.Client.Targets.Xbox360.Xbox360Button.Right, false);
            Thread.Sleep(1000);
            xboxC.SetButtonState(Nefarius.ViGEm.Client.Targets.Xbox360.Xbox360Button.Back, true); // map
            Thread.Sleep(1000);
            xboxC.SetButtonState(Nefarius.ViGEm.Client.Targets.Xbox360.Xbox360Button.Back, false);
            Thread.Sleep(1000);
            */

            Thread.Sleep(1000);
            xboxC.SetSliderValue(Nefarius.ViGEm.Client.Targets.Xbox360.Xbox360Slider.RightTrigger, 255);
            Thread.Sleep(1500);
            xboxC.SetSliderValue(Nefarius.ViGEm.Client.Targets.Xbox360.Xbox360Slider.RightTrigger, 0);
            Thread.Sleep(1000);

            //xboxC.SetButtonState(Nefarius.ViGEm.Client.Targets.Xbox360.Xbox360Button.RightShoulder, true); //attack
            Thread.Sleep(1000);
            //xboxC.SetButtonState(Nefarius.ViGEm.Client.Targets.Xbox360.Xbox360Button.RightShoulder, false);
            Thread.Sleep(1000);

            /*
            while (true)
                try
                {
                    // blocks for 250ms to not burn CPU cycles if no report is available
                    // an overload is available that blocks indefinitely until the device is disposed, your choice!
                    var buffer = ds4.AwaitRawOutputReport(250, out var timedOut);

                    if (timedOut)
                    {
                        Console.WriteLine("Timed out");
                        continue;
                    }

                    // you got a new report, parse it and do whatever you need to do :)
                    // here we simply hex-dump the contents
                    Console.WriteLine($"[OUT] {string.Join(" ", buffer.Select(b => b.ToString("X2")))}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Thread.Sleep(1000);
                }
            */
            xboxC.Disconnect();

        } // Main
    } // class Program
} // namespace FeederDemoCS



// initializes the SDK instance
