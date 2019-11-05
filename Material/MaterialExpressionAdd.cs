using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionAdd : Node
    {
        public UnresolvedExpressionReference A { get; }
        public UnresolvedExpressionReference B { get; }

        public MaterialExpressionAdd(string name, UnresolvedExpressionReference a, UnresolvedExpressionReference b, int editorX, int editorY) : base(name, editorX, editorY)
        {
            A = a;
            B = b;
        }
    }

    public class MaterialExpressionAddProcessor : NodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionAdd";

        public MaterialExpressionAddProcessor()
        {
            AddRequiredAttribute("Name", PropertyDataType.String);

            AddRequiredProperty("MaterialExpressionEditorX", PropertyDataType.Integer);
            AddRequiredProperty("MaterialExpressionEditorY", PropertyDataType.Integer);

            AddOptionalProperty("A", PropertyDataType.ExpressionReference);
            AddOptionalProperty("B", PropertyDataType.ExpressionReference);

            AddIgnoredProperty("MaterialExpressionGuid");
            AddIgnoredProperty("Material");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionAdd(node.FindAttributeValue("Name"), ValueUtil.ParseExpressionReference(node.FindPropertyValue("A")), ValueUtil.ParseExpressionReference(node.FindPropertyValue("B")), ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")), ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")));
        }
    }
}
