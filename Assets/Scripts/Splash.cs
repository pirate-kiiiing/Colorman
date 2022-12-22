using UnityEngine;

public class Splash : AnimatableNode
{
    protected Color Color { get; private set; }

    public override void Init(GameObject gameObject, NodeProperty property)
    {
        base.Init(gameObject, property);

        var splashProperty = property as SplashProperty;

        Color = splashProperty.Color;

        var renderer = GetComponent<SpriteRenderer>();
        renderer.color = Color.ToUnityColor();
    }

    public override void ApplyMove()
    {
        Player.Instance.Color = Color;

        AudioManager.Instance.Play(nameof(Splash));

        ReplaceWithTile();
    }

    protected void ApplySplash(int x, int y, float renderDelay)
    {
        Node node = Board.Instance.GetNode(x, y);
        node.ApplySplash(Color, renderDelay);
    }

    protected void ApplySplash(Vector2Int point, float renderDelay)
        => ApplySplash(point.x, point.y, renderDelay);

    protected bool IsSplashable(int x, int y)
    {
        return Board.Instance.GetNode(x, y) is Tile;
    }

    protected void ReplaceWithTile()
    {
        var tileProperty = new TileProperty(Point, Color);
        Node tile = Nodes.Instance.Create(tileProperty);
        
        Board.Instance.ReplaceNode(this, tile);

        Destroy(gameObject);
    }
}
