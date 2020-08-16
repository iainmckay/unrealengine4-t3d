using System.Linq;
using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Map
{
    public class SpotLightActor : BaseActorNode
    {
        public string FolderPath { get; }
        public string SpotLightComponentName { get; }
        public string LightComponentName { get; }

        public SpotLightComponent SpotLightComponent => Children.First(node => node.Name == SpotLightComponentName) as SpotLightComponent;
        public SpotLightComponent LightComponent => SpotLightComponent;

        public SpotLightActor(string name, ResourceReference archetype, string actorLabel, string folderPath, string spotLightComponentName, string lightComponentName, string rootComponentName, Node[] children)
            : base(name, actorLabel, rootComponentName, archetype, children)
        {
            FolderPath = folderPath;
            SpotLightComponentName = spotLightComponentName;
            LightComponentName = lightComponentName;
        }
    }

    public class SpotLightActorProcessor : BaseActorNodeProcessor
    {
        public override string Class => "/Script/Engine.SpotLight";

        public SpotLightActorProcessor()
        {
            AddRequiredProperty("ActorLabel", PropertyDataType.String);
            AddRequiredProperty("LightComponent", PropertyDataType.String);
            AddRequiredProperty("SpotLightComponent", PropertyDataType.String);
            AddRequiredProperty("RootComponent", PropertyDataType.String);

            AddOptionalProperty("FolderPath", PropertyDataType.String);
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new SpotLightActor(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseResourceReference(node.FindAttributeValue("Archetype")),
                node.FindPropertyValue("ActorLabel"),
                node.FindPropertyValue("FolderPath"),
                node.FindPropertyValue("SpotLightComponent"),
                node.FindPropertyValue("LightComponent"),
                node.FindPropertyValue("RootComponent"),
                children
            );
        }
    }
}
