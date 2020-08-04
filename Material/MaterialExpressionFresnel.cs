using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionFresnel : Node
    {
        public ParsedPropertyBag BaseReflectFractionIn { get; }
        public ParsedPropertyBag ExponentIn { get; }
        public ParsedPropertyBag Power { get; }

        public MaterialExpressionFresnel(string name, ParsedPropertyBag baseReflectFractionIn, ParsedPropertyBag exponentIn, ParsedPropertyBag power, int editorX, int editorY) : base(name, editorX, editorY)
        {
            BaseReflectFractionIn = baseReflectFractionIn;
            ExponentIn = exponentIn;
            Power = power;
        }
    }

    public class MaterialExpressionFresnelProcessor : NodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionFresnel";

        public MaterialExpressionFresnelProcessor()
        {
            AddRequiredAttribute("Name", PropertyDataType.String);

            AddOptionalProperty("BaseReflectFractionIn", PropertyDataType.AttributeList);
            AddOptionalProperty("ExponentIn", PropertyDataType.AttributeList);
            AddOptionalProperty("MaterialExpressionEditorX", PropertyDataType.Integer);
            AddOptionalProperty("MaterialExpressionEditorY", PropertyDataType.Integer);
            AddOptionalProperty("Power", PropertyDataType.AttributeList);

            AddIgnoredProperty("Material");
            AddIgnoredProperty("MaterialExpressionGuid");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionFresnel(
                node.FindAttributeValue("Name"), 
                ValueUtil.ParseAttributeList(node.FindPropertyValue("BaseReflectFractionIn") ?? null),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("ExponentIn") ?? null),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Power") ?? null),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX") ?? "0"), 
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY") ?? "0")
            );
        }
    }
}
