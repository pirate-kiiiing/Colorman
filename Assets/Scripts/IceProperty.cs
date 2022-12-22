using UnityEngine;

public class IceProperty : NodeProperty
{
    public override NodeType Type { get; } = NodeType.Ice;

    public byte Health { get; set; }

    public IceProperty(
        Vector2Int point, 
        byte health = Ice.DefaultHealth) : base(point)
    {
        Health = health;
    }
}
