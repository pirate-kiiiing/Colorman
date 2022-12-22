using UnityEngine;
using UnityEngine.UI;

public class PaintObjective : ObjectiveBase
{
    [HideInInspector]
    public Color Color;

    void Start()
    {
        Validate();

        Image image = transform.Find(nameof(Color)).GetComponent<Image>();
        image.color = Color.ToUnityColor();
        transform.name = $"{Color}{(nameof(Objective))}";

        Refresh();
    }

    protected override string text
        => $"{Objectives.Instance.GetCount(Color)} / " +
            $"{Objectives.Instance.GetObjectiveCount(Color)}";

    protected override bool isComplete =>
        Objectives.Instance.GetCount(Color) 
        >= Objectives.Instance.GetObjectiveCount(Color);
}
