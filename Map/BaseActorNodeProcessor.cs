using System.Linq;
using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Map
{
    public abstract class BaseActorNode : Node
    {
        public string ActorLabel { get; }
        public SpawnCollisionHandlingMethod SpawnCollisionHandlingMethod { get; }
        public string FolderPath { get; }

        public ResourceReference Archetype { get; }
        public string ParentActorName { get; }

        public string RootComponentName { get; }

        public BaseComponent RootComponent => Children.First(node => node.Name == RootComponentName) as BaseComponent;

        protected BaseActorNode(string name, string actorLabel, SpawnCollisionHandlingMethod spawnCollisionHandlingMethod, string folderPath, string rootComponentName, ResourceReference archetype, Node[] children = null, string parentActorName = null)
            : base(name, children)
        {
            ActorLabel = actorLabel;
            SpawnCollisionHandlingMethod = spawnCollisionHandlingMethod;
            FolderPath = folderPath;
            RootComponentName = rootComponentName;
            Archetype = archetype;
            ParentActorName = parentActorName;
        }
    }
    
    public abstract class BaseActorNodeProcessor : NodeProcessor
    {
        public abstract string Class { get; }

        public BaseActorNodeProcessor()
        {
            AddRequiredAttribute("Name", PropertyDataType.String);
            AddRequiredAttribute("Archetype", PropertyDataType.ResourceReference);

            AddOptionalAttribute("ParentActor", PropertyDataType.String);

            AddRequiredProperty("RootComponent", PropertyDataType.String);

            AddOptionalProperty("ActorLabel", PropertyDataType.String);
            AddOptionalProperty("FolderPath", PropertyDataType.String);
            AddOptionalProperty("SpawnCollisionHandlingMethod", PropertyDataType.String);

            AddIgnoredAttribute("Class");
        }

        public override bool Supports(ParsedNode node)
        {
            return node.SectionType == "Actor" && Class == node.AttributeBag.FindProperty("Class")?.Value;
        }
    }
}
