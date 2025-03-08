namespace Parser.Tests 
{
    using Paradox.Lexing;
    using static Paradox.Lexing.Tokenizer;


    public class Lexing
    {
        
        [Fact]
        public void TokenizationTest()
        {
            var result = Tokenizer.Tokenize("var:foo.bar = { foobar }");
            Console.WriteLine($"[{string.Join(", ", result.Select(t => t.ToString()))}]");
            Assert.Equal(
                [
                    new Token(TokenType.Identifier, "var:foo.bar"),
                    new Token(TokenType.EqualSign),
                    new Token(TokenType.BracketOpen),
                    new Token(TokenType.Identifier, "foobar"),
                    new Token(TokenType.BracketClose)
                ],
                result
            );
        }
    }
}