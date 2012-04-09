using System.Collections.Generic;

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
