using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionSubtract : MaterialNode
    {
        public ParsedPropertyBag A { get; }
        public ParsedPropertyBag B { get; }
        public float ConstA { get; }
        public float ConstB { get; }

        public MaterialExpressionSubtract(string name, int editorX, int editorY, ParsedPropertyBag a, ParsedPropertyBag b, float constA, float constB)
            : base(name, editorX, editorY)
        {
            A = a;
            B = b;
            ConstA = constA;
            ConstB = constB;
        }
    }

    public class MaterialExpressionSubtractProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionSubtract";

        public MaterialExpressionSubtractProcessor()
        {
            AddOptionalProperty("A", PropertyDataType.AttributeList);
            AddOptionalProperty("B", PropertyDataType.AttributeList);

            AddOptionalProperty("ConstA", PropertyDataType.Float);
            AddOptionalProperty("ConstB", PropertyDataType.Float);
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionSubtract(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("A")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("B")),
                ValueUtil.ParseFloat(node.FindPropertyValue("ConstA") ?? "1.0"),
                ValueUtil.ParseFloat(node.FindPropertyValue("ConstB") ?? "1.0")
            );
        }
    }
}
