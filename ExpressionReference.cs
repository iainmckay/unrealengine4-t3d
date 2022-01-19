namespace JollySamurai.UnrealEngine4.T3D
{
    public class ExpressionReference
    {
        public string ClassName { get; }
        public string NodeName { get; }

        public ExpressionReference(string className, string nodeName)
        {
            ClassName = className;
            NodeName = nodeName;
        }
    }
}
