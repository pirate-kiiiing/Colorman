using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsVideo : AdsBase, IUnityAdsListener
{
    public static AdsVideo Instance;

    private static bool isInitialized = false;
    private static bool hasNetwork;
    private static Action onAdsDidFinish;
    // this has to be static..
    private static bool? isRewardedSkip;

    public GameObject _skip;

    void Awake()
    {
        Instance = this;

        onAdsDidFinish = null;
    }

    void Start()
    {
        base.Init();

        if (isInitialized == false)
        {
            Advertisement.AddListener(this);
            isInitialized = true;
            hasNetwork = GameManager.HasNetwork();
        }
        if (GameManager.HasNetwork() == true)
        {
            LoadAll();
        }
        else
        {
            _skip.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        bool hasNetworkNow = GameManager.HasNetwork();
        if (hasNetwork == hasNetworkNow) return;

        if (hasNetworkNow == true)
        {
            // regained connection
            LoadAll();
            AdsBanner.Instance.Load();
        }
        else
        {
            // lost connection
            _skip.SetActive(false);
        }

        hasNetwork = hasNetworkNow;
    }

    public void PlaySkippable(Action onAdsDidFinish)
    {
        if (onAdsDidFinish == null)
        {
            Debug.LogError($"Skippable {nameof(PlaySkippable)} must have {nameof(onAdsDidFinish)} defined");
            return;
        }

        AdsVideo.onAdsDidFinish = onAdsDidFinish;

        Advertisement.Show(PlacementId.Skippable);
    }

    public void PlayRewarded(bool isSkip)
    {
        if (TutorialManager.Instance != null &&
            TutorialManager.Instance.isActiveAndEnabled == true)
        {
            return;
        }

        AdsVideo.isRewardedSkip = isSkip;
        
        Advertisement.Show(PlacementId.Rewarded);
    }

    public void OnUnityAdsReady(string placementId)
    {
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId == PlacementId.Skippable)
        {
            onAdsDidFinish();
            onAdsDidFinish = null;

            AdsManager.ResetCooldown();
        }
        else if (placementId == PlacementId.Rewarded)
        {
            if (isRewardedSkip == null)
            {
                Debug.LogError($"{nameof(isRewardedSkip)} cannot be null");
                return;
            }
            else if (isRewardedSkip == true)
            {
                GameManager.Instance.LoadNextScene();
            }
            else // more turns
            {
                Player.Instance.Revive();
                GameManager.Instance.ShowOutOfMoves(show: false);
                GameManager.Instance.ShowSkipReplay(show: true);
            }

            isRewardedSkip = null;

            AdsManager.ResetCooldown(bonus: 1);
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.LogError(message);

        onAdsDidFinish = null;
        isRewardedSkip = null;
    }

    public void OnUnityAdsDidStart(string placementId)
    {
    }

    public void LoadAll()
    {
        StartCoroutine(Load(PlacementId.Skippable));
        StartCoroutine(Load(PlacementId.Rewarded));
    }

    private IEnumerator Load(string placementId)
    {
        if (Advertisement.IsReady(placementId) == false)
        {
            Advertisement.Load(placementId);
        }
        
        while (Advertisement.IsReady(placementId) == false)
        {
            yield return new WaitForSeconds(0.5f);
        }

        if (placementId == PlacementId.Rewarded)
        {
            _skip.SetActive(true);
        }
    }
}
