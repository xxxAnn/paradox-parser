namespace Paradox.Lexing
{
    public enum TokenType
    {
        Identifier,
        BracketOpen,
        BracketClose,
        EqualSign,
        Dot,
        Colon
    }

    public class Token
    {
        private interface IToken
        {
            TokenType Type { get; }
        }
        public class Identifier : IToken
        {
            public string Value;
            public TokenType Type { get; }

            public Identifier(TokenType type, string value)
            {
                Value = value;
                Type = type;
            }
        }
        public class Literal : IToken
        {
            public TokenType Type { get; }

            public Literal(TokenType type)
            {
                Type = type;
            }
        }

        IToken TokenValue;

        public Token(TokenType type, string value)
        {
            TokenValue = new Identifier(type, value);
        }
        public Token(TokenType type)
        {
            TokenValue = new Literal(type);
        }

        // overwrite for printing this type
        public override string ToString()
        {
            return TokenValue switch
            {
                Identifier i => $"{i.Type}({i.Value})",
                Literal l => $"{l.Type}",
                _ => throw new System.Exception("Unknown token type")
            };
        }
    }
}