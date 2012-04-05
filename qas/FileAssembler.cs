using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Quasar.DCPU;

namespace Quasar.Assembler
{
    class FileAssembler
    {
        private readonly LineAssembler instructionReader;

        public FileAssembler(LineAssembler instructionReader)
        {
            this.instructionReader = instructionReader;
        }

        /// <summary>
        /// assemble an entire file, parsing options, comments, and so on
        /// </summary>
        /// <param name="text">each line of text in the file</param>
        /// <returns>object code</returns>
        public byte[] Assemble(IEnumerable<string> text)
        {
            List<IInstruction> code = new List<IInstruction>();

            foreach (var line in text)
            {
                IInstruction instr = instructionReader.Assemble(line);
                code.Add(instr);
            }

            Program obj = new Program(code);
            return obj.GetObjectCode();
        }
    }
}
