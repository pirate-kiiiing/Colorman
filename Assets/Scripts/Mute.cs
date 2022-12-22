using UnityEngine;
using UnityEngine.UI;

public class Mute : ThemedUI
{
    public Image Image;

    private static bool muted;
    public bool Muted
    {
        get => muted;
        set
        {
            if (muted == value) return;

            muted = value;

            Color color = GameManager.Instance.ThemeColor;
            string spriteName = GetSpriteName(color);
            Sprite sprite = SpriteManager.Instance.GetSprite(spriteName);

            Image.sprite = sprite;

            AudioManager.Instance.MuteAll(Muted);

            SaveSystem.Save();
        }
    }

    public void Toggle() => Muted = !Muted;

    protected override string GetSpriteName(Color color = Color.Red)
        => (Muted == true)
            ? $"Mute_{color}"
            : $"Sound_{color}";
}
