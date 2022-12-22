using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsBanner : AdsBase
{
    public static AdsBanner Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        base.Init();

        Load();
    }

    public void Load()
    {
        StartCoroutine(ShowBanner());
    }

    IEnumerator ShowBanner()
    {
        while (Advertisement.IsReady(PlacementId.Banner) == false)
        {
            yield return new WaitForSeconds(0.5f);
        }

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show(PlacementId.Banner);
    }
}
