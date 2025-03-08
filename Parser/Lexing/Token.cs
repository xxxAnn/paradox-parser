namespace Paradox.Lexing
{
    public enum TokenType
    {
        Identifier,
        BracketOpen,
        BracketClose,
        EqualSign
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

        public TokenType Type => TokenValue.Type;
        public string Value => TokenValue switch
        {
            Identifier i => i.Value,
            _ => throw new System.Exception("Token is not an identifier")
        };

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

        // override equality operator
        public override bool Equals(object? obj)
        {
            if (obj is Token other)
            {
                return TokenValue switch
                {
                    Identifier i => other.TokenValue is Identifier oi && i.Type == oi.Type && i.Value == oi.Value,
                    Literal l => other.TokenValue is Literal ol && l.Type == ol.Type,
                    _ => false
                };
            }
            return false;
        }

        // override hash code
        public override int GetHashCode()
        {
            return TokenValue switch
            {
                Identifier i => i.Value.GetHashCode() ^ i.Type.GetHashCode(),
                Literal l => l.Type.GetHashCode(),
                _ => 0
            };
        }
    }
}