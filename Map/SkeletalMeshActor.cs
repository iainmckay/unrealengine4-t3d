using System.Linq;
using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Map
{
    public class SkeletalMeshActor : BaseActorNode
    {
        public string SkeletalMeshComponentName { get; }

        public SkeletalMeshComponent SkeletalMeshComponent => Children.First(node => node.Name == SkeletalMeshComponentName) as SkeletalMeshComponent;

        public SkeletalMeshActor(string name, ResourceReference archetype, string actorLabel, SpawnCollisionHandlingMethod spawnCollisionHandlingMethod, string folderPath, string rootComponentName, Node[] children, string staticMeshComponentName)
            : base(name, actorLabel, spawnCollisionHandlingMethod, folderPath, rootComponentName, archetype, children)
        {
            SkeletalMeshComponentName = staticMeshComponentName;
        }
    }

    public class SkeletalMeshActorProcessor : BaseActorNodeProcessor
    {
        public override string Class => "/Script/Engine.SkeletalMeshActor";

        public SkeletalMeshActorProcessor()
        {
            AddRequiredProperty("SkeletalMeshComponent", PropertyDataType.String);
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new SkeletalMeshActor(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseResourceReference(node.FindAttributeValue("Archetype")),
                node.FindPropertyValue("ActorLabel"),
                ValueUtil.ParseSpawnCollisionHandlingMethod(node.FindPropertyValue("SpawnCollisionHandlingMethod")),
                node.FindPropertyValue("FolderPath"),
                node.FindPropertyValue("RootComponent"),
                children,
                node.FindPropertyValue("SkeletalMeshComponent")
            );
        }
    }
}
