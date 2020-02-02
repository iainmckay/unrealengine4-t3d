using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionComment : Node
    {
        public int SizeX { get; }
        public int SizeY { get; }
        public string Text { get; }

        public MaterialExpressionComment(string name, string text, int sizeX, int sizeY, int editorX, int editorY)
            : base(name, editorX, editorY)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            Text = text;
        }
    }

    public class MaterialExpressionCommentProcessor : NodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionComment";

        public MaterialExpressionCommentProcessor()
        {
            AddRequiredAttribute("Name", PropertyDataType.String);

            AddRequiredProperty("SizeX", PropertyDataType.Integer);
            AddRequiredProperty("SizeY", PropertyDataType.Integer);
            AddRequiredProperty("Text", PropertyDataType.String);

            AddOptionalProperty("MaterialExpressionEditorX", PropertyDataType.Integer);
            AddOptionalProperty("MaterialExpressionEditorY", PropertyDataType.Integer);

            AddIgnoredProperty("MaterialExpressionGuid");
            AddIgnoredProperty("Material");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionComment(node.FindAttributeValue("Name"), node.FindPropertyValue("Text"), ValueUtil.ParseInteger(node.FindPropertyValue("SizeX")), ValueUtil.ParseInteger(node.FindPropertyValue("SizeY")), ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX") ?? "0"), ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY") ?? "0"));
        }
    }
}
