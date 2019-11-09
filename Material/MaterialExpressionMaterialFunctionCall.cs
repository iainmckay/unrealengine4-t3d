using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionMaterialFunctionCall : Node
    {
        public ExpressionReference MaterialFunction { get; }
        public ExpressionReference[] FunctionInputs { get; }

        public MaterialExpressionMaterialFunctionCall(string name, ExpressionReference materialFunction, ExpressionReference[] functionInputs, int editorX, int editorY) : base(name, editorX, editorY)
        {
            MaterialFunction = materialFunction;
            FunctionInputs = functionInputs;
        }
    }

    public class MaterialExpressionMaterialFunctionCallProcessor : NodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionMaterialFunctionCall";

        public MaterialExpressionMaterialFunctionCallProcessor()
        {
            AddRequiredAttribute("Name", PropertyDataType.String);

            AddRequiredProperty("MaterialExpressionEditorX", PropertyDataType.Integer);
            AddRequiredProperty("MaterialExpressionEditorY", PropertyDataType.Integer);
            AddRequiredProperty("MaterialFunction", PropertyDataType.ExpressionReference);
            AddOptionalProperty("FunctionInputs", PropertyDataType.ExpressionReference | PropertyDataType.Array);

            AddIgnoredProperty("FunctionOutputs");
            AddIgnoredProperty("Material");
            AddIgnoredProperty("MaterialExpressionGuid");
            AddIgnoredProperty("Outputs");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            ParsedProperty functionInputList = node.FindProperty("FunctionInputs");
            ParsedProperty functionOutputList = node.FindProperty("FunctionOutputs");

            return new MaterialExpressionMaterialFunctionCall(node.FindAttributeValue("Name"), ValueUtil.ParseExpressionReference(node.FindPropertyValue("MaterialFunction")), ValueUtil.ParseExpressionReferenceArray(functionInputList.Elements), ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")), ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")));
        }
    }
}
