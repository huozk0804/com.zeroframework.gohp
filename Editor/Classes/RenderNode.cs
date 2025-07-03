using UnityEngine;

namespace ZeroFramework.Editor.Package
{
    public class RenderNode
    {
        private readonly Nodes nodes;
        public Node Node { get; }
        
        public Vector2 Position { get; set; }
        public Rect Rect => new Rect(Position.x, Position.y, 200, 150);

        public RenderNode(Node node)
        {
            Node = node;
        }
    }
}