using UnityEngine;

public class WallProperty : NodeProperty
{
    public override NodeType Type { get; } = NodeType.Wall;

    public WallProperty(Vector2Int point) : base(point)
    {
    }
}
