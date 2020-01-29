using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionLinearInterpolate : Node
    {
        public ParsedPropertyBag A { get; }
        public ParsedPropertyBag B { get; }
        public ParsedPropertyBag Alpha { get; }

        public MaterialExpressionLinearInterpolate(string name, ParsedPropertyBag a, ParsedPropertyBag b, ParsedPropertyBag alpha, int editorX, int editorY) : base(name, editorX, editorY)
        {
            A = a;
            B = b;
            Alpha = alpha;
        }
    }

    public class MaterialExpressionLinearInterpolateProcessor : NodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionLinearInterpolate";

        public MaterialExpressionLinearInterpolateProcessor()
        {
            AddRequiredAttribute("Name", PropertyDataType.String);

            AddRequiredProperty("MaterialExpressionEditorX", PropertyDataType.Integer);
            AddRequiredProperty("MaterialExpressionEditorY", PropertyDataType.Integer);

            AddOptionalProperty("A", PropertyDataType.AttributeList);
            AddOptionalProperty("Alpha", PropertyDataType.AttributeList);
            AddOptionalProperty("B", PropertyDataType.AttributeList);

            AddIgnoredProperty("Material");
            AddIgnoredProperty("MaterialExpressionGuid");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionLinearInterpolate(node.FindAttributeValue("Name"), ValueUtil.ParseAttributeList(node.FindPropertyValue("A")), ValueUtil.ParseAttributeList(node.FindPropertyValue("B")), ValueUtil.ParseAttributeList(node.FindPropertyValue("Alpha")), ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")), ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")));
        }
    }
}
