using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TradeValley.UI;

namespace TradeValley.Character
{
    public class Player : Character
    {
        #region Input Class
        [System.Serializable]
        public class InputManager
        {
            public bool up;
            public bool down;
            public bool left;
            public bool right;
            public Vector2 direction;
            public bool attack;

            public void UpdateInputs()
            {
                up = Input.GetButton("up");
                down = Input.GetButton("down");
                left = Input.GetButton("left");
                right = Input.GetButton("right");

                direction = Vector2.zero;

                if(up)
                    direction += Vector2.up;
                if(down)
                    direction += Vector2.down;
                if(left)
                    direction += Vector2.left;
                if(right)
                    direction += Vector2.right;

                attack = Input.GetButtonDown("attack");

            }
        }
        [SerializeField] private InputManager inputs = new InputManager();
        #endregion
        [SerializeField] private float maxEnergyValue = 100;
        [SerializeField] private float _energyValue;
        private float EnergyValue
        {
            get => _energyValue;
            set
            {
                if(value > maxEnergyValue)
                {
                    _energyValue = maxEnergyValue;
                }
                else if(value < 0)
                {
                    _energyValue = 0;
                }
                else
                    _energyValue = value;

                energyUI.MyCurrentValue = _energyValue;
            }
        }
        [SerializeField] private Stat energyUI;
        

        
        protected override void Awake()
        {
            base.Awake();
            _energyValue = maxEnergyValue;
            energyUI.Initialize(maxEnergyValue, maxEnergyValue);
            
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();
            HandleInputs();
            
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        void HandleInputs()
        {
            inputs.UpdateInputs();
            moveDirection = inputs.direction;
            if(inputs.attack)
                attackRoutine = StartCoroutine(Attack());
        }

        protected override IEnumerator Attack()
        {
            if(!IsAttacking && !IsMoving)
            {
                IsAttacking = true;
                EnergyValue -= 10f;
                yield return new WaitForSeconds(0.5f);
                Debug.Log("Done Attacking");
                IsAttacking = false;

            }
        }
    }
}