using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionDesaturation : Node
    {
        public UnresolvedExpressionReference Input { get; }
        public UnresolvedExpressionReference Fraction { get; }

        public MaterialExpressionDesaturation(string name, UnresolvedExpressionReference input, UnresolvedExpressionReference fraction, int editorX, int editorY) : base(name, editorX, editorY)
        {
            Input = input;
            Fraction = fraction;
        }
    }

    public class MaterialExpressionDesaturationProcessor : NodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionDesaturation";

        public MaterialExpressionDesaturationProcessor()
        {
            AddRequiredAttribute("Name", PropertyDataType.String);

            AddRequiredProperty("MaterialExpressionEditorX", PropertyDataType.Integer);
            AddRequiredProperty("MaterialExpressionEditorY", PropertyDataType.Integer);

            AddOptionalProperty("Input", PropertyDataType.ExpressionReference);
            AddOptionalProperty("Fraction", PropertyDataType.ExpressionReference);

            AddIgnoredProperty("Material");
            AddIgnoredProperty("MaterialExpressionGuid");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionDesaturation(node.FindAttributeValue("Name"), ValueUtil.ParseExpressionReference(node.FindPropertyValue("Input")), ValueUtil.ParseExpressionReference(node.FindPropertyValue("Fraction")), ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")), ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")));
        }
    }
}
