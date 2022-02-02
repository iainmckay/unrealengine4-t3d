using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionSceneTexture : MaterialNode
    {
        public ParsedPropertyBag Coordinates { get; }
        public SceneTextureId SceneTextureId { get; }
        public bool Filtered { get; }

        public MaterialExpressionSceneTexture(string name, int editorX, int editorY, ParsedPropertyBag coordinates, SceneTextureId sceneTextureId, bool bFiltered)
            : base(name, editorX, editorY)
        {
            Coordinates = coordinates;
            SceneTextureId = sceneTextureId;
            Filtered = bFiltered;
        }
    }

    public class MaterialExpressionSceneTextureProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionSceneTexture";

        public MaterialExpressionSceneTextureProcessor()
        {
            AddOptionalProperty("Coordinates", PropertyDataType.AttributeList);
            AddOptionalProperty("SceneTextureId", PropertyDataType.SceneTextureId);
            AddOptionalProperty("bFiltered", PropertyDataType.Boolean);
        }
        
        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionSceneTexture(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Coordinates")),
                ValueUtil.ParseSceneTextureId(node.FindPropertyValue("SceneTextureId")),
                ValueUtil.ParseBoolean(node.FindPropertyValue("bFiltered"))
            );
        }
    }
}
