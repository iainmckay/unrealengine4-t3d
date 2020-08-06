using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionTime : Node
    {
        public MaterialExpressionTime(string name, int editorX, int editorY) : base(name, editorX, editorY)
        {
        }
    }

    public class MaterialExpressionTimeProcessor : NodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionTime";

        public MaterialExpressionTimeProcessor()
        {
            AddRequiredAttribute("Name", PropertyDataType.String);

            AddOptionalProperty("MaterialExpressionEditorX", PropertyDataType.Integer);
            AddOptionalProperty("MaterialExpressionEditorY", PropertyDataType.Integer);

            AddIgnoredProperty("Material");
            AddIgnoredProperty("MaterialExpressionGuid");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionTime(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX") ?? "0"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY") ?? "0")
            );
        }
    }
}
