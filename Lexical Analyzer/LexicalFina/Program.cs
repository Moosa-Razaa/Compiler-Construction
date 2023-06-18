using System;
using System.Linq;
using System.IO;

namespace LexicalFina
{
    class Program
    {
        static void Main()
        {
            //Breakers
            char[] lineBreakers = { '(', ')', '[', ']', ',', ';', ':', '{', '}', '\n', ' ', '\'', '.' };
            char[] lineBreakers2 = { '(', ')', '[', ']', ',', ';', ':', '{', '}', '\n', ' ', '\'' };
            char[] operators = { '%', '^', '+', '-', '*', '/', '>', '<', '=', '!', '>' };

            //Console.Write("Please enter the path of the file : ");
            string path = @"C:\Users\Moosa Raza\Desktop\Assignment.txt";
            int lineNumber = 0;
            string token = string.Empty;
            using( StreamReader reader = new StreamReader(path) )
            {
                do
                {
                    string line = reader.ReadLine();
                    for( int i = 0; i < line.Length; i ++ )
                    {
                        //Whitespace Case --Completed.
                        #region
                        if (char.IsWhiteSpace(line[i]))
                        {
                            continue;
                        }
                        #endregion

                        //Character Case. --Completed.
                        #region
                        else if (line[i] == '\'')
                        {
                            //Checking for the previous tokens.
                            if (token != string.Empty)
                            {
                                //Call a method that checks that where does this token belongs.
                                token = string.Empty;
                            }

                            token += line[i];
                            if ((i + 2) < line.Length)
                            {
                                //Checking for the backslash in the character and passing that
                                //to the function to verify for the character.

                                //Method also set the token to empty.

                                if (line[i + 1] == '\\')
                                {
                                    try
                                    {
                                        token += line[++i];
                                        token += line[++i];
                                        token += line[++i];
                                        //Call the character check method.
                                        if (token[token.Length - 1] != '\'')
                                        {
                                            Token.WriteLexemeError(token, lineNumber);
                                        }
                                        else
                                        {
                                            Characters.CharacterValidator(token.Substring(1, token.Length-2), lineNumber);
                                        }

                                        token = string.Empty;
                                    }
                                    catch (ArgumentOutOfRangeException)
                                    {
                                        Token.WriteLexemeError(token, lineNumber);
                                        token = string.Empty;
                                        lineNumber++;
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        token += line[++i];
                                        token += line[++i];
                                        //Call the character check method.
                                        //Call the character check method.
                                        if (token[token.Length - 1] != '\'')
                                        {
                                            Token.WriteLexemeError(token, lineNumber);
                                        }
                                        else
                                        {
                                            Characters.CharacterValidator(token.Substring(1, token.Length - 2), lineNumber);
                                        }
                                        token = string.Empty;
                                    }
                                    catch (ArgumentOutOfRangeException)
                                    {
                                        Token.WriteLexemeError(token, lineNumber);
                                        token = string.Empty;
                                        //lineNumber++;
                                    }
                                }
                            }
                            else
                            {
                                //Write lexeme error as the character is not properly enclosed.
                                if ((i + 1) < line.Length)
                                {
                                    token += line[++i];
                                    Token.WriteLexemeError(token, lineNumber);
                                }
                                else
                                {
                                    Token.WriteLexemeError(token, lineNumber);
                                }
                            }
                        }
                        #endregion

                        //String Case. --Completed.
                        #region
                        else if (line[i] == '\"')
                        {
                            bool valid = false;
                            if (token != string.Empty)
                            {
                                //Call the method that checks where does that string belong.
                                token = string.Empty;
                            }

                            token += line[i];

                            if ((i + 1) >= line.Length)
                            {
                                //Lexeme Error.
                                Token.WriteLexemeError(token, lineNumber);
                                continue;
                            }

                            while ((i + 1) < line.Length)
                            {
                                token += line[++i];
                                if (line[i] == '\\')
                                {
                                    if (i + 1 < line.Length)
                                    {
                                        token += line[++i];
                                        continue;
                                    }
                                    else
                                    {
                                        //Lexeme error.
                                        Token.WriteLexemeError(token, lineNumber);
                                        break;
                                    }
                                }
                                if (line[i] == '\"')
                                {
                                    //Call the method to check for valid string constant.
                                    StringConstants.ValidateString(token.Substring(1, token.Length - 2), lineNumber);
                                    token = string.Empty;
                                    valid = true;
                                    break;
                                }
                            }

                            if (!valid)
                            {
                                //Lexeme Error
                                Token.WriteLexemeError(token, lineNumber);
                                token = string.Empty;
                            }
                        }
                        #endregion

                        //Integer or Float Case. --Completed.
                        #region
                        else if (char.IsDigit(line[i]) || line[i] == '+' || line[i] == '-' )
                        {
                            bool dot = false;
                            if( token == string.Empty )
                            {
                                //Method to check where the current token bleongs.
                                token = string.Empty;
                            }

                            token += line[i];

                            while( (i + 1) < line.Length )
                            {
                                if( lineBreakers2.Contains(line[i + 1]) || operators.Contains(line[i + 1]) || (dot && line[i + 1] == '.') )
                                {
                                    break;
                                }
                                if( line[++i] == '.' )
                                {
                                    dot = true;
                                }
                                token += line[i];
                            }

                            //Method to check for integers and float both.
                            if( token == "+" )
                            {
                                if( i + 1 < line.Length && (line[i + 1] == '=' || line[i + 1] == '+') )
                                {
                                    token += line[++i];
                                    PunctuatorsOperators.OperatorsValidator(token, lineNumber);
                                }
                                else
                                {
                                    PunctuatorsOperators.OperatorsValidator(token, lineNumber);
                                }
                            }
                            else if( token == "-" )
                            {
                                if (i + 1 < line.Length && (line[i + 1] == '=' || line[i + 1] == '-'))
                                {
                                    token += line[++i];
                                    PunctuatorsOperators.OperatorsValidator(token, lineNumber);
                                }
                                else if ((i + 1 < line.Length) && line[i + 1] == '>')
                                {
                                    token += line[++i];
                                    //Method to check -- operator.
                                    PunctuatorsOperators.OperatorsValidator(token, lineNumber);
                                    token = string.Empty;
                                }
                                else
                                {
                                    PunctuatorsOperators.OperatorsValidator(token, lineNumber);
                                }
                            }
                            else
                            {
                                IntegerFloatConstants.IntegerFloatValidator(token, lineNumber);
                            }                            
                            token = string.Empty;
                        }
                        #endregion

                        //Operator Case. --Compeleted.
                        #region
                        else if( operators.Contains(line[i]) )
                        {
                            if( token != string.Empty )
                            {
                                //Method to check the token and where it belongs.
                                token = string.Empty;
                            }

                            token += line[i];

                            if( Array.IndexOf(operators, line[i]) > 1 && Array.IndexOf(operators, line[i]) <= 10 )
                            {
                                if( (i + 1 < line.Length) && line[i] == '+' && line[i + 1] == '+' )
                                {
                                    token += line[i + 1];
                                    //Method to check ++ operator.
                                    PunctuatorsOperators.OperatorsValidator(token, lineNumber);
                                    token = string.Empty;
                                }
                                else if( (i + 1 < line.Length) && line[i] == '-' && line[i + 1] == '-' )
                                {
                                    token += line[i + 1];
                                    //Method to check -- operator.
                                    PunctuatorsOperators.OperatorsValidator(token, lineNumber);
                                    token = string.Empty;
                                }
                                else if ((i + 1 < line.Length) && line[i] == '-' && line[i + 1] == '>')
                                {
                                    token += line[i + 1];
                                    //Method to check -> operator.
                                    PunctuatorsOperators.OperatorsValidator(token, lineNumber);
                                    token = string.Empty;
                                }
                                else if ( (i + 1 < line.Length)  && line[i + 1] == '=' )
                                {
                                    token += line[++i];
                                    //Method to check for the operator.
                                    PunctuatorsOperators.OperatorsValidator(token, lineNumber);
                                    token = string.Empty;
                                }
                                else
                                {
                                    //Method to check for the operator.
                                    PunctuatorsOperators.OperatorsValidator(token, lineNumber);
                                    token = string.Empty;
                                }
                            }
                            else
                            {
                                PunctuatorsOperators.OperatorsValidator(token, lineNumber);
                                token = string.Empty;
                            }
                            
                        }
                        #endregion

                        //identifier Case. --Completed.
                        #region
                        else if( char.IsLetter(line[i]) )
                        {
                            if( token !=  string.Empty )
                            {
                                //Method to check where that token belongs.
                                token = string.Empty;
                            }

                            token += line[i];

                            while( (i + 1 < line.Length) && !(lineBreakers.Contains(line[i + 1]) || operators.Contains(line[i+1])) )
                            {
                                token += line[++i];
                            }

                            //Method to check for the identifiers and keywords.
                            Identifiers.IdentifierValidator(token, lineNumber);
                            token = string.Empty;
                        }
                        #endregion

                        //Float case starting with '.' --Completed.
                        #region
                        else if ( line[i] == '.' )
                        {
                            if( token == string.Empty )
                            {
                                //Method to check for the token.
                                token = string.Empty;
                            }

                            token += line[i];

                            if( (i + 1 < line.Length) && !char.IsDigit(line[i + 1]) )
                            {
                                //Make the token for dot.
                                IntegerFloatConstants.IntegerFloatValidator(token, lineNumber);
                                token = string.Empty;
                            }
                            else
                            {
                                while ((i + 1 < line.Length) && char.IsDigit(line[i + 1]))
                                {
                                    token += line[++i];
                                }

                                //Check the for valid float.
                                IntegerFloatConstants.IntegerFloatValidator(token, lineNumber);
                                token = string.Empty;
                            }                           
                        }
                        #endregion

                        //Punctuators Case. --Completed.
                        #region
                        else if ( lineBreakers.Contains(line[i]) && Array.IndexOf(lineBreakers, line[i]) < 9 )
                        {
                            if( token != string.Empty )
                            {
                                //Method to check where that token belongs.
                                token = string.Empty;
                            }

                            token += line[i];
                            //Method to save the punctuator.
                            PunctuatorsOperators.PunctuatorValidator(token, lineNumber);
                            token = string.Empty;
                        }
                        #endregion

                        //Variable initialization Case. --Completed.
                        #region
                        else if ( line[i] == '~' )
                        {
                            if( token != string.Empty )
                            {
                                token = string.Empty;
                            }                         

                            token += line[i];
                            Identifiers.VariableInitializer(token, lineNumber);
                            if ( char.IsLetter(line[i + 1]) )
                            {
                                token += line[++i];
                            }
                            else
                            {
                                //Lexeme Error.
                                //Console.WriteLine("I am here.");
                                Token.WriteLexemeError(token, lineNumber);
                                continue;
                            }
                            while ((i + 1 < line.Length) && !(lineBreakers.Contains(line[i + 1]) || operators.Contains(line[i + 1]) || char.IsWhiteSpace(line[i + 1])))
                            {
                                token += line[++i];
                            }
                            //Call method to check for valid variable name.
                            Identifiers.IdentifierValidator(token.Substring(1, token.Length-1), lineNumber);
                            token = string.Empty;
                        }
                        #endregion

                        //Comments Case. --Completed.
                        #region
                        else if ( line[i] == '#' )
                        {
                            break;
                        }
                        #endregion

                        //OR Case. --Completed.
                        #region
                        else if ( line[i] == '|' )
                        {
                            token += line[i];
                            if( i + 1 < line.Length && line[i + 1] == '|' )
                            {
                                token += line[++i];
                                PunctuatorsOperators.OperatorsValidator(token, lineNumber);
                                token = string.Empty;
                            }
                            else
                            {
                                Token.WriteLexemeError(token, lineNumber);
                                token = string.Empty;
                            }
                        }
                        #endregion

                        //AND Case. --Completed.
                        #region
                        else if (line[i] == '&')
                        {
                            token += line[i];
                            if (i + 1 < line.Length && line[i + 1] == '&')
                            {
                                token += line[++i];
                                PunctuatorsOperators.OperatorsValidator(token, lineNumber);
                                token = string.Empty;
                            }
                            else
                            {
                                Token.WriteLexemeError(token, lineNumber);
                                token = string.Empty;
                            }
                        }
                        #endregion

                        //Other Cases.
                        #region
                        else
                        {
                            if( token == string.Empty )
                            {
                                token = string.Empty;
                            }
                            token += line[i];
                            Token.WriteLexemeError(token, lineNumber);
                            token = string.Empty;
                        }
                        #endregion
                    }
                    lineNumber++;
                }
                while (!reader.EndOfStream);
            }
            Console.WriteLine("Lexical Analyzer Completed. Hope for the best!");
            Console.ReadKey();
        }
    }
}
