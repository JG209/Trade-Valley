using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TradeValley.Character
{
    public abstract class Character : MonoBehaviour
    {
        // [Header("References")]
        protected Animator animator;
        private Rigidbody2D rb;
        protected Coroutine attackRoutine;

        // [Header("Configurations")]
        protected Vector2 moveDirection;

        [Tooltip("Player movement speed")]
        [SerializeField] private float _moveSpeed = 1f;

        private bool _isAttacking = false;
        
        /// <summary>
        /// Set the isAttacking and the animator attack parameter
        /// </summary>
        protected bool IsAttacking
        {
            get => _isAttacking;
            set {
                if(attackRoutine == null) return;

                StopCoroutine(attackRoutine);
                _isAttacking = value;
                animator.SetBool("attack", _isAttacking);
            }
        }
        public bool IsMoving
        {
            get{ return moveDirection.x != 0 || moveDirection.y != 0; }
        }


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
        /// Set the x and y paramater, and makes the animation faces the rigth side
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
                IsAttacking = false;
            }
            else if(_isAttacking)
            {
                ActivateLayer("Attack_Layer");
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

        protected virtual IEnumerator Attack()
        {
            yield return null;
        }

    }
}
