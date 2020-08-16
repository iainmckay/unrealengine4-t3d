using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Map
{
    public class Level : Node
    {
        public Level(string name, Node[] children)
            : base(name, 0, 0, children)
        {
        }
    }

    public class LevelProcessor : NodeProcessor
    {
        public LevelProcessor()
            : base()
        {
            AddRequiredAttribute("NAME", PropertyDataType.String);
        }

        public override bool Supports(ParsedNode node)
        {
            return node.SectionType == "Level";
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new Level(node.FindAttributeValue("NAME"), children);
        }
    }
}
