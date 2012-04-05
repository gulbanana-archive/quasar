﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quasar.ABI
{
    /// <summary>
    /// QFX format program, fully bound, with all strong refs resolved
    /// </summary>
    class FreestandingExecutable : IExecutable
    {
        /// <summary>executable code, located at address 0</summary>
        private readonly CodeSection text;
        /// <summary>non-executable data, located between code and heap</summary>
        private readonly DataSection globals;

        public FreestandingExecutable(CodeSection csect, DataSection dsect)
        {
            text = csect;
            globals = dsect;
        }

        public byte[] Assemble()
        {
            return text.Assemble()
                .Concat(globals.Assemble())
                .ToArray();
        }

        public string FileExtension
        {
            get { return "qfx"; }
        }
    }
}