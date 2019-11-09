namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public abstract class ParameterNode<T> : ParameterNode
    {
        public T DefaultValue { get; }

        public ParameterNode(string name, string parameterName, T defaultValue, int editorX, int editorY) : base(name, parameterName, editorX, editorY)
        {
            DefaultValue = defaultValue;
        }
    }

    public abstract class ParameterNode : Node
    {
        public string ParameterName { get; }
        
        public ParameterNode(string name, string parameterName, int editorX, int editorY) : base(name, editorX, editorY)
        {
            ParameterName = parameterName;
        }
    }
}
