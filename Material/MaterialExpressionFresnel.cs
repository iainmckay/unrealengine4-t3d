using JollySamurai.UnrealEngine4.T3D.Common;
using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionFresnel : Node
    {
        public ParsedPropertyBag BaseReflectFractionIn { get; }
        public float BaseReflectFraction { get; }
        public ParsedPropertyBag ExponentIn { get; }
        public float Exponent { get; }
        public ParsedPropertyBag Power { get; }
        public ParsedPropertyBag Normal { get; }

        public MaterialExpressionFresnel(string name, ParsedPropertyBag baseReflectFractionIn, float baseReflectFraction, ParsedPropertyBag exponentIn, float exponent, ParsedPropertyBag power, ParsedPropertyBag normal, int editorX, int editorY) : base(name, editorX, editorY)
        {
            BaseReflectFractionIn = baseReflectFractionIn;
            BaseReflectFraction = baseReflectFraction;
            ExponentIn = exponentIn;
            Exponent = exponent;
            Power = power;
            Normal = normal;
        }
    }

    public class MaterialExpressionFresnelProcessor : ObjectNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionFresnel";

        public MaterialExpressionFresnelProcessor()
        {
            AddRequiredAttribute("Name", PropertyDataType.String);

            AddOptionalProperty("BaseReflectFraction", PropertyDataType.Float);
            AddOptionalProperty("BaseReflectFractionIn", PropertyDataType.AttributeList);
            AddOptionalProperty("Exponent", PropertyDataType.Float);
            AddOptionalProperty("ExponentIn", PropertyDataType.AttributeList);
            AddOptionalProperty("MaterialExpressionEditorX", PropertyDataType.Integer);
            AddOptionalProperty("MaterialExpressionEditorY", PropertyDataType.Integer);
            AddOptionalProperty("Normal", PropertyDataType.AttributeList);
            AddOptionalProperty("Power", PropertyDataType.AttributeList);

            AddIgnoredProperty("Material");
            AddIgnoredProperty("MaterialExpressionGuid");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionFresnel(
                node.FindAttributeValue("Name"), 
                ValueUtil.ParseAttributeList(node.FindPropertyValue("BaseReflectFractionIn") ?? null),
                ValueUtil.ParseFloat(node.FindPropertyValue("BaseReflectFraction") ?? "0.04"),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("ExponentIn") ?? null),
                ValueUtil.ParseFloat(node.FindPropertyValue("Exponent") ?? "5.0"),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Power") ?? null),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Normal") ?? null),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX") ?? "0"), 
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY") ?? "0")
            );
        }
    }
}
