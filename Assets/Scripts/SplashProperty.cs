using UnityEngine;

public class SplashProperty : NodeProperty
{
    public override NodeType Type { get; }
    public Color Color { get; set; }

    public SplashProperty(
        Vector2Int point, 
        NodeType nodeType,
        Color color) : base(point)
    {
        Type = nodeType;
        Color = color;
    }
}
