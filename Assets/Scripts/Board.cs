using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using TileMaps = UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public const byte WIDTH = 9;
    public const byte HEIGHT = 11;

    public static Board Instance;

    public TileMaps.Tilemap _board;

    private Node[,] nodes;
    private bool isClickable;

    void Awake()
    {
        Instance = this;
        isClickable = false;

        StartCoroutine(SlowlyAcceptClicks());
    }

    public void Init()
    {
        nodes = new Node[HEIGHT, WIDTH];

        InitBoard();

        Validate();
    }

    void Update()
    {
        if (IsClickable() == false) return;
        if (GameManager.Instance.IsStageComplete == true)
        {
            GameManager.Instance.LoadNextScene();

            return;
        }
        
        Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2Int clickPoint = GetPoint(clickPosition);

        //Debug.Log("Clicked " + clickPoint);

        if (IsValidNode(clickPoint) == false) return;

        Node targetNode = GetNode(clickPoint);

        if (Player.Instance.CanMoveTo(targetNode) == false) return;

        Player.Instance.MoveTo(targetNode);
    }

    public void ReplaceNode(Node original, Node replace)
    {
        nodes[original.Point.y, original.Point.x] = replace;
    }

    public Vector2 VectorToWorld(Vector2Int vector)
    {
        return _board.CellToWorld(vector.ToVector3());
    }

    public Node GetNode(int x, int y) => nodes[y, x];
    public Node GetNode(Vector2Int v) => GetNode(v.x, v.y);
    public bool IsValidNode(int x, int y) => x >= 0 && x < WIDTH && y >= 0 && y < HEIGHT;
    public bool IsValidNode(Vector2Int v) => IsValidNode(v.x, v.y);
    public Vector2Int GetPoint(Vector3 v) => _board.WorldToCell(v).ToVector2();
    public Vector3 GetWorldPosition(Vector2Int v) => _board.CellToWorld(v.ToVector3());

    private bool IsClickable()
    {
        if (isClickable == false) return false;
        if (Player.Instance.State == PlayerState.Hidden) return false;
        if (Player.Instance.IsMoving() == true) return false;
        if (GameManager.Instance.IsOutOfMoves() == true) return false;
        if (TutorialManager.Instance != null && TutorialManager.Instance.isActiveAndEnabled == true) return false;
        if (Input.GetMouseButtonDown(0) == false) return false;
        if (Setting.Instance.Active == true) return false;

        return true;
    }

    private IEnumerator SlowlyAcceptClicks()
    {
        for (int i = 0; i < 1; i++)
        {
            yield return new WaitForSeconds(0.5f);

            isClickable = true;
        }
    }

    private Node CreateNode(TileBase gameTile, Vector2Int point)
    {
        if (gameTile == null) return Nodes.Instance.Create(new HoleProperty(point));

        switch (gameTile.name)
        {
            case nameof(TileType.TileWhite): return Nodes.Instance.Create(new TileProperty(point, Color.White.LevelAdjustedColor()));
            case nameof(TileType.TileRed): return Nodes.Instance.Create(new TileProperty(point, Color.Red.LevelAdjustedColor()));
            case nameof(TileType.TileYellow): return Nodes.Instance.Create(new TileProperty(point, Color.Yellow.LevelAdjustedColor()));
            case nameof(TileType.TileGreen): return Nodes.Instance.Create(new TileProperty(point, Color.Green.LevelAdjustedColor()));
            case nameof(TileType.TileBlue): return Nodes.Instance.Create(new TileProperty(point, Color.Blue.LevelAdjustedColor()));

            case nameof(TileType.PaintWhite): return Nodes.Instance.Create(new PaintProperty(point, Color.White.LevelAdjustedColor()));
            case nameof(TileType.PaintRed): return Nodes.Instance.Create(new PaintProperty(point, Color.Red.LevelAdjustedColor()));
            case nameof(TileType.PaintYellow): return Nodes.Instance.Create(new PaintProperty(point, Color.Yellow.LevelAdjustedColor()));
            case nameof(TileType.PaintGreen): return Nodes.Instance.Create(new PaintProperty(point, Color.Green.LevelAdjustedColor()));
            case nameof(TileType.PaintBlue): return Nodes.Instance.Create(new PaintProperty(point, Color.Blue.LevelAdjustedColor()));

            case nameof(TileType.Wall): return Nodes.Instance.Create(new WallProperty(point));
            case nameof(TileType.Hole): return Nodes.Instance.Create(new HoleProperty(point));

            case nameof(TileType.Ice1): return Nodes.Instance.Create(new IceProperty(point, health: 1));
            case nameof(TileType.Ice2): return Nodes.Instance.Create(new IceProperty(point, health: 2));
            case nameof(TileType.Ice3): return Nodes.Instance.Create(new IceProperty(point, health: 3));

            case nameof(TileType.WarpGray): return Nodes.Instance.Create(new WarpProperty(point, Color.White, key: 1));
            case nameof(TileType.WarpRed): return Nodes.Instance.Create(new WarpProperty(point, Color.Red, key: 2));
            case nameof(TileType.WarpYellow): return Nodes.Instance.Create(new WarpProperty(point, Color.Yellow, key: 3));
            case nameof(TileType.WarpGreen): return Nodes.Instance.Create(new WarpProperty(point, Color.Green, key: 4));
            case nameof(TileType.WarpBlue): return Nodes.Instance.Create(new WarpProperty(point, Color.Blue, key: 5));

            case nameof(TileType.SplashCrossWhite): return Nodes.Instance.Create(new SplashProperty(point, NodeType.SplashCross, Color.White.LevelAdjustedColor()));
            case nameof(TileType.SplashCrossRed): return Nodes.Instance.Create(new SplashProperty(point, NodeType.SplashCross, Color.Red.LevelAdjustedColor()));
            case nameof(TileType.SplashCrossYellow): return Nodes.Instance.Create(new SplashProperty(point, NodeType.SplashCross, Color.Yellow.LevelAdjustedColor()));
            case nameof(TileType.SplashCrossGreen): return Nodes.Instance.Create(new SplashProperty(point, NodeType.SplashCross, Color.Green.LevelAdjustedColor()));
            case nameof(TileType.SplashCrossBlue): return Nodes.Instance.Create(new SplashProperty(point, NodeType.SplashCross, Color.Blue.LevelAdjustedColor()));

            case nameof(TileType.SplashSquareWhite): return Nodes.Instance.Create(new SplashProperty(point, NodeType.SplashSquare, Color.White.LevelAdjustedColor()));
            case nameof(TileType.SplashSquareRed): return Nodes.Instance.Create(new SplashProperty(point, NodeType.SplashSquare, Color.Red.LevelAdjustedColor()));
            case nameof(TileType.SplashSquareYellow): return Nodes.Instance.Create(new SplashProperty(point, NodeType.SplashSquare, Color.Yellow.LevelAdjustedColor()));
            case nameof(TileType.SplashSquareGreen): return Nodes.Instance.Create(new SplashProperty(point, NodeType.SplashSquare, Color.Green.LevelAdjustedColor()));
            case nameof(TileType.SplashSquareBlue): return Nodes.Instance.Create(new SplashProperty(point, NodeType.SplashSquare, Color.Blue.LevelAdjustedColor()));

            case nameof(TileType.Finish): return Nodes.Instance.Create(new FinishProperty(point));
        }

        throw new ArgumentException($"Invalid {nameof(gameTile)} {gameTile.name}");
    }

    private void InitBoard()
    {
        for (byte y = 0; y < HEIGHT; y++)
        {
            for (byte x = 0; x < WIDTH; x++)
            {
                var point = new Vector2Int(x, y);
                TileBase gameTile = _board.GetTile(point.ToVector3());
                Node node = CreateNode(gameTile, point);
                _board.SetTile(point.ToVector3(), null);

                nodes[y, x] = node;
            }
        }
    }

    private void Validate()
    {
        Warp.Validate();
    }
}

public static class TileTypeExtensions
{
    public static bool IsWarp(this TileType type)
    {
        return type >= TileType.Warp && type <= TileType.WarpBlue;
    }

    public static TileType GetIceType(byte health)
    {
        switch (health)
        {
            case 1: return TileType.Ice1;
            case 2: return TileType.Ice2;
            case 3: return TileType.Ice3;
        }

        throw new ArgumentException($"Invalid IceType {nameof(health)} {health}");
    }
}

public enum TileType
{
    TileWhite = 0,
    TileRed,
    TileYellow,
    TileGreen,
    TileBlue,

    PaintWhite,
    PaintRed,
    PaintYellow,
    PaintGreen,
    PaintBlue,

    Ice1,
    Ice2,
    Ice3,
    Wall,
    Hole,

    Warp,
    WarpGray,
    WarpRed,
    WarpYellow,
    WarpGreen,
    WarpBlue,

    SplashCrossWhite,
    SplashCrossRed,
    SplashCrossYellow,
    SplashCrossGreen,
    SplashCrossBlue,

    SplashSquareWhite,
    SplashSquareRed,
    SplashSquareYellow,
    SplashSquareGreen,
    SplashSquareBlue,

    FinishOpened,
    Finish,
}