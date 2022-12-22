using UnityEngine;

public class Title : MonoBehaviour
{
    public GameObject _title;

    void Start()
    {
        _title.transform.localScale = GetScale();
    }

    private Vector3 GetScale()
    {
        float ratio = GameManager.GetAspectRatio();
        float scale = 0.5f;

        if (ratio > 2.1f) scale = 0.5f;
        else if (ratio > 1.9f) scale = 0.55f;
        else if (ratio > 1.5f) scale = 0.6f;
        else if (ratio > 1.4f) scale = 0.7f;
        else if (ratio > 1.3f) scale = 0.8f;
        else scale = 0.85f;

        return new Vector3(scale, scale, scale);
    }
}
