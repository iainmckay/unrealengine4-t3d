using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionTextureObjectParameter : ParameterNode<ResourceReference>
    {
        public SamplerType SamplerType { get; }

        public MaterialExpressionTextureObjectParameter(string name, int editorX, int editorY, string parameterName, ResourceReference resource, SamplerType samplerType)
            : base(name, editorX, editorY, parameterName, resource)
        {
            SamplerType = samplerType;
        }
    }

    public class MaterialExpressionTextureObjectParameterProcessor : ParameterNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionTextureObjectParameter";

        public MaterialExpressionTextureObjectParameterProcessor()
        {
            AddRequiredProperty("ParameterName", PropertyDataType.String);

            AddOptionalProperty("Texture", PropertyDataType.ResourceReference);
            AddOptionalProperty("SamplerType", PropertyDataType.SamplerType);

            AddIgnoredProperty("ExpressionGUID");
            AddIgnoredProperty("Group");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionTextureObjectParameter(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")),
                node.FindPropertyValue("ParameterName"),
                ValueUtil.ParseResourceReference(node.FindPropertyValue("Texture")),
                ValueUtil.ParseSamplerType(node.FindPropertyValue("SamplerType"))
            );
        }
    }
}
