using System.Linq;
using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Map
{
    public class StaticMeshActor : BaseActorNode
    {
        public string FolderPath { get; }
        public string StaticMeshComponentName { get; }

        public StaticMeshComponent StaticMeshComponent => Children.First(node => node.Name == StaticMeshComponentName) as StaticMeshComponent;

        public StaticMeshActor(string name, ResourceReference archetype, string actorLabel, string folderPath, string staticMeshComponentName, string rootComponentName, Node[] children)
            : base(name, actorLabel, rootComponentName, archetype, children)
        {
            FolderPath = folderPath;
            StaticMeshComponentName = staticMeshComponentName;
        }
    }

    public class StaticMeshActorProcessor : BaseActorNodeProcessor
    {
        public override string Class => "/Script/Engine.StaticMeshActor";

        public StaticMeshActorProcessor()
        {
            AddRequiredProperty("ActorLabel", PropertyDataType.String);
            AddRequiredProperty("StaticMeshComponent", PropertyDataType.String);
            AddRequiredProperty("RootComponent", PropertyDataType.String);

            AddOptionalProperty("FolderPath", PropertyDataType.String);
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new StaticMeshActor(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseResourceReference(node.FindAttributeValue("Archetype")),
                node.FindPropertyValue("ActorLabel"),
                node.FindPropertyValue("FolderPath"),
                node.FindPropertyValue("StaticMeshComponent"),
                node.FindPropertyValue("RootComponent"),
                children
            );
        }
    }
}
