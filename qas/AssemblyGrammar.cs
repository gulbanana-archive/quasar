using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Irony.Parsing;

namespace Quasar.Assembler
{
    [Language("Assembly", "1.0", "Assembler instructions for the DCPU-16")]
    class AssemblyGrammar : Grammar
    {
        public AssemblyGrammar() : base(caseSensitive:false)
        {
            #region parser settings
            this.UsesNewLine = true;
            #endregion

            #region symbols
            var directive_name = new IdentifierTerminal("directive_name");
            directive_name.AllFirstChars = ".";
            var label_name = new IdentifierTerminal("label_name", IdOptions.IsNotKeyword);
            label_name.AllFirstChars = ":";
            var symbol = new IdentifierTerminal("symbol", IdOptions.IsNotKeyword);

            var literal = new NumberLiteral("literal", NumberOptions.IntOnly);
            literal.AddPrefix("0x", NumberOptions.Hex);
            literal.AddPrefix("0b", NumberOptions.Binary);

            var comment = new CommentTerminal("comment", ";", "\n", "\r", "\r\n");
            NonGrammarTerminals.Add(comment);

            var program = new NonTerminal("program");
            var statement_list = new NonTerminal("statement_list");
            var statement = new NonTerminal("statement");
            var directive = new NonTerminal("directive");

            var labelled_instruction = new NonTerminal("labelled_instruction");
            var label_opt = new NonTerminal("label_opt");
            var label = new NonTerminal("label");

            var instruction = new NonTerminal("instruction");
            var basic = new NonTerminal("basic_instruction");
            var nonbasic = new NonTerminal("nonbasic_instruction");
            var macro = new NonTerminal("macro");

            var basic_opcode = new NonTerminal("basic_opcode");
            var nonbasic_opcode = new NonTerminal("nonbasic_opcode");

            var value = new NonTerminal("value");
            var register = new NonTerminal("register");
            var counter = new NonTerminal("register");
            var synonym = new NonTerminal("synonym");

            var address = new NonTerminal("address");
            var pointer = new NonTerminal("pointer");
            var register_indirection = new NonTerminal("register_indirection");
            var address_operator = new NonTerminal("address_operator");
            #endregion symbols

            #region parser settings
            this.UsesNewLine = true;
            this.Root = program;
            #endregion

#region grammar rules
            //basic structure
            program.Rule = statement_list;
            statement_list.Rule = statement + ((NewLine + statement_list) | Empty);
            //MakeListRule(program, NewLine, statement, TermListOptions.AllowTrailingDelimiter | TermListOptions.AllowEmpty);
            statement.Rule = directive | labelled_instruction | comment | Empty;
            directive.Rule = directive_name;

            //lines and labels
            labelled_instruction.Rule = label_opt + instruction;
            label_opt.Rule = label | Empty;
            label.Rule = label_name;

            //instructions
            instruction.Rule = basic | nonbasic | macro;
            basic.Rule = basic_opcode + value + "," + value;
            nonbasic.Rule = nonbasic_opcode + value;
            macro.Rule = ToTerm("MACRO");   //XXX no macros yet

            //opcodes
            basic_opcode.Rule = ToTerm("SET") | "ADD" | "SUB" | "MUL" | "DIV" | "MOD" | "SHL" | "SHR" | "AND" | "BOR" | "XOR" | "IFE" | "IFN" | "IFG" | "IFB";
            nonbasic_opcode.Rule = ToTerm("JSR");

            //values
            value.Rule = literal | register | counter | synonym | address;
            register.Rule = ToTerm("A") | "B" | "C" | "X" | "Y" | "Z" | "I" | "J";
            counter.Rule = ToTerm("SP") | "PC" | "O";
            synonym.Rule = ToTerm("POP") | "PEEK" | "PUSH";

            address.Rule = pointer | symbol;
            pointer.Rule = ToTerm("[") + (literal | register | register_indirection) + ToTerm("]");
            register_indirection.Rule = (register + address_operator + literal) | (literal + address_operator + register);
            address_operator.Rule = ToTerm("+") | "-";

            this.Root = program;
#endregion
            
        }
    }
}
