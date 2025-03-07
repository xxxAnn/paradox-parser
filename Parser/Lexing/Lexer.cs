
namespace Paradox.Lexing 
{
    using System;
    using System.Collections.Generic;
    using Pidgin;
    using static Pidgin.Parser;

    public static class Tokenizer
    {
        // Parser for identifiers: sequences of letters and digits starting with a letter
        private static readonly Parser<char, Token> Identifier =
            Map(
                value => new Token(TokenType.Identifier, value),
                Letter.Then(LetterOrDigit.ManyString(), (first, rest) => first + rest)
            );

        // Parser for the '=' symbol
        private static readonly Parser<char, Token> EqualSign =
            Char('=').ThenReturn(new Token(TokenType.EqualSign));

        // Parser for the '{' symbol
        private static readonly Parser<char, Token> BracketOpen =
            Char('{').ThenReturn(new Token(TokenType.BracketOpen));

        // Parser for the '}' symbol
        private static readonly Parser<char, Token> BracketClose =
            Char('}').ThenReturn(new Token(TokenType.BracketClose));

        // Parser for the '.' symbol
        private static readonly Parser<char, Token> Dot =
            Char('.').ThenReturn(new Token(TokenType.Dot));

        // Parser for the ':' symbol
        private static readonly Parser<char, Token> Colon =
            Char(':').ThenReturn(new Token(TokenType.Colon));

        // Combined parser for all token types
        private static readonly Parser<char, Token> TokenParser =
            OneOf(Identifier, EqualSign, BracketOpen, BracketClose, Dot, Colon);

        // Parser that skips whitespace
        private static readonly Parser<char, Unit> SkipWhitespace =
            SkipWhitespaces;

        // Main parser that tokenizes the input
        public static readonly Parser<char, IEnumerable<Token>> TokenizerParser =
            TokenParser.SeparatedAndOptionallyTerminated(SkipWhitespace);

        // Method to tokenize input text
        public static IEnumerable<Token> Tokenize(string input)
        {
            var result = TokenizerParser.Parse(input);
            if (result.Success)
            {
                return result.Value;
            }
            else
            {
                throw new Exception($"Parsing failed: {result.Error}");
            }
        }
    }
}