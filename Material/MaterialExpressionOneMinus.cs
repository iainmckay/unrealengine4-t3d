using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionOneMinus : Node
    {
        public ParsedPropertyBag Input { get; }

        public MaterialExpressionOneMinus(string name, ParsedPropertyBag input, int editorX, int editorY) : base(name, editorX, editorY)
        {
            Input = input;
        }
    }

    public class MaterialExpressionOneMinusProcessor : NodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionOneMinus";

        public MaterialExpressionOneMinusProcessor()
        {
            AddRequiredAttribute("Name", PropertyDataType.String);
            AddRequiredProperty("Input", PropertyDataType.AttributeList);

            AddOptionalProperty("MaterialExpressionEditorX", PropertyDataType.Integer);
            AddOptionalProperty("MaterialExpressionEditorY", PropertyDataType.Integer);

            AddIgnoredProperty("MaterialExpressionGuid");
            AddIgnoredProperty("Material");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionOneMinus(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Input")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX") ?? "0"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY") ?? "0")
            );
        }
    }
}
