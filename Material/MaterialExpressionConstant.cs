using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionConstant : Node
    {
        public float R { get; }

        public MaterialExpressionConstant(string name, float r, int editorX, int editorY)
            : base(name, editorX, editorY)
        {
            R = r;
        }
    }

    public class MaterialExpressionConstantProcessor : NodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionConstant";

        public MaterialExpressionConstantProcessor()
        {
            AddRequiredAttribute("Name", PropertyDataType.String);

            AddOptionalProperty("MaterialExpressionEditorX", PropertyDataType.Integer);
            AddOptionalProperty("MaterialExpressionEditorY", PropertyDataType.Integer);
            AddOptionalProperty("R", PropertyDataType.Float);

            AddIgnoredProperty("Material");
            AddIgnoredProperty("MaterialExpressionGuid");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionConstant(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseFloat(node.FindPropertyValue("R") ?? "0.0"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX") ?? "0"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY") ?? "0")
            );
        }
    }
}
