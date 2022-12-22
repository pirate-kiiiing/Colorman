using UnityEngine;

public class AnimatableNode : Node
{
    public float AnimationSpeed = 1f;

    protected Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();

        animator.speed = AnimationSpeed;
    }
}
