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
    }

    public void ToNextLevel()
    {
        GameManager.Instance.LoadNextScene();
    }
}
