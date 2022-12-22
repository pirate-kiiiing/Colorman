using UnityEngine;

public class HideIOS : MonoBehaviour
{
    void Start()
    {
        if (GameManager.GetPlatform() == Platforms.Android) return;
        
        gameObject.SetActive(false);
    }
}
