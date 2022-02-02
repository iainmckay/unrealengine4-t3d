using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionWorldPosition : MaterialNode
    {
        public WorldPositionIncludedOffsets WorldPositionIncludedOffsets { get; }

        public MaterialExpressionWorldPosition(string name, int editorX, int editorY, WorldPositionIncludedOffsets worldPositionIncludedOffsets)
            : base(name, editorX, editorY)
        {
            WorldPositionIncludedOffsets = worldPositionIncludedOffsets;
        }
    }

    public class MaterialExpressionWorldPositionProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionWorldPosition";

        public MaterialExpressionWorldPositionProcessor()
        {
            AddOptionalProperty("WorldPositionShaderOffset", PropertyDataType.WorldPositionIncludedOffsets);
        }
        
        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionWorldPosition(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")),
                ValueUtil.ParseWorldPositionIncludedOffsets(node.FindPropertyValue("WorldPositionIncludedOffsets"))
            );
        }
    }
}
