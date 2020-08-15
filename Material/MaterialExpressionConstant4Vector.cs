using JollySamurai.UnrealEngine4.T3D.Common;
using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionConstant4Vector : VectorConstantNode
    {
        public MaterialExpressionConstant4Vector(string name, Vector4 constant, int editorX, int editorY)
            : base(name, constant, editorX, editorY)
        {
        }
    }

    public class MaterialExpressionConstant4VectorProcessor : ObjectNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionConstant4Vector";

        public MaterialExpressionConstant4VectorProcessor()
        {
            AddRequiredAttribute("Name", PropertyDataType.String);

            AddRequiredProperty("Constant", PropertyDataType.Vector4);

            AddOptionalProperty("MaterialExpressionEditorX", PropertyDataType.Integer);
            AddOptionalProperty("MaterialExpressionEditorY", PropertyDataType.Integer);

            AddIgnoredProperty("Material");
            AddIgnoredProperty("MaterialExpressionGuid");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionConstant4Vector(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseVector4(node.FindPropertyValue("Constant") ?? "(R=0.0,G=0.0,B=0.0,A=0.0)"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX") ?? "0"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY") ?? "0")
            );
        }
    }
}
