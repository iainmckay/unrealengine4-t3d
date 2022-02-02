using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionFloor : MaterialNode
    {
        public ParsedPropertyBag Input { get; }

        public MaterialExpressionFloor(string name, int editorX, int editorY, ParsedPropertyBag input)
            : base(name, editorX, editorY)
        {
            Input = input;
        }
    }

    public class MaterialExpressionFloorProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionFloor";

        public MaterialExpressionFloorProcessor()
        {
            AddOptionalProperty("Input", PropertyDataType.AttributeList);
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionFloor(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Input"))
            );
        }
    }
}
