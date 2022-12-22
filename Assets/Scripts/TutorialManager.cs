using System.Collections;
using System.Linq;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    public GameObject Tutorial;
    public SpriteRenderer TextImage;
    public GameObject _tutorialManager;

    [HideInInspector]
    public bool IsMouseInputReady;

    private void Awake()
    {
        Instance = this;

        IsMouseInputReady = false;
    }

    private void Start()
    {
        string spriteName = $"{TextImage.name}Tutorial";

        TextImage.sprite = SpriteManager.Instance.GetSprite(spriteName);
        Tutorial.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (IsMouseInputReady == false) return;
        if (Input.GetMouseButtonUp(0) == false) return;

        StartCoroutine(Deactivate());
    }

    public void ShowNext()
    {
        Tutorial.SetActive(true);
    }

    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(0.01f);

        gameObject.SetActive(false);
    }
}
