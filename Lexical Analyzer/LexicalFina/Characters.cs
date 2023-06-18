using System.Collections.Generic;

namespace LexicalFina
{
    class Characters
    {
        public static void CharacterValidator(string word, int lineno)
        {
            string result = word;
            char chr;
            Token tk = new Token();
            var validChars = new List<char>() { '\'', '\\', '0', '\n', '\"', '\r', '\b', '\t' };
            var validCharsrem = new List<char>() { '\'', '\\', '\"' };
            bool status = false;
            if (word.Length == 1)
            {
                char r = word[0];
                if (r == '\\')
                {

                    Token.WriteLexemeError(word, lineno);
                    status = true;
                }
                else
                if (validCharsrem.Contains(result[0]))
                {
                    Token.WriteLexemeError(word, lineno);
                    status = true;
                }
                else
                if (char.TryParse(result, out chr))
                {
                    tk.ClassPart = "char_const";
                    tk.ValuePart = word;
                    tk.LineNumber = lineno;
                    Token.WriteToken(tk);
                    status = true;
                }
                else
                {
                    Token.WriteLexemeError(word, lineno);
                    status = true;

                }
            }
            if (word.Length < 3)
            {

                {
                    for (int i = 0; i < word.Length; i++)
                    {
                        if (status == false)
                        {
                            if (validChars.Contains(result[0]))
                            {
                                tk.ClassPart = "char_const";
                                tk.ValuePart = word;
                                tk.LineNumber = lineno;
                                Token.WriteToken(tk);
                                status = true;
                            }
                            else if (char.TryParse(result, out chr))
                            {
                                tk.ClassPart = "char_const";
                                tk.ValuePart = word;
                                tk.LineNumber = lineno;
                                Token.WriteToken(tk);
                                status = true;
                            }
                            else
                            {
                                Token.WriteLexemeError(word, lineno);
                                status = true;

                            }
                        }
                    }
                }
            }
            else
            {
                Token.WriteLexemeError(word, lineno);
            }

        }
    }
}
