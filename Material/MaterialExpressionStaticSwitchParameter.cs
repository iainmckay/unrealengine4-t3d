using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionStaticSwitchParameter : Node
    {
        public string ParameterName { get; }
        public bool DefaultValue { get; }
        public UnresolvedExpressionReference A { get; }
        public UnresolvedExpressionReference B { get; }

        public MaterialExpressionStaticSwitchParameter(string name, string parameterName, bool defaultValue, UnresolvedExpressionReference a, UnresolvedExpressionReference b, int editorX, int editorY) : base(name, editorX, editorY)
        {
            ParameterName = parameterName;
            DefaultValue = defaultValue;
            A = a;
            B = b;
        }
    }

    public class MaterialExpressionStaticSwitchParameterProcessor : NodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionStaticSwitchParameter";

        public MaterialExpressionStaticSwitchParameterProcessor()
        {
            AddRequiredAttribute("Name", PropertyDataType.String);

            AddRequiredProperty("DefaultValue", PropertyDataType.Boolean);
            AddRequiredProperty("MaterialExpressionEditorX", PropertyDataType.Integer);
            AddRequiredProperty("MaterialExpressionEditorY", PropertyDataType.Integer);
            AddRequiredProperty("ParameterName", PropertyDataType.String);

            AddOptionalProperty("A", PropertyDataType.ExpressionReference);
            AddOptionalProperty("B", PropertyDataType.ExpressionReference);

            AddIgnoredProperty("ExpressionGUID");
            AddIgnoredProperty("Material");
            AddIgnoredProperty("MaterialExpressionGuid");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionStaticSwitchParameter(node.FindAttributeValue("Name"), node.FindPropertyValue("ParameterName"), ValueUtil.ParseBoolean(node.FindPropertyValue("DefaultValue")), ValueUtil.ParseExpressionReference(node.FindPropertyValue("A")), ValueUtil.ParseExpressionReference(node.FindPropertyValue("B")), ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")), ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")));
        }
    }
}
