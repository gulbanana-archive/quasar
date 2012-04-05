using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                throw new ArgumentException("no input files specified");

            //Compose object dependency graph
            var parser = new FileAssembler(
                new LineAssembler(),
                new QFXBuilder());
            
            foreach (var inputFile in cli.SourceFiles)
            {
                if (!File.Exists(inputFile))
                    throw new ArgumentException("input file not found: " + inputFile);

                var text = File.ReadLines(inputFile);
                var program = parser.Parse(text);

                var outputFile = System.IO.Path.GetFileNameWithoutExtension(inputFile) + "." + program.FileExtension;
                File.WriteAllBytes(outputFile, program.Assemble());
            }
        }
    }
}
