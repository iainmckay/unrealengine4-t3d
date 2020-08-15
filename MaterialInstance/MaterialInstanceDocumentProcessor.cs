using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.MaterialInstance
{
    public class MaterialInstanceDocumentProcessor : DocumentProcessor<MaterialInstance>
    {
        public MaterialInstanceDocumentProcessor()
        {
            AddNodeProcessor(new ObjectInstanceProcessor());
        }

        protected override bool IsIgnoredNode(ParsedNode parsedNode)
        {
            return false;
        }
    }
}
