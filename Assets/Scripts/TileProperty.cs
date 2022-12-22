using UnityEngine;

public class TileProperty : NodeProperty
{
    public override NodeType Type { get; } = NodeType.Tile;
    public Color Color { get; }

    public TileProperty(Vector2Int point, Color color) : base(point)
    {
        Color = color;
    }
}
