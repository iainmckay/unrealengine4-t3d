using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionLinearInterpolate : Node
    {
        public UnresolvedExpressionReference A { get; }
        public UnresolvedExpressionReference B { get; }
        public UnresolvedExpressionReference Alpha { get; }

        public MaterialExpressionLinearInterpolate(string name, UnresolvedExpressionReference a, UnresolvedExpressionReference b, UnresolvedExpressionReference alpha, int editorX, int editorY) : base(name, editorX, editorY)
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

            AddOptionalProperty("A", PropertyDataType.ExpressionReference);
            AddOptionalProperty("Alpha", PropertyDataType.ExpressionReference);
            AddOptionalProperty("B", PropertyDataType.ExpressionReference);

            AddIgnoredProperty("Material");
            AddIgnoredProperty("MaterialExpressionGuid");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionLinearInterpolate(node.FindAttributeValue("Name"), ValueUtil.ParseExpressionReference(node.FindPropertyValue("A")), ValueUtil.ParseExpressionReference(node.FindPropertyValue("B")), ValueUtil.ParseExpressionReference(node.FindPropertyValue("Alpha")), ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")), ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")));
        }
    }
}
