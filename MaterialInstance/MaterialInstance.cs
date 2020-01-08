﻿using System.Linq;
 using JollySamurai.UnrealEngine4.T3D.Parser;
 using JollySamurai.UnrealEngine4.T3D.Processor;

 namespace JollySamurai.UnrealEngine4.T3D.MaterialInstance
{
    public class MaterialInstance : Node
    {
        public ParsedPropertyBag[] ScalarParameters { get; }
        public ParsedPropertyBag[] TextureParameters { get; }

        public MaterialInstance(Node[] children, string name, ParsedPropertyBag[] scalarParameters, ParsedPropertyBag[] textureParameters)
            : base(name, -1, -1, children)
        {
            ScalarParameters = scalarParameters;
            TextureParameters = textureParameters;
        }
    }

    public class MaterialInstanceProcessor : NodeProcessor
    {
        public override string Class {
            get { return "/Script/Engine.MaterialInstanceConstant"; }
        }

        public MaterialInstanceProcessor() : base()
        {
            AddRequiredProperty("Parent", PropertyDataType.String);

            AddOptionalProperty("ScalarParameterValues", PropertyDataType.AttributeList | PropertyDataType.Array);
            AddOptionalProperty("TextureParameterValues", PropertyDataType.AttributeList | PropertyDataType.Array);

            AddIgnoredProperty("LightingGuid");
            AddIgnoredProperty("ParameterStateId");
            AddIgnoredProperty("ReferencedTextureGuids");
            AddIgnoredProperty("ThumbnailInfo");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            ParsedProperty expressionList = node.FindProperty("Expressions");

            return new MaterialInstance(
                children,
                node.FindAttributeValue("Name"),
                ValueUtil.ParseAttributeListArray(node.FindProperty("ScalarParameterValues")?.Elements),
                ValueUtil.ParseAttributeListArray(node.FindProperty("TextureParameterValues")?.Elements)
            );
        }
    }
}
