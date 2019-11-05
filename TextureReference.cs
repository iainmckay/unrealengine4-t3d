namespace JollySamurai.UnrealEngine4.T3D
{
    public class TextureReference
    {
        public TextureType Type { get; }
        public string FileName { get; }

        public TextureReference(TextureType type, string fileName)
        {
            Type = type;
            FileName = fileName;
        }
    }

    public enum TextureType
    {
        Texture2D
    }
}
