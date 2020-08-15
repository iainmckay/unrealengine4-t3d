using System.Collections.Generic;
using System.Diagnostics;
using JollySamurai.UnrealEngine4.T3D.Common;
using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionMaterialFunctionCall : Node
    {
        public ExpressionReference MaterialFunction { get; }
        public ParsedPropertyBag[] FunctionInputs { get; }

        public MaterialExpressionMaterialFunctionCall(string name, ExpressionReference materialFunction, ParsedPropertyBag[] functionInputs, int editorX, int editorY)
            : base(name, editorX, editorY)
        {
            MaterialFunction = materialFunction;
            FunctionInputs = functionInputs;
        }
    }

    public class MaterialExpressionObjectFunctionCallProcessor : ObjectNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionMaterialFunctionCall";

        public MaterialExpressionObjectFunctionCallProcessor()
        {
            AddRequiredAttribute("Name", PropertyDataType.String);

            AddRequiredProperty("MaterialFunction", PropertyDataType.ExpressionReference);

            AddOptionalProperty("FunctionInputs", PropertyDataType.AttributeList | PropertyDataType.Array);
            AddOptionalProperty("MaterialExpressionEditorX", PropertyDataType.Integer);
            AddOptionalProperty("MaterialExpressionEditorY", PropertyDataType.Integer);

            AddIgnoredProperty("FunctionOutputs");
            AddIgnoredProperty("Material");
            AddIgnoredProperty("MaterialExpressionGuid");
            AddIgnoredProperty("Outputs");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            var functionInputList = new List<ParsedPropertyBag>();

            foreach (var parsedProperty in node.FindProperty("FunctionInputs").Elements) {
                functionInputList.Add(ValueUtil.ParseAttributeList(parsedProperty.Value));
            }

            return new MaterialExpressionMaterialFunctionCall(node.FindAttributeValue("Name"), ValueUtil.ParseExpressionReference(node.FindPropertyValue("MaterialFunction")), functionInputList.ToArray(), ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX") ?? "0"), ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY") ?? "0"));
        }
    }
}
