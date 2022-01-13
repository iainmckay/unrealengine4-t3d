using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionFrac : MaterialNode
    {
        public ParsedPropertyBag Input { get; }

        public MaterialExpressionFrac(string name, int editorX, int editorY, ParsedPropertyBag input)
            : base(name, editorX, editorY)
        {
            Input = input;
        }
    }

    public class MaterialExpressionFracProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionFrac";

        public MaterialExpressionFracProcessor()
        {
            AddRequiredProperty("Input", PropertyDataType.AttributeList);
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionFrac(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Input"))
            );
        }
    }
}
