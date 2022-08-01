using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TradeValley.Character
{
    public class GearSocket : MonoBehaviour
    {
        public Animator MyAnimator { get; set; }

        protected SpriteRenderer spriteRenderer;

        private Animator parentAnimator;

        private AnimatorOverrideController animatorOverrideController;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            parentAnimator = GetComponentInParent<Animator>();
            MyAnimator = GetComponent<Animator>();

            animatorOverrideController = new AnimatorOverrideController(MyAnimator.runtimeAnimatorController);

            MyAnimator.runtimeAnimatorController = animatorOverrideController;
            
        }

        public void SetXAndY(float x, float y)
        {
            //Set animator parameters
            MyAnimator.SetFloat("x", x);
            MyAnimator.SetFloat("y", y);
        }

        public void ActivateLayer(string layerName)
        {
            for (int i = 0; i < MyAnimator.layerCount; i++)
            {
                MyAnimator.SetLayerWeight(i, 0);
            }

            MyAnimator.SetLayerWeight(MyAnimator.GetLayerIndex(layerName), 1);
        }

        public void Equip(AnimationClip[] animations)
        {
            spriteRenderer.color = Color.white;// Makes sure to show the sprite renderer
            animatorOverrideController["Attack_back"] = animations[0];
            animatorOverrideController["Attack_front"] = animations[1];
            animatorOverrideController["Attack_left"] = animations[2];
            animatorOverrideController["Attack_right"] = animations[3];

            animatorOverrideController["Idle_back"] = animations[4];
            animatorOverrideController["Idle_front"] = animations[5];
            animatorOverrideController["Idle_left"] = animations[6];
            animatorOverrideController["Idle_right"] = animations[7];

            animatorOverrideController["Walk_back"] = animations[8];
            animatorOverrideController["Walk_front"] = animations[9];
            animatorOverrideController["Walk_left"] = animations[10];
            animatorOverrideController["Walk_right"] = animations[11];
        }

        public void Dequip()
        {
            animatorOverrideController["Attack_back"] = null;
            animatorOverrideController["Attack_front"] = null;
            animatorOverrideController["Attack_left"] = null;
            animatorOverrideController["Attack_right"] = null;

            animatorOverrideController["Idle_back"] = null;
            animatorOverrideController["Idle_front"] = null;
            animatorOverrideController["Idle_left"] = null;
            animatorOverrideController["Idle_right"] = null;

            animatorOverrideController["Walk_back"] = null;
            animatorOverrideController["Walk_front"] = null;
            animatorOverrideController["Walk_left"] = null;
            animatorOverrideController["Walk_right"] = null;

            Color c = spriteRenderer.color; //Makes the sprite renderer not be showed anymore
            c.a = 0;
            spriteRenderer.color = c;
        }
        
    }
}
