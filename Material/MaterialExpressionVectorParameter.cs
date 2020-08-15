using JollySamurai.UnrealEngine4.T3D.Common;
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

    public class MaterialExpressionVectorParameterProcessor : ObjectNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionVectorParameter";

        public MaterialExpressionVectorParameterProcessor()
        {
            AddRequiredAttribute("Name", PropertyDataType.String);

            AddRequiredProperty("ParameterName", PropertyDataType.String);

            AddOptionalProperty("DefaultValue", PropertyDataType.Vector4);
            AddOptionalProperty("MaterialExpressionEditorX", PropertyDataType.Integer);
            AddOptionalProperty("MaterialExpressionEditorY", PropertyDataType.Integer);

            AddIgnoredProperty("ExpressionGUID");
            AddIgnoredProperty("Material");
            AddIgnoredProperty("MaterialExpressionGuid");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionVectorParameter(
                node.FindAttributeValue("Name"),
                node.FindPropertyValue("ParameterName"),
                ValueUtil.ParseVector4(node.FindPropertyValue("DefaultValue") ?? "(R=0.0,G=0.0,B=0.0,A=0.0)"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX") ?? "0"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY") ?? "0")
            );
        }
    }
}
