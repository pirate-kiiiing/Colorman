using UnityEngine;

public class WarpProperty : NodeProperty
{
    public override NodeType Type { get; } = NodeType.Warp;
    public Color Color;
    public byte Key { get; }

    public WarpProperty(
        Vector2Int point, 
        Color color,
        byte key) : base(point)
    {
        Color = color;
        Key = key;
    }
}
