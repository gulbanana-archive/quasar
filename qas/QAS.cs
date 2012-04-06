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
                cli.SourceFiles = new string[] { "C:\\Users\\banana\\Documents\\quasar\\test\\notch-sample.s" };

            //Compose object dependency graph
            var parser = new FileAssembler(
                new AssemblyGrammar(),
                new QFXBuilder());
            
            foreach (var inputFile in cli.SourceFiles)
            {
                parser.Assemble(inputFile);
            }
        }
    }
}
