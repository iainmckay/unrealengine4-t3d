using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionFresnel : MaterialNode
    {
        public ParsedPropertyBag BaseReflectFractionIn { get; }
        public float BaseReflectFraction { get; }
        public ParsedPropertyBag ExponentIn { get; }
        public float Exponent { get; }
        public ParsedPropertyBag Power { get; }
        public ParsedPropertyBag Normal { get; }

        public MaterialExpressionFresnel(string name, int editorX, int editorY, ParsedPropertyBag baseReflectFractionIn, float baseReflectFraction, ParsedPropertyBag exponentIn, float exponent, ParsedPropertyBag power, ParsedPropertyBag normal)
            : base(name, editorX, editorY)
        {
            BaseReflectFractionIn = baseReflectFractionIn;
            BaseReflectFraction = baseReflectFraction;
            ExponentIn = exponentIn;
            Exponent = exponent;
            Power = power;
            Normal = normal;
        }
    }

    public class MaterialExpressionFresnelProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionFresnel";

        public MaterialExpressionFresnelProcessor()
        {
            AddOptionalProperty("BaseReflectFraction", PropertyDataType.Float);
            AddOptionalProperty("BaseReflectFractionIn", PropertyDataType.AttributeList);
            AddOptionalProperty("Exponent", PropertyDataType.Float);
            AddOptionalProperty("ExponentIn", PropertyDataType.AttributeList);
            AddOptionalProperty("Normal", PropertyDataType.AttributeList);
            AddOptionalProperty("Power", PropertyDataType.AttributeList);
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionFresnel(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("BaseReflectFractionIn")),
                ValueUtil.ParseFloat(node.FindPropertyValue("BaseReflectFraction") ?? "0.04"),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("ExponentIn")),
                ValueUtil.ParseFloat(node.FindPropertyValue("Exponent") ?? "5.0"),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Power")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Normal"))
            );
        }
    }
}
