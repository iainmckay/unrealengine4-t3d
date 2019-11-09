namespace JollySamurai.UnrealEngine4.T3D.Parser
{
    public class ParsedDocument
    {
        public ParsedNode RootNode { get; }

        private ParsedDocument(ParsedNode rootNode)
        {
            RootNode = rootNode;
        }

        public static ParsedDocument From(string content)
        {
            DocumentParser documentParser = new DocumentParser(content);
            ParsedNode rootNode = documentParser.Parse();

            return new ParsedDocument(rootNode);
        }
    }
}
