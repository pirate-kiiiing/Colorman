using UnityEngine;

public class Scroll : MonoBehaviour
{
    public float speed = 5f;
    public float clampPosition = 80f;    // clamping position

    private Vector2 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * speed, clampPosition);

        transform.position = startPosition + Vector2.left * newPosition;
    }
}
