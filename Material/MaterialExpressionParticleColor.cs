using JollySamurai.UnrealEngine4.T3D.Parser;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionParticleColor : MaterialNode
    {
        public MaterialExpressionParticleColor(string name, int editorX, int editorY)
            : base(name, editorX, editorY)
        {
        }
    }

    public class MaterialExpressionParticleColorProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionParticleColor";

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionParticleColor(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY"))
            );
        }
    }
}
