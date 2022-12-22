public enum NodeType
{
    Tile,

    Paint,

    Ice,

    Wall,

    Hole,

    Warp,

    Finish,

    SplashCross,

    SplashSquare,
}

public static class NodeExtensions
{
    public static bool IsObstacle(this NodeType type)
    {
        switch (type)
        {
            case NodeType.Hole: return true;
            case NodeType.Wall: return true;

            default: return false;
        }
    }
}
