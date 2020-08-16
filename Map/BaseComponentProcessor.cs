using JollySamurai.UnrealEngine4.T3D.Common;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Map
{
    public abstract class BaseComponent : Node
    {
        public ResourceReference Archetype { get; }

        protected BaseComponent(string name, ResourceReference archetype, Node[] children = null)
            : base(name, 0, 0, children)
        {
            Archetype = archetype;
        }
    }

    public abstract class BaseComponentProcessor : ObjectNodeProcessor
    {
        public BaseComponentProcessor()
        {
            AddRequiredAttribute("Archetype", PropertyDataType.ResourceReference);
            AddRequiredAttribute("Name", PropertyDataType.String);

            AddIgnoredAttribute("Class");
        }
    }
}
