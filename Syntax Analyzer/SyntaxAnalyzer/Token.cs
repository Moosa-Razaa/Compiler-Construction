using System;

namespace SyntaxAnalyzer
{
    class Token
    {
        public string ClassPart { get; set; }
        public string ValuePart { get; set; }
        public int LineNumber { get; set; }
    }
}
