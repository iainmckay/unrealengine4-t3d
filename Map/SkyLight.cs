using System.Linq;
using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Map
{
    public class SkyLight : BaseActorNode
    {
        public SkyLightComponent SkyLightComponent => Children.First(node => node.Name == RootComponentName) as SkyLightComponent;

        public SkyLight(string name, ResourceReference archetype, string actorLabel, SpawnCollisionHandlingMethod spawnCollisionHandlingMethod, string folderPath, string rootComponentName, Node[] children, string parentActorName)
            : base(name, actorLabel, spawnCollisionHandlingMethod, folderPath, rootComponentName, archetype, children, parentActorName)
        {
        }
    }

    public class SkyLightProcessor : BaseActorNodeProcessor
    {
        public override string Class => "/Script/Engine.SkyLight";

        public SkyLightProcessor()
        {
            AddIgnoredProperty("Component");
            AddIgnoredProperty("LightComponent");
            AddIgnoredProperty("SpriteComponent");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new SkyLight(
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
