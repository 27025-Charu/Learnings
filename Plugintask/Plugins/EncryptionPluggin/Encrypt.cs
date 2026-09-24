using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginDLLS;

namespace EncryptionPlugin
{
    public class Encrypt : IPlugin
    {
        public string Name => "Encryption";

        public void Execute()
        {
            Console.WriteLine("Executing the Encryption");
        }
    }
}
