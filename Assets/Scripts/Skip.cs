public class Skip : ThemedUI
{
    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        SetColor(GameManager.Instance.ThemeColor);

        transform.position = GetScreenPosition(transform.position);
    }

    protected override string GetSpriteName(Color color = Color.Red)
        => $"{SpriteNames.Video}_{color}";
}
