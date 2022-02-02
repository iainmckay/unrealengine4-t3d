using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionDepthFade : MaterialNode
    {
        public ExpressionReference InOpacity { get; }
        public ExpressionReference FadeDistance { get; }
        public float OpacityDefault { get; }
        public float FadeDistanceDefault { get; }

        public MaterialExpressionDepthFade(string name, int editorX, int editorY, ExpressionReference inOpacity, ExpressionReference fadeDistance, float opacityDefault, float fadeDistanceDefault)
            : base(name, editorX, editorY)
        {
            InOpacity = inOpacity;
            FadeDistance = fadeDistance;
            OpacityDefault = opacityDefault;
            FadeDistanceDefault = fadeDistanceDefault;
        }
    }

    public class MaterialExpressionDepthFadeProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionDepthFade";

        public MaterialExpressionDepthFadeProcessor()
        {
            AddOptionalProperty("InOpacity", PropertyDataType.ExpressionReference);
            AddOptionalProperty("FadeDistance", PropertyDataType.ExpressionReference);
            AddOptionalProperty("OpacityDefault", PropertyDataType.Float);
            AddOptionalProperty("FadeDistanceDefault", PropertyDataType.Float);
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionDepthFade(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")),
                ValueUtil.ParseExpressionReference(node.FindPropertyValue("InOpacity")),
                ValueUtil.ParseExpressionReference(node.FindPropertyValue("FadeDistance")),
                ValueUtil.ParseFloat(node.FindPropertyValue("OpacityDefault") ?? "1.0"),
                ValueUtil.ParseFloat(node.FindPropertyValue("FadeDistanceDefault") ?? "100.0")
            );
        }
    }
}
