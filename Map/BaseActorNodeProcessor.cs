using System.Linq;
using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Map
{
    public abstract class BaseActorNode : Node
    {
        public string ActorLabel { get; }

        public ResourceReference Archetype { get; }

        public string RootComponentName { get; }

        public BaseComponent RootComponent => Children.First(node => node.Name == RootComponentName) as BaseComponent;

        protected BaseActorNode(string name, string actorLabel, string rootComponentName, ResourceReference archetype, Node[] children = null)
            : base(name, 0, 0, children)
        {
            ActorLabel = actorLabel;
            RootComponentName = rootComponentName;
            Archetype = archetype;
        }
    }
    
    public abstract class BaseActorNodeProcessor : NodeProcessor
    {
        public abstract string Class { get; }

        public BaseActorNodeProcessor()
        {
            AddRequiredAttribute("Archetype", PropertyDataType.ResourceReference);
            AddRequiredAttribute("Name", PropertyDataType.String);

            AddIgnoredAttribute("Class");
        }

        public override bool Supports(ParsedNode node)
        {
            return node.SectionType == "Actor" && this.Class == node.AttributeBag.FindProperty("Class")?.Value;
        }
    }
}
