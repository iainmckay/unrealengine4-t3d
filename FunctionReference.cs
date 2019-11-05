namespace JollySamurai.UnrealEngine4.T3D
{
    public class FunctionReference
    {
        public string Type { get; }
        public string Name { get; }

        public FunctionReference(string type, string name)
        {
            Type = type;
            Name = name;
        }
    }
}
