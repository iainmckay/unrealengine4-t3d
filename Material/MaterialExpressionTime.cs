using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionTime : MaterialNode
    {
        public MaterialExpressionTime(string name, int editorX, int editorY)
            : base(name, editorX, editorY)
        {
        }
    }

    public class MaterialExpressionTimeProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionTime";

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionTime(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY"))
            );
        }
    }
}
