using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Irony.Parsing;

using Quasar.DCPU;
using Quasar.DCPU.Values;

namespace Quasar.Assembler
{
    class ValueBuilder : IValueBuilder
    {
        public IValue BuildValue(ParseTreeNode visitee)
        {
            switch (visitee.Term.Name)
            {
                //many rules just structure the syntax and have no semantic meaning
                case "value":
                case "register":
                    return BuildValue(visitee.ChildNodes[0]);

                case "literal":
                    return new LiteralValue((ushort)(int)visitee.Token.Value);

                case "A":
                case "B":
                case "C":
                case "X":
                case "Y":
                case "Z":
                case "I":
                case "J":
                    return new RegisterValue(visitee.Token.Text);

                //A parse element we don't know about - this should not happen!
                default:
                    string nodeInfo = string.Format(
                        "Unknown node <{0}{1}>",
                        visitee.Term.Name,
                        visitee.Token != null ?
                            string.Format(" token=\"{0}\"", visitee.Token.Text) :
                            "");
                    //throw new FormatException(nodeInfo);
                    Console.WriteLine(nodeInfo);

                    foreach (var node in visitee.ChildNodes)
                        BuildValue(node);

                    return null;
                    break;
            }
        }
    }
}
