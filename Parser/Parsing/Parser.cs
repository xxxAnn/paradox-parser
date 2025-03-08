using System;
using System.Collections.Generic;
using System.Linq;
using Pidgin;
using static Pidgin.Parser;
using Paradox.Lexing;
using static Paradox.Parsing.Tree<string>;

namespace Paradox.Parsing
{
    public static class Parser
    {
        private static Parser<Token, Token> Token(TokenType type) => 
            Parser<Token>.Token(t => t.Type == type);

        private static readonly Parser<Token, string> Identifier =
            Token(TokenType.Identifier).Select(t => t.Value);

        private static readonly Parser<Token, Node> Expression = null!;

        private static readonly Parser<Token, Node> ScopeNode =
            Try(
                from key in Identifier
                from eq in Token(TokenType.EqualSign)
                from open in Token(TokenType.BracketOpen)
                from elements in Rec(() => Expression).AtLeastOnce()
                from close in Token(TokenType.BracketClose)
                select new Tree<string>.Node(new Tree<string>.ScopeNode(key, elements.ToList()))
            );

        private static readonly Parser<Token, Node> LeafNode =
            Identifier.Select(id => new Node(new LeafNode(id)));

        static Parser()
        {
            Expression = ScopeNode.Or(LeafNode);
        }

        public static Tree<string> Parse(IEnumerable<Token> tokens)
        {
            var tokenArray = tokens as Token[] ?? tokens.ToArray();
            var result = Expression.ParseOrThrow(tokenArray);
            return new Tree<string>(result);
        }
    }
}