using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionConstant3Vector : VectorConstantNode
    {
        public MaterialExpressionConstant3Vector(string name, Vector4 constant, int editorX, int editorY)
            : base(name, constant, editorX, editorY)
        {
        }
    }

    public class MaterialExpressionConstant3VectorProcessor : VectorConstantProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionConstant3Vector";

        public MaterialExpressionConstant3VectorProcessor() : base()
        {
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionConstant3Vector(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseVector4(node.FindPropertyValue("Constant")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX") ?? "0"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY") ?? "0")
            );
        }
    }
}
