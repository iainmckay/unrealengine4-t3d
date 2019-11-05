using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace JollySamurai.UnrealEngine4.T3D.Parser
{
    public class Parser
    {
        private static readonly char[] TokenTerminatorList = new char[] {
            '=',
        };

        private static readonly char[] WhitespaceList = new char[] {
            ' ', '\t', ',',
        };

        private string _content;
        private int _contentLength;
        private int _cursorPosition;
        private StringBuilder _tokenBuffer;

        internal Parser(string content)
        {
            _content = content;
            _contentLength = content.Length;
            _tokenBuffer = new StringBuilder();
        }

        public ParsedNode Parse()
        {
            return ParseNode();
        }

        protected ParsedNode ParseNode()
        {
            ExpectToken("Begin");
            ExpectToken("Object");

            ParsedPropertyBag attributeBag = ReadAttributeList();
            List<ParsedNode> childNodes = new List<ParsedNode>();
            List<ParsedProperty> propertyList = new List<ParsedProperty>();

            MoveToNextLine();

            while (! ReachedEndOfDocument()) {
                SkipWhitespace();

                string token = PeekToken();

                if (token == "Begin") {
                    childNodes.Add(ParseNode());
                } else if (token == "End") {
                    ExpectToken("End");
                    ExpectToken("Object");

                    MoveToNextLine();

                    break;
                } else {
                    ParsedProperty property = ReadProperty();

                    // FIXME: remove traces, replace with exceptions and error handling
                    Trace.Assert(property != null);

                    propertyList.Add(property);
                }
            }

            childNodes = PostProcessNodes(childNodes.ToArray());

            return new ParsedNode(new ParsedNodeBag(childNodes.ToArray()), attributeBag, new ParsedPropertyBag(propertyList.ToArray()));
        }

        public void ExpectToken(string expectedToken)
        {
            string actualToken = ReadToken();

            if (actualToken != expectedToken) {
                throw new Exception($"Expected \"{expectedToken}\" but got \"{actualToken}\"");
            }
        }

        public ParsedPropertyBag ReadAttributeList()
        {
            List<ParsedProperty> attributeList = new List<ParsedProperty>();

            while (! ReachedEndOfDocument() && ! IsEndOfLine(PeekCharacter())) {
                ParsedProperty property = ReadAttribute();

                if (property == null) {
                    break;
                }

                attributeList.Add(property);
            }

            return new ParsedPropertyBag(attributeList.ToArray());
        }

        public ParsedProperty ReadAttribute()
        {
            string key = ReadToken();

            ExpectToken("=");

            string value = ReadToken();

            return new ParsedProperty(key, value);
        }

        public ParsedProperty ReadProperty()
        {
            string key = ReadToken();

            ExpectToken("=");

            string value = ReadUntilEndOfLine();

            if (value.StartsWith("\"") && value.EndsWith("\"")) {
                value = value.Substring(1, value.Length - 2);
            }

            return new ParsedProperty(key, value);
        }


        public string ReadToken()
        {
            return ReadUntil(TokenTerminatorList);
        }

        public string PeekToken()
        {
            return ReadUntil(TokenTerminatorList, false);
        }

        public void SkipWhitespace()
        {
            while (! ReachedEndOfDocument() && IsWhitespaceCharacter(PeekCharacter())) {
                ReadCharacter();
            }
        }

        public void MoveToNextLine()
        {
            ReadUntilEndOfLine();
        }

        public string ReadUntilEndOfLine()
        {
            _tokenBuffer.Clear();

            while (! ReachedEndOfDocument()) {
                var nextCharacter = ReadCharacter();

                if (IsEndOfLine(nextCharacter)) {
                    // read carriage return followed by new line
                    if (! ReachedEndOfDocument() && IsEndOfLine(PeekCharacter())) {
                        ReadCharacter();
                    }

                    break;
                }

                _tokenBuffer.Append(nextCharacter);
            }

            return _tokenBuffer.ToString();
        }

        public string ReadUntil(char[] terminatorList, bool enableBufferConsumption = true)
        {
            _tokenBuffer.Clear();

            bool isInsideString = false;
            char stringOpener = '\0';
            int peekOffset = 0;

            while (! ReachedEndOfDocument()) {
                char nextCharacter = PeekCharacter(enableBufferConsumption ? -1 : peekOffset++);
                bool isStringStartOrEnd = false;

                if (nextCharacter == '"' || nextCharacter == '\'') {
                    if (isInsideString && nextCharacter == stringOpener) {
                        isStringStartOrEnd = true;
                    } else if (! isInsideString) {
                        isStringStartOrEnd = true;
                        stringOpener = nextCharacter;
                    }
                }

                if (isStringStartOrEnd) {
                    isInsideString = ! isInsideString;
                }

                // we want to keep terminators in the buffer because those are needed for handling things like attribute
                // key/value pairs. these expressions all contribute to keeping terminators in the buffer and returning
                // them when expected.

                bool isTerminationCharacter = IsTerminationCharacter(nextCharacter, terminatorList);
                bool isWhitespaceCharacter = IsWhitespaceCharacter(nextCharacter);
                bool isEndOfLine = IsEndOfLine(nextCharacter);

                bool shouldReturn = isEndOfLine || (! isInsideString && (isWhitespaceCharacter || isTerminationCharacter));
                bool shouldConsume = (isStringStartOrEnd || isInsideString) || ! (isTerminationCharacter && _tokenBuffer.Length != 0) && ! isEndOfLine;
                bool shouldAppend = true;

                if (isStringStartOrEnd && nextCharacter != '\'') {
                    shouldAppend = false;
                } else {
                    shouldAppend = (isInsideString && ! isEndOfLine) || (! isWhitespaceCharacter && shouldConsume);
                }

                if (shouldAppend) {
                    _tokenBuffer.Append(nextCharacter);
                }

                if (enableBufferConsumption && shouldConsume) {
                    ReadCharacter();
                }

                if (shouldReturn || ReachedEndOfDocument()) {
                    return _tokenBuffer.ToString();
                }
            }

            throw new Exception("Unexpected end of document reached while reading token");
        }

        public char ReadCharacter()
        {
            char nextCharacter = PeekCharacter();
            _cursorPosition++;

            return nextCharacter;
        }

        public char GetCharacter(int position)
        {
            if (IsPositionPastEndOfDocument(position)) {
                throw new InvalidOperationException("Attempted to read character past the end of the document");
            }

            return _content[position];
        }

        public char PeekCharacter(int peekOffset = -1)
        {
            return GetCharacter(_cursorPosition + (peekOffset != -1 ? peekOffset : 0));
        }

        public bool ReachedEndOfDocument()
        {
            return IsPositionPastEndOfDocument(_cursorPosition);
        }

        public bool IsPositionPastEndOfDocument(int position)
        {
            return position >= _contentLength;
        }

        public bool IsTerminationCharacter(char character, char[] terminatorList)
        {
            return terminatorList.Contains(character);
        }

        public bool IsWhitespaceCharacter(char character)
        {
            return WhitespaceList.Contains(character);
        }

        public bool IsEndOfLine(char character)
        {
            return character == '\n' || character == '\r';
        }

        private List<ParsedNode> PostProcessNodes(ParsedNode[] nodes)
        {
            List<ParsedNode> newList = new List<ParsedNode>();

            foreach (ParsedNode parsedNode in nodes) {
                // find nodes that only have a name, then combine them with the expected node that has both attributes
                if (parsedNode.AttributeBag.HasProperty("Class")) {
                    newList.Add(parsedNode);
                } else {
                    string name = parsedNode.AttributeBag.FindProperty("Name").Value;
                    ParsedNode mainNode = nodes.SingleOrDefault(n => n.AttributeBag.HasProperty("Class") && n.AttributeBag.HasPropertyWithValue("Name", name));

                    if (mainNode != null) {
                        newList.Remove(mainNode);
                        newList.Add(new ParsedNode(mainNode.Children, mainNode.AttributeBag, parsedNode.PropertyBag));
                    }
                }
            }

            return newList;
        }
    }
}
