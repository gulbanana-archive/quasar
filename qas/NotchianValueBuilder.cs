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
    class NotchianValueBuilder : IValueBuilder
    {
        public IValue BuildValue(ParseTreeNode visitee)
        {
            switch (visitee.Term.Name)
            {
                //many rules just structure the syntax and have no semantic meaning
                case "value":
                case "register":
                case "synonym":
                case "address":
                    return BuildValue(visitee.ChildNodes[0]);

                //values in the next word or encoded directly
                case "literal":
                    return new LiteralValue((ushort)(int)visitee.Token.Value);

                //values which refer directly to a register
                case "A":
                case "B":
                case "C":
                case "X":
                case "Y":
                case "Z":
                case "I":
                case "J":
                case "SP":
                case "PC":
                case "O":
                    return new RegisterValue(visitee.Token.Text);

                //values which refer to the stack
                case "PUSH":
                case "POP":
                case "PEEK":
                    return new SynonymValue(visitee.Token.Text);

                //values which refer to labels
                case "symbol":
                    return new UnresolvedSymbolValue(visitee.Token.Text);

                //values which refer to memory
                case "pointer":
                    return BuildAddress(visitee.ChildNodes[1]);

                //A parse element we don't know about - this should not happen!
                default:
                    string nodeInfo = string.Format(
                        "Unknown value node <{0}{1}>",
                        visitee.Term.Name,
                        visitee.Token != null ?
                            string.Format(" token=\"{0}\"", visitee.Token.Text) :
                            "");
                    throw new FormatException(nodeInfo);
            }
        }

        private IValue BuildAddress(ParseTreeNode visitee)
        {
            switch (visitee.Term.Name)
            {
                //many rules just structure the syntax and have no semantic meaning
                case "target":
                case "register":
                    return BuildAddress(visitee.ChildNodes[0]);

                //these rules build special "pointer" versions of the value nodes with different value encoding
                case "literal":
                    return new LiteralPointer((ushort)(int)visitee.Token.Value);

                case "A":
                case "B":
                case "C":
                case "X":
                case "Y":
                case "Z":
                case "I":
                case "J":
                    return new RegisterPointer(visitee.Token.Text, null);
                
                case "indirection":
                    var left = visitee.ChildNodes[0];
                    var right = visitee.ChildNodes[2];

                    if (left.Term.Name == "offset")
                        return new RegisterPointer(right.ChildNodes[0].Token.Text, BuildValue(left.ChildNodes[0]));
                    else
                        return new RegisterPointer(left.ChildNodes[0].Token.Text, BuildValue(right.ChildNodes[0]));

                //A parse element we don't know about - this should not happen!
                default:
                    string nodeInfo = string.Format(
                        "Unknown address node <{0}{1}>",
                        visitee.Term.Name,
                        visitee.Token != null ?
                            string.Format(" token=\"{0}\"", visitee.Token.Text) :
                            "");
                    throw new FormatException(nodeInfo);
            }
        }
    }
}
