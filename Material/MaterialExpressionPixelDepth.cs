using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionPixelDepth : MaterialNode
    {
        public MaterialExpressionPixelDepth(string name, int editorX, int editorY)
            : base(name, editorX, editorY)
        {
        }
    }

    public class MaterialExpressionPixelDepthProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionPixelDepth";

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionPixelDepth(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY"))
            );
        }
    }
}
