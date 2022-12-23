using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
#if DEBUG
    private const bool testMode = true;
#else
    private const bool testMode = false;
#endif

#if UNITY_IOS
    private const Platforms platform = Platforms.IOS;
#else
    private const Platforms platform = Platforms.Android;
#endif
    
    public static GameManager Instance;
    public static int StartLevel;

    public const int BonusMoves = 5;

    [HideInInspector]
    public int Level;

    private bool isStageComplete = false;
    [HideInInspector]
    public bool IsStageComplete
    {
        get => isStageComplete;
        set
        {
            if (isStageComplete == value) return;

            isStageComplete = value;

            if (isStageComplete == true)
            {
                Complete.SetActive(true);
                ToNextLevel.SetActive(true);
                Fireworks.Play();
                ShowSkipReplay(false);
                
                AudioManager.Instance.Play("Yay");
            }
        }
    }

    [HideInInspector]
    public Color ThemeColor { get; set; }

    public Text CurrentLevel;
    public GameObject Complete;
    public GameObject ToNextLevel;
    public ParticleSystem Fireworks;
    public GameObject _outOfMoves;
    public GameObject _outOfMovesPanel; 
    public GameObject _buttonRestart;
    public GameObject _buttonLevel;
    public GameObject _buttonLeft;
    public GameObject _buttonRight;

    void Awake()
    {
        Instance = this;
        
        Fireworks.Stop();
        ToNextLevel.SetActive(false);
        Level = SaveSystem.Data.Level;
        //Level = (byte)SceneManager.GetActiveScene().buildIndex;
        ThemeColor = GetThemeColor(Level);
        StartLevel = (StartLevel == 0) ? Level : StartLevel;

        var buttonImage = _buttonLevel.GetComponent<Image>();
        buttonImage.raycastTarget = IsTestMode() == true;

        if (IsTestMode() == false)
        {
            _buttonLeft.SetActive(false);
            _buttonRight.SetActive(false);
        }
    }

    void Start()
    {
        Application.targetFrameRate = 60;
        CurrentLevel.text = $"LEVEL {Level}";
        Complete.SetActive(false);
        ToNextLevel.SetActive(false);
        _outOfMoves.SetActive(false);
        _outOfMoves.transform.localPosition = new Vector3(0, 0, 0);

        if (Level > GetTotalLevelCount() &&
            TutorialManager.Instance != null)
        {
            TutorialManager.Instance.gameObject.SetActive(false);
        }

        LevelManager.Instance.Init();
        Board.Instance.Init();
        Player.Instance.Init();

        UpdateGameState();
    }

    public static Platforms GetPlatform() => platform;

    public static string GetGameId()
        => (GetPlatform() == Platforms.IOS)
            ? Constants.GameIds.Apple
            : Constants.GameIds.Google;

    public static bool IsTestMode() => testMode;

    // moves gameObject away from camera to make it disappear
    public static void RemoveFromCamera(GameObject gameObject)
    {
        gameObject.transform.position = new Vector3(-6f, 0, 0);
    }

    public static void LoadNextScene(int level)
    {
        int levelCount = GetTotalLevelCount();
        int scene = (level % levelCount == 0)
            ? levelCount
            : level % levelCount;
        
        SceneManager.LoadScene(scene);
    }

    public void LoadNextScene()
    {
        SaveSystem.Data.Level = ++Level;
        SaveSystem.Save();

        LoadNextScene(Level);
    }

    public bool IsOutOfMoves()
    {
        return _outOfMoves.activeSelf;
    }

    public void ShowOutOfMoves(bool show)
    {
        _outOfMoves.SetActive(show);

        if (show == true)
        {
            AudioManager.Instance.Play("GameOver");
            _buttonRestart.SetActive(false);
            StartCoroutine(ActivateGameOver());
        }
        else
        {
            var panel = _outOfMovesPanel.GetComponent<GameOverPanel>();

            panel.Refresh();
        }
    }

    IEnumerator ActivateGameOver()
    {
        yield return new WaitForSeconds(1.5f);

        var panel = _outOfMovesPanel.GetComponent<GameOverPanel>();
        
        panel.Activate();
    }

    public void Restart(bool deferAd = false)
    {
        if (TutorialManager.Instance != null &&
            TutorialManager.Instance.isActiveAndEnabled == true)
        {
            return;
        }

#if DEBUG
        int scene = GetScene(SceneManager.GetActiveScene().buildIndex);
#else
        int scene = GetScene(Level);
#endif
        SceneManager.LoadScene(scene);
    }

    public static bool HasNetwork()
        => Application.internetReachability != NetworkReachability.NotReachable;

    public void ShowSkipReplay(bool show)
    {
        _buttonRestart.SetActive(show);
    }

    public void UpdateGameState()
    {
        IsStageComplete = Objectives.Instance.IsComplete();
    }

    public void OpenLink(string url)
    {
        Application.OpenURL(url);
    }

    public void ResetLevel()
    {
        Level = 1;
        SaveSystem.Data.Level = Level;
        SaveSystem.Save();

        LoadNextScene(Level);
    }

    public void ToScene(bool next)
    {
        if (next == true) Level++;
        else Level--;

        SaveSystem.Data.Level = Level;
        SaveSystem.Save();

        LoadNextScene(Level);
    }

    public static float GetAspectRatio() => (float) Screen.height / (float) Screen.width;

    public static int GetTotalLevelCount()
        => SceneManager.sceneCountInBuildSettings - 1;

    private static Color GetThemeColor(int level)
    {
        return Color.Red;

        //level %= 100;

        //if (0 <= level && level <= 25) return Color.Red;
        //else if (26 <= level && level <= 50) return Color.Yellow;
        //else if (51 <= level && level <= 75) return Color.Green;
        //return Color.Blue;
    }

    private static int GetScene(int level)
    {
        int levelCount = GetTotalLevelCount();
        
        return (level % levelCount == 0)
            ? levelCount
            : level % levelCount;
    }
}
