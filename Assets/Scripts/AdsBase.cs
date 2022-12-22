using UnityEngine;
using UnityEngine.Advertisements;

public class AdsBase : MonoBehaviour
{
    protected readonly static string gameId = GameManager.GetGameId();
    protected readonly static bool testMode = GameManager.IsTestMode();
    
    private static bool isInitialized = false;

    protected void Init()
    {
        if (isInitialized == true) return;

        isInitialized = true;
        
        Advertisement.Initialize(gameId, testMode);
    }
}

public class PlacementId
{
    public const string Skippable = "video";
    public const string Rewarded = "rewardedVideo";
    public const string Banner = "Banner";
}
