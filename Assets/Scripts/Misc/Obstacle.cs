//*****************************
//*****************************
//Script made by inScopeStudios
//*****************************
//*****************************
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TradeValley.Misc
{
    public class Obstacle : MonoBehaviour
    {
        /// <summary>
        /// The obstacles spriterenderer 
        /// </summary>
        public SpriteRenderer MySpriteRenderer { get; set; }

        /// <summary>
        /// Color to use the the obstacle isn't faded
        /// </summary>
        private Color defaultColor;

        /// <summary>
        /// Color to use the the obstacle is faded out
        /// </summary>
        private Color fadedColor;


        // Use this for initialization
        void Start()
        {
            //Creates a reference to the spriterendere
            MySpriteRenderer = GetComponent<SpriteRenderer>();

            //Creates the colors
            defaultColor = MySpriteRenderer.color;
            fadedColor = defaultColor;
            fadedColor.a = 0.7f;
        }

        /// <summary>
        /// Fades out the obstacle
        /// </summary>
        public void FadeOut()
        {
            MySpriteRenderer.color = fadedColor;
        }

        /// <summary>
        /// Fades in the obstacle
        /// </summary>
        public void FadeIn()
        {
            MySpriteRenderer.color = defaultColor;
        }

    }
}