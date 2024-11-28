using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouchGrass
{
    internal interface InputHandlerInterface
    {
        void HandleCommand(String command);
        void Disconnect();
    }
    internal abstract class InputHandlerFactory
    {
        public static InputHandlerInterface getInstance()
        {
            //return new XBoxCommandHandler();
            return new ChromeIntegrationHandler();
        }
    }
    
}
