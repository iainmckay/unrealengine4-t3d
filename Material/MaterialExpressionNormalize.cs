using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionNormalize : MaterialNode
    {
        public ParsedPropertyBag VectorInput { get; }

        public MaterialExpressionNormalize(string name, int editorX, int editorY, ParsedPropertyBag vectorInput)
            : base(name, editorX, editorY)
        {
            VectorInput = vectorInput;
        }
    }

    public class MaterialExpressionNormalizeProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionNormalize";

        public MaterialExpressionNormalizeProcessor()
        {
            AddOptionalProperty("VectorInput", PropertyDataType.AttributeList);
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionNormalize(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("VectorInput"))
            );
        }
    }
}
