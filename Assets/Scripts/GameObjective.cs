using System;

public abstract class GameObjective
{
    public byte Objective;
}

[Serializable]
public class ColorObjective : GameObjective
{
    public Color Color;

    public ColorObjective(Color color, byte objective)
    {
        Color = color;
        Objective = objective;
    }
}

[Serializable]
public class IceObjective : GameObjective
{
    public IceObjective(byte objective)
    {
        Objective = objective;
    }
}
