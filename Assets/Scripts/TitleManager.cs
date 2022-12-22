using UnityEngine;

public class TitleManager : MonoBehaviour
{
    private const float titleAnimationSpeed = 1.1f;
    private const float titleCompleteSpeed = 0.15f;

    public static TitleManager Instance;

    public GameObject TabToContinue;
    public GameObject Title;
    public RuntimeAnimatorController TitleComplete;

    [HideInInspector]
    private bool isReady;
    public bool IsReady
    {
        get => isReady;
        set
        {
            isReady = value;

            TabToContinue.SetActive(isReady);
        }
    }

    private SaveData data;
    private Animator titleAnimator;

    void Awake()
    {
        Instance = this;

        IsReady = false;

        titleAnimator = Title.GetComponent<Animator>();
        titleAnimator.speed = 0f;
    }

    void Start()
    {
        Application.targetFrameRate = 60;

        data = SaveSystem.Data;
    }

    void Update()
    {
        UpdateTitleAnimation();

        if (IsReady == false) return;
        if (Input.GetMouseButtonUp(0) == false) return;

        GameManager.LoadNextScene(data.Level);
    }

    public void ActivateTitle()
    {
        titleAnimator.speed = titleAnimationSpeed;
    }

    private void UpdateTitleAnimation()
    {
        if (titleAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1) return;
        if (titleAnimator.GetCurrentAnimatorStateInfo(0).IsName(nameof(TitleComplete)) == true) return;
        
        titleAnimator.runtimeAnimatorController = TitleComplete;
        titleAnimator.speed = titleCompleteSpeed;
    }
}
