using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public byte Moves;
    public byte Red;
    public byte Yellow;
    public byte Green;
    public byte Blue;
    public byte Ice;
    public Vector2Int _playerStartingPoint;
    public PlayerDirection _playerStartingDirection;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (Moves == 0)
        {
            throw new ArgumentException($"{nameof(Moves)} must be greater than 0");
        }
    }

    public void Init()
    {
        var objectives = new List<GameObjective>
        {
            new ColorObjective(Color.Red.LevelAdjustedColor(), Red),
            new ColorObjective(Color.Yellow.LevelAdjustedColor(), Yellow),
            new ColorObjective(Color.Green.LevelAdjustedColor(), Green),
            new ColorObjective(Color.Blue.LevelAdjustedColor(), Blue),
        };

        objectives = objectives.OrderBy(x => (x as ColorObjective).Color).ToList();
        objectives.Add(new IceObjective(Ice));
        
        if (objectives.Count(g => g.Objective == 0) == objectives.Count)
        {
            throw new ArgumentException($"All {nameof(objectives)} cannot be 0");
        }

        global::Objectives.Instance.Create(objectives);
    }
}
