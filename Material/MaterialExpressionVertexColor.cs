using JollySamurai.UnrealEngine4.T3D.Parser;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionVertexColor : MaterialNode
    {
        public MaterialExpressionVertexColor(string name, int editorX, int editorY)
            : base(name, editorX, editorY)
        {
        }
    }

    public class MaterialExpressionVertexColorProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionVertexColor";

        public MaterialExpressionVertexColorProcessor()
        {
            AddIgnoredProperty("bCollapsed");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionVertexColor(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY"))
            );
        }
    }
}
