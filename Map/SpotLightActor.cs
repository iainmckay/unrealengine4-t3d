using System.Linq;
using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Map
{
    public class SpotLightActor : BaseActorNode
    {
        public string SpotLightComponentName { get; }
        public string LightComponentName { get; }

        public SpotLightComponent SpotLightComponent => Children.First(node => node.Name == SpotLightComponentName) as SpotLightComponent;
        public SpotLightComponent LightComponent => SpotLightComponent;

        public SpotLightActor(string name, ResourceReference archetype, string actorLabel, string folderPath, string rootComponentName, Node[] children, string spotLightComponentName, string lightComponentName)
            : base(name, actorLabel, folderPath, rootComponentName, archetype, children)
        {
            SpotLightComponentName = spotLightComponentName;
            LightComponentName = lightComponentName;
        }
    }

    public class SpotLightActorProcessor : BaseActorNodeProcessor
    {
        public override string Class => "/Script/Engine.SpotLight";

        public SpotLightActorProcessor()
        {
            AddRequiredProperty("LightComponent", PropertyDataType.String);
            AddRequiredProperty("SpotLightComponent", PropertyDataType.String);
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new SpotLightActor(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseResourceReference(node.FindAttributeValue("Archetype")),
                node.FindPropertyValue("ActorLabel"),
                node.FindPropertyValue("FolderPath"),
                node.FindPropertyValue("RootComponent"),
                children,
                node.FindPropertyValue("SpotLightComponent"),
                node.FindPropertyValue("LightComponent")
            );
        }
    }
}
