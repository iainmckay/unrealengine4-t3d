using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionTextureSampleParameter2D : ParameterNode<TextureReference>
    {
        public ParsedPropertyBag Coordinates { get; }

        public TextureReference Texture { get; }
        public SamplerType SamplerType { get; }

        public MaterialExpressionTextureSampleParameter2D(string name, string parameterName, ParsedPropertyBag coordinates, TextureReference texture, SamplerType samplerType, int editorX, int editorY)
            : base(name, parameterName, null, editorX, editorY)
        {
            Coordinates = coordinates;
            Texture = texture;
            SamplerType = samplerType;
        }
    }

    public class MaterialExpressionTextureSampleParameter2DProcessor : NodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionTextureSampleParameter2D";

        public MaterialExpressionTextureSampleParameter2DProcessor()
        {
            AddRequiredAttribute("Name", PropertyDataType.String);

            AddRequiredProperty("MaterialExpressionEditorX", PropertyDataType.Integer);
            AddRequiredProperty("MaterialExpressionEditorY", PropertyDataType.Integer);
            AddRequiredProperty("ParameterName", PropertyDataType.String);
            AddRequiredProperty("Texture", PropertyDataType.TextureReference);

            AddOptionalProperty("Coordinates", PropertyDataType.AttributeList);
            AddOptionalProperty("SamplerType", PropertyDataType.SamplerType);

            AddIgnoredProperty("ExpressionGUID");
            AddIgnoredProperty("Material");
            AddIgnoredProperty("MaterialExpressionGuid");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionTextureSampleParameter2D(node.FindAttributeValue("Name"), node.FindPropertyValue("ParameterName"), ValueUtil.ParseAttributeList(node.FindPropertyValue("Coordinates")), ValueUtil.ParseTextureReference(node.FindPropertyValue("Texture")), ValueUtil.ParseSamplerType(node.FindPropertyValue("SamplerType")), ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")), ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")));
        }
    }
}
