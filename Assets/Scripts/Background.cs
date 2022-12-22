using System;
using UnityEngine;

public class Background : MonoBehaviour
{
    private SpriteRenderer renderer;

    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        string spriteName =
            (isDayTime() == true)
            ? $"{SpriteNames.Background}Day"
            : $"{SpriteNames.Background}Night";

        renderer.sprite = SpriteManager.Instance.GetSprite(spriteName);

        foreach (Transform child in transform)
        {
            var childRenderer = child.GetComponent<SpriteRenderer>();
            childRenderer.sprite = SpriteManager.Instance.GetSprite(spriteName);
        }
    }

    private bool isDayTime()
    {
        return 6 <= DateTime.Now.Hour
            && DateTime.Now.Hour < 18;
    }
}
