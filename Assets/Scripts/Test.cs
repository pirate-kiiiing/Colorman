using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public static Test Instance;

    public GameObject TestObject;
    
    [HideInInspector]
    public string Text
    {
        get => text.text;
        set
        {
            if (text.text == value) return;

            text.text = value;
        }
    }

    private Text text;

    void Awake()
    {
        Instance = this;

        text = TestObject.GetComponentInChildren<Text>();
    }

    void Start()
    {
        bool isTest = GameManager.IsTestMode();
        // todo
        //isTest = true;
        TestObject.SetActive(isTest);

        text.text = AdsManager.Instance.GetState();
    }

    void FixedUpdate()
    {
        text.text = AdsManager.Instance.GetState();
    }

    public void ToNextLevel()
    {
        GameManager.Instance.LoadNextScene();
    }
}
