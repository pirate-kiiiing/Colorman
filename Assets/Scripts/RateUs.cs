using System;
using System.Collections;
using UnityEngine;

public class RateUs : MonoBehaviour
{
    public static RateUs Instance;
    
    private const string id = "com.GoldStone.Colorman";
    private const string appleId = "1523943271";

    public GameObject GameObject;

    [HideInInspector]
    public bool Active { get; set; }

    void Awake()
    {
        Instance = this;
        Active = false;
    }

    public void Activate()
    {
        if (DateTime.Now < SaveSystem.Data.RateUsTimer ||
            TutorialManager.Instance != null ||
            GameManager.Instance.Level <= 20 ||
            GameManager.Instance.Level - GameManager.StartLevel < 5)
        {
            return;
        }
        
        StartCoroutine(Activate(delay: 0.5f));

        Active = true;
    }

    public void Yes()
    {
        // https://www.youtube.com/watch?v=n3-bp5jAHXY
        if (GameManager.GetPlatform() == Platforms.Android)
        {
            Application.OpenURL($"market://details?id={id}");
        }
        else
        {
            Application.OpenURL($"itms-apps://itunes.apple.com/app/id{appleId}");
        }

        SaveSystem.Data.RateUsTimer = DateTime.MaxValue;
        SaveSystem.Save();

        Active = false;
        GameObject.SetActive(false);
    }

    public void Later()
    {
        SaveSystem.Data.RateUsTimer = DateTime.Now.AddMinutes(30);
        SaveSystem.Save();

        Active = false;
        GameObject.SetActive(false);
    }

    private IEnumerator Activate(float delay)
    {
        for (int i = 0; i < 1; i++)
        {
            yield return new WaitForSeconds(delay);

            transform.localPosition = new Vector3(0f, 0f, 0f);
        }
    }
}
