using System;
using System.Text.RegularExpressions;

namespace LexicalFina
{
    class IntegerFloatConstants
    {
        public static void IntegerFloatValidator(string word, int line)
        {
            Token t = new Token();
            Regex intreg = new Regex("^[+-]?[0-9]+$");
            Regex fltreg = new Regex("^[+-]?[0-9]*.[0-9]+$");
            if (intreg.IsMatch(word))
            {
                t.ClassPart = "int-const";
                t.ValuePart = word;
                t.LineNumber = line;
                Token.WriteToken(t);
            }
            else if (fltreg.IsMatch(word))
            {
                t.ClassPart = "float-const";
                t.ValuePart = word;
                t.LineNumber = line;
                Token.WriteToken(t);
            }
            else
            {
                t.ClassPart = "error";
                t.ValuePart = word;
                t.LineNumber = line;
                Token.WriteLexemeError(word, line);
            }
        }
    }
}
