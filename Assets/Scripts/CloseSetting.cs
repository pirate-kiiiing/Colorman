public class CloseSetting : ThemedUI
{
    public static CloseSetting Instance;

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }

    void Start()
    {
        SetColor(GameManager.Instance.ThemeColor);
    }

    public void Close()
    {
        Setting.Instance.Active = false;
    }

    protected override string GetSpriteName(Color color = Color.Red) 
        => $"{SpriteNames.Cancel}_{color}";
}
