using System.Text;

namespace Builder.Exercise
{
    internal class Program
    {
        public class CodeElement
        {
            public string? TypeName, FieldName;
            public List<CodeElement> Elements = new List<CodeElement>();
            private const int indentSize = 2;

            public CodeElement() { }

            public CodeElement(string? typeName, string? fieldName)
            {
                TypeName = typeName;
                FieldName = fieldName;
            }

            private string ToStringImpl(int indent)
            {
                var sb = new StringBuilder();
                var i = new string(' ', indentSize * indent);

                sb.Append($"{i}public {(!string.IsNullOrWhiteSpace(TypeName) ? TypeName : "class")}");

                if (!string.IsNullOrWhiteSpace(FieldName))
                {
                    sb.Append($" {FieldName}");
                    sb.Append(!string.IsNullOrWhiteSpace(TypeName) ? $";" : $"\n{i}{{");
                    sb.Append("\n");
                }

                foreach (var e in Elements)
                    sb.Append(e.ToStringImpl(indent + 1));

                if (string.IsNullOrWhiteSpace(TypeName)) sb.Append($"{i}}}");
                
                return sb.ToString();
            }

            public override string ToString()
            {
                return ToStringImpl(0);
            }
        }

        public class CodeBuilder
        {
            private readonly string rootName;

            public CodeBuilder(string rootName)
            {
                this.rootName = rootName;
                root.FieldName = rootName;
            }

            public CodeBuilder AddField(string fieldName, string typeName)
            {
                root.Elements.Add(new CodeElement(typeName, fieldName));
                return this;
            }

            public override string? ToString()
            {
                return root.ToString();
            }

            public void Clear()
            {
                root = new CodeElement { FieldName = rootName };
            }

            CodeElement root = new CodeElement();
        }

        static void Main(string[] args)
        {
            var cb = new CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");
            Console.WriteLine(cb);
        }
    }
}
