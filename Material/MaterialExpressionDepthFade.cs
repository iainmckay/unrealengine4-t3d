using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionDepthFade : MaterialNode
    {
        public ParsedPropertyBag A { get; }
        public ParsedPropertyBag B { get; }
        public float OpacityDefault { get; }
        public float FadeDistanceDefault { get; }

        public MaterialExpressionDepthFade(string name, int editorX, int editorY, ParsedPropertyBag a, ParsedPropertyBag b, float opacityDefault, float fadeDistanceDefault)
            : base(name, editorX, editorY)
        {
            A = a;
            B = b;
            OpacityDefault = opacityDefault;
            FadeDistanceDefault = fadeDistanceDefault;
        }
    }

    public class MaterialExpressionDepthFadeProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionDepthFade";

        public MaterialExpressionDepthFadeProcessor()
        {
            AddOptionalProperty("A", PropertyDataType.AttributeList);
            AddOptionalProperty("B", PropertyDataType.AttributeList);
            AddOptionalProperty("OpacityDefault", PropertyDataType.Float);
            AddOptionalProperty("FadeDistanceDefault", PropertyDataType.Float);
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionDepthFade(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("A")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("B")),
                ValueUtil.ParseFloat(node.FindPropertyValue("OpacityDefault") ?? "1.0"),
                ValueUtil.ParseFloat(node.FindPropertyValue("FadeDistanceDefault") ?? "100.0")
            );
        }
    }
}
