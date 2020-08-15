using JollySamurai.UnrealEngine4.T3D.Common;
using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionTextureObjectParameter : ParameterNode<ResourceReference>
    {
        public MaterialExpressionTextureObjectParameter(string name, string parameterName, ResourceReference resource, int editorX, int editorY)
            : base(name, parameterName, resource, editorX, editorY)
        {
        }
    }

    public class MaterialExpressionTextureObjectParameterProcessor : ObjectNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionTextureObjectParameter";

        public MaterialExpressionTextureObjectParameterProcessor()
        {
            AddRequiredAttribute("Name", PropertyDataType.String);

            AddRequiredProperty("ParameterName", PropertyDataType.String);
            AddRequiredProperty("Texture", PropertyDataType.ResourceReference);

            AddOptionalProperty("MaterialExpressionEditorX", PropertyDataType.Integer);
            AddOptionalProperty("MaterialExpressionEditorY", PropertyDataType.Integer);

            AddIgnoredProperty("ExpressionGUID");
            AddIgnoredProperty("Material");
            AddIgnoredProperty("MaterialExpressionGuid");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionTextureObjectParameter(
                node.FindAttributeValue("Name"),
                node.FindPropertyValue("ParameterName"),
                ValueUtil.ParseResourceReference(node.FindPropertyValue("Texture")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX") ?? "0"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY") ?? "0")
            );
        }
    }
}
