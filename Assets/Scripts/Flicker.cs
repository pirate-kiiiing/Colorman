using System.Collections;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    public GameObject GameObject;
    public float interval = 1f;

    private Vector3 scale;

    void Start()
    {
        scale = transform.localScale;

        StartCoroutine(Toggle());
    }

    private IEnumerator Toggle()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            gameObject.transform.localScale = new Vector3(0f, 0f, 0f);

            yield return new WaitForSeconds(interval);

            gameObject.transform.localScale = scale;
        }
    }
}
