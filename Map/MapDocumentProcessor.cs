using System.Linq;
using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Map
{
    public class MapDocumentProcessor : DocumentProcessor<Map>
    {
        private readonly string[] IgnoredNodes = {
            "/Script/DatasmithContent.DatasmithAssetUserData",
        };
        
        public MapDocumentProcessor()
        {
            AddNodeProcessor(new LevelProcessor());
            AddNodeProcessor(new MapProcessor());
            AddNodeProcessor(new PointLightActorProcessor());
            AddNodeProcessor(new PointLightComponentProcessor());
            AddNodeProcessor(new SpotLightActorProcessor());
            AddNodeProcessor(new SpotLightComponentProcessor());
            AddNodeProcessor(new StaticMeshActorProcessor());
            AddNodeProcessor(new StaticMeshComponentProcessor());
        }

        protected override bool IsIgnoredNode(ParsedNode parsedNode)
        {
            var nodeClass = parsedNode.AttributeBag.FindProperty("Class")?.Value;

            return IgnoredNodes.Count(s => s == nodeClass) != 0;
        }
    }
}
