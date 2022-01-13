using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionMultiply : MaterialNode
    {
        public bool Collapsed { get; }
        public ParsedPropertyBag A { get; }
        public ParsedPropertyBag B { get; }
        public float ConstA { get; }
        public float ConstB { get; }

        public MaterialExpressionMultiply(string name, int editorX, int editorY, bool collapsed, ParsedPropertyBag a, ParsedPropertyBag b, float constA, float constB)
            : base(name, editorX, editorY)
        {
            Collapsed = collapsed;
            A = a;
            B = b;
            ConstA = constA;
            ConstB = constB;
        }
    }

    public class MaterialExpressionMultiplyProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionMultiply";

        public MaterialExpressionMultiplyProcessor()
        {
            AddOptionalProperty("bCollapsed", PropertyDataType.Boolean);
            AddOptionalProperty("A", PropertyDataType.AttributeList);
            AddOptionalProperty("B", PropertyDataType.AttributeList);
            AddOptionalProperty("ConstA", PropertyDataType.Float);
            AddOptionalProperty("ConstB", PropertyDataType.Float);

            AddIgnoredProperty("bRealtimePreview");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionMultiply(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")),
                ValueUtil.ParseBoolean(node.FindPropertyValue("bCollapsed")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("A")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("B")),
                ValueUtil.ParseFloat(node.FindPropertyValue("ConstA")),
                ValueUtil.ParseFloat(node.FindPropertyValue("ConstB") ?? "1.0")
            );
        }
    }
}
