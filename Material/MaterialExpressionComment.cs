using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionComment : MaterialNode
    {
        public int SizeX { get; }
        public int SizeY { get; }
        public string Text { get; }

        public MaterialExpressionComment(string name, int editorX, int editorY, string text, int sizeX, int sizeY)
            : base(name, editorX, editorY)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            Text = text;
        }
    }

    public class MaterialExpressionCommentProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionComment";

        public MaterialExpressionCommentProcessor()
        {
            AddRequiredProperty("SizeX", PropertyDataType.Integer);
            AddRequiredProperty("SizeY", PropertyDataType.Integer);
            AddRequiredProperty("Text", PropertyDataType.String);
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionComment(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")),
                node.FindPropertyValue("Text"),
                ValueUtil.ParseInteger(node.FindPropertyValue("SizeX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("SizeY"))
            );
        }
    }
}
