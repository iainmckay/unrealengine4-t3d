using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionTextureObject : MaterialNode
    {
        public ResourceReference Texture { get; }
        public SamplerType SamplerType { get; }

        public MaterialExpressionTextureObject(string name, int editorX, int editorY, ResourceReference texture, SamplerType samplerType)
            : base(name, editorX, editorY)
        {
            Texture = texture;
            SamplerType = samplerType;
        }
    }

    public class MaterialExpressionTextureObjectProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionTextureObject";

        public MaterialExpressionTextureObjectProcessor()
        {
            AddOptionalProperty("SamplerType", PropertyDataType.SamplerType);
            AddOptionalProperty("Texture", PropertyDataType.ResourceReference);
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionTextureObject(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")),
                ValueUtil.ParseResourceReference(node.FindPropertyValue("Texture")),
                ValueUtil.ParseSamplerType(node.FindPropertyValue("SamplerType"))
            );
        }
    }
}
