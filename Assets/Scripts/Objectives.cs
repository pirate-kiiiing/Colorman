using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Objectives : ThemedUI
{
    private class ObjectiveDetail
    {
        // Current paint count
        public uint Count { get; set; }

        public ObjectiveBase Objective { get; set; }
    }

    public static Objectives Instance;

    private static List<List<float>> positions = new List<List<float>>()
    {
        new List<float>() { 0f },
        new List<float>() { -120f, 120f },
        new List<float>() { -240f, 0f, 240f },
        new List<float>() { -330f, -110f, 110f, 330f },
        new List<float>() { -380f, -190f, 0f, 190f, 380f },
    };

    public PaintObjective _paintObjective;
    public IceBreakObjective _iceBreakObjective;

    private Dictionary<Color, ObjectiveDetail> colorObjectiveMap;
    private ObjectiveDetail iceObjectiveDetail;

    protected override void Awake()
    {
        base.Awake();

        Instance = this;

        colorObjectiveMap = new Dictionary<Color, ObjectiveDetail>();
    }

    void Start()
    {
        SetColor(GameManager.Instance.ThemeColor);
    }

    public void Create(IEnumerable<GameObjective> objectives)
    {
        objectives = objectives.Where(o => o.Objective > 0);

        IList<ObjectiveBase> gameObjectives = new List<ObjectiveBase>();

        foreach (GameObjective objective in objectives)
        {
            ObjectiveBase gameObjective;

            if (objective is ColorObjective colorObjective)
            {
                gameObjective = Create(colorObjective);
            }
            else if (objective is IceObjective iceObjective)
            {
                gameObjective = Create(iceObjective);
            }
            else
            {
                throw new ArgumentException($"Invalid {nameof(gameObjective)}");
            }

            gameObjectives.Add(gameObjective);
        }

        for (int i = 0; i < gameObjectives.Count; i++)
        {
            ObjectiveBase activeObjective = gameObjectives[i];
            activeObjective.transform.localPosition =
                new Vector3(positions[gameObjectives.Count - 1][i], 0f, 0f);
        }
    }

    public bool IsComplete()
    {
        foreach (ObjectiveDetail info in colorObjectiveMap.Values)
        {
            if (info.Count < info.Objective.Objective) return false;
        }

        if (iceObjectiveDetail != null &&
            iceObjectiveDetail.Count < iceObjectiveDetail.Objective.Objective)
        {
            return false;
        }

        return true;
    }

    public uint GetCount(Color color)
        => colorObjectiveMap.ContainsKey(color)
            ? colorObjectiveMap[color].Count
            : 0;

    public uint GetObjectiveCount(Color color)
        => colorObjectiveMap.ContainsKey(color)
            ? colorObjectiveMap[color].Objective.Objective
            : 0;

    public uint GetIceCount() => iceObjectiveDetail.Count;

    public uint GetIceObjectiveCount() => iceObjectiveDetail.Objective.Objective;

    public void Incremment(Color color)
    {
        if (colorObjectiveMap.ContainsKey(color) == false) return;

        colorObjectiveMap[color].Count++;
        RefreshObjective(color);
    }

    public void IncrementIce()
    {
        if (iceObjectiveDetail == null) return;
        
        iceObjectiveDetail.Count++;

        iceObjectiveDetail.Objective.Refresh();
    }

    public void Decrement(Color color)
    {
        if (colorObjectiveMap.ContainsKey(color) == false)
        {
            return;
        }
        if (colorObjectiveMap[color].Count <= 0)
        {
            throw new InvalidOperationException($"Cannot decrement {color} which is already {colorObjectiveMap[color].Count}");
        }

        colorObjectiveMap[color].Count--;
        RefreshObjective(color);
    }

    private ObjectiveBase Create(ColorObjective colorObjective)
    {
        PaintObjective paintObjective = Instantiate(_paintObjective, transform);
        paintObjective.Color = colorObjective.Color;
        paintObjective.Objective = colorObjective.Objective;

        colorObjectiveMap.Add(colorObjective.Color, new ObjectiveDetail
        {
            Count = 0,
            Objective = paintObjective,
        });

        return paintObjective;
    }

    private ObjectiveBase Create(IceObjective iceObjective)
    {
        IceBreakObjective iceBreakObjective = Instantiate(_iceBreakObjective, transform);
        iceBreakObjective.Objective = iceObjective.Objective;

        iceObjectiveDetail = new ObjectiveDetail()
        {
            Count = 0,
            Objective = iceBreakObjective,
        };

        return iceBreakObjective;
    }

    private void RefreshObjective(Color color)
    {
        if (colorObjectiveMap[color].Objective == null) return;

        colorObjectiveMap[color].Objective.Refresh();
    }

    protected override string GetSpriteName(Color color = Color.Red)
        => $"{color}_L";
}
