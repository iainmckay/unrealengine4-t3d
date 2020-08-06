using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialDocumentProcessor : DocumentProcessor<Material>
    {
        public MaterialDocumentProcessor()
        {
            AddNodeProcessor(new MaterialProcessor());
            AddNodeProcessor(new MaterialExpressionAddProcessor());
            AddNodeProcessor(new MaterialExpressionAppendVectorProcessor());
            AddNodeProcessor(new MaterialExpressionClampProcessor());
            AddNodeProcessor(new MaterialExpressionCommentProcessor());
            AddNodeProcessor(new MaterialExpressionConstantProcessor());
            AddNodeProcessor(new MaterialExpressionConstant2VectorProcessor());
            AddNodeProcessor(new MaterialExpressionConstant3VectorProcessor());
            AddNodeProcessor(new MaterialExpressionConstant4VectorProcessor());
            AddNodeProcessor(new MaterialExpressionDesaturationProcessor());
            AddNodeProcessor(new MaterialExpressionDivideProcessor());
            AddNodeProcessor(new MaterialExpressionFresnelProcessor());
            AddNodeProcessor(new MaterialExpressionLinearInterpolateProcessor());
            AddNodeProcessor(new MaterialExpressionMaterialFunctionCallProcessor());
            AddNodeProcessor(new MaterialExpressionMultiplyProcessor());
            AddNodeProcessor(new MaterialExpressionOneMinusProcessor());
            AddNodeProcessor(new MaterialExpressionScalarParameterProcessor());
            AddNodeProcessor(new MaterialExpressionSineProcessor());
            AddNodeProcessor(new MaterialExpressionStaticSwitchParameterProcessor());
            AddNodeProcessor(new MaterialExpressionSubtractProcessor());
            AddNodeProcessor(new MaterialExpressionTextureCoordinateProcessor());
            AddNodeProcessor(new MaterialExpressionTextureObjectParameterProcessor());
            AddNodeProcessor(new MaterialExpressionTextureSampleProcessor());
            AddNodeProcessor(new MaterialExpressionTextureSampleParameter2DProcessor());
            AddNodeProcessor(new MaterialExpressionTimeProcessor());
            AddNodeProcessor(new MaterialExpressionVectorParameterProcessor());

            IgnoreNode("/Script/UnrealEd.SceneThumbnailInfoWithPrimitive");
        }
    }
}
