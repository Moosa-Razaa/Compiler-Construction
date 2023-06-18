using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LexicalFina
{
    class PunctuatorsOperators
    {
        static Dictionary<string, string> punctuators = new Dictionary<string, string>();
        static Dictionary<string, string> operators = new Dictionary<string, string>();

        //Constructor adding values to the dictionaries.
        static PunctuatorsOperators()
        {
            operators.Add("+", "pm");
            operators.Add("-", "pm");
            operators.Add("*", "mdm");
            operators.Add("/", "mdm");
            operators.Add("%", "mdm");
            operators.Add("<", "ro");
            operators.Add(">", "ro");
            operators.Add("<=", "ro");
            operators.Add(">=", "ro");
            operators.Add("==", "ro");
            operators.Add("!=", "ro");
            operators.Add("->", "Inheritance");
            operators.Add("&&", "&&");
            operators.Add("||", "||");
            operators.Add("++", "Inc-Dec");
            operators.Add("--", "Inc-Dec");
            operators.Add("=", "=");
            operators.Add("+=", "CompoundAssignment");
            operators.Add("-=", "CompoundAssignment");
            operators.Add("/=", "CompoundAssignment");
            //operators.Add("%=", "ca");

            punctuators.Add(";", ";");
            punctuators.Add(":", ":");
            punctuators.Add(",", ",");
            punctuators.Add(".", ".");
            punctuators.Add("(", "(");
            punctuators.Add(")", ")");
            punctuators.Add("{", "{");
            punctuators.Add("}", "}");
            punctuators.Add("[", "[");
            punctuators.Add("]", "]");
        }

        //Validating the operators.
        public static void OperatorsValidator(string word, int lineno)
        {
            var opera_array = operators.Keys.ToArray();
            Token tk = new Token();
            if (opera_array.Contains(word))
            {
                var k = operators.Where(x => x.Key == word).Select(x => x.Value).FirstOrDefault();
                tk.ClassPart = k.ToString();
                tk.ValuePart = word;
                tk.LineNumber = lineno;
                Token.WriteToken(tk);
            }
            else
            {
                tk.ClassPart = "invalid_punctuator";
                tk.ValuePart = word;
                tk.LineNumber = lineno;
                Token.WriteLexemeError(word, lineno);
            }
        }

        public static void PunctuatorValidator(string word, int lineno)
        {
            var punc_array = punctuators.Keys.ToArray();
            Token tk = new Token();
            if (punc_array.Contains(word))
            {
                var k = punctuators.Where(x => x.Key == word).Select(x => x.Value).FirstOrDefault();
                tk.ClassPart = k.ToString();
                tk.ValuePart = word;
                tk.LineNumber = lineno;
                Token.WriteToken(tk);
            }
            else
            {
                tk.ClassPart = "invalid_punctuator";
                tk.ValuePart = word;
                tk.LineNumber = lineno;
                Token.WriteLexemeError(word, lineno);
            }
        }
    }
}
