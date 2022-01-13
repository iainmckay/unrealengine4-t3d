using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionPower : MaterialNode
    {
        public bool Collapsed { get; }
        public ParsedPropertyBag Value { get; }
        public ParsedPropertyBag Exponent { get; }
        public float ConstExponent { get; }

        public MaterialExpressionPower(string name, int editorX, int editorY, bool collapsed, ParsedPropertyBag value, ParsedPropertyBag exponent, float constExponent)
            : base(name, editorX, editorY)
        {
            Collapsed = collapsed;
            Value = value;
            Exponent = exponent;
            ConstExponent = constExponent;
        }
    }

    public class MaterialExpressionPowerProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionPower";

        public MaterialExpressionPowerProcessor()
        {
            AddRequiredProperty("Base", PropertyDataType.AttributeList);

            AddOptionalProperty("bCollapsed", PropertyDataType.Boolean);
            AddOptionalProperty("Exponent", PropertyDataType.AttributeList);
            AddOptionalProperty("ConstExponent", PropertyDataType.Float);
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionPower(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")),
                ValueUtil.ParseBoolean(node.FindPropertyValue("bCollapsed")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Base")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Exponent")),
                ValueUtil.ParseFloat(node.FindPropertyValue("ConstExponent") ?? "2.0")
            );
        }
    }
}
