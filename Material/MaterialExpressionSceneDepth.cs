using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionSceneDepth : MaterialNode
    {
        public MaterialSceneAttributeInputMode InputMode { get; }
        public ParsedPropertyBag Input { get; }
        public Vector2 ConstInput { get; }

        public MaterialExpressionSceneDepth(string name, int editorX, int editorY, MaterialSceneAttributeInputMode inputMode, ParsedPropertyBag input, Vector2 constInput)
            : base(name, editorX, editorY)
        {
            InputMode = inputMode;
            Input = input;
            ConstInput = constInput;
        }
    }

    public class MaterialExpressionSceneDepthProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionSceneDepth";

        public MaterialExpressionSceneDepthProcessor()
        {
            AddOptionalProperty("InputMode", PropertyDataType.MaterialSceneAttributeInputMode);
            AddOptionalProperty("Input", PropertyDataType.AttributeList);
            AddOptionalProperty("ConstInput", PropertyDataType.Vector2);
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionSceneDepth(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")),
                ValueUtil.ParseMaterialSceneAttributeInputMode(node.FindPropertyValue("InputMode")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Input")),
                ValueUtil.ParseVector2(node.FindPropertyValue("ConstInput"))
            );
        }
    }
}
