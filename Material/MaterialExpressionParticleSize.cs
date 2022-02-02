using JollySamurai.UnrealEngine4.T3D.Parser;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionParticleSize : MaterialNode
    {
        public MaterialExpressionParticleSize(string name, int editorX, int editorY)
            : base(name, editorX, editorY)
        {
        }
    }

    public class MaterialExpressionParticleSizeProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionParticleSize";

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionParticleSize(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY"))
            );
        }
    }
}
