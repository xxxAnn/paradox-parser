namespace Parser.Tests 
{
    using Paradox.Parsing;
    using Paradox.Lexing;
    using Paradox.Resolution;

    public class Parsing
    {
        
        [Fact]
        public void ParsingTest()
        {
            var result = Parser.Parse(Tokenizer.Tokenize("var:foo.bar = { foobar = { barfoo foobarfoo } }"));


            Console.WriteLine(result);
            //Console.WriteLine("HELLO");
        }

        [Fact]
        public void ResolutionTest() 
        {
            var tree = Parser.Parse(Tokenizer.Tokenize("var:foo.bar = { foobar = { barfoo foobarfoo } }"));
            var result = Resolver.Resolve(tree);

            Console.WriteLine(result);
        }

    }
}