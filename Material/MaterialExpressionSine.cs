using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionSine : MaterialNode
    {
        public ParsedPropertyBag Input { get; }
        public float Period { get; }

        public MaterialExpressionSine(string name, int editorX, int editorY, ParsedPropertyBag input, float period)
            : base(name, editorX, editorY)
        {
            Input = input;
            Period = period;
        }
    }

    public class MaterialExpressionSineProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionSine";

        public MaterialExpressionSineProcessor()
        {
            AddRequiredProperty("Input", PropertyDataType.AttributeList);

            AddOptionalProperty("Period", PropertyDataType.Float);
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionSine(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Input")),
                ValueUtil.ParseFloat(node.FindPropertyValue("Period") ?? "1.0")
            );
        }
    }
}
