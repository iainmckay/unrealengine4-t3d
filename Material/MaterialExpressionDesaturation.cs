using JollySamurai.UnrealEngine4.T3D.Common;
using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionDesaturation : Node
    {
        public bool Collapsed { get; }
        public ParsedPropertyBag Input { get; }
        public ParsedPropertyBag Fraction { get; }

        public MaterialExpressionDesaturation(string name, bool collapsed, ParsedPropertyBag input, ParsedPropertyBag fraction, int editorX, int editorY) : base(name, editorX, editorY)
        {
            Collapsed = collapsed;
            Input = input;
            Fraction = fraction;
        }
    }

    public class MaterialExpressionDesaturationProcessor : ObjectNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionDesaturation";

        public MaterialExpressionDesaturationProcessor()
        {
            AddRequiredAttribute("Name", PropertyDataType.String);

            AddOptionalProperty("bCollapsed", PropertyDataType.Boolean);
            AddOptionalProperty("Input", PropertyDataType.AttributeList);
            AddOptionalProperty("Fraction", PropertyDataType.AttributeList);
            AddOptionalProperty("MaterialExpressionEditorX", PropertyDataType.Integer);
            AddOptionalProperty("MaterialExpressionEditorY", PropertyDataType.Integer);

            AddIgnoredProperty("bRealtimePreview");
            AddIgnoredProperty("Material");
            AddIgnoredProperty("MaterialExpressionGuid");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionDesaturation(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseBoolean(node.FindPropertyValue("bCollapsed") ?? "False"),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Input")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Fraction")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX") ?? "0"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY") ?? "0")
            );
        }
    }
}
