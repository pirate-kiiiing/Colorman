using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class UnityExtension
{
    public static IEnumerable<Transform> GetChildren(this Transform transform)
    {
        foreach (Transform child in transform)
        {
            yield return child;
        }
    }

    public static IEnumerable<Transform> GetActiveChildren(this Transform transform)
    {
        return GetChildren(transform).Where(t => t.gameObject.activeSelf);
    }

    public static Vector3 GetDeltaPosition(
        this Transform transform,
        Vector2 moveDirection)
    {
        Vector3 pointPosition;

        if (moveDirection.x > 0)
        {
            // player's moving right
            pointPosition = transform.position - new Vector3(Constants.POSITION_OFFSET, 0, 0);
        }
        else if (moveDirection.x < 0)
        {
            // player's moving left - detect node change 1 pixel early
            pointPosition = transform.position + new Vector3(Constants.POSITION_OFFSET - 0.1f, 0, 0);
        }
        else if (moveDirection.y > 0)
        {
            // player's moving up
            pointPosition = transform.position - new Vector3(0, Constants.POSITION_OFFSET, 0);
        }
        else // if (moveDirection.y < 0)
        {
            // player's moving down - detect node change 1 pixel early
            pointPosition = transform.position + new Vector3(0, Constants.POSITION_OFFSET - 0.1f, 0);
        }

        return pointPosition;
    }

    public static SystemLanguage GetGameLanguage(this SystemLanguage language)
    {
        switch (language)
        {
            case SystemLanguage.Korean:
            case SystemLanguage.English:
            case SystemLanguage.Japanese:
            //case SystemLanguage.French:
            //case SystemLanguage.German:
            //case SystemLanguage.Spanish:
            //case SystemLanguage.Portuguese:
            //case SystemLanguage.ChineseTraditional:
            //case SystemLanguage.ChineseSimplified:
                return language;

            //case SystemLanguage.Chinese:
            //    return SystemLanguage.ChineseSimplified;

            default:
                return SystemLanguage.English;
        }
    }
}
