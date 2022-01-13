using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionConstant : MaterialNode
    {
        public float R { get; }

        public MaterialExpressionConstant(string name, int editorX, int editorY, float r)
            : base(name, editorX, editorY)
        {
            R = r;
        }
    }

    public class MaterialExpressionConstantProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionConstant";

        public MaterialExpressionConstantProcessor()
        {
            AddOptionalProperty("R", PropertyDataType.Float);
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionConstant(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")),
                ValueUtil.ParseFloat(node.FindPropertyValue("R"))
            );
        }
    }
}
