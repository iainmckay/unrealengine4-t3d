using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Common
{
    public abstract class ObjectNodeProcessor : NodeProcessor
    {
        public abstract string Class { get; }

        public ObjectNodeProcessor()
            : base()
        {
            AddIgnoredAttribute("Class");
        }

        public override bool Supports(ParsedNode node)
        {
            return node.SectionType == "Object" && this.Class == node.AttributeBag.FindProperty("Class")?.Value;
        }
    }
}
