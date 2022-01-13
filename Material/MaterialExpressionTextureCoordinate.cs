using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionTextureCoordinate : MaterialNode
    {
        public bool Collapsed { get; }
        public int CoordinateIndex { get; }
        public float UTiling { get; }
        public float VTiling { get; }

        public MaterialExpressionTextureCoordinate(string name, int editorX, int editorY, bool collapsed, int coordinateIndex, float uTiling, float vTiling)
            : base(name, editorX, editorY)
        {
            Collapsed = collapsed;
            CoordinateIndex = coordinateIndex;
            UTiling = uTiling;
            VTiling = vTiling;
        }
    }

    public class MaterialExpressionTextureCoordinateProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionTextureCoordinate";

        public MaterialExpressionTextureCoordinateProcessor()
        {
            AddOptionalProperty("bCollapsed", PropertyDataType.Boolean);
            AddOptionalProperty("CoordinateIndex", PropertyDataType.Integer);
            AddOptionalProperty("UTiling", PropertyDataType.Float);
            AddOptionalProperty("VTiling", PropertyDataType.Float);
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionTextureCoordinate(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")),
                ValueUtil.ParseBoolean(node.FindPropertyValue("bCollapsed")),
                ValueUtil.ParseInteger(node.FindPropertyValue("CoordinateIndex")),
                ValueUtil.ParseFloat(node.FindPropertyValue("UTiling") ?? "1.0"),
                ValueUtil.ParseFloat(node.FindPropertyValue("VTiling") ?? "1.0")
            );
        }
    }
}
