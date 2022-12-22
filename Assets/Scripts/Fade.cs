using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public float FadePerSecond = 2.5f;
    public float DelayInSeconds = 0f;

    private const float targetAlpha = 0.95f;

    private static int completionCount;

    private void Start()
    {
        IList<Transform> children = transform.GetChildren().ToList();

        completionCount = children.Count;
        
        foreach (Transform child in children)
        {
            Renderer renderer = child.GetComponent<Renderer>();
            Material material = renderer.material;

            StartCoroutine(FadeIn(material));
        }
    }

    private IEnumerator FadeIn(Material material)
    {
        UnityEngine.Color resultColor = material.color;
        material.color = new UnityEngine.Color(resultColor.r, resultColor.g, resultColor.b, 0);
        
        yield return new WaitForSeconds(DelayInSeconds);

        while (material.color.a < targetAlpha)
        {
            UnityEngine.Color newColor = material.color;
            float newAlpha = Math.Min(targetAlpha, newColor.a + (FadePerSecond * Time.deltaTime));
            
            material.color =
                new UnityEngine.Color(
                    newColor.r,
                    newColor.g,
                    newColor.b,
                    newAlpha);

            yield return null;
        }

        if (--completionCount <= 0)
        {
            TutorialManager.Instance.IsMouseInputReady = true;
            TutorialManager.Instance.ShowNext();
        }
    }
}
