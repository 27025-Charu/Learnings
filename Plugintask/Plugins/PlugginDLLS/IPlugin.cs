using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginDLLS
{
    public interface IPlugin
    {
        public string Name { get;}
        public void Execute();
    }
}
