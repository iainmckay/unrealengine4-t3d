using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionClamp : Node
    {
        public ExpressionReference Input { get; }
        public ExpressionReference Min { get; }
        public ExpressionReference Max { get; }

        public MaterialExpressionClamp(string name, ExpressionReference input, ExpressionReference min, ExpressionReference max, int editorX, int editorY) : base(name, editorX, editorY)
        {
            Input = input;
            Min = min;
            Max = max;
        }
    }

    public class MaterialExpressionClampProcessor : NodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionClamp";

        public MaterialExpressionClampProcessor()
        {
            AddRequiredAttribute("Name", PropertyDataType.String);

            AddRequiredProperty("Input", PropertyDataType.ExpressionReference);
            AddRequiredProperty("MaterialExpressionEditorX", PropertyDataType.Integer);
            AddRequiredProperty("MaterialExpressionEditorY", PropertyDataType.Integer);
            AddRequiredProperty("Max", PropertyDataType.ExpressionReference);
            AddRequiredProperty("Min", PropertyDataType.ExpressionReference);

            AddIgnoredProperty("Material");
            AddIgnoredProperty("MaterialExpressionGuid");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionClamp(node.FindAttributeValue("Name"), ValueUtil.ParseExpressionReference(node.FindPropertyValue("Input")), ValueUtil.ParseExpressionReference(node.FindPropertyValue("Min")), ValueUtil.ParseExpressionReference(node.FindPropertyValue("Max")), ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")), ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")));
        }
    }
}
