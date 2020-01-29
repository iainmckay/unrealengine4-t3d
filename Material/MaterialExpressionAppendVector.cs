using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionAppendVector : Node
    {
        public ParsedPropertyBag A { get; }
        public ParsedPropertyBag B { get; }

        public MaterialExpressionAppendVector(string name, ParsedPropertyBag a, ParsedPropertyBag b, int editorX, int editorY) : base(name, editorX, editorY)
        {
            A = a;
            B = b;
        }
    }

    public class MaterialExpressionAppendVectorProcessor : NodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionAppendVector";

        public MaterialExpressionAppendVectorProcessor()
        {
            AddRequiredAttribute("Name", PropertyDataType.String);

            AddRequiredProperty("MaterialExpressionEditorX", PropertyDataType.Integer);
            AddRequiredProperty("MaterialExpressionEditorY", PropertyDataType.Integer);

            AddOptionalProperty("A", PropertyDataType.AttributeList);
            AddOptionalProperty("B", PropertyDataType.AttributeList);

            AddIgnoredProperty("Material");
            AddIgnoredProperty("MaterialExpressionGuid");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionAppendVector(node.FindAttributeValue("Name"), ValueUtil.ParseAttributeList(node.FindPropertyValue("A")), ValueUtil.ParseAttributeList(node.FindPropertyValue("B")), ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")), ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")));
        }
    }
}
