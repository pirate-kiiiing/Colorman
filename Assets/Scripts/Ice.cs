using UnityEngine;

public class Ice : Node
{
    public const byte DefaultHealth = 3;

    public static Ice Destroyed = null;

    private byte health;
    public byte Health
    {
        get { return health; }
        set
        {
            health = value;

            if (health <= 0) return;

            renderer.sprite = SpriteManager.Instance.GetSprite(nameof(Ice) + health);
        }
    }

    private new SpriteRenderer renderer;

    public override void Init(GameObject gameObject, NodeProperty property)
    {
        base.Init(gameObject, property);

        renderer = GetComponent<SpriteRenderer>();
        IceProperty iceProperty = property as IceProperty;

        Health = iceProperty.Health;
    }

    public override void ApplyMove()
    {
        AudioManager.Instance.Play(nameof(Ice));

        if (--Health > 0) return;

        Destroyed = this;
    }

    public void Destroy()
    {
        //AudioManager.Instance.Play(nameof(Ice) + "Break");

        Objectives.Instance.IncrementIce();

        var holeProperty = new HoleProperty(Point);
        Node hole = Nodes.Instance.Create(holeProperty);

        Board.Instance.ReplaceNode(this, hole);

        Destroy(gameObject);
    }
}
