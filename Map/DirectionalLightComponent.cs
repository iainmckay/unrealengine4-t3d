using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Map
{
    public class DirectionalLightComponent : BaseComponent, IMobility
    {
        public float Intensity { get; }
        public Vector4 LightColor { get; }
        public Mobility Mobility { get; }
        public bool CastShadows { get; }
        public float SpecularScale { get; }
        public float SourceRadius { get; }
        public float SourceLength { get; }
        public ResourceReference LightFunctionReference { get; }
        public float SoftSourceRadius { get; }

        public DirectionalLightComponent(string name, ResourceReference archetype, Vector3 relativeLocation, Rotator relativeRotation, Vector3 relativeScale3D, Node[] children, float intensity, Vector4 lightColor, Mobility mobility, bool castShadows, float specularScale, float softSourceRadius, float sourceRadius, float sourceLength, ResourceReference lightFunctionReference)
            : base(name, archetype, relativeLocation, relativeScale3D, relativeRotation, children)
        {
            Intensity = intensity;
            LightColor = lightColor;
            Mobility = mobility;
            CastShadows = castShadows;
            SpecularScale = specularScale;
            SourceRadius = sourceRadius;
            SourceLength = sourceLength;
            SoftSourceRadius = softSourceRadius;
            LightFunctionReference = lightFunctionReference;
        }
    }

    public class DirectionalLightComponentProcessor : BaseComponentProcessor
    {
        public override string Class => "/Script/Engine.DirectionalLightComponent";

        public DirectionalLightComponentProcessor()
        {
            AddOptionalProperty("CastShadows", PropertyDataType.Boolean);
            AddOptionalProperty("Intensity", PropertyDataType.Float);
            AddOptionalProperty("LightColor", PropertyDataType.Vector4);
            AddOptionalProperty("LightFunctionMaterial", PropertyDataType.ResourceReference);
            AddOptionalProperty("Mobility", PropertyDataType.Mobility);
            AddOptionalProperty("SpecularScale", PropertyDataType.Float);
            AddOptionalProperty("SoftSourceRadius", PropertyDataType.Float);
            AddOptionalProperty("SourceRadius", PropertyDataType.Float);
            AddOptionalProperty("SourceLength", PropertyDataType.Float);

            AddIgnoredProperty("LightGuid");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new DirectionalLightComponent(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseResourceReference(node.FindAttributeValue("Archetype")),
                ValueUtil.ParseVector3(node.FindPropertyValue("RelativeLocation")),
                ValueUtil.ParseRotator(node.FindPropertyValue("RelativeRotation")),
                ValueUtil.ParseVector3(node.FindPropertyValue("RelativeScale3D") ?? "(X=1.0,Y=1.0,Z=1.0)"),
                children,
                ValueUtil.ParseFloat(node.FindPropertyValue("Intensity") ?? "5000.0"),
                ValueUtil.ParseVector4(node.FindPropertyValue("LightColor") ?? "(R=255,G=255,B=255,A=255)"),
                ValueUtil.ParseMobility(node.FindPropertyValue("Mobility")),
                ValueUtil.ParseBoolean(node.FindPropertyValue("CastShadows") ?? "True"),
                ValueUtil.ParseFloat(node.FindPropertyValue("SpecularScale") ?? "1.0"),
                ValueUtil.ParseFloat(node.FindPropertyValue("SoftSourceRadius")),
                ValueUtil.ParseFloat(node.FindPropertyValue("SourceRadius")),
                ValueUtil.ParseFloat(node.FindPropertyValue("SourceLength")),
                ValueUtil.ParseResourceReference(node.FindPropertyValue("LightFunctionMaterial"))
            );
        }
    }
}
