using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Quasar.DCPU;

namespace Quasar.ABI
{
    /// <summary>
    /// A generic segment of machine code which can contain instructions and data
    /// </summary>
    public class BasicSegment : ISegment
    {
        private readonly List<IInstruction> instructions;
        private readonly List<Label> absoluteLabels;

        public BasicSegment(IEnumerable<IInstruction> text, IEnumerable<Label> references)
        {
            instructions = text.ToList();
            absoluteLabels = references.ToList();
        }

        public IList<Label> Labels
        {
            get
            {
                return absoluteLabels.AsReadOnly();
            }
        }

        public IList<IInstruction> Instructions
        {
            get
            {
                return instructions.AsReadOnly();
            }
        }

        public ushort AssembledLength
        {
            get { throw new NotImplementedException(); }
        }

        public ushort[] Assemble(AssemblyContext ctx)
        {
            var image = new ushort[instructions.AssembledLength()];
            ushort memoryIndex = 0;

            foreach (var instruction in instructions)
            {
                Buffer.BlockCopy(instruction.Assemble(ctx), 0, image, memoryIndex * 2, instruction.AssembledLength * 2);
                memoryIndex += instruction.AssembledLength;
            }

            return image;
        }

        public ushort Base { get; set; }
    }
}
