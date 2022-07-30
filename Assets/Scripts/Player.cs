using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        }
    }
    [SerializeField] private InputManager inputs = new InputManager();
    #endregion
    

    

    
    protected override void Awake()
    {
        base.Awake();
        
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        inputs.UpdateInputs();
        moveDirection = inputs.direction;
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    
}
