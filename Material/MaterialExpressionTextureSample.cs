using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionTextureSample : Node
    {
        public ParsedPropertyBag Coordinates { get; }

        public TextureReference Texture { get; }
        public ParsedPropertyBag TextureObject { get; }

        public MaterialExpressionTextureSample(string name, ParsedPropertyBag coordinates, TextureReference texture, ParsedPropertyBag textureObject, int editorX, int editorY)
            : base(name, editorX, editorY)
        {
            Coordinates = coordinates;
            Texture = texture;
            TextureObject = textureObject;
        }
    }

    public class MaterialExpressionTextureSampleProcessor : NodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionTextureSample";

        public MaterialExpressionTextureSampleProcessor()
        {
            AddRequiredAttribute("Name", PropertyDataType.String);

            AddOptionalProperty("Coordinates", PropertyDataType.AttributeList);
            AddOptionalProperty("MaterialExpressionEditorX", PropertyDataType.Integer);
            AddOptionalProperty("MaterialExpressionEditorY", PropertyDataType.Integer);
            AddOptionalProperty("Texture", PropertyDataType.TextureReference);
            AddOptionalProperty("TextureObject", PropertyDataType.AttributeList);

            AddIgnoredProperty("Material");
            AddIgnoredProperty("MaterialExpressionGuid");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionTextureSample(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Coordinates")),
                ValueUtil.ParseTextureReference(node.FindPropertyValue("Texture")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("TextureObject")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX") ?? "0"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY") ?? "0")
            );
        }
    }
}
