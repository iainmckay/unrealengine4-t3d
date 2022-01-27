using JollySamurai.UnrealEngine4.T3D.Parser;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionConstant3Vector : VectorConstantNode
    {
        public MaterialExpressionConstant3Vector(string name, int editorX, int editorY, Vector4 constant)
            : base(name, editorX, editorY, constant)
        {
        }
    }

    public class MaterialExpressionConstant3VectorProcessor : VectorConstantProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionConstant3Vector";

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionConstant3Vector(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")),
                ValueUtil.ParseVector4(node.FindPropertyValue("Constant"))
            );
        }
    }
}
