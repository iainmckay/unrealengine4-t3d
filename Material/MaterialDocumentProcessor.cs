using System.Linq;
using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialDocumentProcessor : DocumentProcessor<Material>
    {
        private readonly string[] IgnoredNodes = {
            "/Script/UnrealEd.SceneThumbnailInfoWithPrimitive",
        };

        public MaterialDocumentProcessor()
        {
            AddNodeProcessor(new MaterialProcessor());
            AddNodeProcessor(new MaterialExpressionAddProcessor());
            AddNodeProcessor(new MaterialExpressionAppendVectorProcessor());
            AddNodeProcessor(new MaterialExpressionClampProcessor());
            AddNodeProcessor(new MaterialExpressionCommentProcessor());
            AddNodeProcessor(new MaterialExpressionComponentMaskProcessor());
            AddNodeProcessor(new MaterialExpressionConstantProcessor());
            AddNodeProcessor(new MaterialExpressionConstant2VectorProcessor());
            AddNodeProcessor(new MaterialExpressionConstant3VectorProcessor());
            AddNodeProcessor(new MaterialExpressionConstant4VectorProcessor());
            AddNodeProcessor(new MaterialExpressionDepthFadeProcessor());
            AddNodeProcessor(new MaterialExpressionDesaturationProcessor());
            AddNodeProcessor(new MaterialExpressionDivideProcessor());
            AddNodeProcessor(new MaterialExpressionFracProcessor());
            AddNodeProcessor(new MaterialExpressionFresnelProcessor());
            AddNodeProcessor(new MaterialExpressionLinearInterpolateProcessor());
            AddNodeProcessor(new MaterialExpressionObjectFunctionCallProcessor());
            AddNodeProcessor(new MaterialExpressionMultiplyProcessor());
            AddNodeProcessor(new MaterialExpressionOneMinusProcessor());
            AddNodeProcessor(new MaterialExpressionPannerProcessor());
            AddNodeProcessor(new MaterialExpressionPowerProcessor());
            AddNodeProcessor(new MaterialExpressionScalarParameterProcessor());
            AddNodeProcessor(new MaterialExpressionSineProcessor());
            AddNodeProcessor(new MaterialExpressionStaticBoolParameterProcessor());
            AddNodeProcessor(new MaterialExpressionStaticSwitchParameterProcessor());
            AddNodeProcessor(new MaterialExpressionStaticSwitchProcessor());
            AddNodeProcessor(new MaterialExpressionSubtractProcessor());
            AddNodeProcessor(new MaterialExpressionTextureCoordinateProcessor());
            AddNodeProcessor(new MaterialExpressionTextureObjectParameterProcessor());
            AddNodeProcessor(new MaterialExpressionTextureObjectProcessor());
            AddNodeProcessor(new MaterialExpressionTextureSampleProcessor());
            AddNodeProcessor(new ObjectExpressionTextureSampleParameter2DProcessor());
            AddNodeProcessor(new MaterialExpressionTimeProcessor());
            AddNodeProcessor(new MaterialExpressionVectorParameterProcessor());
            AddNodeProcessor(new MaterialExpressionVertexNormalWSProcessor());
        }

        protected override bool IsIgnoredNode(ParsedNode parsedNode)
        {
            var nodeClass = parsedNode.AttributeBag.FindProperty("Class")?.Value;

            return IgnoredNodes.Count(s => s == nodeClass) != 0;
        }
    }
}
