using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Quasar.DCPU.Exceptions;
using Quasar.DCPU.Values;

namespace Quasar.DCPU
{
    public class Core
    {
        readonly ushort[] ram;
        readonly ushort[] registers;
        ushort PC, SP, O;

        int delay;

        public Core()
        {
            ram = new ushort[10000];
            registers = new ushort[8];
            PC = O = 0x0000;
            SP = 0xffff;
            delay = 0;
        }

        public void LoadImage(ushort[] image, ushort offset)
        {
            Array.Copy(image, 0, ram, offset, image.Length);
        }

        public void Step()
        {
            //Count down for instructions still in flight
            if (delay > 0)
            {
                --delay;
                return;
            }

            //Retrieve new instruction word
            var instr = ram[PC++];
            var opcode = (Opcode)(instr & 0x000F);

            var a = (Value)(instr & 1008 >> 4);   //0000 0011 1111 0000
            var b = (Value)(instr & 64512 >> 10); //1111 1100 0000 0000

            if (opcode == Opcode.Nonbasic)
            {
                var extop = (ExtendedOpcode)a;
                var arg = ValueAccessor(b);
                throw new DCPUException("nonbasic opcodes not implemented");
            }
            else if (opcode <= Opcode.ExclusiveOr)
            {
                var dest = ValueMutator(a);
                var src = ValueAccessor(b);
                switch (opcode)
                {
                    case Opcode.Set:
                        dest(src);
                        break;

                    default:
                        throw new DCPUException(string.Format("0x{1:X}: unrecognised operation code 0x{0:X}", opcode, PC));
                }
            }
            else
            {
                var srcB = ValueAccessor(a);
                var srcA = ValueAccessor(b);
                switch (opcode)
                {
                    case Opcode.IfEqual:
                    case Opcode.IfNotEqual:
                    case Opcode.IfGreater:
                    case Opcode.IfBitwise:

                    default:
                        throw new DCPUException(string.Format("0x{1:X}: unrecognised operation code 0x{0:X}", opcode, PC));
                }
            }
            
        }

        private ushort ValueAccessor(Value bitPattern)
        {
            if (bitPattern >= Value.RegisterLiteral && bitPattern < Value.RegisterAddress)
                switch ((Register)bitPattern)
                {
                    case Register.PC: return PC;
                    case Register.O: return O;
                    case Register.SP: return SP;
                    default:
                        return registers[bitPattern - Value.RegisterLiteral];
                }

            else if (bitPattern >= Value.RegisterAddress && bitPattern < Value.RegDispAddress)
                return ram[registers[bitPattern - Value.RegisterAddress]];

            else if (bitPattern >= Value.RegDispAddress && bitPattern < Value.Pop)
            {
                ++delay;
                var disp = ram[PC++];
                return ram[registers[bitPattern - Value.RegDispAddress] + disp];
            }

            else if (bitPattern == Value.Pop)
                return ram[SP++];

            else if (bitPattern == Value.Peek)
                return ram[SP];

            else if (bitPattern == Value.Push)
                return ram[--SP];

            else if (bitPattern == Value.StackPointer)
                return SP;

            else if (bitPattern == Value.ProgramCounter)
                return PC;

            else if (bitPattern == Value.Overflow)
                return O;

            else if (bitPattern == Value.NextWordAddress)
            {
                ++delay;
                return ram[ram[PC++]];
            }

            else if (bitPattern == Value.NextWordLiteral)
            {
                ++delay;
                return ram[PC++];
            }

            else
            {
                return bitPattern - Value.DirectLiteral;
            }
        }

        private Action<ushort> ValueMutator(Value bitPattern)
        {
            if (bitPattern >= Value.RegisterLiteral && bitPattern < Value.RegisterAddress)
                switch ((Register)bitPattern)
                {
                    case Register.PC: return x => PC = x;
                    case Register.O: return x => O = x;
                    case Register.SP: return x => SP = x;
                    default:
                        return x => registers[bitPattern - Value.RegisterLiteral] = x;
                }

            else if (bitPattern >= Value.RegisterAddress && bitPattern < Value.RegDispAddress)
                return x => ram[registers[bitPattern - Value.RegisterAddress]] = x;

            else if (bitPattern >= Value.RegDispAddress && bitPattern < Value.Pop)
            {
                ++delay;
                var disp = ram[++PC];
                return x => ram[registers[bitPattern - Value.RegDispAddress] + disp] = x;
            }

            else if (bitPattern == Value.Pop)
                return x => ram[SP++] = x;

            else if (bitPattern == Value.Peek)
                return x => ram[SP] = x;

            else if (bitPattern == Value.Push)
                return x => ram[--SP] = x;

            else if (bitPattern == Value.StackPointer)
                return x => SP = x;

            else if (bitPattern == Value.ProgramCounter)
                return x => PC = x;

            else if (bitPattern == Value.Overflow)
                return x => O = x;

            else if (bitPattern == Value.NextWordAddress)
            {
                ++delay;
                return x => ram[ram[++PC]] = x;
            }

            else if (bitPattern == Value.NextWordLiteral)
            {
                ++delay;
                return x => ram[++PC] = x;
            }

            else
            {
                return x => { ;};
            }
        }

        public string Dump()
        {
            var statusReport = new StringBuilder();
            statusReport.AppendLine("A    B    C    X    Y    Z    I    J");
            statusReport.AppendFormat("{0:X4} {1:X4} {2:X4} {3:X4} {4:X4} {5:X4} {6:X4} {7:X4}\n", registers[0], registers[1], registers[2], registers[3], registers[4], registers[5], registers[6], registers[7]);
            statusReport.AppendLine("PC   O    SP");
            statusReport.AppendFormat("{0:X4} {1:X4} {2:X4}\n", PC, O, SP);

            return statusReport.ToString();
        }
    }
}
