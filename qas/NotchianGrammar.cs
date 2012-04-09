using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Irony.Parsing;

namespace Quasar.Assembler
{
    [Language("Assembly", "1.0", "Notch-style assembler instructions for the DCPU-16")]
    class NotchianGrammar : Grammar
    {
        public NotchianGrammar() : base(caseSensitive:false)
        {
            #region parser settings
            this.UsesNewLine = true;
            #endregion

            #region symbols
            var directive = new IdentifierTerminal("directive");
            directive.AllFirstChars = ".";
            var label = new IdentifierTerminal("label", IdOptions.IsNotKeyword);
            label.AllFirstChars = ":";
            var symbol = new IdentifierTerminal("symbol", IdOptions.IsNotKeyword);

            var literal = new NumberLiteral("literal", NumberOptions.IntOnly);
            literal.AddPrefix("0x", NumberOptions.Hex);
            literal.AddPrefix("0b", NumberOptions.Binary);

            var comment = new CommentTerminal("comment", ";", "\n", "\r", "\r\n");
            NonGrammarTerminals.Add(comment);

            var program = new NonTerminal("program");
            var statement_list = new NonTerminal("statement_list");
            var statement_list_cont = new NonTerminal("statement_list_cont");
            var statement = new NonTerminal("statement");

            var labelled_instruction = new NonTerminal("labelled_instruction");
            var label_opt = new NonTerminal("label_opt");

            var instruction = new NonTerminal("instruction");
            var basic = new NonTerminal("basic_instruction");
            var nonbasic = new NonTerminal("nonbasic_instruction");
            var datum = new NonTerminal("macro");

            var basic_opcode = new NonTerminal("basic_opcode");
            var nonbasic_opcode = new NonTerminal("nonbasic_opcode");

            var value = new NonTerminal("value");
            var register = new NonTerminal("register");
            var counter = new NonTerminal("register");
            var synonym = new NonTerminal("synonym");

            var address = new NonTerminal("address");
            var pointer = new NonTerminal("pointer");
            var target = new NonTerminal("target");
            var register_indirection = new NonTerminal("register_indirection");
            #endregion symbols

            #region parser settings
            this.UsesNewLine = true;
            this.Root = program;
            #endregion

            #region grammar rules
            //basic structure
            program.Rule = statement_list + Eof;
            statement_list.Rule = statement + statement_list_cont | Empty;
            statement_list_cont.Rule = NewLine + statement_list;
            statement.Rule = directive | label | labelled_instruction | Empty;

            //lines and labels
            labelled_instruction.Rule = label_opt + instruction;
            label_opt.Rule = label | Empty;

            //instructions
            instruction.Rule = basic | nonbasic | datum;
            basic.Rule = basic_opcode + value + "," + value;
            nonbasic.Rule = nonbasic_opcode + value;
            datum.Rule = ToTerm("DAT");

            //opcodes
            basic_opcode.Rule = ToTerm("SET") | "ADD" | "SUB" | "MUL" | "DIV" | "MOD" | "SHL" | "SHR" | "AND" | "BOR" | "XOR" | "IFE" | "IFN" | "IFG" | "IFB";
            nonbasic_opcode.Rule = ToTerm("JSR");

            //values
            value.Rule = literal | register | counter | synonym | address;
            register.Rule = ToTerm("A") | "B" | "C" | "X" | "Y" | "Z" | "I" | "J";
            counter.Rule = ToTerm("SP") | "PC" | "O";
            synonym.Rule = ToTerm("POP") | "PEEK" | "PUSH";

            address.Rule = pointer | symbol;
            pointer.Rule = ToTerm("[") + target + ToTerm("]");
            target.Rule = literal | register | register_indirection;
            register_indirection.Rule = (register + ToTerm("+") + literal) | (literal + ToTerm("+") + register);

            this.Root = program;
#endregion
            
        }
    }
}
