using JollySamurai.UnrealEngine4.T3D.Common;
using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionTextureSampleParameter2D : ParameterNode<ResourceReference>
    {
        public ParsedPropertyBag Coordinates { get; }
        public ResourceReference Texture { get; }
        public SamplerType SamplerType { get; }

        public MaterialExpressionTextureSampleParameter2D(string name, string parameterName, ParsedPropertyBag coordinates, ResourceReference texture, SamplerType samplerType, int editorX, int editorY)
            : base(name, parameterName, null, editorX, editorY)
        {
            Coordinates = coordinates;
            Texture = texture;
            SamplerType = samplerType;
        }
    }

    public class ObjectExpressionTextureSampleParameter2DProcessor : ObjectNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionTextureSampleParameter2D";

        public ObjectExpressionTextureSampleParameter2DProcessor()
        {
            AddRequiredAttribute("Name", PropertyDataType.String);

            AddRequiredProperty("ParameterName", PropertyDataType.String);
            AddRequiredProperty("Texture", PropertyDataType.ResourceReference);

            AddOptionalProperty("Coordinates", PropertyDataType.AttributeList);
            AddOptionalProperty("MaterialExpressionEditorX", PropertyDataType.Integer);
            AddOptionalProperty("MaterialExpressionEditorY", PropertyDataType.Integer);
            AddOptionalProperty("SamplerType", PropertyDataType.SamplerType);

            AddIgnoredProperty("ExpressionGUID");
            AddIgnoredProperty("Material");
            AddIgnoredProperty("MaterialExpressionGuid");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionTextureSampleParameter2D(node.FindAttributeValue("Name"), node.FindPropertyValue("ParameterName"), ValueUtil.ParseAttributeList(node.FindPropertyValue("Coordinates")), ValueUtil.ParseResourceReference(node.FindPropertyValue("Texture")), ValueUtil.ParseSamplerType(node.FindPropertyValue("SamplerType")), ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX") ?? "0"), ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY") ?? "0"));
        }
    }
}
