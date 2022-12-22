using System.Collections.Generic;
using UnityEngine;

public class Constants
{
    public const float POSITION_OFFSET = 0.5f;

    public class GameIds
    {
        public const string Apple = "3615596";
        public const string Google = "3615597";
    }

    public static IList<SystemLanguage> SupportedLanguages = new List<SystemLanguage>()
    {
        SystemLanguage.Korean,
        SystemLanguage.English,
        SystemLanguage.Japanese,
        SystemLanguage.Chinese,
        SystemLanguage.ChineseSimplified,
        SystemLanguage.ChineseTraditional,
        SystemLanguage.French,
        SystemLanguage.German,
        SystemLanguage.Spanish,
        SystemLanguage.Portuguese,
    };
}

public enum Platforms
{
    IOS,
    Android,
}