using JollySamurai.UnrealEngine4.T3D.Common;
using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionSine : Node
    {
        public ParsedPropertyBag Input { get; }
        public float Period { get; }

        public MaterialExpressionSine(string name, ParsedPropertyBag input, float period, int editorX, int editorY) : base(name, editorX, editorY)
        {
            Input = input;
            Period = period;
        }
    }

    public class MaterialExpressionSineProcessor : ObjectNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionSine";

        public MaterialExpressionSineProcessor()
        {
            AddRequiredAttribute("Name", PropertyDataType.String);

            AddRequiredProperty("Input", PropertyDataType.AttributeList);

            AddOptionalProperty("MaterialExpressionEditorX", PropertyDataType.Integer);
            AddOptionalProperty("MaterialExpressionEditorY", PropertyDataType.Integer);
            AddOptionalProperty("Period", PropertyDataType.Float);

            AddIgnoredProperty("Material");
            AddIgnoredProperty("MaterialExpressionGuid");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionSine(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Input")),
                ValueUtil.ParseFloat(node.FindPropertyValue("Period") ?? "1.0"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX") ?? "0"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY") ?? "0")
            );
        }
    }
}
