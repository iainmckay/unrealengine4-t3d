using JollySamurai.UnrealEngine4.T3D.Parser;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionVertexNormalWS : MaterialNode
    {
        public MaterialExpressionVertexNormalWS(string name, int editorX, int editorY)
            : base(name, editorX, editorY)
        {
        }
    }

    public class MaterialExpressionVertexNormalWSProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionVertexNormalWS";

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionVertexNormalWS(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY"))
            );
        }
    }
}
