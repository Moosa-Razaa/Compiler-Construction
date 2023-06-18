using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LexicalFina
{
    class Identifiers
    {
        static Dictionary<string, string> keywords = new Dictionary<string, string>();
        static Dictionary<string, string> oop = new Dictionary<string, string>();

        //Constructor adding all the Keywords of the language to the
        //keyword and oop dictionary.

        static Identifiers()
        {
            
            keywords.Add("~", "~");
            //keywords.Add("main", "Main_Method-Class");
            keywords.Add("defualt", "Default");
            keywords.Add("#", "Comment");
            keywords.Add("ForCondition", "While");
            keywords.Add("ForFirst", "DoWhile");
            keywords.Add("^", "^");
            keywords.Add("Array", "Array");
            keywords.Add("VarArray", "VarArray");
            keywords.Add("ForTrue", "WhileTrue");
            //keywords.Add("Do_While", "Do_While");
            //keywords.Add("While", "While");
            keywords.Add("integer", "Integer-Class");
            keywords.Add("floor", "Floor_Method");
            keywords.Add("length", "Length_Property");
            keywords.Add("ceil", "Ceil_Method");
            keywords.Add("round", "Round_Method");
            keywords.Add("set", "Set");
            keywords.Add("Override", "Override");
            keywords.Add("interface", "Interface");
            keywords.Add("void", "Void");
            keywords.Add("this", "This");
            keywords.Add("bool", "Bool");
            keywords.Add("byte", "Byte");
            keywords.Add("char", "Character");
            keywords.Add("float", "Float");
            keywords.Add("break", "Break");
            keywords.Add("continue", "Continue");
            keywords.Add("true", "True");
            keywords.Add("false", "False");
            keywords.Add("if", "If");
            keywords.Add("else", "Else");
            keywords.Add("For", "For");
            keywords.Add("return", "Return");
            keywords.Add("new", "New");

            //---------------------------------------------------------------------------//

            oop.Add("Class", "Class");
            oop.Add("Abstract_class", "Abstract");
            oop.Add("Public", "Public");
            oop.Add("Private", "Private");
            oop.Add("Chained", "Chained");
            oop.Add("Interface", "Interface");
            oop.Add("Static", "Static");
            oop.Add("method", "Method");

        }

        public static void VariableInitializer(string word, int lineno)
        {
            Token tk = new Token();
            if( word == "~" )
            {
                var k = keywords.Where(x => x.Key == word).Select(x => x.Value).FirstOrDefault();
                tk.ClassPart = k.ToString();
                tk.ValuePart = word;
                tk.LineNumber = lineno;
                Token.WriteToken(tk);
            }
            else
            {
                tk.ClassPart = "invalid_identifier";
                tk.ValuePart = word;
                tk.LineNumber = lineno;
                Token.WriteLexemeError(word, lineno);
            }
        }

        public static void IdentifierValidator(string word, int lineno)
        {
            var key_array = keywords.Keys.ToArray();
            var oop_array = oop.Keys.ToArray();

            Token tk = new Token();
            Regex idreg = new Regex("^[a-zA-Z][a-zA-Z0-9_]*$");
            if (idreg.IsMatch(word))
            {
                if (key_array.Contains(word))
                {
                    var k = keywords.Where(x => x.Key == word).Select(x => x.Value).FirstOrDefault();
                    tk.ClassPart = k.ToString();
                    tk.ValuePart = word;
                    tk.LineNumber = lineno;
                    Token.WriteToken(tk);
                }
                else if (oop_array.Contains(word))
                {
                    var k = oop.Where(x => x.Key == word).Select(x => x.Value).FirstOrDefault();
                    tk.ClassPart = k.ToString();
                    tk.ValuePart = word;
                    tk.LineNumber = lineno;
                    Token.WriteToken(tk);
                }
                else
                {
                    tk.ClassPart = "identifier";
                    tk.ValuePart = word;
                    tk.LineNumber = lineno;
                    Token.WriteToken(tk);
                    //identifier.Add(word, "identifier");
                }
            }
            else
            {
                tk.ClassPart = "invalid_identifier";
                tk.ValuePart = word;
                tk.LineNumber = lineno;
                Token.WriteLexemeError(word, lineno);
                //identifier.Add(word, "invalid_identifier");
            }
        }
    }
}
