using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Quasar.DCPU;
using Quasar.DCPU.Exceptions;

namespace Quasar.Emulator
{
    class BinaryExecutor
    {
        private readonly Core cpu;

        public BinaryExecutor()
        {
            cpu = new Core();
        }

        public bool Load(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("input file not found: " + filename);
                return false;
            }

            byte[] image8 = File.ReadAllBytes(filename);
            ushort[] image16 = new ushort[image8.Length / 2];
            for (int i = 0; i < image16.Length; i++)
                image16[i] = BitConverter.ToUInt16(image8, i * 2);

            cpu.LoadImage(image16, 0);

            return true;
        }

        public void Execute()
        {
            try
            {
                while (true)
                {
                    cpu.Step();
                    Console.WriteLine(cpu.Dump());
                }
            }
            catch (DCPUException de)
            {
                Console.WriteLine(de.Message);
                Console.WriteLine("----------------------------------------");
                Console.WriteLine(cpu.Dump());
                return;
            }
        }
    }
}
