namespace Parser.Tests 
{
    using Paradox.Lexing;
    using static Paradox.Lexing.Tokenizer;


    public class UnitTest1
    {
        
        [Fact]
        public void TokenizationTest()
        {
            var result = Tokenizer.Tokenize("hello = { world }");
            Console.WriteLine($"[{string.Join(", ", result.Select(t => t.ToString()))}]");
            Assert.Equal(
                [
                    new Token(TokenType.Identifier, "hello"),
                    new Token(TokenType.EqualSign),
                    new Token(TokenType.BracketOpen),
                    new Token(TokenType.Identifier, "world"),
                    new Token(TokenType.BracketClose)
                ],
                result
            );
        }
    }
}