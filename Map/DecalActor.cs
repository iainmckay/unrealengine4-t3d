using System.Linq;
using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Map
{
    public class DecalActor : BaseActorNode
    {
        public string DecalComponentName { get; }

        public DecalComponent DecalComponent => Children.First(node => node.Name == DecalComponentName) as DecalComponent;

        public DecalActor(string name, ResourceReference archetype, string actorLabel, SpawnCollisionHandlingMethod spawnCollisionHandlingMethod, string folderPath, string rootComponentName, Node[] children, string decalComponentName)
            : base(name, actorLabel, spawnCollisionHandlingMethod, folderPath, rootComponentName, archetype, children)
        {
            DecalComponentName = decalComponentName;
        }
    }

    public class DecalActorProcessor : BaseActorNodeProcessor
    {
        public override string Class => "/Script/Engine.DecalActor";

        public DecalActorProcessor()
        {
            AddRequiredProperty("Decal", PropertyDataType.String);
            
            AddIgnoredProperty("ArrowComponent");
            AddIgnoredProperty("SpriteComponent");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new DecalActor(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseResourceReference(node.FindAttributeValue("Archetype")),
                node.FindPropertyValue("ActorLabel"),
                ValueUtil.ParseSpawnCollisionHandlingMethod(node.FindPropertyValue("SpawnCollisionHandlingMethod")),
                node.FindPropertyValue("FolderPath"),
                node.FindPropertyValue("RootComponent"),
                children,
                node.FindPropertyValue("Decal")
            );
        }
    }
}
