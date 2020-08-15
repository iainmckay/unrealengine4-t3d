using JollySamurai.UnrealEngine4.T3D.Common;
using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionTextureObject : Node
    {
        public ResourceReference Texture { get; }

        public MaterialExpressionTextureObject(string name, ResourceReference texture, SamplerType samplerType, int editorX, int editorY)
            : base(name, editorX, editorY)
        {
            Texture = texture;
        }
    }

    public class MaterialExpressionTextureObjectProcessor : ObjectNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionTextureObject";

        public MaterialExpressionTextureObjectProcessor()
        {
            AddRequiredAttribute("Name", PropertyDataType.String);

            AddOptionalProperty("MaterialExpressionEditorX", PropertyDataType.Integer);
            AddOptionalProperty("MaterialExpressionEditorY", PropertyDataType.Integer);
            AddOptionalProperty("SamplerType", PropertyDataType.SamplerType);
            AddOptionalProperty("Texture", PropertyDataType.ResourceReference);

            AddIgnoredProperty("Material");
            AddIgnoredProperty("MaterialExpressionGuid");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionTextureObject(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseResourceReference(node.FindPropertyValue("Texture")),
                ValueUtil.ParseSamplerType(node.FindPropertyValue("SamplerType")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX") ?? "0"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY") ?? "0")
            );
        }
    }
}
