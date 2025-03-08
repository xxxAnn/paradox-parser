namespace Paradox.Parsing
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Tree<T>
    {
        public interface ITreeNode
        {
            T Name { get; }
            string ToString(int indent);
            Tree<U>.ITreeNode Map<U>(Func<T, U> f);
        }

        public class ScopeNode : ITreeNode
        {
            public T Name { get; }
            public List<Node> Children;
            public ScopeNode(T name, List<Node> children)
            {
                Name = name;
                Children = children;
            }

            public Tree<U>.ScopeNode Map<U>(Func<T, U> f)
            {
                return new Tree<U>.ScopeNode(f(Name), Children.Select(c => c.Map(f)).ToList());
            }

            public ScopeNode(T name, IEnumerable<Node> children)
            {
                Name = name;
                Children = new List<Node>(children);
            }

            public string ToString(int indent)
            {
                // Name(C1, C2, C3)
                var sb = new StringBuilder();
                sb.Append($"{new string(' ', indent)}{Name}(");
                foreach (var child in Children)
                {
                    sb.Append(child.Value.ToString(indent));
                    sb.Append(", ");
                }

                if (Children.Count > 0)
                {
                    sb.Remove(sb.Length - 2, 2);
                }

                sb.Append(")");
                return sb.ToString();
            }

            Tree<U>.ITreeNode ITreeNode.Map<U>(Func<T, U> f)
            {
                return Map(f);
            }
        }

        public class LeafNode : ITreeNode
        {
            public T Name { get; }

            public Tree<U>.LeafNode Map<U>(Func<T, U> f)
            {
                return new Tree<U>.LeafNode(f(Name));
            }

            public LeafNode(T name)
            {
                Name = name;
            }

            public string ToString(int indent)
            {
                return $"{new string(' ', indent)}{Name}";
            }

            Tree<U>.ITreeNode ITreeNode.Map<U>(Func<T, U> f)
            {
                return Map(f);
            }
        }

        // Wrapper
        public class Node
        {
            public ITreeNode Value;

            public Node(ITreeNode value)
            {
                Value = value;
            }

            public override string ToString()
            {
                return Value.ToString(0);
            }

            public Tree<U>.Node Map<U>(Func<T, U> f)
            {
                return new Tree<U>.Node(Value.Map(f));
            }

        }

        Node Root;

        public Tree(Node root)
        {
            Root = root;
        }

        public override string ToString()
        {
            return Root.ToString();
        }

        // Takes a function from T -> U
        public Tree<U> Map<U>(Func<T, U> f)
        {
            // we must iterate over the tree and apply f to each node
            // we can do this with a recursive function
            return new Tree<U>(Root.Map(f));
        }
    }
}
