using System.Linq;
using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Map
{
    public class ExponentialHeightFog : BaseActorNode
    {
        public ExponentialHeightFogComponent ExponentialHeightFogComponent => Children.First(node => node.Name == RootComponentName) as ExponentialHeightFogComponent;

        public ExponentialHeightFog(string name, ResourceReference archetype, string actorLabel, SpawnCollisionHandlingMethod spawnCollisionHandlingMethod, string folderPath, string rootComponentName, Node[] children, string parentActorName)
            : base(name, actorLabel, spawnCollisionHandlingMethod, folderPath, rootComponentName, archetype, children, parentActorName)
        {
        }
    }

    public class ExponentialHeightFogProcessor : BaseActorNodeProcessor
    {
        public override string Class => "/Script/Engine.ExponentialHeightFog";

        public ExponentialHeightFogProcessor()
        {
            AddIgnoredProperty("Component");
            AddIgnoredProperty("SpriteComponent");
        }
        
        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new ExponentialHeightFog(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseResourceReference(node.FindAttributeValue("Archetype")),
                node.FindPropertyValue("ActorLabel"),
                ValueUtil.ParseSpawnCollisionHandlingMethod(node.FindPropertyValue("SpawnCollisionHandlingMethod")),
                node.FindPropertyValue("FolderPath"),
                node.FindPropertyValue("RootComponent"),
                children,
                node.FindAttributeValue("ParentActor")
            );
        }
    }
}
