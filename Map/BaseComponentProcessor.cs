using JollySamurai.UnrealEngine4.T3D.Common;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Map
{
    public abstract class BaseComponent : Node, ILocation, IRotation, IScale3D
    {
        public ResourceReference Archetype { get; }
        public Vector3 Location { get; }
        public Vector3 Scale3D { get; }
        public Rotator Rotation { get; }

        protected BaseComponent(string name, ResourceReference archetype, Vector3 relativeLocation, Vector3 relativeScale3D, Rotator relativeRotation, Node[] children = null)
            : base(name, children)
        {
            Archetype = archetype;
            Location = relativeLocation;
            Scale3D = relativeScale3D;
            Rotation = relativeRotation;
        }
    }

    public abstract class BaseComponentProcessor : ObjectNodeProcessor
    {
        public BaseComponentProcessor()
        {
            AddRequiredAttribute("Archetype", PropertyDataType.ResourceReference);

            AddOptionalProperty("RelativeLocation", PropertyDataType.Vector3);
            AddOptionalProperty("RelativeRotation", PropertyDataType.Rotator);
            AddOptionalProperty("RelativeScale3D", PropertyDataType.Vector3);
        }
    }
}
