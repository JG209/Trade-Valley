using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected Vector2 moveDirection;

    [Tooltip("Player movement speed")]
    [SerializeField] private float _moveSpeed = 1f;

    public bool IsMoving{
        get{ return moveDirection.x != 0 || moveDirection.y != 0; }
    }

    private Animator animator;

    private Rigidbody2D rb;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        HandleAnimation(moveDirection);

    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    protected virtual void Move()
    {
        rb.velocity = moveDirection.normalized * _moveSpeed;
    }

    /// <summary>
    /// Makes the animation faces the rigth side
    /// </summary>
    /// <param name="direction">The direction vector</param>
    public void HandleAnimation(Vector2 direction)
    {
        //Check if the player is standing still to control his walk animation
        if(IsMoving)
        {
            ActivateLayer("Walk_Layer");
            animator.SetFloat("x", direction.x);
            animator.SetFloat("y", direction.y);
        }
        else
            ActivateLayer("Idle_Layer");


        
        
    }
    /// <summary>
    /// Deactivate all the animator layers before activating another
    /// </summary>
    /// <param name="layerName">The layer to be activated</param>
    public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < animator.layerCount; i++)
        {
            animator.SetLayerWeight(i, 0);
        }

        animator.SetLayerWeight(animator.GetLayerIndex(layerName), 1);
    } 
}
