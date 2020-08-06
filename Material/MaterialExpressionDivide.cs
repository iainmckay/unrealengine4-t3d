using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionDivide : Node
    {
        public ParsedPropertyBag A { get; }
        public ParsedPropertyBag B { get; }
        public float ConstA { get; }
        public float ConstB { get; }

        public MaterialExpressionDivide(string name, ParsedPropertyBag a, ParsedPropertyBag b, float constA, float constB, int editorX, int editorY) : base(name, editorX, editorY)
        {
            A = a;
            B = b;
            ConstA = constA;
            ConstB = constB;
        }
    }

    public class MaterialExpressionDivideProcessor : NodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionDivide";

        public MaterialExpressionDivideProcessor()
        {
            AddRequiredAttribute("Name", PropertyDataType.String);

            AddOptionalProperty("A", PropertyDataType.AttributeList);
            AddOptionalProperty("B", PropertyDataType.AttributeList);
            AddOptionalProperty("ConstA", PropertyDataType.Float);
            AddOptionalProperty("ConstB", PropertyDataType.Float);
            AddOptionalProperty("MaterialExpressionEditorX", PropertyDataType.Integer);
            AddOptionalProperty("MaterialExpressionEditorY", PropertyDataType.Integer);

            AddIgnoredProperty("MaterialExpressionGuid");
            AddIgnoredProperty("Material");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionDivide(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("A")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("B")),
                ValueUtil.ParseFloat(node.FindPropertyValue("ConstA") ?? "1.0"),
                ValueUtil.ParseFloat(node.FindPropertyValue("ConstB") ?? "2.0"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX") ?? "0"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY") ?? "0")
            );
        }
    }
}
