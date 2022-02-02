using JollySamurai.UnrealEngine4.T3D.Parser;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionCameraPositionWS : MaterialNode
    {
        public MaterialExpressionCameraPositionWS(string name, int editorX, int editorY)
            : base(name, editorX, editorY)
        {
        }
    }

    public class MaterialExpressionCameraPositionWSProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionCameraPositionWS";

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionCameraPositionWS(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY"))
            );
        }
    }
}
