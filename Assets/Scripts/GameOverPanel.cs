using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    public float move_speed;

    private static Vector3 startingPosition = new Vector3(0f, -2300f, 0f);
    private Vector3? targetPosition = null;

    private void Start()
    {
        Refresh();
    }

    void Update()
    {
        if (targetPosition == null) return;

        transform.localPosition = 
            Vector3.MoveTowards(
                transform.localPosition,
                targetPosition.Value,
                Time.deltaTime * move_speed);

        if (transform.localPosition == targetPosition.Value)
        {
            targetPosition = null;
        }
    }

    public void Activate()
    {
        targetPosition = new Vector3(0, 0, 0);
    }

    public void Refresh()
    {
        transform.localPosition = startingPosition;
    }
}
