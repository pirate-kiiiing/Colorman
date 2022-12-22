public class Restart : ThemedUI
{
    public bool FixPosition;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        SetColor(GameManager.Instance.ThemeColor);

        if (FixPosition == false)
        {
            transform.position = GetScreenPosition(transform.position);
        }
    }

    protected override string GetSpriteName(Color color = Color.Red)
        => $"{SpriteNames.Restart}_{color}";
}
