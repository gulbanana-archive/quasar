using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Irony.Parsing;
using Quasar.ABI;
using Quasar.DCPU;
using Quasar.DCPU.Instructions;

namespace Quasar.Assembler
{
    class NotchianSegmentBuilder : ISegmentFactory
    {
        private readonly IValueBuilder valueBuilder;

        private SegmentType context;
        private Stack<string> symbolicLabels;
        private Queue<Label> relativeLabels;
        private Queue<IInstruction> instructions;
        private List<ISegment> segments;

        public NotchianSegmentBuilder(IValueBuilder valueParser)
        {
            valueBuilder = valueParser;

            symbolicLabels = new Stack<string>();
            relativeLabels = new Queue<Label>();
            instructions = new Queue<IInstruction>();
            segments = new List<ISegment>();
        }

        public void BuildSegments(ParseTreeNode node)
        {
            switch (node.Term.Name)
            {
                //a program is divided into segments separated by directives
                case "program":
                    context = SegmentType.Basic;
                    BuildSegments(node.ChildNodes[0]);
                    FinaliseSegment();
                    break;

                case "directive":
                    FinaliseSegment();
                    InitialiseSegment(node.Token.Text);
                    break;

                //many rules just structure the syntax and have no semantic meaning
                case "statement_list":
                case "statement_list_cont":
                case "statement":
                case "labelled_instruction":
                case "instruction":
                case "label_opt":
                case "DAT":
                case "data_list":
                case "datum":
                    foreach (var child in node.ChildNodes)
                        BuildSegments(child);
                    break;

                //add labels to a stack which is applied whenever we reach an instruction
                case "label":
                    string labelName = node.Token.Text.TrimStart(':');
                    symbolicLabels.Push(labelName);
                    break;

                //whenever an instruction is parsed, we first pop the label stack to the current relative address
                case "basic_instruction":
                    ApplyLabels();
                    var bop = node.ChildNodes[0].ChildNodes[0].Token.Text;
                    var dest = valueBuilder.BuildValue(node.ChildNodes[1]);
                    var src = valueBuilder.BuildValue(node.ChildNodes[3]); //node 2 is a comma!
                    instructions.Enqueue(new BasicInstruction(bop, dest, src));
                    break;

                case "nonbasic_instruction":
                    ApplyLabels();
                    var nbop = node.ChildNodes[0].ChildNodes[0].Token.Text;
                    var arg = valueBuilder.BuildValue(node.ChildNodes[1]);
                    instructions.Enqueue(new NonBasicInstruction(nbop, arg));
                    break;

                case "data":
                    ApplyLabels();
                    foreach (var child in node.ChildNodes)
                        BuildSegments(child);
                    break;

                //create pseudo-instructions for constant data
                case "literal":
                    var literal = (ushort)(int)node.Token.Value;
                    instructions.Enqueue(new LiteralData(literal));
                    break;

                case "character_string":
                    var characters = (string)node.Token.Value;
                    instructions.Enqueue(new CharacterData(characters));
                    break;

                //A parse element we don't know about - this should not happen!
                default:
                    string nodeInfo = string.Format(
                        "Unknown node <{0}{1}>", 
                        node.Term.Name,
                        node.Token != null ?
                            string.Format(" token=\"{0}\"", node.Token.Text) :
                            "");
                    //throw new FormatException(nodeInfo);
                    Console.WriteLine(nodeInfo);

                    foreach (var child in node.ChildNodes)
                        BuildSegments(child);
                    break;
            }
        }

        private void ApplyLabels()
        {
            ushort segmentRelativeAddress = instructions.AssembledLength();

            foreach (var symbol in symbolicLabels.PopAll())
                relativeLabels.Enqueue(new Label(symbol, segmentRelativeAddress));
        }

        private void InitialiseSegment(string directive)
        {
            switch (directive)
            {
                case ".text":
                    context = SegmentType.Code;
                    break;

                case ".data":
                    context = SegmentType.Data;
                    break;

                default:
                    throw new FormatException("Unknown segment type " + directive);
            }
        }

        public IEnumerable<ISegment> Segments
        {
            get { return segments; }
        }

        private void FinaliseSegment()
        {
            if (instructions.Count > 0)
            {
                ISegment newSegment;

                switch (context)
                {
                    case SegmentType.Basic:
                        newSegment = new BasicSegment(instructions.DequeueAll(), relativeLabels.DequeueAll());
                        break;

                    case SegmentType.Code:
                        newSegment = new CodeSegment(instructions.DequeueAll(), relativeLabels.DequeueAll());
                        newSegment.Base = segments.AssembledLength();
                        break;

                    default:
                        throw new FormatException("Unimplemented segment type " + context);
                }

                segments.Add(newSegment);
            }
        }
    }
}
