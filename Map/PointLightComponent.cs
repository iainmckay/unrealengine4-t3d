using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Map
{
    public class PointLightComponent : BaseComponent, ILocation, IRotation, IScale3D
    {
        public float AttenuationRadius { get; }
        public float Intensity { get; }
        public Vector4 LightColor { get; }
        public Mobility Mobility { get; }
        public bool CastShadows { get; }
        public Vector3 Location { get; }
        public Vector3 Scale3D { get; }
        public float SpecularScale { get; }
        public float SourceRadius { get; }
        public float SourceLength { get; }
        public Rotator Rotation { get; }
        
        public PointLightComponent(string name, ResourceReference archetype, float attenuationRadius, float intensity, Vector4 lightColor, Mobility mobility, bool castShadows, Vector3 relativeLocation, Rotator relativeRotation, Vector3 relativeScale3D, float specularScale, float sourceRadius, float sourceLength, Node[] children)
            : base(name, archetype, children)
        {
            AttenuationRadius = attenuationRadius;
            Intensity = intensity;
            LightColor = lightColor;
            Mobility = mobility;
            CastShadows = castShadows;
            Location = relativeLocation;
            Scale3D = relativeScale3D;
            SpecularScale = specularScale;
            SourceRadius = sourceRadius;
            SourceLength = sourceLength;
            Rotation = relativeRotation;
        }
    }

    public class PointLightComponentProcessor : BaseComponentProcessor
    {
        public override string Class => "/Script/Engine.PointLightComponent";

        public PointLightComponentProcessor() : base()
        {
            AddOptionalProperty("AttenuationRadius", PropertyDataType.Float);
            AddOptionalProperty("CastShadows", PropertyDataType.Boolean);
            AddOptionalProperty("Intensity", PropertyDataType.Float);
            AddOptionalProperty("LightColor", PropertyDataType.Vector4);
            AddOptionalProperty("Mobility", PropertyDataType.Mobility);
            AddOptionalProperty("RelativeLocation", PropertyDataType.Vector3);
            AddOptionalProperty("RelativeRotation", PropertyDataType.Rotator);
            AddOptionalProperty("RelativeScale3D", PropertyDataType.Vector3);
            AddOptionalProperty("SpecularScale", PropertyDataType.Float);
            AddOptionalProperty("SourceRadius", PropertyDataType.Float);
            AddOptionalProperty("SourceLength", PropertyDataType.Float);

            AddIgnoredProperty("LightGuid");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new PointLightComponent(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseResourceReference(node.FindAttributeValue("Archetype")),
                ValueUtil.ParseFloat(node.FindPropertyValue("AttenuationRadius") ?? "1000.0"),
                ValueUtil.ParseFloat(node.FindPropertyValue("Intensity") ?? "5000.0"),
                ValueUtil.ParseVector4(node.FindPropertyValue("LightColor") ?? "(R=255,G=255,B=255,A=255)"),
                ValueUtil.ParseMobility(node.FindPropertyValue("Mobility")),
                ValueUtil.ParseBoolean(node.FindPropertyValue("CastShadows") ?? "True"),
                ValueUtil.ParseVector3(node.FindPropertyValue("RelativeLocation")),
                ValueUtil.ParseRotator(node.FindPropertyValue("RelativeRotation")),
                ValueUtil.ParseVector3(node.FindPropertyValue("RelativeScale3D") ?? "(X=1.0,Y=1.0,Z=1.0)"),
                ValueUtil.ParseFloat(node.FindPropertyValue("SpecularScale") ?? "1.0"),
                ValueUtil.ParseFloat(node.FindPropertyValue("SourceRadius") ?? "0.0"),
                ValueUtil.ParseFloat(node.FindPropertyValue("SourceLength") ?? "0.0"),
                children
            );
        }
    }
}
