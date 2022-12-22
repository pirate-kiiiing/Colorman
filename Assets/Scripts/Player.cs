using UnityEngine;

public class Player : MonoBehaviour
{
    public const float MOVE_SPEED = 16f;
    public const float ANIMATION_SPEED = 1.75f;
    private const float ANIMATION_VICTORY_SPEED = 0.5f;
    private const float ANIMATION_DEAD_SPEED = 0.5f;

    public static Player Instance { get; set; }

    public RuntimeAnimatorController AliveAnimatorController;
    public RuntimeAnimatorController VictoryAnimatorController;
    public RuntimeAnimatorController DeadAnimatorController;

    private Color color;
    public Color Color
    {
        get { return color; }
        set
        {
            if (color == value) return;

            color = value;
            spriteRenderer.color = color.ToUnityColor();
        }
    }

    [HideInInspector] // The node the player's currently at
    public Node CurNode { get; set; }
    [HideInInspector]
    public Node TargetNode;
    private PlayerState state;
    [HideInInspector]
    public PlayerState State
    {
        get => state;
        set 
        {
            if (state == value) return;
            
            state = value;
            
            if (Test.Instance != null)
            {
                //Test.Instance.Text = state.ToString();
            }
        }
    }
    [HideInInspector]
    public PlayerDirection Direction;

    private Animator animator;
    private Vector2 moveDirection;
    private SpriteRenderer spriteRenderer;

    private byte _moves;
    private byte moves
    {
        get => _moves;
        set
        {
            if (_moves == value) return;

            _moves = value;
            
            Moves.Instance.SetMoveCount(_moves);
        }
    }

    void Awake()
    {
        Validate();

        Instance = this;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        transform.name = nameof(Player);
        animator.speed = 0f;
        
        State = PlayerState.Idle;
        moveDirection = Vector2.zero;
        TargetNode = null;
    }

    public void Init()
    {
        Vector2Int startingPoint = LevelManager.Instance._playerStartingPoint;
        moves = LevelManager.Instance.Moves;
        Direction = LevelManager.Instance._playerStartingDirection;

        Place(startingPoint);

        Node startingNode = Board.Instance.GetNode(startingPoint);
        
        if (startingNode is Tile tile)
        {
            tile.SetColor(Color);
        }
        else if (startingNode is Paint paint)
        {
            Color = paint.Color;
        }
        else if (startingNode is Ice ice)
        {
            ice.ApplyMove();
        }
    }

    void Update()
    {
        if (IsMoving() == false) return;
        if (Ice.Destroyed != null) Ice.Destroyed.Destroy();
        
        transform.position =
            Vector2.MoveTowards(
                transform.position,
                TargetNode.Point,
                Time.deltaTime * MOVE_SPEED);

        Vector3 pointPosition = transform.GetDeltaPosition(moveDirection);
        Vector2Int curPoint = Board.Instance.GetPoint(pointPosition);

        if (curPoint != CurNode.Point)
        {
            CurNode = Board.Instance.GetNode(curPoint);
            CurNode.ApplyMove();
            
            GameManager.Instance.UpdateGameState();
        }

        if (GameManager.Instance.IsStageComplete == true)
        {
            // Finish moving is already called in case of Warp
            if (CurNode is Warp == false)
            {
                FinishMoving(CurNode);
            }
#if UNITY_ANDROID || UNITY_IOS        
            Handheld.Vibrate();
#endif
        }
        else if (CurNode is Warp == false 
            && TargetNode != null 
            && CurNode == TargetNode)
        {
            FinishMoving(TargetNode);
        }
    }

    public void Place(Vector2Int point)
    {
        Vector2 position = Board.Instance.VectorToWorld(point);

        position += new Vector2(Constants.POSITION_OFFSET, Constants.POSITION_OFFSET);

        transform.position = position;

        CurNode = Board.Instance.GetNode(point);

        if (Direction == PlayerDirection.Right)
        {
            spriteRenderer.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else if (Direction == PlayerDirection.Left)
        {
            spriteRenderer.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
    }

    public void Place(Node node)
    {
        Place(node.Point);
    }

    public void MoveTo(Node node)
    {
        //Debug.Log($"{nameof(Player)} {nameof(MoveTo)}: " + node.transform.name);

        State = PlayerState.Moving;
        TargetNode = node;
        moveDirection = node.Point - CurNode.Point;
        animator.speed = ANIMATION_SPEED;

        if (moveDirection.x > 0)
        {
            spriteRenderer.transform.rotation = new Quaternion(0, 0, 0, 0);
            Direction = PlayerDirection.Right;
        }
        else if (moveDirection.x < 0)
        {
            spriteRenderer.transform.rotation = new Quaternion(0, 180, 0, 0);
            Direction = PlayerDirection.Left;
        } // up
        else if (moveDirection.y > 0)
        {
            if (Direction == PlayerDirection.Up) return;
            else if (Direction == PlayerDirection.Down) 
                spriteRenderer.transform.Rotate(0, 180, 0);
            else
                spriteRenderer.transform.Rotate(0, 0, 90);

            Direction = PlayerDirection.Up;
        } // down
        else if (moveDirection.y < 0)
        {
            if (Direction == PlayerDirection.Down) return;
            else if (Direction == PlayerDirection.Up)
                spriteRenderer.transform.Rotate(0, 180, 0);
            else
                spriteRenderer.transform.Rotate(0, 0, -90);

            Direction = PlayerDirection.Down;
        }
    }

    public bool CanMoveTo(Node node)
    {
        if (GameManager.Instance.IsStageComplete == true) return false;
        if (CurNode.Point == node.Point) return false;
        if (CurNode.Point.x != node.Point.x &&
            CurNode.Point.y != node.Point.y) return false;
        if (CurNode.HasObstacleOnTheWay(node) == true) return false;

        return true;
    }

    public bool IsMoving() => 
        TargetNode != null &&
        State >= PlayerState.Moving;

    public void FinishMoving(Node targetNode)
    {
        State = PlayerState.Idle;
        CurNode = targetNode;
        this.TargetNode = null;
        moveDirection = Vector2Int.zero;
        ResetAnimation();

        Place(CurNode);

        moves--;

        if (GameManager.Instance.IsStageComplete == true)
        {
            animator.runtimeAnimatorController = VictoryAnimatorController;
            animator.speed = ANIMATION_VICTORY_SPEED;
        }
        else if (moves == 0)
        {
            Kill();
            GameManager.Instance.ShowOutOfMoves(show: true);
        }
    }

    public void Revive()
    {
        animator.runtimeAnimatorController = AliveAnimatorController;
        animator.speed = 0f;
        moves += GameManager.BonusMoves;
    }

    public void Show(bool show)
    {
        if (show == true)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            State = PlayerState.Idle;
        }
        else
        {
            gameObject.transform.localScale = new Vector3(0, 0, 0);
            State = PlayerState.Hidden;
        }
    }

    private void Validate()
    {
        
    }

    private void Kill()
    {
        animator.runtimeAnimatorController = DeadAnimatorController;
        animator.speed = ANIMATION_DEAD_SPEED;
    }

    private void ResetAnimation()
    {
        animator.Play(nameof(Player), -1, 0f);
        animator.speed = 0f;
    }
}

public enum PlayerState
{
    Idle = 0,
    Hidden,
    Moving,
}

public enum PlayerDirection
{
    Up = 0,
    Right = 1,
    Down = 2,
    Left = 3,
}