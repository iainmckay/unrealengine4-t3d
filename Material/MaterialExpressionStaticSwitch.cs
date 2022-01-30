using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionStaticSwitch : MaterialNode
    {
        public ExpressionReference A { get; }
        public ExpressionReference B { get; }
        public ExpressionReference Value { get; }
        public bool DefaultValue { get; }

        public MaterialExpressionStaticSwitch(string name, int editorX, int editorY, ExpressionReference a, ExpressionReference b, ExpressionReference value, bool defaultValue)
            : base(name, editorX, editorY)
        {
            A = a;
            B = b;
            Value = value;
            DefaultValue = defaultValue;
        }
    }

    public class MaterialExpressionStaticSwitchProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionStaticSwitch";

        public MaterialExpressionStaticSwitchProcessor()
        {
            AddOptionalProperty("A", PropertyDataType.ExpressionReference);
            AddOptionalProperty("B", PropertyDataType.ExpressionReference);
            AddOptionalProperty("Value", PropertyDataType.ExpressionReference);
            AddOptionalProperty("DefaultValue", PropertyDataType.Boolean);
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionStaticSwitch(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")),
                ValueUtil.ParseExpressionReference(node.FindPropertyValue("A")),
                ValueUtil.ParseExpressionReference(node.FindPropertyValue("B")),
                ValueUtil.ParseExpressionReference(node.FindPropertyValue("Value")),
                ValueUtil.ParseBoolean(node.FindPropertyValue("DefaultValue"))
            );
        }
    }
}
