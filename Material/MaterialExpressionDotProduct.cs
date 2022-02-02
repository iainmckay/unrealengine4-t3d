using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionDotProduct : MaterialNode
    {
        public ParsedPropertyBag A { get; }
        public ParsedPropertyBag B { get; }

        public MaterialExpressionDotProduct(string name, int editorX, int editorY, ParsedPropertyBag a, ParsedPropertyBag b)
            : base(name, editorX, editorY)
        {
            A = a;
            B = b;
        }
    }

    public class MaterialExpressionDotProductProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionDotProduct";

        public MaterialExpressionDotProductProcessor()
        {
            AddOptionalProperty("A", PropertyDataType.AttributeList);
            AddOptionalProperty("B", PropertyDataType.AttributeList);
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionDotProduct(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("A")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("B"))
            );
        }
    }
}
