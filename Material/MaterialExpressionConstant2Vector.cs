using JollySamurai.UnrealEngine4.T3D.Common;
using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionConstant2Vector : Node
    {
        public float R { get; }
        public float G { get; }

        public MaterialExpressionConstant2Vector(string name, float r, float g, int editorX, int editorY)
            : base(name, editorX, editorY)
        {
            R = r;
            G = g;
        }
    }

    public class MaterialExpressionConstant2VectorProcessor : ObjectNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionConstant2Vector";

        public MaterialExpressionConstant2VectorProcessor()
        {
            AddRequiredAttribute("Name", PropertyDataType.String);

            AddRequiredProperty("R", PropertyDataType.Float);
            AddRequiredProperty("G", PropertyDataType.Float);

            AddOptionalProperty("MaterialExpressionEditorX", PropertyDataType.Integer);
            AddOptionalProperty("MaterialExpressionEditorY", PropertyDataType.Integer);

            AddIgnoredProperty("Material");
            AddIgnoredProperty("MaterialExpressionGuid");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionConstant2Vector(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseFloat(node.FindPropertyValue("R") ?? "0.0"),
                ValueUtil.ParseFloat(node.FindPropertyValue("G") ?? "0.0"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX") ?? "0"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY") ?? "0")
            );
        }
    }
}
