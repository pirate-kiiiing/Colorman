using System.Collections;
using UnityEngine;

public class Tile : Node
{
    public Color Color { get; private set; }

    private new SpriteRenderer renderer;
    
    public override void Init(GameObject gameObject, NodeProperty property)
    {
        base.Init(gameObject, property);

        renderer = GetComponent<SpriteRenderer>();

        TileProperty tileProperty = property as TileProperty;

        Increment(Color);

        SetColor(tileProperty.Color);
    }

    public void SetColor(
        Color color, 
        float renderDelay = 0f)
    {
        if (Color == color) return;

        Decrement(Color);

        Color = color;

        if (renderDelay == 0f)
        {
            ChangeColor();
        }
        else
        {
            StartCoroutine(ChangeColor(renderDelay));
        }

        Increment(Color);

        return;
    }

    public override void ApplyMove()
    {
        // change tile color
        SetColor(Player.Instance.Color);
        AudioManager.Instance.Play(nameof(Tile));
    }

    public override void ApplySplash(Color color, float renderDelay)
    {
        SetColor(color, renderDelay: renderDelay);
    }

    private IEnumerator ChangeColor(float renderDelay)
    {
        for (int i = 0; i < 1; i++)
        {
            yield return new WaitForSeconds(renderDelay);

            ChangeColor();
        }
    }

    private void ChangeColor()
    {
        renderer.color = Color.ToUnityColor();
    }

    private void Increment(Color color)
    {
        Objectives.Instance.Incremment(color);
    }

    private void Decrement(Color color)
    {
        Objectives.Instance.Decrement(color);
    }
}
