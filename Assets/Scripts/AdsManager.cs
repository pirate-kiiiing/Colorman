using System;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance;

    private const int COUNT_DOWN_BASE = 5;
    private const float SHOW_EVERY_X_SECONDS = 240;

    private static System.Random random = new System.Random();
    private static int countDown = GetNewCountDown();
    private static float timeShown = 0.0f;

    void Awake()
    {
        Instance = this;
    }

    public void Execute(Action callback, bool deferAd = false)
    {
        if (deferAd == false || countDown > 1)
        {
            countDown--;
        }

        if (ShouldShow() == true)
        {
            AdsVideo.Instance.PlaySkippable(callback);
        }
        else
        {
            callback();
        }
    }

    public static void ResetCooldown(int bonus = 0)
    {
        countDown = GetNewCountDown(bonus);
        timeShown = Time.time;
    }

    public string GetState()
        => $"{countDown}, {Math.Max(0, (int)(SHOW_EVERY_X_SECONDS - Time.time + timeShown))}";

    private static int GetNewCountDown(int bonus = 0)
    {
        return COUNT_DOWN_BASE + bonus + random.Next(2);
    }

    private bool ShouldShow()
    {
        if (countDown <= 0) return true;
        if (Time.time - timeShown >= SHOW_EVERY_X_SECONDS) return true;

        return false;
    }
}
