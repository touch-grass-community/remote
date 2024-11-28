using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Targets.Xbox360;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TouchGrass
{
    internal class XBoxCommandHandler : InputHandlerInterface
    {
        readonly IXbox360Controller Controller;

        public XBoxCommandHandler() {
            var client = new ViGEmClient();
            Controller = client.CreateXbox360Controller();

            Controller.Connect();
        }

        public void Disconnect()
        {
            Controller.Disconnect();
        }

        public void HandleCommand(String command)
        {
            List<int> Args = command.Split(':').Select(int.Parse).ToList();
            Commands cmd = (Commands) Args[0];

            if (!Enum.IsDefined(typeof(Commands), cmd))
            {
                Console.WriteLine($"{cmd} is not a defined enum value.");
                return;
            }
            switch (cmd)
            {
                case Commands.UP:
                    PressButton(Xbox360Button.Up);
                    break;

                case Commands.LEFT:
                    PressButton(Xbox360Button.Left);
                    break;

                case Commands.RIGHT:
                    PressButton(Xbox360Button.Right);
                    break;

                case Commands.DOWN:
                    PressButton(Xbox360Button.Down);
                    break;

                case Commands.A:
                    PressButton(Xbox360Button.A);
                    PressButton(Xbox360Slider.RightTrigger);
                    break;

                case Commands.B:
                    PressButton(Xbox360Button.B);
                    break;

                case Commands.X:
                    PressButton(Xbox360Button.X);
                    break;

                case Commands.Y:
                    PressButton(Xbox360Button.Y);
                    break;

                case Commands.LPAD:
                    PressButton(Xbox360Button.LeftThumb);
                    break;

                case Commands.RPAD:
                    PressButton(Xbox360Button.RightThumb);
                    break;

                case Commands.L1:
                    PressButton(Xbox360Button.LeftShoulder);
                    break;

                case Commands.L2:
                    PressButton(Xbox360Slider.LeftTrigger);
                    break;

                case Commands.R1:
                    PressButton(Xbox360Button.RightShoulder);
                    break;

                case Commands.R2:
                    PressButton(Xbox360Slider.RightTrigger);
                    break;

                case Commands.SELECT:
                    PressButton(Xbox360Button.Back);
                    break;

                case Commands.START:
                    PressButton(Xbox360Button.Start);
                    break;

                case Commands.LSTICK:
                    MoveStick(Xbox360Axis.LeftThumbX, Xbox360Axis.LeftThumbY, Args[1], Args[2], 1.0);
                    break;

                case Commands.RSTICK:
                    MoveStick(Xbox360Axis.RightThumbX, Xbox360Axis.RightThumbY, Args[1], Args[2], 0.5);
                    break;

                default:
                    Console.WriteLine("Something went wrong: " + cmd);
                    break;
            }
        }

        void PressButton(Xbox360Button Button)
        {
            Controller.SetButtonState(Button, true);
            Thread.Sleep(200);
            Controller.SetButtonState(Button, false);
        }

        void PressButton(Xbox360Slider Button)
        {
            Controller.SetSliderValue(Button, 255);
            Thread.Sleep(200);
            Controller.SetSliderValue(Button, 0);
        }

        void MoveStick(Xbox360Axis X, Xbox360Axis Y, int DirectionInt, int Intensity, double sensitivityCoeff)
        {
            CommandDirection Direction = (CommandDirection) DirectionInt;
            double IntensityCoeff = Intensity / 4.0;
           
            if (!Enum.IsDefined(typeof(CommandDirection), Direction))
            {
                Console.WriteLine($"{Direction} is not a defined enum value.");
                return;
            }
            (short ValueX, short ValueY) = GetAxisValues(Direction);
         
            Controller.SetAxisValue(X, ((short)(ValueX * IntensityCoeff * sensitivityCoeff)));
            Controller.SetAxisValue(Y, ((short)(ValueY * IntensityCoeff * sensitivityCoeff)));
        }

        (short, short) GetAxisValues(CommandDirection Direction)
        {
            switch (Direction)
            {     
                case CommandDirection.UP:
                    return (0, short.MaxValue);
                case CommandDirection.UPRIGHT:
                    return (short.MaxValue, short.MaxValue);
                case CommandDirection.RIGHT:
                    return (short.MaxValue, 0);
                case CommandDirection.DOWNRIGHT:
                    return (short.MaxValue, short.MinValue);
                case CommandDirection.DOWN:
                    return (0, short.MinValue);
                case CommandDirection.DOWNLEFT:
                    return (short.MinValue, short.MinValue);
                case CommandDirection.LEFT:
                    return (short.MinValue, 0);
                case CommandDirection.UPLEFT:
                    return (short.MinValue, short.MaxValue);
            }
            return (0, 0);
        }
    }
}
