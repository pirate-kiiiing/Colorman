using UnityEngine;
using UnityEngine.UI;

public class Setting : ThemedUI
{
    public static Setting Instance;

    public GameObject SettingLayout;

    private bool isActive = true;
    [HideInInspector]
    public bool Active
    {
        get => isActive;
        set
        {
            if (isActive == value) return;

            isActive = value;

            SettingLayout.SetActive(isActive);
        }
    }

    private Image image;

    protected override void Awake()
    {
        base.Awake();

        Instance = this;

        SettingLayout.transform.localPosition
            = new Vector3(0f, 0f, 0f);
    }

    void Start()
    {
        SetColor(GameManager.Instance.ThemeColor);
        Active = false;

        transform.position = GetScreenPosition(transform.position);
    }

    public void ToggleSetting()
    {
        if (GameManager.Instance.IsStageComplete == true ||
            GameManager.Instance.IsOutOfMoves() == true ||
            (TutorialManager.Instance != null &&
            TutorialManager.Instance.isActiveAndEnabled)) return;

        Active = !Active;
    }

    protected override string GetSpriteName(Color color = Color.Red)
        => $"{nameof(Setting)}_{color}";
}
