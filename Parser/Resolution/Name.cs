// A name in the language
// For example a.b.c is Name(Path("a", "b", "c"))
using System.Text;

namespace Paradox.Resolution 
{
    public class Name 
    {

        public interface IName {};

        public IName[] Components { get; }

        public class Path : IName
        {
            public string inner;
            public Path(string value) 
            {
                inner = value;
            }
        }

        public class Ref : IName
        {
            public string Name { get; }
            public string Value { get; }
            public Ref(string name, string value) 
            {
                Value = value;
                Name = name;
            }
        }

        public Name(params IName[] components) 
        {
            Components = components;
        }

        public override string ToString() 
        {
            // We wanna print out Name(Path("a", "b", "c"))

            var sb = new StringBuilder();
            sb.Append("Name(");
            for (int i = 0; i < Components.Length; i++) 
            {
                if (i != 0) 
                {
                    sb.Append(", ");
                }
                if (Components[i] is Path path) 
                {
                    sb.Append("Path(");
                    sb.Append(path.inner);
                    sb.Append(')');
                } 
                else if (Components[i] is Ref @ref) 
                {
                    sb.Append("Ref(");
                    sb.Append(@ref.Name);
                    sb.Append(", ");
                    sb.Append(@ref.Value);
                    sb.Append(')');
                }
            }
            sb.Append(')');
            return sb.ToString();
        }
    }
}