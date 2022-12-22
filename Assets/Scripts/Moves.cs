using UnityEngine;
using UnityEngine.UI;

public class Moves : ThemedUI
{
    public static Moves Instance;

    public Text _count;

    protected override void Awake()
    {
        base.Awake();

        Instance = this;
    }

    void Start()
    {
        SetColor(GameManager.Instance.ThemeColor);
        
        transform.position = GetScreenPosition(transform.position);
    }

    public void SetMoveCount(byte moveCount)
    {
        _count.text = moveCount.ToString();
    }

    protected override string GetSpriteName(Color color = Color.Red)
        => $"{color}_S";
}
