using UnityEngine;

public class PaintProperty : NodeProperty
{
    public override NodeType Type { get; } = NodeType.Paint;

    public Color Color { get; set; }

    public PaintProperty(Vector2Int point, Color color) : base(point)
    {
        Color = color;
    }
}
