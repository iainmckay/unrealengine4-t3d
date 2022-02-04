using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionTextureSampleParameter2D : ParameterNode<ResourceReference>
    {
        public ParsedPropertyBag Coordinates { get; }
        public ResourceReference Texture { get; }
        public SamplerType SamplerType { get; }

        public MaterialExpressionTextureSampleParameter2D(string name, int editorX, int editorY, string parameterName, ParsedPropertyBag coordinates, ResourceReference texture, SamplerType samplerType)
            : base(name, editorX, editorY, parameterName, null)
        {
            Coordinates = coordinates;
            Texture = texture;
            SamplerType = samplerType;
        }
    }

    public class MaterialExpressionTextureSampleParameter2DProcessor : ParameterNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionTextureSampleParameter2D";

        public MaterialExpressionTextureSampleParameter2DProcessor()
        {
            AddRequiredProperty("Texture", PropertyDataType.ResourceReference);

            AddOptionalProperty("Coordinates", PropertyDataType.AttributeList);
            AddOptionalProperty("ParameterName", PropertyDataType.String);
            AddOptionalProperty("SamplerType", PropertyDataType.SamplerType);

            AddIgnoredProperty("bRealtimePreview");
            AddIgnoredProperty("ExpressionGUID");
            AddIgnoredProperty("Group");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionTextureSampleParameter2D(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")),
                node.FindPropertyValue("ParameterName") ?? "Param",
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Coordinates")),
                ValueUtil.ParseResourceReference(node.FindPropertyValue("Texture")),
                ValueUtil.ParseSamplerType(node.FindPropertyValue("SamplerType"))
            );
        }
    }
}
