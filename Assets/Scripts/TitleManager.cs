using UnityEngine;

public class TitleManager : MonoBehaviour
{
    private const float titleAnimationSpeed = 1.1f;
    private const float titleCompleteSpeed = 0.15f;

    public static TitleManager Instance;

    public GameObject NewGame;
    public GameObject Continue;
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

            NewGame.SetActive(isReady);
            Continue.SetActive(isReady);
            
            if (isReady == true && SaveSystem.Data.Level == 1) Continue.SetActive(false);
        }
    }

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
    }

    void Update()
    {
        UpdateTitleAnimation();
    }

    public void ActivateTitle()
    {
        titleAnimator.speed = titleAnimationSpeed;
    }

    public void ResetLevel()
    {
        SaveSystem.Data.Level = 1;
        SaveSystem.Save();
        
        ToScene();
    }

    public void ToScene()
    {
        GameManager.LoadNextScene(SaveSystem.Data.Level);
    }

    private void UpdateTitleAnimation()
    {
        if (titleAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1) return;
        if (titleAnimator.GetCurrentAnimatorStateInfo(0).IsName(nameof(TitleComplete)) == true) return;
        
        titleAnimator.runtimeAnimatorController = TitleComplete;
        titleAnimator.speed = titleCompleteSpeed;
    }
}
