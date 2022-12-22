using UnityEngine;

public class HoleProperty : NodeProperty
{
    public override NodeType Type { get; } = NodeType.Hole;

    public HoleProperty(Vector2Int point) : base(point)
    { 
    }
}
