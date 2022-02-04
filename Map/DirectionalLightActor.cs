using System.Linq;
using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Map
{
    public class DirectionalLightActor : BaseActorNode
    {
        public string DirectionalLightComponentName { get; }

        public DirectionalLightComponent DirectionalLightComponent => Children.First(node => node.Name == DirectionalLightComponentName) as DirectionalLightComponent;

        public DirectionalLightActor(string name, ResourceReference archetype, string actorLabel, SpawnCollisionHandlingMethod spawnCollisionHandlingMethod, string folderPath, string rootComponentName, Node[] children, string directionalLightComponentName)
            : base(name, actorLabel, spawnCollisionHandlingMethod, folderPath, rootComponentName, archetype, children)
        {
            DirectionalLightComponentName = directionalLightComponentName;
        }
    }

    public class DirectionalLightActorProcessor : BaseActorNodeProcessor
    {
        public override string Class => "/Script/Engine.DirectionalLight";

        public DirectionalLightActorProcessor()
        {
            AddRequiredProperty("DirectionalLightComponent", PropertyDataType.String);

            AddIgnoredProperty("LightComponent");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new DirectionalLightActor(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseResourceReference(node.FindAttributeValue("Archetype")),
                node.FindPropertyValue("ActorLabel"),
                ValueUtil.ParseSpawnCollisionHandlingMethod(node.FindPropertyValue("SpawnCollisionHandlingMethod")),
                node.FindPropertyValue("FolderPath"),
                node.FindPropertyValue("RootComponent"),
                children,
                node.FindPropertyValue("DirectionalLightComponent")
            );
        }
    }
}
