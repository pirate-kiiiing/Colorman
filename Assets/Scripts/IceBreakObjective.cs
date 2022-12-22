public class IceBreakObjective : ObjectiveBase
{
    void Start()
    {
        Validate();

        Refresh();
    }

    protected override string text =>
        $"{Objectives.Instance.GetIceCount()} / " +
        $"{Objectives.Instance.GetIceObjectiveCount()}";

    protected override bool isComplete =>
        Objectives.Instance.GetIceCount() 
        >= Objectives.Instance.GetIceObjectiveCount();
}
