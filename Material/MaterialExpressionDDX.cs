using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionDDX : MaterialNode
    {
        public ParsedPropertyBag Value { get; }

        public MaterialExpressionDDX(string name, int editorX, int editorY, ParsedPropertyBag value)
            : base(name, editorX, editorY)
        {
            Value = value;
        }
    }

    public class MaterialExpressionDDXProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionDDX";

        public MaterialExpressionDDXProcessor()
        {
            AddOptionalProperty("Value", PropertyDataType.AttributeList);
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionDDX(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Value"))
            );
        }
    }
}
