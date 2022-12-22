using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTitle : MonoBehaviour
{
    private const float outsideScreenYMin = -6f;
    private const float outsideScreenYMax = 8f;
    private const float entrySpeed = 8f;
    private static Vector3 entryPosition = new Vector2(-3f, -0.5f);

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private PlayerMoveInfo curMove;
    private System.Random random;
    private Queue<PlayerMoveInfo> moves = new Queue<PlayerMoveInfo>(new PlayerMoveInfo[]
    {
        new PlayerMoveInfo
        {
            Destination = GetStartPosition(),
            StartDelay = 0.5f,
            AnimationSpeed = 0f,
            MoveSpeed = 0f,
            Direction = PlayerDirection.Right,
        },
        new PlayerMoveInfo
        {
            Destination = entryPosition,
            StartDelay = 0f,
            AnimationSpeed = 0f,
            MoveSpeed = GetMoveSpeed(entrySpeed),
            Direction = PlayerDirection.Right,
            AudioName = "ColormanEntry",
            
        },
        new PlayerMoveInfo
        {
            Destination = entryPosition,
            StartDelay = 0.5f,
            AnimationSpeed = 0f,
            MoveSpeed = GetMoveSpeed(entrySpeed),
            Direction = PlayerDirection.Left,
            AudioName = nameof(Tile),
        },
        new PlayerMoveInfo
        {
            Destination = entryPosition,
            StartDelay = 0.5f,
            AnimationSpeed = 0f,
            MoveSpeed = GetMoveSpeed(entrySpeed),
            Direction = PlayerDirection.Right,
            AudioName = nameof(Tile),
        },
        new PlayerMoveInfo
        {
            Destination = GetStartPosition(),
            StartDelay = 1f,
            AnimationSpeed = 0f,
            MoveSpeed = GetMoveSpeed(entrySpeed),
            Direction = PlayerDirection.Left,
            AudioName = "ColormanExit",
        },
        // change direction only
        new PlayerMoveInfo
        {
            Destination = GetStartPosition(),
            StartDelay = 0f,
            AnimationSpeed = 0f,
            MoveSpeed = GetMoveSpeed(entrySpeed),
            Direction = PlayerDirection.Right,
        },
        new PlayerMoveInfo
        {
            StartPosition = new Vector2(-GetOutsideScreenX(), 1.5f),
            Destination = new Vector2(GetOutsideScreenX(), 1.5f),
            StartDelay = 1f,
            AnimationSpeed = Player.ANIMATION_SPEED,
            MoveSpeed = GetMoveSpeed(Player.MOVE_SPEED),
            Direction = PlayerDirection.Right,
            ActivateTitle = true,
            AudioName = "ColormanMoving",
        }, 
    });

    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        random = new System.Random();
        curMove = null;
    }

    void Start()
    {
        animator.speed = 0f;
        transform.position = GetStartPosition();

        NextMove();
    }

    void Update()
    {
        if (curMove == null) return;

        transform.position =
            Vector2.MoveTowards(
                transform.position,
                curMove.Destination,
                Time.deltaTime * curMove.MoveSpeed);

        if (transform.position.x >= GetOutsideScreenX())
        {
            TitleManager.Instance.IsReady = true;
        }
        if (transform.position == curMove.Destination)
        {
            NextMove();
        }
    }

    private void NextMove()
    {
        curMove = null;

        PlayerMoveInfo move = moves.Dequeue();

        if (moves.Count <= 0)
        {
            moves.Enqueue(CreateNextMove(move));
        }

        StartCoroutine(NextMove(move));
    }

    private IEnumerator NextMove(PlayerMoveInfo move)
    {
        yield return new WaitForSeconds(move.StartDelay);

        curMove = move;
        animator.speed = move.AnimationSpeed;

        if (move.StartPosition != null)
        {
            transform.position = move.StartPosition.Value;
        }
        if (move.ActivateTitle == true)
        {
            yield return new WaitForSeconds(0.1f);

            TitleManager.Instance.ActivateTitle();
        }
        if (move.AudioName != null)
        {
            AudioManager.Instance.Play(move.AudioName);
        }

        if (move.Direction == PlayerDirection.Right)
        {
            spriteRenderer.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else if (move.Direction == PlayerDirection.Left)
        {
            spriteRenderer.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
    }

    private PlayerMoveInfo CreateNextMove(PlayerMoveInfo move)
    {
        float nextX = (move.Destination.x > 0)
            ? -GetOutsideScreenX() : GetOutsideScreenX();
        float nextY = (float) random.NextDouble(outsideScreenYMin, outsideScreenYMax);
        PlayerDirection nextDirection = (nextX > 0)
            ? PlayerDirection.Right : PlayerDirection.Left;
        
        return new PlayerMoveInfo
        {
            StartPosition = new Vector2(move.Destination.x, nextY),
            Destination = new Vector2(nextX, nextY),
            StartDelay = 1f,
            MoveSpeed = GetMoveSpeed(Player.MOVE_SPEED),
            AnimationSpeed = Player.ANIMATION_SPEED,
            Direction = nextDirection,
            AudioName = "ColormanMoving",
        };
    }

    private static Vector3 GetStartPosition()
    {
        float ratio = GameManager.GetAspectRatio();
        // phone
        if (ratio > 1.5f) return new Vector2(-7.5f, -0.5f);
        // tablet
        else return new Vector2(-9f, -0.5f);
    }

    private static float GetOutsideScreenX()
    {
        float ratio = GameManager.GetAspectRatio();
        // phone
        if (ratio > 1.5f) return 7.5f;
        // tablet
        else return 9f;
    }

    private static float GetMoveSpeed(float speed)
    {
        float ratio = GameManager.GetAspectRatio();
        // phone
        if (ratio > 1.5f) return speed;
        // tablet
        else return speed * 1.25f;
    }

    private class PlayerMoveInfo
    {
        public Vector3? StartPosition { get; set; }
        public Vector3 Destination { get; set; }
        public float StartDelay { get; set; }
        public float MoveSpeed { get; set; }
        public float AnimationSpeed { get; set; }
        public PlayerDirection Direction { get; set; }
        public bool ActivateTitle { get; set; }
        public string AudioName { get; set; }
    }
}
