using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quasar.Assembler
{
    class QAS
    {
        static void Main(string[] args)
        {
            var cli = new CLIOptions();
            new CommandLine.CommandLineParser().ParseArguments(args, cli);
            var parser = new FileAssembler(new LineAssembler());

            if (cli.SourceFiles.Count == 0)
                throw new ArgumentException("no input files specified");
            
            foreach (var inputFile in cli.SourceFiles)
            {
                if (!File.Exists(inputFile))
                    throw new ArgumentException("input file not found: " + inputFile);

                var text = File.ReadLines(inputFile);
                var objectCode = parser.Assemble(text);

                var outputFile = System.IO.Path.GetFileNameWithoutExtension(inputFile) + DCPU.Spec.Extension;
            }
        }
    }
}
