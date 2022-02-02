using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionAbs : MaterialNode
    {
        public ParsedPropertyBag Input { get; }

        public MaterialExpressionAbs(string name, int editorX, int editorY, ParsedPropertyBag input)
            : base(name, editorX, editorY)
        {
            Input = input;
        }
    }

    public class MaterialExpressionAbsProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionAbs";

        public MaterialExpressionAbsProcessor()
        {
            AddOptionalProperty("Input", PropertyDataType.AttributeList);

            AddIgnoredProperty("bRealtimePreview");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionAbs(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Input"))
            );
        }
    }
}
