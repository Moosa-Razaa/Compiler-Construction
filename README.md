# COMPILER CONSTRUCTION IMPLEMENTATION

### Lexical Analyzer

This part of the Compiler Construction parses the code and breaks it down into sub-tokens and attached the type with it. 
Suppose:

`1. int a = 25;`

Lexical Analyzer will break it down as:

Type | Value | Line Number
-----|-------|------------|
Keyword | int | 1
variable | a | 1 
Equals | = | 1
Number Literal | 25 | 1
Semi-Colon | ; | 1

In English, it is like separating parts of speech and making tokens from it.

### Syntax Analyzer

This part of the compiler consumes the tokens formed in Lexical Analyzer and try to verify the syntax. In other words, if we talk in English language, the general and base formula is:

`Subject + Verb + Object`

So, Syntax Analyzer will verify whether the current tokens are in order that they make a valid English syntax.
Same analogy works in Compilers as well, Syntax Analyzer verifies that tokens forms a valid language syntax.

### Semantic Analyzer

In English, 

`Chicken eats human`

This sentence perfectly fits the general English sentence formula which is mentioned above, but the meaning of the sentence doesn't makes any sense. The same way, Semantic Analyzer verifies that the tokens form a relevant meaning. Some basic tasks of Semantic Analyzer are:

- Checking whether the variable exists in the current context.
- Verifying that correct value is being feeded to the variable, for example, number literal is being assigned to a variable of type string.
- Verifying the accessibility of class members and much more.
