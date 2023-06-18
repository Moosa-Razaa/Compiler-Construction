using System;
using System.Collections.Generic;
using System.IO;

namespace SyntaxAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"F:\Projects\Compiler Construction\Lexical Analyzer\LexicalFina\LexicalOutput\Lexical.txt";
            List<Token> tokenList = new List<Token>();
            string[] rawTokens;

            //Reading data from file and breaking it into tokens.
            using ( StreamReader reader = new StreamReader(path) )
            {
                rawTokens = reader.ReadToEnd().Split('\n');
                           
                //Iterating on rawTokens to separate the value part, class part and line number.
                foreach( string str in rawTokens )
                {
                    if (String.IsNullOrEmpty(str)) continue;

                    Token currentToken = new Token();

                    //Separating for the comma class case.
                    if( str.Contains("Comma-Class") )
                    {
                        currentToken.ClassPart = "Comma-Class";
                        currentToken.ValuePart = ",";
                        currentToken.LineNumber = Convert.ToInt32(str.Substring(19, 2));
                    }

                    //Char_Const case.
                    else if( str.Contains("char_const") )
                    {
                        currentToken.ClassPart = "char_const";
                        if( str[15] == '\\' )
                        {
                            currentToken.ValuePart = str.Substring(15, 2);
                            currentToken.LineNumber = Convert.ToInt32(str.Substring(20, 2));
                        }
                        else
                        {
                            currentToken.ValuePart = str[15].ToString();
                            currentToken.LineNumber = Convert.ToInt32(str.Substring(19, 2));
                        }
                    }

                    //String-Const case.
                    else if( str.Contains("string-const") )
                    {
                        currentToken.ClassPart = "string-const";
                        for( int i = str.Length - 1; i >= 0; i -- )
                        {
                            if( str[i] == ',' )
                            {
                                currentToken.LineNumber = Convert.ToInt32(str.Substring(i + 1, str.Length - 3 - i));
                                currentToken.ValuePart = str.Substring(16, i - 17);
                                break;
                            }
                        }
                    }

                    //Default case.
                    else
                    {
                        var subToken = str.Split(',');
                        currentToken.ClassPart = subToken[0].Substring(1).Trim();
                        currentToken.ValuePart = subToken[1].Trim();
                        currentToken.LineNumber = Convert.ToInt32(subToken[2].Substring(0, subToken[2].Length - 2));                       
                    }
                    tokenList.Add(currentToken);
                }

                Token endMarker = new Token
                {
                    ClassPart = "$",
                    ValuePart = "EndOfCode",
                    LineNumber = -1
                };
                tokenList.Add(endMarker);
            }

            SyntaxValidator.TokenProp = tokenList;
            if( SyntaxValidator.SyntaxAnalyzer() )
            {
                Console.WriteLine("Nothing wrong with the syntax!");
            }
            else
            {
                Console.WriteLine("Silly blunders there.");
            }
            
            Console.ReadKey();
        }
    }
}
