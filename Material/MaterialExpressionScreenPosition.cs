using JollySamurai.UnrealEngine4.T3D.Parser;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionScreenPosition : MaterialNode
    {
        public MaterialExpressionScreenPosition(string name, int editorX, int editorY)
            : base(name, editorX, editorY)
        {
        }
    }

    public class MaterialExpressionScreenPositionProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionScreenPosition";

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionScreenPosition(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY"))
            );
        }
    }
}
