using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : AnimatableNode
{
    private static Dictionary<byte, Warp> map = new Dictionary<byte, Warp>();
    private static HashSet<byte> used = new HashSet<byte>();

    public RuntimeAnimatorController Gray;
    public RuntimeAnimatorController Red;
    public RuntimeAnimatorController Yellow;
    public RuntimeAnimatorController Green;
    public RuntimeAnimatorController Blue;

    public Warp Twin { get; private set; }

    private byte key;

    void OnDestroy()
    {
        map = new Dictionary<byte, Warp>();
        used = new HashSet<byte>();
    }

    public override void Init(GameObject gameObject, NodeProperty property)
    {
        base.Init(gameObject, property);

        var warpProperty = property as WarpProperty;

        key = warpProperty.Key;
        SetController(warpProperty.Color);

        if (map.ContainsKey(key) == false)
        {
            map.Add(key, this);
        }
        else if (used.Contains(key) == true)
        {
            throw new Exception($"{nameof(Warp)}{key} already has a matching twin.");
        }
        else
        {
            // link warp nodes
            map[key].Twin = this;
            this.Twin = map[key];

            map.Remove(key);
            used.Add(key);
        }
    }

    public static void Validate()
    {
        if (map.Keys.Count > 0 || map.Count > 0)
            throw new Exception("Every warp requires a twin partner.");
    }

    public override void ApplyMove()
    {
        AudioManager.Instance.Play(nameof(Warp));
        
        StartCoroutine(ApplyMove(0.5f));
    }

    private IEnumerator ApplyMove(float delay)
    {
        Player.Instance.Show(false);
        
        yield return new WaitForSeconds(delay);

        Player.Instance.FinishMoving(Twin);
        Player.Instance.Show(true);
    }

    private void SetController(Color color)
    {
        switch (color)
        {
            case Color.White: animator.runtimeAnimatorController = Gray; break;
            case Color.Red: animator.runtimeAnimatorController = Red; break;
            case Color.Yellow: animator.runtimeAnimatorController = Yellow; break;
            case Color.Green: animator.runtimeAnimatorController = Green; break;
            case Color.Blue: animator.runtimeAnimatorController = Blue; break;
            default: throw new ArgumentException($"Invalid {nameof(Warp)} Color");
        }
    }
}
