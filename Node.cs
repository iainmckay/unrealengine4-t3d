namespace JollySamurai.UnrealEngine4.T3D
{
    public abstract class Node
    {
        public string Name { get; }

        public int EditorX { get; }
        public int EditorY { get; }

        public Node[] Children { get; }

        public Node(string name, int editorX, int editorY, Node[] children = null)
        {
            Name = name;
            EditorX = editorX;
            EditorY = editorY;
            Children = children;
        }

        public bool IsClassOf(string className)
        {
            return GetType().Name == className;
        }

        public Node FindChildByName(string name)
        {
            if (Children == null || string.IsNullOrEmpty(name)) {
                return null;
            }

            foreach (var child in Children) {
                if (child.Name == name) {
                    return child;
                }
            }

            return null;
        }
    }
}
