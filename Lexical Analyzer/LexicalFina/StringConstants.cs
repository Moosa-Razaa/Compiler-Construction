using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace LexicalFina
{
    class StringConstants
    {
        public static void ValidateString(string word, int line)
        {
            Token t = new Token();

            //d is the word to be checked.
            string d = word;

            //Regex to check for the string.
            Regex strregg = new Regex("^[A-Za-z]|[A-Za-z_][A-Za-z_]*[A-Za-z0-9]$");

            //Validating the string.
            if (strregg.IsMatch(d) || d.Length == 2)
            {
                //Checking for escape characters.
                bool valid = true;
                char[] escape = { 'n', 't', '0', 'b', 'r', '\'', '\"', '\\' };
                for (int i = 0; i < d.Length; i++)
                {
                    if (d[i] == '\\')
                    {
                        if (i + 1 >= d.Length)
                        {
                            t.ClassPart = "error1";
                            t.ValuePart = d;
                            t.LineNumber = line; //Lexeme Error.
                            Token.WriteLexemeError(d, line);
                            valid = false;
                            break;
                        }

                        if (!escape.Contains(d[i + 1]))
                        {
                            t.ClassPart = "error2";
                            t.ValuePart = d;
                            t.LineNumber = line; //Lexeme Error.
                            Token.WriteLexemeError(d, line);
                            valid = false;
                            break;
                        }
                        i++;
                    }

                    else if (Array.IndexOf(escape, d[i]) > 4)
                    {
                        t.ClassPart = "error3";
                        t.ValuePart = d;
                        t.LineNumber = line; //Lexeme Error.
                        Token.WriteLexemeError(d, line);
                        valid = false;
                        break;
                    }
                }
                if (valid)
                {
                    t.ClassPart = "string-const";
                    t.ValuePart = d;
                    t.LineNumber = line;
                    Token.WriteToken(t);
                }

            }
            else
            {
                t.ClassPart = "error";
                t.ValuePart = d;
                t.LineNumber = line;
                Token.WriteToken(t);
            }
        }
    }
}
