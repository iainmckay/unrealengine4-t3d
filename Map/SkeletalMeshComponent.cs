using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Map
{
    public class SkeletalMeshComponent : BaseComponent, IMobility
    {
        public ResourceReference SkeletalMesh { get; }
        public ResourceReference[] OverrideMaterials { get; }
        public Mobility Mobility { get; }

        public SkeletalMeshComponent(string name, ResourceReference archetype, ResourceReference staticMesh, Vector3 relativeLocation, Rotator relativeRotation, Vector3 relativeScale3D, Node[] children, ResourceReference[] overrideMaterials, Mobility mobility)
            : base(name, archetype, relativeLocation, relativeScale3D, relativeRotation, children)
        {
            SkeletalMesh = staticMesh;
            OverrideMaterials = overrideMaterials;
            Mobility = mobility;
        }
    }

    public class SkeletalMeshComponentProcessor : BaseComponentProcessor
    {
        public override string Class => "/Script/Engine.SkeletalMeshComponent";

        public SkeletalMeshComponentProcessor()
        {
            AddRequiredProperty("SkeletalMesh", PropertyDataType.ResourceReference);

            AddOptionalProperty("Mobility", PropertyDataType.String);
            AddOptionalProperty("OverrideMaterials", PropertyDataType.ResourceReference | PropertyDataType.Array);

            AddIgnoredProperty("ClothingSimulationFactory");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new SkeletalMeshComponent(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseResourceReference(node.FindAttributeValue("Archetype")),
                ValueUtil.ParseResourceReference(node.FindPropertyValue("SkeletalMesh")),
                ValueUtil.ParseVector3(node.FindPropertyValue("RelativeLocation")),
                ValueUtil.ParseRotator(node.FindPropertyValue("RelativeRotation")),
                ValueUtil.ParseVector3(node.FindPropertyValue("RelativeScale3D") ?? "(X=1.0,Y=1.0,Z=1.0)"),
                children,
                ValueUtil.ParseResourceReferenceArray(node.FindProperty("OverrideMaterials")?.Elements),
                ValueUtil.ParseMobility(node.FindPropertyValue("Mobility"))
            );
        }
    }
}
