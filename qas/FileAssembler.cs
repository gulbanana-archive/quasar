using System.Collections.Generic;

using Quasar.DCPU;
using Quasar.ABI;
using Irony.Parsing;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace Quasar.Assembler
{
    class FileAssembler
    {
        private readonly IExecutableFactory executableBuilder;

        private readonly Grammar ironyGrammar;
        private readonly Parser ironyParser;

        public FileAssembler(Grammar grammar, IExecutableFactory executableBuilder)
        {
            this.executableBuilder = executableBuilder;
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
            if (!File.Exists(filename))
            {
                Console.WriteLine("input file not found: " + filename);
                return;
            }

            var source = File.ReadAllText(filename);
            var parseTree = ironyParser.Parse(source, filename);

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

            Console.WriteLine(parseTree.ToXml());
            return;

            var syntaxTree = new Syntax.Program(parseTree.Root);
            var segments = syntaxTree.Flatten();
            var program = executableBuilder.CreateExecutable(segments);
            var outputFile = Path.GetFileNameWithoutExtension(filename) + "." + program.FileExtension;
            File.WriteAllBytes(outputFile, program.Assemble());
        }
    }
}
