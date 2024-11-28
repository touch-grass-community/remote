using Nefarius.ViGEm.Client.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace TouchGrass
{
    internal class ChromeIntegrationHandler : InputHandlerInterface
    {
        readonly IXbox360Controller Controller;

        public ChromeIntegrationHandler()
        {
            
        }

        public void Disconnect()
        {
        }

        public void HandleCommand(String command)
        {
            List<int> Args = command.Split(':').Select(int.Parse).ToList();
            Commands cmd = (Commands)Args[0];

            if (!Enum.IsDefined(typeof(Commands), cmd))
            {
                Console.WriteLine($"{cmd} is not a defined enum value.");
                return;
            }

            switch (cmd)
            {
                
                case Commands.A:
                    SendKeys.SendWait("{SUBTRACT}"); // 10 sec backwards
                    break;

                case Commands.B:
                    SendKeys.SendWait("{ADD}"); // 10 sec forward
                    break;
                    
                case Commands.X:
                    SendKeys.SendWait("q"); // skip intro
                    break;

                case Commands.Y: // next episode
                    SendKeys.SendWait("w");
                    Thread.Sleep(1500);
                    SendKeys.SendWait("e");
                    break;

                case Commands.START: // pause/play
                    SendKeys.SendWait("e");
                    break;

                case Commands.UP: // volume up
                    SendKeys.SendWait("a");
                    break;

                case Commands.DOWN: // volume down
                    SendKeys.SendWait("s");
                    break;

                default:
                    Console.WriteLine("Something went wrong: " + cmd);
                    break;
                    
            }
        }

    }
}
