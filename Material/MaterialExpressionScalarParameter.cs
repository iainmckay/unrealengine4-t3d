using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionScalarParameter : ParameterNode<float>
    {
        public MaterialExpressionScalarParameter(string name, int editorX, int editorY, string parameterName, float defaultValue)
            : base(name, editorX, editorY, parameterName, defaultValue)
        {
        }
    }

    public class MaterialExpressionScalarParameterProcessor : ParameterNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionScalarParameter";

        public MaterialExpressionScalarParameterProcessor()
        {
            AddRequiredProperty("ParameterName", PropertyDataType.String);

            AddOptionalProperty("DefaultValue", PropertyDataType.Float);

            AddIgnoredProperty("ExpressionGUID");
            AddIgnoredProperty("Group");
            AddIgnoredProperty("SliderMax");
            AddIgnoredProperty("SliderMin");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionScalarParameter(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")),
                node.FindPropertyValue("ParameterName"),
                ValueUtil.ParseFloat(node.FindPropertyValue("DefaultValue"))
            );
        }
    }
}
