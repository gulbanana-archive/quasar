﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quasar.DCPU
{
    public interface IValue : IAssemblable
    {
        ushort DirectAssemble();
    }
}
