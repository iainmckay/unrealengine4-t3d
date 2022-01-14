using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Map
{
    public class DecalComponent : BaseComponent
    {
        public ResourceReference DecalMaterial { get; }
        public Vector3 DeclSize { get; }
        
        public DecalComponent(string name, ResourceReference archetype, Vector3 relativeLocation, Rotator relativeRotation, Vector3 relativeScale3D, Node[] children, ResourceReference decalMaterial, Vector3 declSize)
            : base(name, archetype, relativeLocation, relativeScale3D, relativeRotation, children)
        {
            DecalMaterial = decalMaterial;
            DeclSize = declSize;
        }
    }

    public class DecalComponentProcessor : BaseComponentProcessor
    {
        public override string Class => "/Script/Engine.DecalComponent";

        public DecalComponentProcessor()
        {
            AddRequiredProperty("DecalMaterial", PropertyDataType.ResourceReference);
            AddOptionalProperty("DecalSize", PropertyDataType.ResourceReference);
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new DecalComponent(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseResourceReference(node.FindAttributeValue("Archetype")),
                ValueUtil.ParseVector3(node.FindPropertyValue("RelativeLocation")),
                ValueUtil.ParseRotator(node.FindPropertyValue("RelativeRotation")),
                ValueUtil.ParseVector3(node.FindPropertyValue("RelativeScale3D") ?? "(X=1.0,Y=1.0,Z=1.0)"),
                children,
                ValueUtil.ParseResourceReference(node.FindPropertyValue("DecalMaterial")),
                ValueUtil.ParseVector3(node.FindPropertyValue("DeclSize") ?? "(X=128.0,Y=256.0,Z=256.0)")
            );
        }
    }
}
