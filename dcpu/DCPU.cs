using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quasar.Emulator
{
    class DCPU
    {
        static void Main(string[] args)
        {
            //Read commandline
            var cli = new CLIOptions();
            new CommandLine.CommandLineParser().ParseArguments(args, cli);

            if (cli.SourceFiles.Count != 1)
            {
                //Console.WriteLine(cli.Usage());
                //return;
                cli.SourceFiles.Add("C:\\Users\\banana\\Documents\\quasar\\test\\notch1.qbx");
            }

            //Compose object dependency graph
            var emulator = new BinaryExecutor();

            //Go!!
            if (emulator.Load(cli.SourceFiles[0]))
                emulator.Execute();
        }
    }
}
