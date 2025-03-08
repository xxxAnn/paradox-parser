using Paradox.Lexing;
using Paradox.Parsing;
using Pidgin;
using static Pidgin.Parser;

namespace Paradox.Resolution
{
    public static class Resolver 
    {
        public static Tree<Name> Resolve(Tree<string> tree)
        {
            return tree.Map(Parse);
        }

        private static readonly Parser<char, Name.Path> Path =
            Map(v => new Name.Path(v),
                LetterOrDigit.ManyString());

        private static readonly Parser<char, Name.Ref> Ref =
            Try(
                Map((r, _, v) => new Name.Ref(r, v),
                    LetterOrDigit.ManyString(),
                    Char(':'),
                    LetterOrDigit.ManyString()
                )
            );

        private static Parser<char, Name.IName> Component() =>
            Ref.Cast<Name.IName>()
                .Or(Path.Cast<Name.IName>());


        public static Name Parse(string input)
        {
            var result = Component().Separated(Char('.')).Parse(input);
            if (result.Success)
            {
                return new Name(result.Value.ToArray());
            }
            else
            {
                throw new System.Exception($"Parsing failed: {result.Error}");
            }
        }
    }
}