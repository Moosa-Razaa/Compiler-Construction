using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LexicalTokenizer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter the path of your Ryon file.");
            string path = Console.ReadLine();

            //The main token that will be build.
            string token = string.Empty;

            //Word breaker that will break the token.
            char[] lineBreakers = { '(', ')', '\n', '[', ']', ',', ';', ':' };
            char[] operators = { '%', '^', '+', '-', '*', '/', '>', '<', '=', '!' };

            //Reading the RYON text file.
            using( StreamReader reader = new StreamReader(path) )
            {
                do
                {
                    char currentCharacter = (char)reader.Read();

                    //String constant case.
                    if (currentCharacter == '\"')
                    {
                        while (currentCharacter != '\n' || currentCharacter != '\"')
                        {
                            currentCharacter = (char)reader.Read();
                            token += currentCharacter;
                        }
                        //Method to check for string constants.
                        token = string.Empty;
                        continue;
                    }

                    //Characters case.
                    if (currentCharacter == '\'')
                    {
                        currentCharacter = (char)reader.Read();
                        if (currentCharacter == '\\')
                        {
                            token += currentCharacter;
                            //Call the method.
                            continue;
                        }
                        //CalL the method.
                        token = string.Empty;
                        continue;
                    }

                    //Variable case.
                    if (currentCharacter == '~')
                    {
                        string buffer = string.Empty;
                        do
                        {
                            buffer += (char)reader.Read();
                        }
                        while (!lineBreakers.Contains(buffer[buffer.Length - 1]) || buffer[buffer.Length - 1] == '~');
                        //Check for variable name.
                        //Method for checking the variable name.
                        if( buffer == "~" )
                        {
                            //Write lexical error.
                            
                            continue;
                        }

                        token += buffer;

                        //Call the method to check for valid variable name.
                        token = string.Empty;
                        continue;
                    }

                    //Keywords or string indetifiers case.
                    if( char.IsLetter(currentCharacter) )
                    {
                        string buffer = string.Empty;

                        while(true)
                        {
                            buffer += (char)reader.Read();
                            if( !char.IsLetterOrDigit(buffer[buffer.Length - 1]) )
                            {
                                token += buffer;
                                //Method to check for identifiers.
                            }
                        }
                    }
                }
                while (!reader.EndOfStream);
            }
        }
    }
}
