using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionTextureObject : Node
    {
        public TextureReference Texture { get; }

        public MaterialExpressionTextureObject(string name, TextureReference texture, SamplerType samplerType, int editorX, int editorY)
            : base(name, editorX, editorY)
        {
            Texture = texture;
        }
    }

    public class MaterialExpressionTextureObjectProcessor : NodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionTextureObject";

        public MaterialExpressionTextureObjectProcessor()
        {
            AddRequiredAttribute("Name", PropertyDataType.String);

            AddOptionalProperty("MaterialExpressionEditorX", PropertyDataType.Integer);
            AddOptionalProperty("MaterialExpressionEditorY", PropertyDataType.Integer);
            AddOptionalProperty("SamplerType", PropertyDataType.SamplerType);
            AddOptionalProperty("Texture", PropertyDataType.TextureReference);

            AddIgnoredProperty("Material");
            AddIgnoredProperty("MaterialExpressionGuid");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionTextureObject(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseTextureReference(node.FindPropertyValue("Texture")),
                ValueUtil.ParseSamplerType(node.FindPropertyValue("SamplerType")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX") ?? "0"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY") ?? "0")
            );
        }
    }
}
