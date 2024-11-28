using System;
using System.Runtime.InteropServices;




namespace TouchGrass
{
    class KeyboardHandler : InputHandlerInterface
    {
        public void HandleCommand(string command)
        {
            // Map incoming commands to key presses
            switch (command)
            {
                case "up":
                    HoldKey(Keys.W);
                    ReleaseKeys(new[] { Keys.A, Keys.S, Keys.D });
                    break;
                case "left":
                    HoldKey(Keys.A);
                    ReleaseKeys(new[] { Keys.W, Keys.S, Keys.D });
                    break;
                case "right":
                    HoldKey(Keys.D);
                    ReleaseKeys(new[] { Keys.W, Keys.A, Keys.S });
                    break;
                case "down":
                    HoldKey(Keys.S);
                    ReleaseKeys(new[] { Keys.W, Keys.A, Keys.D });
                    break;
                case "idle":
                    ReleaseKeys(new[] { Keys.W, Keys.A, Keys.S, Keys.D });
                    break;
                    // Add other cases as needed...
            }
        }

        public void Disconnect()
        {
          
        }

        private void HoldKey(Keys key)
        {
            // Simulate key press using P/Invoke
            Console.WriteLine($"Holding key {key}");
        }

        private void ReleaseKeys(Keys[] keys)
        {
            foreach (var key in keys)
            {
                Console.WriteLine($"Releasing key {key}");
            }
        }
    }

    enum Keys
    {
        W = 0x11,
        A = 0x1E,
        S = 0x1F,
        D = 0x20
        // Add other key mappings here
    }
}
