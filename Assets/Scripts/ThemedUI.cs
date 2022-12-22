using System;
using UnityEngine;
using UnityEngine.UI;

public class ThemedUI : MonoBehaviour
{
    private const float posCenterX = 4.0f;

    private Image image;

    protected virtual void Awake()
    {
        image = GetComponent<Image>();
    }

    void Start()
    {
        image.sprite = SpriteManager.Instance.GetSprite(GetSpriteName());
    }

    public void SetColor(Color color)
    {
        image.sprite = SpriteManager.Instance.GetSprite(GetSpriteName(color));
    }

    protected virtual string GetSpriteName(Color color = Color.Red)
        => throw new NotImplementedException();

    protected Vector3 GetScreenPosition(Vector3 pos)
    {
        float ratio = GameManager.GetAspectRatio();

        if (pos.x < posCenterX)
        {
            if (ratio > 2.1f) return new Vector3(0.2f, pos.y, pos.z);
            else if (ratio > 1.9f) return new Vector3(-0.1f, pos.y, pos.z);
            else if (ratio > 1.5f) return new Vector3(-0.7f, pos.y, pos.z);
            else return new Vector3(-1f, pos.y, pos.z);
        }
        else
        {
            if (ratio > 2.1f) return new Vector3(7.8f, pos.y, pos.z);
            else if (ratio > 1.9f) return new Vector3(8.1f, pos.y, pos.z);
            else if (ratio > 1.5f) return new Vector3(8.7f, pos.y, pos.z);
            else return new Vector3(9f, pos.y, pos.z);
        }
    }
}
