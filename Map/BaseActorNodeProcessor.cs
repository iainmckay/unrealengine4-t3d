using System.Linq;
using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Map
{
    public abstract class BaseActorNode : Node
    {
        public string FolderPath { get; }
        public string ActorLabel { get; }

        public ResourceReference Archetype { get; }

        public string RootComponentName { get; }

        public BaseComponent RootComponent => Children.First(node => node.Name == RootComponentName) as BaseComponent;

        protected BaseActorNode(string name, string actorLabel, string folderPath, string rootComponentName, ResourceReference archetype, Node[] children = null)
            : base(name, children)
        {
            ActorLabel = actorLabel;
            FolderPath = folderPath;
            RootComponentName = rootComponentName;
            Archetype = archetype;
        }
    }
    
    public abstract class BaseActorNodeProcessor : NodeProcessor
    {
        public abstract string Class { get; }

        public BaseActorNodeProcessor()
        {
            AddRequiredAttribute("Name", PropertyDataType.String);
            AddRequiredAttribute("Archetype", PropertyDataType.ResourceReference);

            AddRequiredProperty("RootComponent", PropertyDataType.String);

            AddOptionalProperty("ActorLabel", PropertyDataType.String);
            AddOptionalProperty("FolderPath", PropertyDataType.String);

            AddIgnoredAttribute("Class");
        }

        public override bool Supports(ParsedNode node)
        {
            return node.SectionType == "Actor" && Class == node.AttributeBag.FindProperty("Class")?.Value;
        }
    }
}
