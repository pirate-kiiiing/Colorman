using System;
using UnityEngine;

public class Node : MonoBehaviour
{
    public GameObject _border;

    public NodeType Type { get; set; }
    public Vector2Int Point { get; set; }

    public virtual void Init(GameObject gameObject, NodeProperty property)
    {
        Type = property.Type;
        Point = property.Point;

        transform.position = property.Point.ToVector3();

        name = $"{this.GetType()} {Point}";

        if (_border != null)
        {
            var border = Instantiate(_border, this.transform);
            border.name = "Border";
        }
    }

    public virtual void ApplyMove()
    {
    }

    public virtual void ApplySplash(Color color, float renderDelay)
    { 
    }

    public bool HasObstacleOnTheWay(Node target)
    {
        Vector2Int dir = (target.Point - Point).ToUnitVector();
        Vector2Int point = Point;

        do
        {
            point += dir;
            NodeType type = Board.Instance.GetNode(point).Type;

            if (type.IsObstacle() == true)
            {
                return true;
            }
        } while (point != target.Point);

        return false;
    }
}
