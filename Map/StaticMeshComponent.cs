using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Map
{
    public class StaticMeshComponent : BaseComponent, ILocation, IRotation, IScale3D
    {
        public ResourceReference StaticMesh { get; }
        public int StaticMeshImportVersion { get; }
        public ResourceReference[] OverrideMaterials { get; }

        public Vector3 Location { get; }
        public Vector3 Scale3D { get; }
        public Rotator Rotation { get; }
        
        public StaticMeshComponent(string name, ResourceReference archetype, ResourceReference staticMesh, int staticMeshImportVersion, Vector3 relativeLocation, Rotator relativeRotation, Vector3 relativeScale3D, ResourceReference[] overrideMaterials, Node[] children)
            : base(name, archetype, children)
        {
            StaticMesh = staticMesh;
            StaticMeshImportVersion = staticMeshImportVersion;
            Location = relativeLocation;
            Scale3D = relativeScale3D;
            Rotation = relativeRotation;
            OverrideMaterials = overrideMaterials;
        }
    }

    public class StaticMeshComponentProcessor : BaseComponentProcessor
    {
        public override string Class => "/Script/Engine.StaticMeshComponent";

        public StaticMeshComponentProcessor() : base()
        {
            AddRequiredProperty("StaticMesh", PropertyDataType.ResourceReference);
            AddRequiredProperty("StaticMeshImportVersion", PropertyDataType.Integer);
            AddRequiredProperty("RelativeLocation", PropertyDataType.Vector3);
            
            AddOptionalProperty("OverrideMaterials", PropertyDataType.ResourceReference | PropertyDataType.Array);
            AddOptionalProperty("RelativeRotation", PropertyDataType.Rotator);
            AddOptionalProperty("RelativeScale3D", PropertyDataType.Vector3);
            
            AddIgnoredProperty("AssetUserData");
            AddIgnoredProperty("MaterialStreamingRelativeBoxes");
            AddIgnoredProperty("StreamingTextureData");
            AddIgnoredProperty("StaticMeshDerivedDataKey");
            AddIgnoredProperty("VisibilityId");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new StaticMeshComponent(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseResourceReference(node.FindAttributeValue("Archetype")),
                ValueUtil.ParseResourceReference(node.FindPropertyValue("StaticMesh")),
                ValueUtil.ParseInteger(node.FindPropertyValue("StaticMeshImportVersion")),
                ValueUtil.ParseVector3(node.FindPropertyValue("RelativeLocation")),
                ValueUtil.ParseRotator(node.FindPropertyValue("RelativeRotation")),
                ValueUtil.ParseVector3(node.FindPropertyValue("RelativeScale3D") ?? "(X=1.0,Y=1.0,Z=1.0)"),
                ValueUtil.ParseResourceReferenceArray(node.FindProperty("OverrideMaterials")?.Elements),
                children
            );
        }
    }
}
