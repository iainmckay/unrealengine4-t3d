using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionDDY : MaterialNode
    {
        public ParsedPropertyBag Value { get; }

        public MaterialExpressionDDY(string name, int editorX, int editorY, ParsedPropertyBag value)
            : base(name, editorX, editorY)
        {
            Value = value;
        }
    }

    public class MaterialExpressionDDYProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionDDY";

        public MaterialExpressionDDYProcessor()
        {
            AddOptionalProperty("Value", PropertyDataType.AttributeList);
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionDDY(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Value"))
            );
        }
    }
}
