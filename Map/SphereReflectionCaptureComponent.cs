using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Map
{
    public class SphereReflectionCaptureComponent : BaseComponent
    {
        public float Brightness { get; }
        public float InfluenceRadius { get; }
        public ReflectionSourceType ReflectionSourceType { get; }
        public ResourceReference Cubemap { get; }
        public float SourceCubemapAngle { get; }
        public Vector3 CaptureOffset { get; }

        public SphereReflectionCaptureComponent(string name, ResourceReference archetype, Vector3 relativeLocation, Rotator relativeRotation, Vector3 relativeScale3D, Node[] children, float brightness, float influenceRadius, ReflectionSourceType reflectionSourceType, ResourceReference cubemap, float sourceCubemapAngle, Vector3 captureOffset)
            : base(name, archetype, relativeLocation, relativeScale3D, relativeRotation, children)
        {
            Brightness = brightness;
            InfluenceRadius = influenceRadius;
            ReflectionSourceType = reflectionSourceType;
            Cubemap = cubemap;
            SourceCubemapAngle = sourceCubemapAngle;
            CaptureOffset = captureOffset;
        }
    }

    public class SphereReflectionCaptureComponentProcessor : BaseComponentProcessor
    {
        public override string Class => "/Script/Engine.SphereReflectionCaptureComponent";

        public SphereReflectionCaptureComponentProcessor()
        {
            AddOptionalProperty("CaptureOffset", PropertyDataType.Vector3);
            AddOptionalProperty("Cubemap", PropertyDataType.ResourceReference);
            AddOptionalProperty("InfluenceRadius", PropertyDataType.Float);
            AddOptionalProperty("SourceCubemapAngle", PropertyDataType.Float);
            AddOptionalProperty("SphereReflectionCaptureSize", PropertyDataType.Float);

            AddIgnoredProperty("CaptureOffsetComponent");
            AddIgnoredProperty("MapBuildDataId");
            AddIgnoredProperty("PreviewInfluenceRadius");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new SphereReflectionCaptureComponent(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseResourceReference(node.FindAttributeValue("Archetype")),
                ValueUtil.ParseVector3(node.FindPropertyValue("RelativeLocation")),
                ValueUtil.ParseRotator(node.FindPropertyValue("RelativeRotation")),
                ValueUtil.ParseVector3(node.FindPropertyValue("RelativeScale3D") ?? "(X=1.0,Y=1.0,Z=1.0)"),
                children,
                ValueUtil.ParseFloat(node.FindPropertyValue("Brightness") ?? "1"),
                ValueUtil.ParseFloat(node.FindPropertyValue("InfluenceRadius") ?? "3000"),
                ValueUtil.ParseReflectionSourceType(node.FindPropertyValue("ReflectionSourceType") ?? "CapturedScene"),
                ValueUtil.ParseResourceReference(node.FindPropertyValue("Cubemap")),
                ValueUtil.ParseFloat(node.FindPropertyValue("SourceCubemapAngle")),
                ValueUtil.ParseVector3(node.FindPropertyValue("CaptureOffset"))
            );
        }
    }
}
