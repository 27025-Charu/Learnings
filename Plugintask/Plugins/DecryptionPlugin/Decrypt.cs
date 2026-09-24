using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginDLLS;

namespace DecryptionPlugin
{
    public class Decrypt : IPlugin
    {
        public string Name => "Decryption";

        public void Execute()
        {
            Console.WriteLine("Executing the decryption");
        }
    }
}
