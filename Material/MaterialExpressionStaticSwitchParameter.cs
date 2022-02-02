using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionStaticSwitchParameter : ParameterNode<bool>
    {
        public ParsedPropertyBag A { get; }
        public ParsedPropertyBag B { get; }

        public MaterialExpressionStaticSwitchParameter(string name, int editorX, int editorY, string parameterName, bool defaultValue, ParsedPropertyBag a, ParsedPropertyBag b)
            : base(name, editorX, editorY, parameterName, defaultValue)
        {
            A = a;
            B = b;
        }
    }

    public class MaterialExpressionStaticSwitchParameterProcessor : ParameterNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionStaticSwitchParameter";

        public MaterialExpressionStaticSwitchParameterProcessor()
        {
            AddOptionalProperty("A", PropertyDataType.AttributeList);
            AddOptionalProperty("B", PropertyDataType.AttributeList);
            AddOptionalProperty("DefaultValue", PropertyDataType.Boolean);
            AddOptionalProperty("ParameterName", PropertyDataType.String);

            AddIgnoredProperty("bRealtimePreview");
            AddIgnoredProperty("ExpressionGUID");
            AddIgnoredProperty("Group");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionStaticSwitchParameter(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")),
                node.FindPropertyValue("ParameterName") ?? "Param",
                ValueUtil.ParseBoolean(node.FindPropertyValue("DefaultValue")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("A")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("B"))
            );
        }
    }
}
