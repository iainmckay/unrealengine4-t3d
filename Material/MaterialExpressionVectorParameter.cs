using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionVectorParameter : ParameterNode<Vector4>
    {
        public MaterialExpressionVectorParameter(string name, int editorX, int editorY, string parameterName, Vector4 defaultValue)
            : base(name, editorX, editorY, parameterName, defaultValue)
        {
        }
    }

    public class MaterialExpressionVectorParameterProcessor : ParameterNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionVectorParameter";

        public MaterialExpressionVectorParameterProcessor()
        {
            AddRequiredProperty("ParameterName", PropertyDataType.String);

            AddOptionalProperty("DefaultValue", PropertyDataType.Vector4);

            AddIgnoredProperty("ExpressionGUID");
            AddIgnoredProperty("Group");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionVectorParameter(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")),
                node.FindPropertyValue("ParameterName"),
                ValueUtil.ParseVector4(node.FindPropertyValue("DefaultValue"))
            );
        }
    }
}
