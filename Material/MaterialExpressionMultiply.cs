using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionMultiply : Node
    {
        public bool Collapsed { get; }
        public ParsedPropertyBag A { get; }
        public ParsedPropertyBag B { get; }

        public MaterialExpressionMultiply(string name, bool collapsed, ParsedPropertyBag a, ParsedPropertyBag b, int editorX, int editorY) : base(name, editorX, editorY)
        {
            Collapsed = collapsed;
            A = a;
            B = b;
        }
    }

    public class MaterialExpressionMultiplyProcessor : NodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionMultiply";

        public MaterialExpressionMultiplyProcessor()
        {
            AddRequiredAttribute("Name", PropertyDataType.String);

            AddOptionalProperty("bCollapsed", PropertyDataType.Boolean);
            AddOptionalProperty("A", PropertyDataType.AttributeList);
            AddOptionalProperty("B", PropertyDataType.AttributeList);
            AddOptionalProperty("MaterialExpressionEditorX", PropertyDataType.Integer);
            AddOptionalProperty("MaterialExpressionEditorY", PropertyDataType.Integer);

            AddIgnoredProperty("Material");
            AddIgnoredProperty("MaterialExpressionGuid");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionMultiply(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseBoolean(node.FindPropertyValue("bCollapsed") ?? "False"),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("A")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("B")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX") ?? "0"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY") ?? "0")
            );
        }
    }
}
