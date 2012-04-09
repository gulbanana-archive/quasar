using System;
using Quasar.ABI;

namespace Quasar.Assembler
{
    class QAS
    {
        static void Main(string[] args)
        {
            //Read commandline
            var cli = new CLIOptions();
            new CommandLine.CommandLineParser().ParseArguments(args, cli);

            if (cli.SourceFiles.Count == 0)
            {
                Console.WriteLine("no input files specified");
                return;
            }

            //Compose object dependency graph
            var parser = new FileAssembler(
                new NotchianGrammar(), 
                new NotchianSegmentBuilder(
                    new NotchianValueBuilder()), 
                new QBXBuilder());
            
            //Go!!
            foreach (var inputFile in cli.SourceFiles)
                parser.Assemble(inputFile);
        }
    }
}
