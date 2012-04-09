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

        public void BuildSegments(ParseTreeNode visitee)
        {
            switch (visitee.Term.Name)
            {
                //a program is divided into segments separated by directives
                case "program":
                    InitialiseSegment(".text");
                    BuildSegments(visitee.ChildNodes[0]);
                    FinaliseSegment();
                    break;

                case "directive":
                    FinaliseSegment();
                    InitialiseSegment(visitee.Token.Text);
                    break;

                //many rules just structure the syntax and have no semantic meaning
                case "statement_list":
                case "statement_list_cont":
                case "statement":
                case "labelled_instruction":
                case "instruction":
                case "label_opt":
                    foreach (var child in visitee.ChildNodes)
                        BuildSegments(child);
                    break;

                //add labels to a stack which is applied whenever we reach an instruction
                case "label":
                    string labelName = visitee.Token.Text.TrimStart(':');
                    symbolicLabels.Push(labelName);
                    break;

                //whenever an instruction is parsed, we first pop the label stack to the current relative address
                case "basic_instruction":
                    ApplyLabels();
                    var op = visitee.ChildNodes[0].ChildNodes[0].Token.Text;
                    var dest = valueBuilder.BuildValue(visitee.ChildNodes[1]);
                    var src = valueBuilder.BuildValue(visitee.ChildNodes[3]); //node 2 is a comma!
                    //new BasicInstruction(op, dest, src);

                    Console.WriteLine("basic_instruction {0} {1},{2}", op, dest, src);
                    break;

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
                        BuildSegments(node);
                    break;
            }
        }

        private void ApplyLabels()
        {
            ushort segmentRelativeAddress = instructions
                .Select(op => op.AssembledLength)
                .Aggregate((ushort)0, MathsEx.Add);

            foreach (var symbol in symbolicLabels.PopAll())
                relativeLabels.Enqueue(new Label(symbol, segmentRelativeAddress));
        }

        private void InitialiseSegment(string directive)
        {
            switch (directive)
            {
                case ".text":
                    context = SegmentType.CSECT;
                    break;

                case ".data":
                    context = SegmentType.DSECT;
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
                switch (context)
                {
                    case SegmentType.CSECT:
                        var newSegment = new CodeSegment(instructions.DequeueAll(), relativeLabels.DequeueAll());
                        newSegment.Base = segments
                            .Select(seg => seg.AssembledLength)
                            .Aggregate((ushort)0, MathsEx.Add);

                        segments.Add(newSegment);
                        break;

                    default:
                        throw new FormatException("Unimplemented segment type " + context);
                }
        }

        private void BuildInstruction(ParseTreeNode node)
        {
            Console.WriteLine("\n-- <instruction> --");

            foreach (var node2 in node.ChildNodes)
                BuildSegments(node2);

            Console.WriteLine("-- </instruction> --");
        }


    }
}
