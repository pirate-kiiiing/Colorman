using UnityEngine;

public class Paint : AnimatableNode
{
    public Color Color { get; private set; }

    public override void Init(GameObject gameObject, NodeProperty property)
    {
        base.Init(gameObject, property);

        PaintProperty paintProperty = property as PaintProperty;
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        Color = paintProperty.Color;
        renderer.color = Color.ToUnityColor();
    }

    public override void ApplyMove()
    {
        AudioManager.Instance.Play(nameof(Paint));

        if (Player.Instance.Color == Color) return;
        // player runs over Paint
        Player.Instance.Color = Color;
    }
}
