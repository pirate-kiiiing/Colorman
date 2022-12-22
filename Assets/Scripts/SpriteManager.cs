using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public static SpriteManager Instance;

    private const string backgroundsPath = "Backgrounds/";
    private const string tilesPath = "Tiles/";
    private const string tutorialsPath = "Tutorials/";
    private const string uiPath = "UI/";

    private static IDictionary<string, Sprite> spriteMap;
    private static IReadOnlyDictionary<string, string> spriteNamePathMap = new Dictionary<string, string>()
    {
        { $"{SpriteNames.Background}Day", backgroundsPath + $"{SpriteNames.Background}Day" },
        { $"{SpriteNames.Background}Night", backgroundsPath + $"{SpriteNames.Background}Night" },
        { $"{nameof(Tile)}", tilesPath + nameof(Tile) },
        { $"{SpriteNames.Ice}{1}", tilesPath + $"{SpriteNames.Ice}{1}" },
        { $"{SpriteNames.Ice}{2}", tilesPath + $"{SpriteNames.Ice}{2}" },
        { $"{SpriteNames.Ice}{3}", tilesPath + $"{SpriteNames.Ice}{3}" },
        { $"{SpriteNames.Finish}", tilesPath + $"{SpriteNames.Finish}" },
    };
    private static IReadOnlyList<string> tutorials = new List<string>()
    {
        nameof(Finish),
        nameof(Ice),
        nameof(Paint),
        nameof(SplashCross),
        nameof(SplashSquare),
        "Move",
        nameof(Objectives),
        nameof(Warp),
    };

    private string[] multiSpritePaths = new string[]
    {
        uiPath + SpriteNames.Icons,
        uiPath + SpriteNames.UI,
    };

    void Awake()
    {
        Instance = this;

        if (spriteMap != null) return;

        spriteMap = new Dictionary<string, Sprite>();

        foreach (string spriteName in spriteNamePathMap.Keys)
        {
            string path = spriteNamePathMap[spriteName];

            spriteMap.Add(spriteName, Resources.Load<Sprite>(path));
        }

        foreach (string spriteName in multiSpritePaths)
        {
            Sprite[] sprites = Resources.LoadAll<Sprite>(spriteName);

            foreach (Sprite sprite in sprites)
            {
                // todo - remove images?
                if (sprite.name.Contains("Icons_")) continue;

                spriteMap.Add(sprite.name, sprite);
            }
        }

        foreach (string tutorial in tutorials)
        {
            SystemLanguage gameLanguage = Application.systemLanguage.GetGameLanguage();
            string path = $"{tutorialsPath}{tutorial}_{gameLanguage}";
            
            spriteMap.Add($"{tutorial}Tutorial", Resources.Load<Sprite>(path));
        }
    }

    public Sprite GetSprite(string name)
    {
        return spriteMap[name];
    }
}

public class SpriteNames
{
    // sprites
    public const string Ice = nameof(Ice);
    public const string Finish = nameof(Finish);
    public const string Background = nameof(Background);
    // multi-sprites
    public const string Icons = nameof(Icons);
    public const string UI = nameof(UI);

    public const string Cancel = nameof(Cancel);
    public const string Restart = nameof(Restart);
    public const string Video = nameof(Video);
}
