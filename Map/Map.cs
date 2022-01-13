using System.Linq;
using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Map
{
    public class Map : Node
    {
        public int LevelCount => Children.Count(node => node is Level);
        public Level[] Levels => Children.OfType<Level>().ToArray();

        public Map(string name, Node[] children)
            : base(name, children)
        {
        }
    }

    public class MapProcessor : NodeProcessor
    {
        public MapProcessor()
        {
            AddRequiredAttribute("Name", PropertyDataType.String);
        }

        public override bool Supports(ParsedNode node)
        {
            return node.SectionType == "Map";
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new Map(node.FindAttributeValue("Name"), children);
        }
    }
}
