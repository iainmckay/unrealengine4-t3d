namespace JollySamurai.UnrealEngine4.T3D.Parser
{
    public class ParsedDocument
    {
        public ParsedNode RootNode { get; }
        public string FileName { get; }

        public ParsedDocument(string fileName, ParsedNode rootNode)
        {
            FileName = fileName;
            RootNode = rootNode;
        }

        public static ParsedDocument From(string fileName, string content)
        {
            DocumentParser documentParser = new DocumentParser(content);
            ParsedNode rootNode = documentParser.Parse();

            return new ParsedDocument(fileName, rootNode);
        }
    }
}
