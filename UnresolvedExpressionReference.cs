using JollySamurai.UnrealEngine4.T3D.Parser;

namespace JollySamurai.UnrealEngine4.T3D
{
    public class UnresolvedExpressionReference
    {
        public string ClassName { get; }
        public string NodeName { get; }
        public ParsedPropertyBag Properties { get; }

        public UnresolvedExpressionReference(string className, string nodeName, ParsedPropertyBag properties)
        {
            ClassName = className;
            NodeName = nodeName;
            Properties = properties;
        }
    }
}
