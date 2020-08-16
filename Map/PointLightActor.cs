using System.Linq;
using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Map
{
    public class PointLightActor : BaseActorNode
    {
        public string FolderPath { get; }
        public string PointLightComponentName { get; }
        public string LightComponentName { get; }

        public PointLightComponent PointLightComponent => Children.First(node => node.Name == PointLightComponentName) as PointLightComponent;
        public PointLightComponent LightComponent => PointLightComponent;

        public PointLightActor(string name, ResourceReference archetype, string actorLabel, string folderPath, string pointLightComponentName, string lightComponentName, string rootComponentName, Node[] children)
            : base(name, actorLabel, rootComponentName, archetype, children)
        {
            FolderPath = folderPath;
            PointLightComponentName = pointLightComponentName;
            LightComponentName = lightComponentName;
        }
    }

    public class PointLightActorProcessor : BaseActorNodeProcessor
    {
        public override string Class => "/Script/Engine.PointLight";

        public PointLightActorProcessor()
        {
            AddRequiredProperty("ActorLabel", PropertyDataType.String);
            AddRequiredProperty("LightComponent", PropertyDataType.String);
            AddRequiredProperty("PointLightComponent", PropertyDataType.String);
            AddRequiredProperty("RootComponent", PropertyDataType.String);

            AddOptionalProperty("FolderPath", PropertyDataType.String);
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new PointLightActor(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseResourceReference(node.FindAttributeValue("Archetype")),
                node.FindPropertyValue("ActorLabel"),
                node.FindPropertyValue("FolderPath"),
                node.FindPropertyValue("PointLightComponent"),
                node.FindPropertyValue("LightComponent"),
                node.FindPropertyValue("RootComponent"),
                children
            );
        }
    }
}
