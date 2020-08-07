using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionPower : Node
    {
        public bool Collapsed { get; }
        public ParsedPropertyBag Value { get; }
        public ParsedPropertyBag Exponent { get; }
        public float ConstExponent { get; }

        public MaterialExpressionPower(string name, bool collapsed, ParsedPropertyBag value, ParsedPropertyBag exponent, float constExponent, int editorX, int editorY) : base(name, editorX, editorY)
        {
            Collapsed = collapsed;
            Value = value;
            Exponent = exponent;
            ConstExponent = constExponent;
        }
    }

    public class MaterialExpressionPowerProcessor : NodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionPower";

        public MaterialExpressionPowerProcessor()
        {
            AddRequiredAttribute("Name", PropertyDataType.String);

            AddRequiredProperty("Base", PropertyDataType.AttributeList);

            AddOptionalProperty("bCollapsed", PropertyDataType.Boolean);
            AddOptionalProperty("Exponent", PropertyDataType.AttributeList);
            AddOptionalProperty("ConstExponent", PropertyDataType.Float);
            AddOptionalProperty("MaterialExpressionEditorX", PropertyDataType.Integer);
            AddOptionalProperty("MaterialExpressionEditorY", PropertyDataType.Integer);

            AddIgnoredProperty("MaterialExpressionGuid");
            AddIgnoredProperty("Material");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionPower(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseBoolean(node.FindPropertyValue("bCollapsed") ?? "False"),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Base")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Exponent")),
                ValueUtil.ParseFloat(node.FindPropertyValue("ConstExponent") ?? "2.0"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX") ?? "0"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY") ?? "0")
            );
        }
    }
}
