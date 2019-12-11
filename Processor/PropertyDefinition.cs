namespace JollySamurai.UnrealEngine4.T3D.Processor
{
    public class PropertyDefinition
    {
        public string Name { get; }
        public PropertyDataType DataType { get; }
        public bool IsRequired { get; }

        public bool IsArray => DataType.HasFlag(PropertyDataType.Array);
        
        public PropertyDefinition(string name, PropertyDataType dataType, bool isRequired)
        {
            Name = name;
            DataType = dataType;
            IsRequired = isRequired;
        }
    }
}
