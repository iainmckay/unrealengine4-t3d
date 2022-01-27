using System.Linq;
using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.MaterialInstance
{
    public class MaterialInstanceDocumentProcessor : DocumentProcessor<MaterialInstance>
    {
        private readonly string[] IgnoredNodes = {
            "/Script/UnrealEd.SceneThumbnailInfoWithPrimitive",
        };

        public MaterialInstanceDocumentProcessor()
        {
            AddNodeProcessor(new MaterialInstanceProcessor());
        }

        protected override bool IsIgnoredNode(ParsedNode parsedNode)
        {
            var nodeClass = parsedNode.AttributeBag.FindProperty("Class")?.Value;

            return IgnoredNodes.Count(s => s == nodeClass) != 0;
        }
    }
}
