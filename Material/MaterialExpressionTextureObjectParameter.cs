using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionTextureObjectParameter : ParameterNode<ResourceReference>
    {
        public MaterialExpressionTextureObjectParameter(string name, int editorX, int editorY, string parameterName, ResourceReference resource)
            : base(name, editorX, editorY, parameterName, resource)
        {
        }
    }

    public class MaterialExpressionTextureObjectParameterProcessor : ParameterNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionTextureObjectParameter";

        public MaterialExpressionTextureObjectParameterProcessor()
        {
            AddRequiredProperty("ParameterName", PropertyDataType.String);

            AddOptionalProperty("Texture", PropertyDataType.ResourceReference);

            AddIgnoredProperty("ExpressionGUID");
            AddIgnoredProperty("Group");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionTextureObjectParameter(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")),
                node.FindPropertyValue("ParameterName"),
                ValueUtil.ParseResourceReference(node.FindPropertyValue("Texture"))
            );
        }
    }
}
