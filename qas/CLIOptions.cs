using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommandLine;

namespace Quasar.Assembler
{
    class CLIOptions
    {
        [ValueList(typeof(List<string>))]
        public IList<string> SourceFiles = null;

        [HelpOption()]
        public string Usage()
        {
            return "qas file...";
        }
    }
}
