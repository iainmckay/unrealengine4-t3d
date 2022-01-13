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

        public MaterialExpressionLinearInterpolate(string name, int editorX, int editorY, bool collapsed, ParsedPropertyBag a, ParsedPropertyBag b, ParsedPropertyBag alpha)
            : base(name, editorX, editorY)
        {
            Collapsed = collapsed;
            A = a;
            B = b;
            Alpha = alpha;
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
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Alpha"))
            );
        }
    }
}
