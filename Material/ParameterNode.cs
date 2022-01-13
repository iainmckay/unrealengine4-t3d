namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public abstract class ParameterNode<T> : ParameterNode
    {
        public T DefaultValue { get; }

        public ParameterNode(string name, int editorX, int editorY, string parameterName, T defaultValue)
            : base(name, editorX, editorY, parameterName)
        {
            DefaultValue = defaultValue;
        }
    }

    public abstract class ParameterNode : MaterialNode
    {
        public string ParameterName { get; }
        
        public ParameterNode(string name, int editorX, int editorY, string parameterName)
            : base(name, editorX, editorY)
        {
            ParameterName = parameterName;
        }
    }
}
