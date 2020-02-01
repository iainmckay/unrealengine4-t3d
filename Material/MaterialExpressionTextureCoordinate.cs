using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionTextureCoordinate : Node
    {
        public bool Collapsed { get; }
        public int CoordinateIndex { get; }

        public MaterialExpressionTextureCoordinate(string name, bool collapsed, int coordinateIndex, int editorX, int editorY) : base(name, editorX, editorY)
        {
            Collapsed = collapsed;
            CoordinateIndex = coordinateIndex;
        }
    }

    public class MaterialExpressionTextureCoordinateProcessor : NodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionTextureCoordinate";

        public MaterialExpressionTextureCoordinateProcessor()
        {
            AddRequiredAttribute("Name", PropertyDataType.String);

            AddOptionalProperty("bCollapsed", PropertyDataType.Boolean);
            AddOptionalProperty("CoordinateIndex", PropertyDataType.Integer);
            AddOptionalProperty("MaterialExpressionEditorX", PropertyDataType.Integer);
            AddOptionalProperty("MaterialExpressionEditorY", PropertyDataType.Integer);

            AddIgnoredProperty("Material");
            AddIgnoredProperty("MaterialExpressionGuid");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionTextureCoordinate(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseBoolean(node.FindPropertyValue("bCollapsed") ?? "False"),
                ValueUtil.ParseInteger(node.FindPropertyValue("CoordinateIndex") ?? "0"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX") ?? "0"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY") ?? "0")
            );
        }
    }
}
