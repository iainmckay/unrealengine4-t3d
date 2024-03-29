﻿using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Map
{
    public class StaticMeshComponent : BaseComponent, IMobility
    {
        public ResourceReference StaticMesh { get; }
        public int StaticMeshImportVersion { get; }
        public ResourceReference[] OverrideMaterials { get; }
        public Mobility Mobility { get; }

        public StaticMeshComponent(string name, ResourceReference archetype, ResourceReference staticMesh, int staticMeshImportVersion, Vector3 relativeLocation, Rotator relativeRotation, Vector3 relativeScale3D, Node[] children, ResourceReference[] overrideMaterials, Mobility mobility)
            : base(name, archetype, relativeLocation, relativeScale3D, relativeRotation, children)
        {
            StaticMesh = staticMesh;
            StaticMeshImportVersion = staticMeshImportVersion;
            OverrideMaterials = overrideMaterials;
            Mobility = mobility;
        }
    }

    public class StaticMeshComponentProcessor : BaseComponentProcessor
    {
        public override string Class => "/Script/Engine.StaticMeshComponent";

        public StaticMeshComponentProcessor()
        {
            AddRequiredProperty("StaticMesh", PropertyDataType.ResourceReference);

            AddOptionalProperty("Mobility", PropertyDataType.String);
            AddOptionalProperty("OverrideMaterials", PropertyDataType.ResourceReference | PropertyDataType.Array);
            AddOptionalProperty("StaticMeshImportVersion", PropertyDataType.Integer);

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
                children,
                ValueUtil.ParseResourceReferenceArray(node.FindProperty("OverrideMaterials")?.Elements),
                ValueUtil.ParseMobility(node.FindPropertyValue("Mobility"))
            );
        }
    }
}
