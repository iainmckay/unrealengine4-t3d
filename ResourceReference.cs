namespace JollySamurai.UnrealEngine4.T3D
{
    public class ResourceReference
    {
        public string Type { get; }
        public string FileName { get; }

        public ResourceReference(string type, string fileName)
        {
            Type = type;
            FileName = fileName;
        }
    }
}
