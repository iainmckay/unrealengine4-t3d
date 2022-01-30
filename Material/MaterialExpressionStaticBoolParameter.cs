using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionStaticBoolParameter : ParameterNode<bool>
    {
        public MaterialExpressionStaticBoolParameter(string name, int editorX, int editorY, string parameterName, bool defaultValue)
            : base(name, editorX, editorY, parameterName, defaultValue)
        {
        }
    }

    public class MaterialExpressionStaticBoolParameterProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionStaticBoolParameter";

        public MaterialExpressionStaticBoolParameterProcessor()
        {
            AddOptionalProperty("DefaultValue", PropertyDataType.Boolean);
            AddOptionalProperty("ParameterName", PropertyDataType.String);

            AddIgnoredProperty("ExpressionGUID");
            AddIgnoredProperty("Group");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionStaticBoolParameter(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")),
                node.FindPropertyValue("ParameterName") ?? "Param",
                ValueUtil.ParseBoolean(node.FindPropertyValue("DefaultValue"))
            );
        }
    }
}
