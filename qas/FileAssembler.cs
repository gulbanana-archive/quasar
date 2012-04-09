using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Irony.Parsing;
using Quasar.ABI;

namespace Quasar.Assembler
{
    class FileAssembler
    {
        private readonly IExecutableFactory executableBuilder;
        private readonly ISegmentFactory segmentBuilder;

        private readonly Grammar ironyGrammar;
        private readonly Parser ironyParser;

        public FileAssembler(Grammar grammar, ISegmentFactory segmentBuilder, IExecutableFactory executableBuilder)
        {
            this.executableBuilder = executableBuilder;
            this.segmentBuilder = segmentBuilder;
            this.ironyGrammar = grammar;
            this.ironyParser = new Parser(ironyGrammar);
        }

        /// <summary>
        /// assemble an entire file, parsing options, comments, and so on
        /// </summary>
        /// <param name="text">each line of text in the file</param>
        /// <returns>object code</returns>
        public void Assemble(string filename)
        {
            //check arguments
            if (!File.Exists(filename))
            {
                Console.WriteLine("input file not found: " + filename);
                return;
            }

            //parse the source
            var source = File.ReadAllText(filename);
            var parseTree = ironyParser.Parse(source, filename);

            //check for parse success
            if (parseTree.Status != ParseTreeStatus.Parsed)
            {
                string[] lines = Regex.Split(source, "\r\n|\r|\n");

                foreach (var message in parseTree.ParserMessages)
                {
                    string offendingLine = lines[message.Location.Line];
                    Console.WriteLine(offendingLine);
                    Console.WriteLine(new string(Enumerable.Repeat(' ', message.Location.Column).ToArray()) + "^");
                    Console.WriteLine("{0}: {1}: {2}", filename, message.Location.Line, message.Message);
                }

                return;
            }

            //build and compile an executable
            try
            {
                segmentBuilder.BuildSegments(parseTree.Root);
                var program = executableBuilder.CreateExecutable(segmentBuilder.Segments);
                var outputFile = Path.GetFileNameWithoutExtension(filename) + "." + program.FileExtension;
                var wordArray = program.Assemble(null);
                var byteArray = new byte[wordArray.Length * 2];
                Buffer.BlockCopy(wordArray, 0, byteArray, 0, byteArray.Length);
                File.WriteAllBytes(outputFile, byteArray);
            }
            catch (FormatException fe)           // error from the segment builder
            {
                Console.WriteLine(fe.Message);
            }
            catch (BadImageFormatException bife) // error from the executable builder
            {
                Console.WriteLine(bife.Message);
            }
        }

    }
}
