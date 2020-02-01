﻿using System.Linq;
 using JollySamurai.UnrealEngine4.T3D.Parser;
 using JollySamurai.UnrealEngine4.T3D.Processor;

 namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class Material : Node
    {
        public ShadingModel ShadingModel { get; }
        public ParsedPropertyBag BaseColor { get; }
        public ParsedPropertyBag Metallic { get; }
        public ParsedPropertyBag Normal { get; }
        public ParsedPropertyBag Roughness { get; }
        public ParsedPropertyBag Specular { get; }
        public ParsedPropertyBag EmissiveColor { get; }
        public ExpressionReference[] Expressions { get; }

        public Material(Node[] children, string name, ShadingModel shadingModel, ParsedPropertyBag baseColor, ParsedPropertyBag metallic, ParsedPropertyBag normal, ParsedPropertyBag roughness, ParsedPropertyBag specular, ParsedPropertyBag emissiveColor, ExpressionReference[] expressionReferences, int editorX, int editorY)
            : base(name, editorX, editorY, children)
        {
            ShadingModel = shadingModel;
            BaseColor = baseColor;
            Metallic = metallic;
            Normal = normal;
            Roughness = roughness;
            Specular = specular;
            EmissiveColor = emissiveColor;
            Expressions = expressionReferences;
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
            AddOptionalProperty("EditorX", PropertyDataType.Integer);
            AddOptionalProperty("EditorY", PropertyDataType.Integer);
            AddOptionalProperty("EmissiveColor", PropertyDataType.AttributeList);
            AddOptionalProperty("Metallic", PropertyDataType.AttributeList);
            AddOptionalProperty("Normal", PropertyDataType.AttributeList);
            AddOptionalProperty("Roughness", PropertyDataType.AttributeList);
            AddOptionalProperty("ShadingModel", PropertyDataType.ShadingModel);
            AddOptionalProperty("Specular", PropertyDataType.AttributeList);

            AddIgnoredProperty("LightingGuid");
            AddIgnoredProperty("ReferencedTextureGuids");
            AddIgnoredProperty("StateId");
            AddIgnoredProperty("ThumbnailInfo");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            ParsedProperty expressionList = node.FindProperty("Expressions");

            return new Material(
                children,
                node.FindAttributeValue("Name"),
                ValueUtil.ParseShadingModel(node.FindPropertyValue("ShadingModel")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("BaseColor")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Metallic")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Normal")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Roughness")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Specular")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("EmissiveColor")),
                ValueUtil.ParseExpressionReferenceArray(node.FindProperty("Expressions").Elements),
                ValueUtil.ParseInteger(node.FindPropertyValue("EditorX") ?? "0"),
                ValueUtil.ParseInteger(node.FindPropertyValue("EditorX") ?? "0")
            );
        }
    }
}
