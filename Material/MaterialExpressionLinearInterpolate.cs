using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionLinearInterpolate : MaterialNode
    {
        public bool Collapsed { get; }
        public ParsedPropertyBag A { get; }
        public ParsedPropertyBag B { get; }
        public ParsedPropertyBag Alpha { get; }
        public float ConstA { get; }
        public float ConstB { get; }
        public float ConstAlpha { get; }

        public MaterialExpressionLinearInterpolate(string name, int editorX, int editorY, bool collapsed, ParsedPropertyBag a, ParsedPropertyBag b, ParsedPropertyBag alpha, float constA, float constB, float constAlpha)
            : base(name, editorX, editorY)
        {
            Collapsed = collapsed;
            A = a;
            B = b;
            Alpha = alpha;
            ConstA = constA;
            ConstB = constB;
            ConstAlpha = constAlpha;
        }
    }

    public class MaterialExpressionLinearInterpolateProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionLinearInterpolate";

        public MaterialExpressionLinearInterpolateProcessor()
        {
            AddOptionalProperty("bCollapsed", PropertyDataType.Boolean);
            AddOptionalProperty("A", PropertyDataType.AttributeList);
            AddOptionalProperty("Alpha", PropertyDataType.AttributeList);
            AddOptionalProperty("B", PropertyDataType.AttributeList);
            AddOptionalProperty("ConstA", PropertyDataType.Float);
            AddOptionalProperty("ConstB", PropertyDataType.Float);
            AddOptionalProperty("ConstAlpha", PropertyDataType.Float);

            AddIgnoredProperty("bRealtimePreview");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionLinearInterpolate(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")),
                ValueUtil.ParseBoolean(node.FindPropertyValue("bCollapsed")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("A")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("B")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Alpha")),
                ValueUtil.ParseFloat(node.FindPropertyValue("ConstA")),
                ValueUtil.ParseFloat(node.FindPropertyValue("ConstB") ?? "1.0"),
                ValueUtil.ParseFloat(node.FindPropertyValue("ConstAlpha") ?? "0.5")
            );
        }
    }
}
