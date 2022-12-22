using System;
using UnityEngine;

public class Nodes : MonoBehaviour
{
    public static Nodes Instance;

    public Grid _grid;
    public GameObject _hole;
    public GameObject _wall;
    public GameObject _tile;
    public GameObject _paint;
    public GameObject _ice;
    public GameObject _warp;
    public GameObject _splashCross;
    public GameObject _splashSquare;
    public GameObject _finish;

    void Awake()
    {
        Instance = this;
    }

    public Node Create(NodeProperty property)
    {
        GameObject gameNode = InstantiateGameNode(property.Type);
        Node node = GetNode(gameNode, property.Type);

        node.Init(gameNode, property);

        return node;
    }

    private GameObject InstantiateGameNode(NodeType nodeType)
    {
        switch (nodeType)
        {
            case NodeType.Hole: return Instantiate(_hole, _grid.transform);
            case NodeType.Wall: return Instantiate(_wall, _grid.transform);
            case NodeType.Tile: return Instantiate(_tile, _grid.transform);
            case NodeType.Paint: return Instantiate(_paint, _grid.transform);
            case NodeType.Ice: return Instantiate(_ice, _grid.transform);
            case NodeType.Warp: return Instantiate(_warp, _grid.transform);
            case NodeType.SplashCross: return Instantiate(_splashCross, _grid.transform);
            case NodeType.SplashSquare: return Instantiate(_splashSquare, _grid.transform);
            case NodeType.Finish: return Instantiate(_finish, _grid.transform);
        }

        throw new ArgumentException($"Invalid {nameof(NodeType)} {nodeType}");
    }

    private Node GetNode(GameObject gameNode, NodeType type)
    {
        switch (type)
        {
            case NodeType.Tile: return gameNode.GetComponent<Tile>();
            case NodeType.Paint: return gameNode.GetComponent<Paint>();
            case NodeType.Wall: return gameNode.GetComponent<Wall>();
            case NodeType.Hole: return gameNode.GetComponent<Hole>();
            case NodeType.Ice: return gameNode.GetComponent<Ice>();
            case NodeType.Warp: return gameNode.GetComponent<Warp>();
            case NodeType.SplashCross: return gameNode.GetComponent<SplashCross>();
            case NodeType.SplashSquare: return gameNode.GetComponent<SplashSquare>();
            case NodeType.Finish: return gameNode.GetComponent<Finish>();
        }

        throw new ArgumentException($"Invalid {nameof(TileType)} {type}");
    }
}
