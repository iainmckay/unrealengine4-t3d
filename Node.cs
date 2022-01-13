namespace JollySamurai.UnrealEngine4.T3D
{
    public abstract class Node
    {
        public string Name { get; }

        public Node[] Children { get; }

        public Node(string name, Node[] children = null)
        {
            Name = name;
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
