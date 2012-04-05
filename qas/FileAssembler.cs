using System.Collections.Generic;

using Quasar.DCPU;
using Quasar.ABI;

namespace Quasar.Assembler
{
    class FileAssembler
    {
        private readonly LineAssembler instructionReader;
        private readonly IExecutableFactory executableBuilder;

        public FileAssembler(LineAssembler instructionReader, IExecutableFactory executableBuilder)
        {
            this.instructionReader = instructionReader;
            this.executableBuilder = executableBuilder;
        }

        /// <summary>
        /// assemble an entire file, parsing options, comments, and so on
        /// </summary>
        /// <param name="text">each line of text in the file</param>
        /// <returns>object code</returns>
        public IExecutable Parse(IEnumerable<string> text)
        {
            List<IInstruction> code = new List<IInstruction>();

            foreach (var line in text)
            {
                IInstruction instr = instructionReader.Parse(line);
                code.Add(instr);
            }

            var csect = new CodeSection(code);
            return executableBuilder.CreateExecutable(csect);
        }
    }
}
