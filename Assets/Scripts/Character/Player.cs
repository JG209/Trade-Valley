using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TradeValley.UI;
using TradeValley.Misc;

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

        private Vector3 min, max;// used to clamp the player position and avoid to get out of the map
        

        
        protected override void Awake()
        {
            base.Awake();
            _energyValue = maxEnergyValue;
            energyUI.Initialize(maxEnergyValue, maxEnergyValue);
            CameraFollow.ON_STAR_CAMERA += SetLimits; //Set the limits that the player can move
            
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
        void ClampPosition()
        {
            if(min == Vector3.zero || max == Vector3.zero){ //Avoid to run the clamp if theres no min or max position seted
                Debug.LogWarning("No min or max player position seted");
                return; 
            }

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, min.x, max.x), Mathf.Clamp(transform.position.y, min.y, max.y), transform.position.z);
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

        public void SetLimits(Vector3 min, Vector3 max)
        {
            this.min = min;
            this.max = max;
        }
    }
}