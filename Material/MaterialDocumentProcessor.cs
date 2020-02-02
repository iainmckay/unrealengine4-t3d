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
            AddNodeProcessor(new MaterialExpressionDesaturationProcessor());
            AddNodeProcessor(new MaterialExpressionLinearInterpolateProcessor());
            AddNodeProcessor(new MaterialExpressionMaterialFunctionCallProcessor());
            AddNodeProcessor(new MaterialExpressionMultiplyProcessor());
            AddNodeProcessor(new MaterialExpressionScalarParameterProcessor());
            AddNodeProcessor(new MaterialExpressionStaticSwitchParameterProcessor());
            AddNodeProcessor(new MaterialExpressionTextureCoordinateProcessor());
            AddNodeProcessor(new MaterialExpressionTextureSampleParameter2DProcessor());
            AddNodeProcessor(new MaterialExpressionVectorParameterProcessor());

            IgnoreNode("/Script/UnrealEd.SceneThumbnailInfoWithPrimitive");
        }
    }
}
