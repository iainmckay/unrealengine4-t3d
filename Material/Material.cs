﻿using System.Linq;
 using JollySamurai.UnrealEngine4.T3D.Parser;
 using JollySamurai.UnrealEngine4.T3D.Processor;

 namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class Material : Node
    {
        public ShadingModel ShadingModel { get; }
        public BlendMode BlendMode { get; }
        public MaterialDomain MaterialDomain { get; }
        public bool IsTwoSided { get; }
        public ParsedPropertyBag BaseColor { get; }
        public ParsedPropertyBag Metallic { get; }
        public ParsedPropertyBag Normal { get; }
        public ParsedPropertyBag Roughness { get; }
        public ParsedPropertyBag Specular { get; }
        public ParsedPropertyBag EmissiveColor { get; }
        public ParsedPropertyBag Opacity { get; }
        public ExpressionReference[] Expressions { get; }
        public ExpressionReference[] EditorComments { get; }

        public Material(Node[] children, string name, ShadingModel shadingModel, BlendMode blendMode, MaterialDomain materialDomain, bool isTwoSided, ParsedPropertyBag baseColor, ParsedPropertyBag metallic, ParsedPropertyBag normal, ParsedPropertyBag roughness, ParsedPropertyBag specular, ParsedPropertyBag emissiveColor, ParsedPropertyBag opacity, ExpressionReference[] expressionReferences, ExpressionReference[] editorComments, int editorX, int editorY)
            : base(name, editorX, editorY, children)
        {
            ShadingModel = shadingModel;
            BlendMode = blendMode;
            MaterialDomain = materialDomain;
            IsTwoSided = isTwoSided;
            BaseColor = baseColor;
            Metallic = metallic;
            Normal = normal;
            Roughness = roughness;
            Specular = specular;
            EmissiveColor = emissiveColor;
            Opacity = opacity;
            Expressions = expressionReferences;
            EditorComments = editorComments;
        }

        public Node ResolveExpressionReference(ExpressionReference reference)
        {
            if (null == reference) {
                return null;
            }

            return Children.SingleOrDefault(node => node.Name == reference.NodeName && node.IsClassOf(reference.ClassName));
        }
    }

    public class MaterialProcessor : NodeProcessor
    {
        public override string Class {
            get { return "/Script/Engine.Material"; }
        }

        public MaterialProcessor() : base()
        {
            AddRequiredAttribute("Name", PropertyDataType.String);

            AddRequiredProperty("Expressions", PropertyDataType.ExpressionReference | PropertyDataType.Array);

            AddOptionalProperty("BaseColor", PropertyDataType.AttributeList);
            AddOptionalProperty("BlendMode", PropertyDataType.BlendMode);
            AddOptionalProperty("EditorComments", PropertyDataType.ExpressionReference | PropertyDataType.Array);
            AddOptionalProperty("EditorX", PropertyDataType.Integer);
            AddOptionalProperty("EditorY", PropertyDataType.Integer);
            AddOptionalProperty("EmissiveColor", PropertyDataType.AttributeList);
            AddOptionalProperty("MaterialDomain", PropertyDataType.MaterialDomain);
            AddOptionalProperty("Metallic", PropertyDataType.AttributeList);
            AddOptionalProperty("Normal", PropertyDataType.AttributeList);
            AddOptionalProperty("Opacity", PropertyDataType.AttributeList);
            AddOptionalProperty("Roughness", PropertyDataType.AttributeList);
            AddOptionalProperty("ShadingModel", PropertyDataType.ShadingModel);
            AddOptionalProperty("Specular", PropertyDataType.AttributeList);
            AddOptionalProperty("TwoSided", PropertyDataType.Boolean);

            AddIgnoredProperty("bCanMaskedBeAssumedOpaque");
            AddIgnoredProperty("bUsedWithParticleSprites");
            AddIgnoredProperty("bUsedWithStaticLighting");
            AddIgnoredProperty("bUsedWithSplineMeshes");
            AddIgnoredProperty("CachedExpressionData");
            AddIgnoredProperty("LightingGuid");
            AddIgnoredProperty("MaterialFunctionInfos");
            AddIgnoredProperty("ParameterGroupData");
            AddIgnoredProperty("ParameterOverviewExpansion");
            AddIgnoredProperty("ReferencedTextureGuids");
            AddIgnoredProperty("ShadingModels");
            AddIgnoredProperty("StateId");
            AddIgnoredProperty("TextureStreamingDataVersion");
            AddIgnoredProperty("ThumbnailInfo");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new Material(
                children,
                node.FindAttributeValue("Name"),
                ValueUtil.ParseShadingModel(node.FindPropertyValue("ShadingModel")),
                ValueUtil.ParseBlendMode(node.FindPropertyValue("BlendMode")),
                ValueUtil.ParseMaterialDomain(node.FindPropertyValue("MaterialDomain")),
                ValueUtil.ParseBoolean(node.FindPropertyValue("TwoSided") ?? "False"),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("BaseColor")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Metallic")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Normal")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Roughness")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Specular")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("EmissiveColor")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Opacity")),
                ValueUtil.ParseExpressionReferenceArray(node.FindProperty("Expressions")?.Elements),
                ValueUtil.ParseExpressionReferenceArray(node.FindProperty("EditorComments")?.Elements),
                ValueUtil.ParseInteger(node.FindPropertyValue("EditorX") ?? "0"),
                ValueUtil.ParseInteger(node.FindPropertyValue("EditorX") ?? "0")
            );
        }
    }
}
