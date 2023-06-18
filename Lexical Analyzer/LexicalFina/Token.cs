using System;
using System.IO;

namespace LexicalFina
{
    class Token
    {
        static string path = @"F:\Projects\Compiler Construction\Lexical Analyzer\LexicalFina\LexicalOutput\Lexical.txt";
        public string ClassPart { get; set; }
        public string ValuePart { get; set; }
        public int LineNumber { get; set; }


        //Constructor making sure that the file exists.
        static Token()
        {
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }
            else
            {
                File.WriteAllText(path, "");
            }
        }

        //Function that will write the tokens to the Tokens.txt file.
        public static void WriteToken( Token token )
        {
            File.AppendAllText(path, "( " + token.ClassPart + " , " + token.ValuePart + " , " + token.LineNumber + " )" + Environment.NewLine );
        }

        public static void WriteLexemeError( string error, int LineNumber )
        {
            File.AppendAllText(path, "( Lexical Error, " + error + " , " + LineNumber + " )" + Environment.NewLine );
        }
    }
}
