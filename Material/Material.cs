using System.Linq;
using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class Material : MaterialNode
    {
        public ParsedPropertyBag AmbientOcclusion { get; }
        public ShadingModel ShadingModel { get; }
        public BlendMode BlendMode { get; }
        public DecalBlendMode DecalBlendMode { get; }
        public MaterialDomain MaterialDomain { get; }
        public TranslucencyLightingMode TranslucencyLightingMode { get; }
        public bool IsTwoSided { get; }
        public ParsedPropertyBag BaseColor { get; }
        public ParsedPropertyBag Metallic { get; }
        public ParsedPropertyBag Normal { get; }
        public ParsedPropertyBag Roughness { get; }
        public ParsedPropertyBag Specular { get; }
        public ParsedPropertyBag EmissiveColor { get; }
        public ParsedPropertyBag Opacity { get; }
        public ParsedPropertyBag OpacityMask { get; }
        public ExpressionReference[] Expressions { get; }
        public ExpressionReference[] EditorComments { get; }
        public int TextureStreamingDataVersion { get; }
        public ParsedPropertyBag[] TextureStreamingData { get; }
        public bool DitherOpacityMask { get; }

        public Material(string name, int editorX, int editorY, Node[] children, ParsedPropertyBag ambientOcclusion, ShadingModel shadingModel, BlendMode blendMode, DecalBlendMode decalBlendMode, MaterialDomain materialDomain, TranslucencyLightingMode translucencyLightingMode, bool isTwoSided, ParsedPropertyBag baseColor, ParsedPropertyBag metallic, ParsedPropertyBag normal, ParsedPropertyBag roughness, ParsedPropertyBag specular, ParsedPropertyBag emissiveColor, ParsedPropertyBag opacity, ParsedPropertyBag opacityMask, ExpressionReference[] expressionReferences, ExpressionReference[] editorComments, int textureStreamingDataVersion, ParsedPropertyBag[] textureStreamingData, bool ditherOpacityMask)
            : base(name, editorX, editorY, children)
        {
            AmbientOcclusion = ambientOcclusion;
            ShadingModel = shadingModel;
            BlendMode = blendMode;
            DecalBlendMode = decalBlendMode;
            MaterialDomain = materialDomain;
            TranslucencyLightingMode = translucencyLightingMode;
            IsTwoSided = isTwoSided;
            BaseColor = baseColor;
            Metallic = metallic;
            Normal = normal;
            Roughness = roughness;
            Specular = specular;
            EmissiveColor = emissiveColor;
            Opacity = opacity;
            OpacityMask = opacityMask;
            Expressions = expressionReferences;
            EditorComments = editorComments;
            TextureStreamingDataVersion = textureStreamingDataVersion;
            TextureStreamingData = textureStreamingData;
            DitherOpacityMask = ditherOpacityMask;
        }

        public MaterialNode ResolveExpressionReference(ExpressionReference reference)
        {
            if (null == reference) {
                return null;
            }

            return Children.SingleOrDefault(node => node.Name == reference.NodeName && node.IsClassOf(reference.ClassName)) as MaterialNode;
        }
    }

    public class MaterialProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.Material";

        public MaterialProcessor()
        {
            AddRequiredProperty("Expressions", PropertyDataType.ExpressionReference | PropertyDataType.Array);

            AddOptionalProperty("AmbientOcclusion", PropertyDataType.AttributeList);
            AddOptionalProperty("BaseColor", PropertyDataType.AttributeList);
            AddOptionalProperty("BlendMode", PropertyDataType.BlendMode);
            AddOptionalProperty("DecalBlendMode", PropertyDataType.DecalBlendMode);
            AddOptionalProperty("DitherOpacityMask", PropertyDataType.Boolean);
            AddOptionalProperty("EditorComments", PropertyDataType.ExpressionReference | PropertyDataType.Array);
            AddOptionalProperty("EditorX", PropertyDataType.Integer);
            AddOptionalProperty("EditorY", PropertyDataType.Integer);
            AddOptionalProperty("EmissiveColor", PropertyDataType.AttributeList);
            AddOptionalProperty("MaterialDomain", PropertyDataType.MaterialDomain);
            AddOptionalProperty("Metallic", PropertyDataType.AttributeList);
            AddOptionalProperty("Normal", PropertyDataType.AttributeList);
            AddOptionalProperty("Opacity", PropertyDataType.AttributeList);
            AddOptionalProperty("OpacityMask", PropertyDataType.AttributeList);
            AddOptionalProperty("Roughness", PropertyDataType.AttributeList);
            AddOptionalProperty("ShadingModel", PropertyDataType.ShadingModel);
            AddOptionalProperty("Specular", PropertyDataType.AttributeList);
            AddOptionalProperty("TextureStreamingDataVersion", PropertyDataType.Integer);
            AddOptionalProperty("TextureStreamingData", PropertyDataType.AttributeList | PropertyDataType.Array);
            AddOptionalProperty("TranslucencyLightingMode", PropertyDataType.TranslucencyLightingMode);
            AddOptionalProperty("TwoSided", PropertyDataType.Boolean);

            AddIgnoredProperty("bCanMaskedBeAssumedOpaque");
            AddIgnoredProperty("bEnableCrackFreeDisplacement");
            AddIgnoredProperty("bEnableResponsiveAA");
            AddIgnoredProperty("bEnableSeparateTranslucency");
            AddIgnoredProperty("bFullyRough");
            AddIgnoredProperty("bUsedWithBeamTrails");
            AddIgnoredProperty("bUsedWithClothing");
            AddIgnoredProperty("bUsedWithInstancedStaticMeshes");
            AddIgnoredProperty("bUsedWithMeshParticles");
            AddIgnoredProperty("bUsedWithParticleSprites");
            AddIgnoredProperty("bUsedWithSkeletalMesh");
            AddIgnoredProperty("bUsedWithStaticLighting");
            AddIgnoredProperty("bUsedWithSplineMeshes");
            AddIgnoredProperty("bUseFullPrecision");
            AddIgnoredProperty("bUseMaterialAttributes");
            AddIgnoredProperty("bUsesDistortion");
            AddIgnoredProperty("CachedExpressionData");
            AddIgnoredProperty("D3D11TessellationMode");
            AddIgnoredProperty("LightingGuid");
            AddIgnoredProperty("MaterialFunctionInfos");
            AddIgnoredProperty("MaxDisplacement");
            AddIgnoredProperty("ParameterGroupData");
            AddIgnoredProperty("ParameterOverviewExpansion");
            AddIgnoredProperty("ReferencedTextureGuids");
            AddIgnoredProperty("ShadingModels");
            AddIgnoredProperty("StateId");
            AddIgnoredProperty("SubsurfaceColor");
            AddIgnoredProperty("TessellationMultiplier");
            AddIgnoredProperty("ThumbnailInfo");
            AddIgnoredProperty("TranslucentShadowDensityScale");
            AddIgnoredProperty("WorldDisplacement");
            AddIgnoredProperty("WorldPositionOffset");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new Material(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("EditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("EditorX")),
                children,
                ValueUtil.ParseAttributeList(node.FindPropertyValue("AmbientOcclusion")),
                ValueUtil.ParseShadingModel(node.FindPropertyValue("ShadingModel")),
                ValueUtil.ParseBlendMode(node.FindPropertyValue("BlendMode")),
                ValueUtil.ParseDecalBlendMode(node.FindPropertyValue("DecalBlendMode")),
                ValueUtil.ParseMaterialDomain(node.FindPropertyValue("MaterialDomain")),
                ValueUtil.ParseTranslucencyLightingMode(node.FindPropertyValue("TranslucencyLightingMode")),
                ValueUtil.ParseBoolean(node.FindPropertyValue("TwoSided")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("BaseColor")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Metallic")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Normal")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Roughness")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Specular")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("EmissiveColor")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Opacity")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("OpacityMask")),
                ValueUtil.ParseExpressionReferenceArray(node.FindProperty("Expressions")?.Elements),
                ValueUtil.ParseExpressionReferenceArray(node.FindProperty("EditorComments")?.Elements),
                ValueUtil.ParseInteger(node.FindPropertyValue("TextureStreamingDataVersion") ?? "1"),
                ValueUtil.ParseAttributeListArray(node.FindProperty("TextureStreamingData")?.Elements),
                ValueUtil.ParseBoolean(node.FindPropertyValue("DitherOpacityMask"))
            );
        }
    }
}
