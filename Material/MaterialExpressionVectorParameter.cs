using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionVectorParameter : ParameterNode<Vector4>
    {
        public MaterialExpressionVectorParameter(string name, string parameterName, Vector4 defaultValue, int editorX, int editorY)
            : base(name, parameterName, defaultValue, editorX, editorY)
        {
        }
    }

    public class MaterialExpressionVectorParameterProcessor : NodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionVectorParameter";

        public MaterialExpressionVectorParameterProcessor()
        {
            AddRequiredAttribute("Name", PropertyDataType.String);

            AddRequiredProperty("DefaultValue", PropertyDataType.Vector4);
            AddRequiredProperty("MaterialExpressionEditorX", PropertyDataType.Integer);
            AddRequiredProperty("MaterialExpressionEditorY", PropertyDataType.Integer);
            AddRequiredProperty("ParameterName", PropertyDataType.String);

            AddIgnoredProperty("ExpressionGUID");
            AddIgnoredProperty("Material");
            AddIgnoredProperty("MaterialExpressionGuid");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionVectorParameter(node.FindAttributeValue("Name"), node.FindPropertyValue("ParameterName"), ValueUtil.ParseVector4(node.FindPropertyValue("DefaultValue")), ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")), ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")));
        }
    }
}
