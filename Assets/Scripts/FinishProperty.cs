using UnityEngine;

public class FinishProperty : NodeProperty
{
    public override NodeType Type { get; } = NodeType.Finish;

    public FinishProperty(Vector2Int point) : base(point)
    {
    }
}
