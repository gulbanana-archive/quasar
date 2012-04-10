using System.Collections.Generic;

using CommandLine;

namespace Quasar.Emulator
{
    class CLIOptions
    {
        [ValueList(typeof(List<string>), MaximumElements=1)]
        public IList<string> SourceFiles = null;

        [HelpOption()]
        public string Usage()
        {
            return "dcpu <executable>";
        }
    }
}
