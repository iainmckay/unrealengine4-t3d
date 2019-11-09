using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionScalarParameter : ParameterNode<float>
    {
        public MaterialExpressionScalarParameter(string name, string parameterName, float defaultValue, int editorX, int editorY)
            : base(name, parameterName, defaultValue, editorX, editorY)
        {
        }
    }

    public class MaterialExpressionScalarParameterProcessor : NodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionScalarParameter";

        public MaterialExpressionScalarParameterProcessor()
        {
            AddRequiredAttribute("Name", PropertyDataType.String);

            AddRequiredProperty("MaterialExpressionEditorX", PropertyDataType.Integer);
            AddRequiredProperty("MaterialExpressionEditorY", PropertyDataType.Integer);
            AddRequiredProperty("ParameterName", PropertyDataType.String);

            AddOptionalProperty("DefaultValue", PropertyDataType.Float);

            AddIgnoredProperty("ExpressionGUID");
            AddIgnoredProperty("Material");
            AddIgnoredProperty("MaterialExpressionGuid");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionScalarParameter(node.FindAttributeValue("Name"), node.FindPropertyValue("ParameterName"), node.HasProperty("DefaultValue") ? ValueUtil.ParseFloat(node.FindPropertyValue("DefaultValue")) : 0, ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")), ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")));
        }
    }
}
