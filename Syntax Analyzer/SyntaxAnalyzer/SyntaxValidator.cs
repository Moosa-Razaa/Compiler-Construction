using System;
using System.Collections.Generic;
using System.Linq;

namespace SyntaxAnalyzer
{
    static class SyntaxValidator
    {
        static int index = 0;
        static List<Token> tokenList = new List<Token>();

        public static List<Token> TokenProp 
        {
            get
            {
                return tokenList;
            }
            set
            {
                tokenList = value;
            }
        }

        //Status : Complete
        private static bool Declaration()
        {
            if( tokenList[index].ClassPart == "~" )
            {
                index++;
                if( tokenList[index].ClassPart == "identifier" )
                {
                    index++;
                    if( Initialize() )
                    {
                        if( List() )
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        //Status : Complete
        private static bool Initialize()
        {
            if (tokenList[index].ClassPart == "=")
            {
                index++;
                if (OE())
                {
                    if (Initialize())
                    {
                        return true;
                    }
                }
            }
            else if (tokenList[index].ClassPart == ";" || tokenList[index].ClassPart == ",") return true;
            return false;
        }

        //Status : Complete
        private static bool List()
        {
            if( tokenList[index].ClassPart == ";" )
            {
                index++;
                return true;
            }
            else if( tokenList[index].ClassPart == "," )
            {
                index++;
                if( tokenList[index].ClassPart == "identifier" )
                {
                    index++;
                    if( Initialize() )
                    {
                        if( List() )
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        //Status : Complete
        private static bool AssignmentStatement()
        {
            if (tokenList[index].ClassPart == "identifier")
            {
                index++;
                if (X())
                {
                    if (AssignmentOperator())
                    {
                        if (OE())
                        {
                            if (tokenList[index].ClassPart == ";")
                            {
                                index++;
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        //Status : Complete 
        private static bool AssignmentOperator()
        {
            if( tokenList[index].ClassPart == "=" || tokenList[index].ClassPart == "CompoundAssignment" )
            {
                index++;
                return true;
            }
            return false;
        }

        //Status : Compelte
        private static bool While()
        {
            if( tokenList[index].ClassPart == "While" )
            {
                index++;
                if( tokenList[index].ClassPart == "(" )
                {
                    index++;
                    if (OE())
                    {
                        if (tokenList[index].ClassPart == ")")
                        {
                            index++;
                            if (tokenList[index].ClassPart == "{")
                            {
                                index++;
                                //<MultipleStatement> will be called.
                                if (MultipleStatement())
                                {
                                    if (tokenList[index].ClassPart == "}")
                                    {
                                        index++;
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        //Status : Complete 
        private static bool MultipleStatement()
        {
            if( tokenList[index].ClassPart == "}" || tokenList[index].ClassPart == "Return" )
            {
                return true;
            }
            else if( tokenList[index].ClassPart == "While" || tokenList[index].ClassPart == "For" || tokenList[index].ClassPart == "If" || tokenList[index].ClassPart == "Array" || tokenList[index].ClassPart == "DoWhile" || tokenList[index].ClassPart == "identifier" || tokenList[index].ClassPart == "~" || tokenList[index].ClassPart == "Inc-Dec" )
            {
                if (SingleStatement())
                {
                    if (MultipleStatement())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //Status : Complete
        private static bool SingleStatement()
        {
            if( tokenList[index].ClassPart == "While" )
            {
                if( While() )
                {
                    return true;
                }
            }
            else if( tokenList[index].ClassPart == "~" )
            {
                index++;
                if( tokenList[index].ClassPart == "identifier" )
                {
                    index++;
                    if (A())
                    {
                        return true;
                    }
                }               
            }
            else if( tokenList[index].ClassPart == "For" )
            {
                if( For() )
                {
                    return true;
                }
            }
            else if( tokenList[index].ClassPart == "If" )
            {
                if( If() )
                {
                    return true;
                }
            }
            else if( tokenList[index].ClassPart == "DoWhile" )
            {
                if( DoWhile() )
                {
                    return true;
                }
            }
            else if( tokenList[index].ClassPart == "Inc-Dec" )
            {
                index++;
                if( tokenList[index].ClassPart == "identifier" )
                {
                    if( X() )
                    {
                        return true;
                    }
                }
            }
            else if( tokenList[index].ClassPart == "identifier" )
            {
                index++;
                if( M() )
                {
                    return true;
                }
            }
            else if( tokenList[index].ClassPart == "Array" )
            {
                if( Array() )
                {
                    return true;
                }
            }
            else if( tokenList[index].ClassPart == "ForTrue" )
            {
                if( ForTrue() )
                {
                    return true;
                }
            }
            return false;
        }

        //Status : Complete
        private static bool A()
        {
            if( tokenList[index].ClassPart == "=" || tokenList[index].ClassPart == ";" || tokenList[index].ClassPart == "," )
            {
                if( Initialize() )
                {
                    if( List() )
                    {
                        return true;
                    }
                }
            }
            else if( tokenList[index].ClassPart == "identifier" )
            {
                index++;
                if( tokenList[index].ClassPart == "=" )
                {
                    index++;
                    if( tokenList[index].ClassPart == "new" )
                    {
                        index++;
                        if( tokenList[index].ClassPart == "identifier" )
                        {
                            index++;
                            if( tokenList[index].ClassPart == "(" )
                            {
                                index++;
                                if (ParameterList())
                                {
                                    if (tokenList[index].ClassPart == ")")
                                    {
                                        index++;
                                        if (tokenList[index].ClassPart == ";")
                                        {
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        //Status : Complete
        private static bool M()
        {
            if( tokenList[index].ClassPart == "." )
            {
                index++;
                if( tokenList[index].ClassPart == "identifier" )
                {
                    index++;
                    if( M() )
                    {
                        return true;
                    }
                }
            }
            else if( tokenList[index].ClassPart == "Inc-Dec" )
            {
                index++;
                if( tokenList[index].ClassPart == ";" )
                {
                    index++;
                    return true;
                }
            }
            else if( tokenList[index].ClassPart == "(" )
            {
                index++;
                if (ParameterList())
                {
                    if (tokenList[index].ClassPart == ")")
                    {
                        index++;
                        if (M3())
                        {
                            return true;
                        }
                    }
                }
            }
            else if( tokenList[index].ClassPart == "[" )
            {
                index++;
                if (OE())
                {
                    if (tokenList[index].ClassPart == "]")
                    {
                        index++;
                        if (M2())
                        {
                            return true;
                        }
                    }
                }
            }
            else if( tokenList[index].ClassPart == "=" || tokenList[index].ClassPart == "CompoundAssignment" )
            {
                if( ASG() )
                {
                    return true;
                }
            }
            return false;
        }

        //Status : Complete 
        private static bool M2()
        {
            if (tokenList[index].ClassPart == ".")
            {
                index++;
                if (tokenList[index].ClassPart == "identifier")
                {
                    index++;
                    if (M())
                    {
                        return true;
                    }
                }
            }
            else if (tokenList[index].ClassPart == "Inc-Dec")
            {
                index++;
                if (tokenList[index].ClassPart == ";")
                {
                    index++;
                    return true;
                }
            }
            else if (tokenList[index].ClassPart == "=" || tokenList[index].ClassPart == "CompoundAssignment")
            {
                if( ASG() )
                {
                    return true;
                }
            }
            return false;
        }

        //Status : Complete 
        private static bool M3()
        {
            if (tokenList[index].ClassPart == ".")
            {
                index++;
                if (tokenList[index].ClassPart == "identifier")
                {
                    index++;
                    if (M())
                    {
                        return true;
                    }
                }
            }
            else if( tokenList[index].ClassPart == "While" || tokenList[index].ClassPart == "~" || tokenList[index].ClassPart == "If" || tokenList[index].ClassPart == "identifier" || tokenList[index].ClassPart == "For" || tokenList[index].ClassPart == "DoWhile" || tokenList[index].ClassPart == "Inc-Dec" || tokenList[index].ClassPart == "Public" || tokenList[index].ClassPart == "Private" || tokenList[index].ClassPart == "Chained" || tokenList[index].ClassPart == "Static" || tokenList[index].ClassPart == "Void" || tokenList[index].ClassPart == "Abstract" || tokenList[index].ClassPart == "Sealed" || tokenList[index].ClassPart == "Array" || tokenList[index].ClassPart == "Class" || tokenList[index].ClassPart == "Interface" || tokenList[index].ClassPart == "$" || tokenList[index].ClassPart == "}" || tokenList[index].ClassPart == "Return" )
            {
                return true;
            }
            return false;
        }

        //Status : Complete
        private static bool ASG()
        {
            if(tokenList[index].ClassPart == "=" || tokenList[index].ClassPart == "CompoundAssignment" )
            {
                if( AssignmentOperator() )
                {
                    if( OE() )
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //Status : Complete 
        private static bool For()
        {
            if( tokenList[index].ClassPart == "For" )
            {
                index++;
                if( tokenList[index].ClassPart == "(" )
                {
                    if( C1() )
                    {
                        if( C2() )
                        {
                            if( tokenList[index].ClassPart == ";" )
                            {
                                index++;
                                if( C3() )
                                {
                                    if( tokenList[index].ClassPart == ")" )
                                    {
                                        index++;
                                        if( tokenList[index].ClassPart == "{" )
                                        {
                                            index++;
                                            if( MultipleStatement() )
                                            {
                                                if( tokenList[index].ClassPart == "}" )
                                                {
                                                    index++;
                                                    return true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        //Status : Complete 
        private static bool C1()
        {
            if( tokenList[index].ClassPart == ";" )
            {
                index++;
                return true;
            }
            else if( tokenList[index].ClassPart == "~" )
            {
                if( Declaration() )
                {
                    return true;
                }
            }
            else if( tokenList[index].ClassPart == "identifier" )
            {
                if( AssignmentStatement() )
                {
                    return true;
                }
            }
            return false;
        }

        //Status : Complete
        private static bool C2()
        {
            if( tokenList[index].ClassPart == "This" || tokenList[index].ClassPart == "Base" || tokenList[index].ClassPart == "identifier" || tokenList[index].ClassPart == "int-const" || tokenList[index].ClassPart == "bool-const" || tokenList[index].ClassPart == "char-const" || tokenList[index].ClassPart == "float-const" || tokenList[index].ClassPart == "(" || tokenList[index].ClassPart == "!" || tokenList[index].ClassPart == "Inc-Dec" )
            {
                if( OE() )
                {
                    return true;
                }
            }
            else if(tokenList[index].ClassPart == ";" )
            {
                return true;
            }
            return false;
        }

        //Status : Complete
        private static bool C3()
        {
            if(tokenList[index].ClassPart == "identifier" )
            {
                index++;
                if( X() )
                {
                    return true;
                }
            }
            else if( tokenList[index].ClassPart == "Inc-Dec" )
            {
                index++;
                if( tokenList[index].ClassPart == "identifier" )
                {
                    index++;
                    if (X())
                    {
                        if (C3Dash())
                        {
                            return true;
                        }
                    }
                }
            }
            else if( tokenList[index].ClassPart == ")" )
            {
                return true;
            }
            return false;
        }

        //Status : Complete
        private static bool C3Dash()
        {
            if( tokenList[index].ClassPart == "=" || tokenList[index].ClassPart == "CompoundAssignment" )
            {
                if( AssignmentOperator() )
                {
                    if( OE() )
                    {
                        return true;
                    }
                }
            }
            else if( tokenList[index].ClassPart == "Inc-Dec" )
            {
                index++;
                return true;
            }
            return false;
        }

        //Status : Complete
        private static bool DoWhile()
        {
            if( tokenList[index].ClassPart == "DoWhile" )
            {
                index++;
                if( tokenList[index].ClassPart == "(" )
                {
                    index++;
                    if (OE())
                    {
                        if (tokenList[index].ClassPart == ")")
                        {
                            index++;
                            if (tokenList[index].ClassPart == "{")
                            {
                                index++;
                                if (MultipleStatement())
                                {
                                    if (tokenList[index].ClassPart == "}")
                                    {
                                        index++;
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        //Status : Complete
        private static bool If()
        {
            if( tokenList[index].ClassPart == "If" )
            {
                index++;
                if( tokenList[index].ClassPart == "(" )
                {
                    index++;
                    if (OE())
                    {
                        if (tokenList[index].ClassPart == ")")
                        {
                            index++;
                            if (tokenList[index].ClassPart == "{")
                            {
                                index++;
                                if (MultipleStatement())
                                {
                                    if (tokenList[index].ClassPart == "}")
                                    {
                                        index++;
                                        if (OptionalElse())
                                        {
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        //Status : Complete 
        private static bool OptionalElse()
        {
            if( tokenList[index].ClassPart == "Else" )
            {
                index++;
                if (tokenList[index].ClassPart == "{")
                {
                    index++;
                    if (MultipleStatement())
                    {
                        if (tokenList[index].ClassPart == "}")
                        {
                            index++;
                        }
                    }
                }
            }
            else if (tokenList[index].ClassPart == "While" || tokenList[index].ClassPart == "~" || tokenList[index].ClassPart == "If" || tokenList[index].ClassPart == "identifier" || tokenList[index].ClassPart == "For" || tokenList[index].ClassPart == "DoWhile" || tokenList[index].ClassPart == "Inc-Dec" || tokenList[index].ClassPart == "Public" || tokenList[index].ClassPart == "Private" || tokenList[index].ClassPart == "Chained" || tokenList[index].ClassPart == "Static" || tokenList[index].ClassPart == "Void" || tokenList[index].ClassPart == "Abstract" || tokenList[index].ClassPart == "Sealed" || tokenList[index].ClassPart == "Array" || tokenList[index].ClassPart == "Class" || tokenList[index].ClassPart == "Interface" || tokenList[index].ClassPart == "$" || tokenList[index].ClassPart == "}" || tokenList[index].ClassPart == "Return")
            {
                return true;
            }
            return false;
        }

        //Status : Complete
        private static bool ObjectDeclaration()
        {
            if( tokenList[index].ClassPart == "~" )
            {
                index++;
                if( tokenList[index].ClassPart == "identifier" )
                {
                    index++;
                    if( tokenList[index].ClassPart == "identifier" )
                    {
                        index++;
                        if( tokenList[index].ClassPart == "=" )
                        {
                            index++;
                            if( tokenList[index].ClassPart == "New" )
                            {
                                index++;
                                if( tokenList[index].ClassPart == "identifier" )
                                {
                                    index++;
                                    if( tokenList[index].ClassPart == "(" )
                                    {
                                        index++;
                                        if (ParameterList())
                                        {
                                            if (tokenList[index].ClassPart == ")")
                                            {
                                                index++;
                                                return true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        //Status : Complete
        private static bool IncDec()
        {
            if( tokenList[index].ClassPart == "Inc-Dec" )
            {
                index++;
                if( tokenList[index].ClassPart == "identifier" )
                {
                    index++;
                    //<X> will be called.
                    if( tokenList[index].ClassPart == ";" )
                    {
                        index++;
                        return true;
                    }
                }
            }
            else if( tokenList[index].ClassPart == "identifier" )
            {
                index++;
                if (X())
                {
                    if (tokenList[index].ClassPart == "Inc-Dec" && tokenList[index + 1].ClassPart == ";")
                    {
                        index += 2;
                        return true;
                    }
                }
            }
            return false;
        }

        //Status : Complete 
        private static bool Function()
        {
            if( tokenList[index].ClassPart == "Public" || tokenList[index].ClassPart == "Private" || tokenList[index].ClassPart == "Chained" )
            {
                if( AccessModifier() && Type() && ReturnType() )
                {
                    if(tokenList[index].ClassPart == "identifier" && tokenList[index].ClassPart == "(")
                    {
                        index += 2;
                        if( Parameter() && tokenList[index].ClassPart == ")" && tokenList[index + 1].ClassPart == "{")
                        {
                            index += 2;
                            if( FunctionBody() )
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        //Status : Complete 
        private static bool AccessModifier()
        {
            if( tokenList[index].ClassPart == "Public" || tokenList[index].ClassPart == "Private" || tokenList[index].ClassPart == "Chained" )
            {
                index++;
                return true;
            }
            else if(tokenList[index].ClassPart == "Static" || tokenList[index].ClassPart == "Void" || tokenList[index].ClassPart == "identifier" )
            {
                return true;
            }
            return false;
        }

        //Status : Complete 
        private static bool Type()
        {
            if(tokenList[index].ClassPart == "Static")
            {
                index++;
                return true;
            }
            else if(tokenList[index].ClassPart == "Void" || tokenList[index].ClassPart == "identifier")
            {
                return true;
            }
            return false;
        }

        //Status : Complete 
        private static bool ReturnType()
        {
            if(tokenList[index].ClassPart == "Void" || tokenList[index].ClassPart == "identifier")
            {
                index++;
                return true;
            }
            return false;
        }

        //Status : Complete 
        private static bool Parameter()
        {
            if(tokenList[index].ClassPart == "identifier")
            {
                index++;
                if( P2() )
                {
                    return true;
                }
            }
            else if(tokenList[index].ClassPart == ")")
            {
                return true;
            }
            return false;
        }

        //Status : Complete 
        private static bool P2()
        {
            if(tokenList[index].ClassPart == ",")
            {
                index++;
                if(tokenList[index].ClassPart == "identifier")
                {
                    index++;
                    if( P2() )
                    {
                        return true;
                    }
                }
            }
            else if(tokenList[index].ClassPart == ")")
            {
                return true;
            }
            return false;
        }

        //Status : Complete 
        private static bool FunctionBody()
        {
            if(tokenList[index].ClassPart == "While" || tokenList[index].ClassPart == "~" || tokenList[index].ClassPart == "For" || tokenList[index].ClassPart == "If" || tokenList[index].ClassPart == "Array" || tokenList[index].ClassPart == "DoWhile" || tokenList[index].ClassPart == "identifier" || tokenList[index].ClassPart == "Inc-Dec" || tokenList[index].ClassPart == "Return" || tokenList[index].ClassPart == "}")
            {
                if( MultipleStatement() )
                {
                    if( Return() )
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //Status : Complete 
        private static bool Return()
        {
            if(tokenList[index].ClassPart == "Return")
            {
                index++;
                if( Return2() )
                {
                    return true;
                }
            }
            else if(tokenList[index].ClassPart == "}")
            {
                return true;
            }
            return false;
        }

        //Status : Complete
        private static bool Return2()
        {
            if(tokenList[index].ClassPart == "(")
            {
                index++;
                if (OE())
                {
                    if (tokenList[index].ClassPart == "}" && tokenList[index + 1].ClassPart == ";")
                    {
                        index += 2;
                        return true;
                    }
                }
            }
            else if(tokenList[index].ClassPart == ";")
            {
                index++;
                return true;
            }
            return false;
        }

        //Status : Complete
        private static bool Interface()
        {
            if(tokenList[index].ClassPart == "Interface")
            {
                index++;
                if(tokenList[index].ClassPart == "identifier" && tokenList[index + 1].ClassPart == "{")
                {
                    index += 2;
                    if (IBody())
                    {
                        if (tokenList[index].ClassPart == "}")
                        {
                            index++;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        //Status : Complete
        private static bool IBody()
        {
            if(tokenList[index].ClassPart == "identifier")
            {
                index++;
                if(tokenList[index].ClassPart == "identifier")
                {
                    index++;
                    if( IBody2() )
                    {
                        return true;
                    }
                }
            }
            else if(tokenList[index].ClassPart == "}")
            {
                return true;
            }
            else if(tokenList[index].ClassPart == "Void")
            {
                index++;
                if(tokenList[index].ClassPart == "identifier" && tokenList[index + 1].ClassPart == "(")
                {
                    index += 2;
                    if( Parameter() && tokenList[index].ClassPart == ";")
                    {
                        index++;
                        if( IBody() )
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        //Status : Complete
        private static bool IBody2()
        {
            if(tokenList[index].ClassPart == "{")
            {
                index++;
                if (IAccessor())
                {
                    if (tokenList[index].ClassPart == "}")
                    {
                        index++;
                        if (IBody())
                        {
                            return true;
                        }
                    }
                }
            }
            else if(tokenList[index].ClassPart == "(")
            {
                index++;
                if(Parameter() && tokenList[index].ClassPart == ")" && tokenList[index + 1].ClassPart == ";")
                {
                    index += 2;
                    if( IBody() )
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //Status : Complete
        private static bool IAccessor()
        {
            if(tokenList[index].ClassPart == "Get")
            {
                index++;
                if(tokenList[index].ClassPart == ";")
                {
                    index++;
                    if( IAccessor2() )
                    {
                        return true;
                    }
                }
            }
            else if(tokenList[index].ClassPart == "Set")
            {
                index++;
                if (tokenList[index].ClassPart == ";")
                {
                    index++;
                    if( IAccessor3() )
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //Status : Complete
        private static bool IAccessor2()
        {
            if( tokenList[index].ClassPart == "Set" )
            {
                index++;
                if(tokenList[index].ClassPart == ";")
                {
                    index++;
                    return true;
                }
            }
            else if(tokenList[index].ClassPart == "}")
            {
                return true;
            }
            return false;
        }

        //Status : Complete
        private static bool IAccessor3()
        {
            if(tokenList[index].ClassPart == "Get")
            {
                index++;
                if( tokenList[index].ClassPart == ";" )
                {
                    index++;
                    return true;
                }
            }
            else if(tokenList[index].ClassPart == "}")
            {
                return true;
            }
            return false;
        }

        //Status : Complete
        private static bool Class()
        {
            if(tokenList[index].ClassPart == "Abstract" || tokenList[index].ClassPart == "Sealed" || tokenList[index].ClassPart == "Class")
            {
                if (Sealed())
                {
                    if (tokenList[index].ClassPart == "Class")
                    {
                        index++;
                        if (tokenList[index].ClassPart == "identifier")
                        {
                            index++;
                            if (Inherit())
                            {
                                if (tokenList[index].ClassPart == "{")
                                {
                                    index++;
                                    if (CBody())
                                    {
                                        if (tokenList[index].ClassPart == "}")
                                        {
                                            index++;
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        //Status : Complete
        private static bool Sealed()
        {
            if(tokenList[index].ClassPart == "Sealed" || tokenList[index].ClassPart == "Abstract")
            {
                index++;
                return true;
            }
            else if(tokenList[index].ClassPart == "Class")
            {
                return true;
            }
            return false;
        }

        //Static : Complete
        private static bool Inherit()
        {
            if(tokenList[index].ClassPart == "->")
            {
                index++;
                if(tokenList[index].ClassPart == "identifier")
                {
                    index++;
                    if( MultipleInherit() )
                    {
                        return true;
                    }
                }
            }
            else if(tokenList[index].ClassPart == "{")
            {
                return true;
            }
            return false;
        }

        //Status : Complete
        private static bool MultipleInherit()
        {
            if(tokenList[index].ClassPart == ",")
            {
                index++;
                if(tokenList[index].ClassPart == "identifier")
                {
                    index++;
                    if( MultipleStatement() )
                    {
                        return true;
                    }
                }
            }
            else if(tokenList[index].ClassPart == "{")
            {
                return true;
            }
            return false;
        }

        //Statuc : Complete
        private static bool CBody()
        {
            if(tokenList[index].ClassPart == "Public" || tokenList[index].ClassPart == "Private" || tokenList[index].ClassPart == "Chained")
            {
                if( AM1() )
                {
                    if( OC() )
                    {
                        return true;
                    }
                }
            }
            else if(tokenList[index].ClassPart == "Static")
            {
                index++;
                if( CB1() )
                {
                    return true;
                }
            }
            else if(tokenList[index].ClassPart == "Constructor")
            {
                if (Constructor())
                {
                    if (CBody())
                    {
                        return true;
                    }
                }
            }
            else if(tokenList[index].ClassPart == "}")
            {
                return true;
            }    
            else if(tokenList[index].ClassPart == "~")
            {
                index++;
                if(tokenList[index].ClassPart == "identifier")
                {
                    index++;
                    if( OB() )
                    {
                        return true;
                    }
                }
            }
            else if(tokenList[index].ClassPart == "Array")
            {
                if (Array())
                {
                    if (CBody())
                    {
                        return true;
                    }
                }
            }
            else if(tokenList[index].ClassPart == "identifier")
            {
                index++;
                if(tokenList[index].ClassPart == "identifier")
                {
                    index++;
                    if( CB2() )
                    {
                        return true;
                    }
                }
            }
            else if(tokenList[index].ClassPart == "Void")
            {
                index++;
                if(tokenList[index].ClassPart == "identifier" && tokenList[index + 1].ClassPart == "(")
                {
                    index += 2;
                    if( Parameter() )
                    {
                        if(tokenList[index].ClassPart == ")" && tokenList[index + 1].ClassPart == "{")
                        {
                            index += 2;
                            if( FunctionBody() )
                            {
                                if(tokenList[index].ClassPart == "}")
                                {
                                    index++;
                                    if( CBody() )
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        //Status : Complete
        private static bool OC()
        {
            if(tokenList[index].ClassPart == "Static")
            {
                index++;
                if( CB1() )
                {
                    return true;
                }
            }
            else if(tokenList[index].ClassPart == "identifier" || tokenList[index].ClassPart == "Void" || tokenList[index].ClassPart == "Array" || tokenList[index].ClassPart == "~")
            {
                return true;
            }
            return false;
        }

        //Status : Complete
        private static bool OB()
        {
            if(tokenList[index].ClassPart == "=" || tokenList[index].ClassPart == ";" || tokenList[index].ClassPart == ",")
            {
                if( Initialize() )
                {
                    if( List() )
                    {
                        if( CBody() )
                        {
                            return true;
                        }
                    }
                }
            }
            else if(tokenList[index].ClassPart == "identifier")
            {
                index++;
                if(tokenList[index].ClassPart == "=" && tokenList[index + 1].ClassPart == "New" && tokenList[index + 2].ClassPart == "identifier" && tokenList[index + 3].ClassPart == "(")
                {
                    index += 4;
                    if (ParameterList())
                    {
                        if (tokenList[index].ClassPart == ")" && tokenList[index + 1].ClassPart == ";")
                        {
                            index += 2;
                            if (CBody())
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        //Status : Complete
        private static bool AM1()
        {
            if(tokenList[index].ClassPart == "Public" || tokenList[index].ClassPart == "Private" || tokenList[index].ClassPart == "Chained" )
            {
                index++;
                return true;
            }
            return false;
        }

        //Status : Complete
        private static bool CB1()
        {
            if(tokenList[index].ClassPart == "identifier")
            {
                index++;
                if(tokenList[index].ClassPart == "identifier")
                {
                    index++;
                    if( CB2() )
                    {
                        return true;
                    }
                }
            }
            else if(tokenList[index].ClassPart == "Void")
            {
                index++;
                if (tokenList[index].ClassPart == "identifier" && tokenList[index + 1].ClassPart == "(")
                {
                    index += 2;
                    if (Parameter())
                    {
                        if (tokenList[index].ClassPart == ")" && tokenList[index + 1].ClassPart == "{")
                        {
                            index += 2;
                            if (FunctionBody())
                            {
                                if (tokenList[index].ClassPart == "}")
                                {
                                    index++;
                                    if (CBody())
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if( tokenList[index].ClassPart == "~" )
            {
                if( Declaration() )
                {
                    if( CBody() )
                    {
                        return true;
                    }
                }
            }
            else if(tokenList[index].ClassPart == "Array")
            {
                if (Array())
                {
                    if (CBody())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //Status : Complete
        private static bool CB2()
        {
            if(tokenList[index].ClassPart == "{")
            {
                index++;
                if (PropertyBody())
                {
                    if (tokenList[index].ClassPart == "}")
                    {
                        index++;
                        if (CBody())
                        {
                            return true;
                        }
                    }
                }
            }
            else if(tokenList[index].ClassPart == ".")
            {
                index++;
                if(tokenList[index].ClassPart == "identifier" && tokenList[index + 1].ClassPart == "{")
                {
                    index += 2;
                    if (PropertyBody())
                    {
                        if (tokenList[index].ClassPart == "}")
                        {
                            index++;
                            if (CBody())
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            else if(tokenList[index].ClassPart == "(")
            {
                index++;
                if( Parameter() )
                {
                    if(tokenList[index].ClassPart == ")" && tokenList[index + 1].ClassPart == "{")
                    {
                        index += 2;
                        if( FunctionBody() )
                        {
                            if(tokenList[index].ClassPart == "}")
                            {
                                index++;
                                if( CBody() )
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        //Status : Complete
        private static bool PropertyBody()
        {
            if(tokenList[index].ClassPart == "Get")
            {
                index++;
                if( PB1() )
                {
                    return true;
                }
            }
            else if(tokenList[index].ClassPart == "Set")
            {
                index++;
                if( PB3() )
                {
                    return true;
                }
            }
            return false;
        }

        //Status : Complete
        private static bool PB1()
        {
            if(tokenList[index].ClassPart == "{")
            {
                index++;
                if (GetBody())
                {
                    if (tokenList[index].ClassPart == "}")
                    {
                        index++;
                        if( PB2() )
                        {
                            return true;
                        }
                    }
                }
            }
            else if(tokenList[index].ClassPart == ";")
            {
                index++;
                if( V() )
                {
                    return true;
                }
            }
            return false;
        }

        //Status : Complete
        private static bool V()
        {
            if(tokenList[index].ClassPart == "Set")
            {
                index++;
                if(tokenList[index].ClassPart == ";")
                {
                    index++;
                    return true;
                }
            }
            else if(tokenList[index].ClassPart == "}")
            {
                return true;
            }
            return false;
        }

        //Status : Complete
        private static bool PB2()
        {
            if (tokenList[index].ClassPart == "Set")
            {
                index++;
                if (tokenList[index].ClassPart == "{")
                {
                    index++;
                    if (MultipleStatement())
                    {
                        if (tokenList[index].ClassPart == "}")
                        {
                            index++;
                            return true;
                        }
                    }
                }
            }
            else if (tokenList[index].ClassPart == "}")
            {
                return true;
            }
            return false;
        }

        //Status : Complete
        private static bool PB3()
        {
            if(tokenList[index].ClassPart == "{")
            {
                index++;
                if( MultipleStatement() && tokenList[index].ClassPart == "}")
                {
                    index++;
                    if( PB4() )
                    {
                        return true;
                    }
                }
            }
            else if(tokenList[index].ClassPart == ";")
            {
                index++;
                if( V1() )
                {
                    return true;
                }
            }
            return false;
        }

        //Status : Complete
        private static bool V1()
        {
            if(tokenList[index].ClassPart == "Get")
            {
                index++;
                if(tokenList[index].ClassPart == ";")
                {
                    index++;
                    return true;
                }
            }
            else if(tokenList[index].ClassPart == "}")
            {
                return true;
            }
            return false;
        }

        //Status : Complete
        private static bool PB4()
        {
            if(tokenList[index].ClassPart == "Get")
            {
                index++;
                if(tokenList[index].ClassPart == "{")
                {
                    index++;
                    if (GetBody())
                    {
                        if (tokenList[index].ClassPart == "}")
                        {
                            index++;
                            return true;
                        }
                    }
                }
            }
            else if(tokenList[index].ClassPart == "}")
            {
                return true;
            }
            return false;
        }

        //Status : Complete
        private static bool GetBody()
        {
            if(tokenList[index].ClassPart == "~" || tokenList[index].ClassPart == "identifier" || tokenList[index].ClassPart == "While" || tokenList[index].ClassPart == "DoWhile" || tokenList[index].ClassPart == "Array" || tokenList[index].ClassPart == "Inc-Dec" || tokenList[index].ClassPart == "Return" || tokenList[index].ClassPart == "If" || tokenList[index].ClassPart == "For" || tokenList[index].ClassPart == "ForTrue" )
            {
                if( MultipleStatement() )
                {
                    if( GetReturn() )
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //Status : Complete
        private static bool GetReturn()
        {
            if(tokenList[index].ClassPart == "Return")
            {
                index++;
                if(tokenList[index].ClassPart == "(")
                {
                    if (OE())
                    {
                        if (tokenList[index].ClassPart == ")" && tokenList[index + 1].ClassPart == ";")
                        {
                            index += 2;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        //Status : Complete
        private static bool Constructor()
        {
            if(tokenList[index].ClassPart == "Constructor")
            {
                index++;
                if(tokenList[index].ClassPart == "identifier" && tokenList[index + 1].ClassPart == "(")
                {
                    index += 2;
                    if( Parameter() )
                    {
                        if(tokenList[index].ClassPart == ")" && tokenList[index + 1].ClassPart == "{")
                        {
                            index += 2;
                            if( MultipleStatement() )
                            {
                                if(tokenList[index].ClassPart == "}")
                                {
                                    index++;
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        //Status : Complete
        private static bool OE()
        {
            if(tokenList[index].ClassPart == "This" || tokenList[index].ClassPart == "string-const" || tokenList[index].ClassPart == "int-const" || tokenList[index].ClassPart == "Base" || tokenList[index].ClassPart == "identifier" || tokenList[index].ClassPart == "float-const" || tokenList[index].ClassPart == "char-const" || tokenList[index].ClassPart == "bool-const" || tokenList[index].ClassPart == "(" || tokenList[index].ClassPart == "Inc-Dec" || tokenList[index].ClassPart == "!")
            {
                if( AE() )
                {
                    if( OE1() )
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //Status : Complete
        private static bool OE1()
        {
            if(tokenList[index].ClassPart == "||")
            {
                index++;
                if (AE())
                {
                    if (OE1())
                    {
                        return true;
                    }
                }
            }
            else if(tokenList[index].ClassPart == "," || tokenList[index].ClassPart == ")" || tokenList[index].ClassPart == "]" || tokenList[index].ClassPart == "*" || tokenList[index].ClassPart == ";" || tokenList[index].ClassPart == "=")
            {
                return true;
            }
            return false;
        }

        //Status : Complete
        private static bool AE()
        {
            if (tokenList[index].ClassPart == "This" || tokenList[index].ClassPart == "string-const" || tokenList[index].ClassPart == "int-const" || tokenList[index].ClassPart == "Base" || tokenList[index].ClassPart == "identifier" || tokenList[index].ClassPart == "float-const" || tokenList[index].ClassPart == "char-const" || tokenList[index].ClassPart == "bool-const" || tokenList[index].ClassPart == "(" || tokenList[index].ClassPart == "Inc-Dec" || tokenList[index].ClassPart == "!")
            {
                if( RE() )
                {
                    if( AE1() )
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //Status : Complete
        private static bool AE1()
        {
            if( tokenList[index].ClassPart == "&&" )
            {
                if (RE())
                {
                    if (AE1())
                    {
                        return true;
                    }
                }
            }
            else if(tokenList[index].ClassPart == "||" || tokenList[index].ClassPart == "," || tokenList[index].ClassPart == ")" || tokenList[index].ClassPart == "]" || tokenList[index].ClassPart == "*" || tokenList[index].ClassPart == ";" || tokenList[index].ClassPart == "=")
            {
                return true;
            }
            return false;
        }

        //Status : Complete
        private static bool RE()
        {
            if (tokenList[index].ClassPart == "This" || tokenList[index].ClassPart == "string-const" || tokenList[index].ClassPart == "int-const" || tokenList[index].ClassPart == "Base" || tokenList[index].ClassPart == "identifier" || tokenList[index].ClassPart == "float-const" || tokenList[index].ClassPart == "char-const" || tokenList[index].ClassPart == "bool-const" || tokenList[index].ClassPart == "(" || tokenList[index].ClassPart == "Inc-Dec" || tokenList[index].ClassPart == "!")
            {
                if( E() )
                {
                    if( RE1() )
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //Status : Complete
        private static bool RE1()
        {
            if( tokenList[index].ClassPart == "ro" )
            {
                index++;
                if (E())
                {
                    if (RE1())
                    {
                        return true;
                    }
                }
            }
            else if( tokenList[index].ClassPart == "&&" || tokenList[index].ClassPart == "||" || tokenList[index].ClassPart == "," || tokenList[index].ClassPart == ")" || tokenList[index].ClassPart == "]" || tokenList[index].ClassPart == "*" || tokenList[index].ClassPart == ";" || tokenList[index].ClassPart == "=")
            {
                return true;
            }
            return false;
        }

        //Status : Complete
        private static bool E()
        {
            if (tokenList[index].ClassPart == "This" || tokenList[index].ClassPart == "string-const" || tokenList[index].ClassPart == "int-const" || tokenList[index].ClassPart == "Base" || tokenList[index].ClassPart == "identifier" || tokenList[index].ClassPart == "float-const" || tokenList[index].ClassPart == "char-const" || tokenList[index].ClassPart == "bool-const" || tokenList[index].ClassPart == "(" || tokenList[index].ClassPart == "Inc-Dec" || tokenList[index].ClassPart == "!")
            {
                if( T() )
                {
                    if( E1() )
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //Status : Complete
        private static bool E1()
        {
            if( tokenList[index].ClassPart == "pm" )
            {
                index++;
                if (T())
                {
                    if (E1())
                    {
                        return true;
                    }
                }
            }
            else if ( tokenList[index].ClassPart == "ro" || tokenList[index].ClassPart == "&&" || tokenList[index].ClassPart == "||" || tokenList[index].ClassPart == "," || tokenList[index].ClassPart == ")" || tokenList[index].ClassPart == "]" || tokenList[index].ClassPart == "*" || tokenList[index].ClassPart == ";" || tokenList[index].ClassPart == "=")
            {
                return true;
            }
            return false;
        }

        //Status : Complete
        private static bool T()
        {
            if (tokenList[index].ClassPart == "This" || tokenList[index].ClassPart == "string-const" || tokenList[index].ClassPart == "int-const" || tokenList[index].ClassPart == "Base" || tokenList[index].ClassPart == "identifier" || tokenList[index].ClassPart == "float-const" || tokenList[index].ClassPart == "char-const" || tokenList[index].ClassPart == "bool-const" || tokenList[index].ClassPart == "(" || tokenList[index].ClassPart == "Inc-Dec" || tokenList[index].ClassPart == "!")
            {
                if( F() )
                {
                    if( T1() )
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //Status : Complete
        private static bool T1()
        {
            if( tokenList[index].ClassPart == "mdm" )
            {
                index++;
                if (F())
                {
                    if (T1())
                    {
                        return true;
                    }
                }
            }
            else if ( tokenList[index].ClassPart == "pm" || tokenList[index].ClassPart == "ro" || tokenList[index].ClassPart == "&&" || tokenList[index].ClassPart == "||" || tokenList[index].ClassPart == "," || tokenList[index].ClassPart == ")" || tokenList[index].ClassPart == "]" || tokenList[index].ClassPart == "*" || tokenList[index].ClassPart == ";" || tokenList[index].ClassPart == "=")
            {
                return true;
            }
            return false;
        }

        //Status : Complete
        private static bool F()
        {
            if( tokenList[index].ClassPart == "This" )
            {
                index++;
                if(tokenList[index].ClassPart == "." && tokenList[index + 1].ClassPart == "identifier")
                {
                    index += 2;
                    if( Z() )
                    {
                        return true;
                    }
                }
            }
            else if(tokenList[index].ClassPart == "Base" )
            {
                index++;
                if (tokenList[index].ClassPart == "." && tokenList[index + 1].ClassPart == "identifier")
                {
                    index += 2;
                    if( Z() )
                    {
                        return true;
                    }
                }
            }
            else if( tokenList[index].ClassPart == "identifier" )
            {
                index++;
                if( Z() )
                {
                    return true;
                }
            }
            else if( tokenList[index].ClassPart == "int-const" || tokenList[index].ClassPart == "float-const" || tokenList[index].ClassPart == "char-const" || tokenList[index].ClassPart == "string-const" || tokenList[index].ClassPart == "bool-const" )
            {
                index++;
                return true;
            }
            else if( tokenList[index].ClassPart == "(" )
            {
                index++;
                if( OE() )
                {
                    if( tokenList[index].ClassPart == ")" )
                    {
                        index++;
                        return true;
                    }
                }
            }
            else if( tokenList[index].ClassPart == "!" )
            {
                index++;
                if( F() )
                {
                    return true;
                }
            }
            else if( tokenList[index].ClassPart == "Inc-Dec" )
            {
                index++;
                if( tokenList[index].ClassPart == "identifier" )
                {
                    index++;
                    if( X() )
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //Status : Complete
        private static bool Z()
        {
            if( tokenList[index].ClassPart == "." )
            {
                index++;
                if( tokenList[index].ClassPart == "identifier" )
                {
                    index++;
                    if( Z() )
                    {
                        return true;
                    }
                }
            }
            else if( tokenList[index].ClassPart == "(" )
            {
                index++;
                if (ParameterList())
                {
                    if (tokenList[index].ClassPart == ")")
                    {
                        index++;
                        if (Z3())
                        {
                            return true;
                        }
                    }
                }
            }
            else if(tokenList[index].ClassPart == "[")
            {
                index++;
                if( OE() )
                {
                    if(tokenList[index].ClassPart == "]")
                    {
                        index++;
                        if( Z2() )
                        {
                            return true;
                        }
                    }
                }
            }
            else if(tokenList[index].ClassPart == "Inc-Dec")
            {
                index++;
                return true;
            }
            else if (tokenList[index].ClassPart == "mdm" || tokenList[index].ClassPart == "pm" || tokenList[index].ClassPart == "ro" || tokenList[index].ClassPart == "&&" || tokenList[index].ClassPart == "||" || tokenList[index].ClassPart == "," || tokenList[index].ClassPart == ")" || tokenList[index].ClassPart == "]" || tokenList[index].ClassPart == "*" || tokenList[index].ClassPart == ";" || tokenList[index].ClassPart == "=")
            {
                return true;
            }
            return false;
        }

        //Status : Complete
        private static bool Z2()
        {
            if( tokenList[index].ClassPart == "." )
            {
                index++;
                if( tokenList[index].ClassPart == "identifier" )
                {
                    index++;
                    if( Z() )
                    {
                        return true;
                    }
                }
            }
            else if( tokenList[index].ClassPart == "Inc-Dec" )
            {
                index++;
                return true;
            }
            else if (tokenList[index].ClassPart == "mdm" || tokenList[index].ClassPart == "pm" || tokenList[index].ClassPart == "ro" || tokenList[index].ClassPart == "&&" || tokenList[index].ClassPart == "||" || tokenList[index].ClassPart == "," || tokenList[index].ClassPart == ")" || tokenList[index].ClassPart == "]" || tokenList[index].ClassPart == "*" || tokenList[index].ClassPart == ";" || tokenList[index].ClassPart == "=")
            {
                return true;
            }
            return false;
        }

        //Status : Complete
        private static bool Z3()
        {
            if( tokenList[index].ClassPart == "." )
            {
                index++;
                if( tokenList[index].ClassPart == "identifier" )
                {
                    index++;
                    if( Z() )
                    {
                        return true;
                    }
                }
            }
            else if (tokenList[index].ClassPart == "mdm" || tokenList[index].ClassPart == "pm" || tokenList[index].ClassPart == "ro" || tokenList[index].ClassPart == "&&" || tokenList[index].ClassPart == "||" || tokenList[index].ClassPart == "," || tokenList[index].ClassPart == ")" || tokenList[index].ClassPart == "]" || tokenList[index].ClassPart == "*" || tokenList[index].ClassPart == ";" || tokenList[index].ClassPart == "=")
            {
                return true;
            }
            return false;
        }

        //Status : Complete
        private static bool Array()
        {
            if( tokenList[index].ClassPart == "Array" )
            {
                index++;
                if( tokenList[index].ClassPart == "[" )
                {
                    index++;
                    if( Array2() )
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //Status : Complete
        private static bool Array2()
        {
            if( tokenList[index].ClassPart == "]" )
            {
                index++;
                if( tokenList[index].ClassPart == "identifier" && tokenList[index + 1].ClassPart == "=" && tokenList[index + 2].ClassPart == "[")
                {
                    index += 3;
                    if (ArrayElements())
                    {
                        if (tokenList[index].ClassPart == "]")
                        {
                            index++;
                            if (tokenList[index].ClassPart == ";")
                            {
                                index++;
                                return true;
                            }
                        }
                    }
                }
            }
            else if (tokenList[index].ClassPart == "This" || tokenList[index].ClassPart == "string-const" || tokenList[index].ClassPart == "int-const" || tokenList[index].ClassPart == "Base" || tokenList[index].ClassPart == "identifier" || tokenList[index].ClassPart == "float-const" || tokenList[index].ClassPart == "char-const" || tokenList[index].ClassPart == "bool-const" || tokenList[index].ClassPart == "(" || tokenList[index].ClassPart == "Inc-Dec" || tokenList[index].ClassPart == "!")
            {
                if( OE() )
                {
                    if( tokenList[index].ClassPart == "]" )
                    {
                        index++;
                        if( tokenList[index].ClassPart == "identifier" && tokenList[index + 1].ClassPart == ";" )
                        {
                            index += 2;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        //Status : Complete
        private static bool ArrayElements()
        {
            if (tokenList[index].ClassPart == "This" || tokenList[index].ClassPart == "string-const" || tokenList[index].ClassPart == "int-const" || tokenList[index].ClassPart == "Base" || tokenList[index].ClassPart == "identifier" || tokenList[index].ClassPart == "float-const" || tokenList[index].ClassPart == "char-const" || tokenList[index].ClassPart == "bool-const" || tokenList[index].ClassPart == "(" || tokenList[index].ClassPart == "Inc-Dec" || tokenList[index].ClassPart == "!")
            {
                if( OE() )
                {
                    if( ArrayElements2() )
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //Status : Complete
        private static bool ArrayElements2()
        {
            if( tokenList[index].ClassPart == "," )
            {
                index++;
                if( ArrayElements() )
                {
                    return true;
                }
            }
            else if( tokenList[index].ClassPart == "]" )
            {
                return true;
            }
            return false;
        }

        //Status : Complete
        private static bool X()
        {
            if( tokenList[index].ClassPart == "." )
            {
                index++;
                if(tokenList[index].ClassPart == "identifier")
                {
                    index++;
                    if( X() )
                    {
                        return true;
                    }
                }
            }
            else if(tokenList[index].ClassPart == "(")
            {
                index++;
                if (ParameterList())
                {
                    if (tokenList[index].ClassPart == ")")
                    {
                        index++;
                        if (X1())
                        {
                            return true;
                        }
                    }
                }
            }
            else if(tokenList[index].ClassPart == "[")
            {
                index++;
                if( OE() )
                {
                    if(tokenList[index].ClassPart == "]")
                    {
                        if( X2() )
                        {
                            return true;
                        }
                    }
                }
            }
            else if(tokenList[index].ClassPart == "mdm" || tokenList[index].ClassPart == "pm" || tokenList[index].ClassPart == "ro" || tokenList[index].ClassPart == "&&" || tokenList[index].ClassPart == "||" || tokenList[index].ClassPart == "," || tokenList[index].ClassPart == ")" || tokenList[index].ClassPart == "]" || tokenList[index].ClassPart == "*" || tokenList[index].ClassPart == ";" || tokenList[index].ClassPart == "=" || tokenList[index].ClassPart == "CompoundAssignment" || tokenList[index].ClassPart == "Inc-Dec")
            {
                return true;
            }
            return false;
        }

        //Status : Complete
        private static bool X1()
        {
            if(tokenList[index].ClassPart == ".")
            {
                index++;
                if (tokenList[index].ClassPart == "identifier")
                {
                    if (X())
                    {
                        return true;
                    }
                }
            }
            else if(tokenList[index].ClassPart == "[")
            {
                index++;
                if( OE() )
                {
                    if(tokenList[index].ClassPart == "]")
                    {
                        index++;
                        if( X2() )
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        //Status : Complete
        private static bool X2()
        {
            if(tokenList[index].ClassPart == ".")
            {
                index++;
                if(tokenList[index].ClassPart == "identifier")
                {
                    index++;
                    if( X() )
                    {
                        return true;
                    }
                }
            }
            else if (tokenList[index].ClassPart == "mdm" || tokenList[index].ClassPart == "pm" || tokenList[index].ClassPart == "ro" || tokenList[index].ClassPart == "&&" || tokenList[index].ClassPart == "||" || tokenList[index].ClassPart == "," || tokenList[index].ClassPart == ")" || tokenList[index].ClassPart == "]" || tokenList[index].ClassPart == "*" || tokenList[index].ClassPart == ";" || tokenList[index].ClassPart == "=" || tokenList[index].ClassPart == "CompoundAssignment" || tokenList[index].ClassPart == "Inc-Dec")
            {
                return true;
            }
            return false;
        }

        //Status : Complete
        private static bool ParameterList()
        {
            if (tokenList[index].ClassPart == "This" || tokenList[index].ClassPart == "string-const" || tokenList[index].ClassPart == "int-const" || tokenList[index].ClassPart == "Base" || tokenList[index].ClassPart == "identifier" || tokenList[index].ClassPart == "float-const" || tokenList[index].ClassPart == "char-const" || tokenList[index].ClassPart == "bool-const" || tokenList[index].ClassPart == "(" || tokenList[index].ClassPart == "Inc-Dec" || tokenList[index].ClassPart == "!")
            {
                if( OE() )
                {
                    if( PL() )
                    {
                        return true;
                    }
                }
            }
            else if( tokenList[index].ClassPart == ")" )
            {
                return true;
            }
            return false;
        }

        //Status : Complete
        private static bool PL()
        {
            if( tokenList[index].ClassPart == "," )
            {
                index++;
                if( OE() )
                {
                    if( PL() )
                    {
                        return true;
                    }
                }
            }
            else if( tokenList[index].ClassPart == ")" )
            {
                return true;
            }
            return false;
        }

        //Status : Complete
        private static bool ForTrue()
        {
            if( tokenList[index].ClassPart == "ForTrue" )
            {
                index++;
                if( tokenList[index].ClassPart == "(" )
                {
                    index++;
                    if( OE() )
                    {
                        if( tokenList[index].ClassPart == ")" && tokenList[index + 1].ClassPart == "{")
                        {
                            index += 2;
                            if( MultipleStatement() )
                            {
                                if(tokenList[index].ClassPart == "}")
                                {
                                    index++;
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        //Status : Complete
        private static bool Method()
        {
            if (tokenList[index].ClassPart == "Method")
            {
                index++;
                if (ReturnType() && tokenList[index].ClassPart == "identifier" && tokenList[index + 1].ClassPart == "(" )
                {
                    index += 2;
                    if( Parameter() && tokenList[index].ClassPart == ")")
                    {
                        index++;
                        if( tokenList[index].ClassPart == "{" )
                        {
                            index++;
                            if( FunctionBody() )
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        //Status : Complete
        private static bool Start()
        {
            if(tokenList[index].ClassPart == "While" || tokenList[index].ClassPart == "~" || tokenList[index].ClassPart == "For" || tokenList[index].ClassPart == "If" || tokenList[index].ClassPart == "Array" || tokenList[index].ClassPart == "ForFirst" || tokenList[index].ClassPart == "identifier" || tokenList[index].ClassPart == "Inc-Dec")
            {
                if( SingleStatement() )
                {
                    if( Start() )
                    {
                        return true;
                    }
                }
            }
            else if(tokenList[index].ClassPart == "Method")
            {
                if( Method() )
                {
                    if( Start() )
                    {
                        return true;
                    }
                }
            }
            else if(tokenList[index].ClassPart == "Abstract" || tokenList[index].ClassPart == "Sealed" || tokenList[index].ClassPart == "Class")
            {
                if( Class() )
                {
                    if( Start() )
                    {
                        return true;
                    }
                }
            }
            else if(tokenList[index].ClassPart == "Interface")
            {
                if( Interface() )
                {
                    if( Start() )
                    {
                        return true;
                    }
                }
            }
            else if(tokenList[index].ClassPart == "$")
            {
                return true;
            }
            return false;
        }

        //Status : Complete
        public static bool SyntaxAnalyzer()
        {
            if( Start() )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
