using System.Linq;
using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Map
{
    public class PointLightActor : BaseActorNode
    {
        public string PointLightComponentName { get; }
        public string LightComponentName { get; }

        public PointLightComponent PointLightComponent => Children.First(node => node.Name == PointLightComponentName) as PointLightComponent;
        public PointLightComponent LightComponent => PointLightComponent;

        public PointLightActor(string name, ResourceReference archetype, string actorLabel, SpawnCollisionHandlingMethod spawnCollisionHandlingMethod, string folderPath, string rootComponentName, Node[] children, string pointLightComponentName, string lightComponentName)
            : base(name, actorLabel, spawnCollisionHandlingMethod, folderPath, rootComponentName, archetype, children)
        {
            PointLightComponentName = pointLightComponentName;
            LightComponentName = lightComponentName;
        }
    }

    public class PointLightActorProcessor : BaseActorNodeProcessor
    {
        public override string Class => "/Script/Engine.PointLight";

        public PointLightActorProcessor()
        {
            AddRequiredProperty("LightComponent", PropertyDataType.String);
            AddRequiredProperty("PointLightComponent", PropertyDataType.String);
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new PointLightActor(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseResourceReference(node.FindAttributeValue("Archetype")),
                node.FindPropertyValue("ActorLabel"),
                ValueUtil.ParseSpawnCollisionHandlingMethod(node.FindPropertyValue("SpawnCollisionHandlingMethod")),
                node.FindPropertyValue("FolderPath"),
                node.FindPropertyValue("RootComponent"),
                children,
                node.FindPropertyValue("PointLightComponent"),
                node.FindPropertyValue("LightComponent")
            );
        }
    }
}
