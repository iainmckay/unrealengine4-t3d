using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionConstant4Vector : VectorConstantNode
    {
        public MaterialExpressionConstant4Vector(string name, int editorX, int editorY, Vector4 constant)
            : base(name, editorX, editorY, constant)
        {
        }
    }

    public class MaterialExpressionConstant4VectorProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionConstant4Vector";

        public MaterialExpressionConstant4VectorProcessor()
        {
            AddOptionalProperty("Constant", PropertyDataType.Vector4);
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionConstant4Vector(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")),
                ValueUtil.ParseVector4(node.FindPropertyValue("Constant"))
            );
        }
    }
}
