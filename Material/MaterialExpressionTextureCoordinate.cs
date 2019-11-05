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

            AddRequiredProperty("bCollapsed", PropertyDataType.Boolean);
            AddRequiredProperty("CoordinateIndex", PropertyDataType.Integer);
            AddRequiredProperty("MaterialExpressionEditorX", PropertyDataType.Integer);
            AddRequiredProperty("MaterialExpressionEditorY", PropertyDataType.Integer);

            AddIgnoredProperty("Material");
            AddIgnoredProperty("MaterialExpressionGuid");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionTextureCoordinate(node.FindAttributeValue("Name"), ValueUtil.ParseBoolean(node.FindPropertyValue("bCollapsed")), ValueUtil.ParseInteger(node.FindPropertyValue("CoordinateIndex")), ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")), ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")));
        }
    }
}
