using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionConstant2Vector : MaterialNode
    {
        public float R { get; }
        public float G { get; }

        public MaterialExpressionConstant2Vector(string name, int editorX, int editorY, float r, float g)
            : base(name, editorX, editorY)
        {
            R = r;
            G = g;
        }
    }

    public class MaterialExpressionConstant2VectorProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionConstant2Vector";

        public MaterialExpressionConstant2VectorProcessor()
        {
            AddOptionalProperty("G", PropertyDataType.Float);
            AddOptionalProperty("R", PropertyDataType.Float);
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionConstant2Vector(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")),
                ValueUtil.ParseFloat(node.FindPropertyValue("R")),
                ValueUtil.ParseFloat(node.FindPropertyValue("G"))
            );
        }
    }
}
