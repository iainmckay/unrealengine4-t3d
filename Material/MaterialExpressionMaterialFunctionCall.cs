using System.Collections.Generic;
using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionMaterialFunctionCall : MaterialNode
    {
        public ExpressionReference MaterialFunction { get; }
        public ParsedPropertyBag[] FunctionInputs { get; }

        public MaterialExpressionMaterialFunctionCall(string name, int editorX, int editorY, ExpressionReference materialFunction, ParsedPropertyBag[] functionInputs)
            : base(name, editorX, editorY)
        {
            MaterialFunction = materialFunction;
            FunctionInputs = functionInputs;
        }
    }

    public class MaterialExpressionObjectFunctionCallProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionMaterialFunctionCall";

        public MaterialExpressionObjectFunctionCallProcessor()
        {
            AddRequiredProperty("MaterialFunction", PropertyDataType.ExpressionReference);

            AddOptionalProperty("FunctionInputs", PropertyDataType.AttributeList | PropertyDataType.Array);

            AddIgnoredProperty("FunctionOutputs");
            AddIgnoredProperty("Outputs");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            var functionInputList = new List<ParsedPropertyBag>();

            foreach (var parsedProperty in node.FindProperty("FunctionInputs")?.Elements ?? ParsedPropertyBag.Empty.Properties) {
                functionInputList.Add(ValueUtil.ParseAttributeList(parsedProperty?.Value));
            }

            return new MaterialExpressionMaterialFunctionCall(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")),
                ValueUtil.ParseExpressionReference(node.FindPropertyValue("MaterialFunction")),
                functionInputList.ToArray()
            );
        }
    }
}
