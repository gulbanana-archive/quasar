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
                cli.SourceFiles = new string[] { "C:\\Users\\banana\\Documents\\quasar\\test\\basic-instructions.s" };

            //Compose object dependency graph
            var parser = new FileAssembler(
                new NotchianGrammar(), 
                new NotchianSegmentBuilder(
                    new ValueBuilder()), 
                new QFXBuilder());
            
            //Go!!
            foreach (var inputFile in cli.SourceFiles)
                parser.Assemble(inputFile);
        }
    }
}
