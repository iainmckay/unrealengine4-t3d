using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionTextureSample : MaterialNode
    {
        public ParsedPropertyBag Coordinates { get; }

        public ResourceReference Texture { get; }
        public ParsedPropertyBag TextureObject { get; }
        public SamplerType SamplerType { get; }

        public MaterialExpressionTextureSample(string name, int editorX, int editorY, ParsedPropertyBag coordinates, ResourceReference texture, ParsedPropertyBag textureObject, SamplerType samplerType)
            : base(name, editorX, editorY)
        {
            Coordinates = coordinates;
            Texture = texture;
            TextureObject = textureObject;
            SamplerType = samplerType;
        }
    }

    public class MaterialExpressionTextureSampleProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionTextureSample";

        public MaterialExpressionTextureSampleProcessor()
        {
            AddOptionalProperty("Coordinates", PropertyDataType.AttributeList);
            AddOptionalProperty("SamplerType", PropertyDataType.SamplerType);
            AddOptionalProperty("Texture", PropertyDataType.ResourceReference);
            AddOptionalProperty("TextureObject", PropertyDataType.AttributeList);

            AddIgnoredProperty("bCollapsed");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionTextureSample(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Coordinates")),
                ValueUtil.ParseResourceReference(node.FindPropertyValue("Texture")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("TextureObject")),
                ValueUtil.ParseSamplerType(node.FindPropertyValue("SamplerType"))
            );
        }
    }
}
