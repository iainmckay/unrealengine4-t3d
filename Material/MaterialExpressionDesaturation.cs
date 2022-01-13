using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionDesaturation : MaterialNode
    {
        public bool Collapsed { get; }
        public ParsedPropertyBag Input { get; }
        public ParsedPropertyBag Fraction { get; }

        public MaterialExpressionDesaturation(string name, int editorX, int editorY, bool collapsed, ParsedPropertyBag input, ParsedPropertyBag fraction)
            : base(name, editorX, editorY)
        {
            Collapsed = collapsed;
            Input = input;
            Fraction = fraction;
        }
    }

    public class MaterialExpressionDesaturationProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionDesaturation";

        public MaterialExpressionDesaturationProcessor()
        {
            AddOptionalProperty("bCollapsed", PropertyDataType.Boolean);
            AddOptionalProperty("Input", PropertyDataType.AttributeList);
            AddOptionalProperty("Fraction", PropertyDataType.AttributeList);

            AddIgnoredProperty("bRealtimePreview");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionDesaturation(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")),
                ValueUtil.ParseBoolean(node.FindPropertyValue("bCollapsed")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Input")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Fraction"))
            );
        }
    }
}
