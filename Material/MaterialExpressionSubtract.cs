using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionSubtract : Node
    {
        public ParsedPropertyBag A { get; }
        public ParsedPropertyBag B { get; }

        public MaterialExpressionSubtract(string name, ParsedPropertyBag a, ParsedPropertyBag b, int editorX, int editorY) : base(name, editorX, editorY)
        {
            A = a;
            B = b;
        }
    }

    public class MaterialExpressionSubtractProcessor : NodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionSubtract";

        public MaterialExpressionSubtractProcessor()
        {
            AddRequiredAttribute("Name", PropertyDataType.String);

            AddRequiredProperty("A", PropertyDataType.AttributeList);
            AddRequiredProperty("B", PropertyDataType.AttributeList);

            AddOptionalProperty("MaterialExpressionEditorX", PropertyDataType.Integer);
            AddOptionalProperty("MaterialExpressionEditorY", PropertyDataType.Integer);

            AddIgnoredProperty("MaterialExpressionGuid");
            AddIgnoredProperty("Material");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionSubtract(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("A")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("B")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX") ?? "0"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY") ?? "0")
            );
        }
    }
}
