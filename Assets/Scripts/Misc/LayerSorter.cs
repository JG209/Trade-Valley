using System;
//*****************************
//*****************************
//Script made by inScopeStudios
//*****************************
//*****************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TradeValley.Misc
{
    public class LayerSorter : MonoBehaviour, IComparable
    {
        /// <summary>
        /// A reference to the players spriteRenderer
        /// </summary>
        [SerializeField]private SpriteRenderer[] renderers;

        //A list of all obstacles that the player is colliding with
        private List<Obstacle> obstacles = new List<Obstacle>();

        // Use this for initialization
        void Start ()
        {
            //Creates the reference to the players spriterenderer
            // renderers = transform.parent.GetComponent<SpriteRenderer>();
        }
        
        /// <summary>
        /// When the player hits an obstacle
        /// </summary>
        /// <param name="collision"></param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Obstacle") //If we hit an obstacle
            {
                //Creates a reference to the obstacle
                Obstacle o = collision.GetComponent<Obstacle>();

                //Fades out the tree, so that we can see the player beheind it
                o.FadeOut();
                foreach (SpriteRenderer sr in renderers)
                {
                    //If we aren't colliding with anything else or we are colliding with something with a less sort order
                    if (obstacles.Count == 0 || o.MySpriteRenderer.sortingOrder -1 < sr.sortingOrder)
                    {
                        //Change the sortorder to be beheind what we just hit
                        sr.sortingOrder = o.MySpriteRenderer.sortingOrder - 1;
                    }
                    
                }

                //Adds the obstacle to the list, so that we can keep track of it
                obstacles.Add(o);
            }
            
        }

        /// <summary>
        /// When we stop colliding with an obstacle
        /// </summary>
        /// <param name="collision"></param>
        private void OnTriggerExit2D(Collider2D collision)
        {
            //If we stopped colliding with an obstacle
        if (collision.tag == "Obstacle")
            {
                //Creates a reference to the obstacle
                Obstacle o = collision.GetComponent<Obstacle>();

                //Fades in the tree so that we can't see through it
                o.FadeIn();
                //Removes the obstacle from the list
                obstacles.Remove(o);

                foreach (SpriteRenderer sr in renderers)
                {
                    //We don't have any other obstacles
                    if (obstacles.Count == 0)
                    {
                        sr.sortingOrder = 200;
                    }
                    else//We have other obstacles and we need to change the sortorder based on those obstacles.
                    {
                        CompareTo(obstacles);
                        obstacles.Sort();
                        sr.sortingOrder = obstacles[0].MySpriteRenderer.sortingOrder - 1;
                    }
                }
            
            }
        }

        public int CompareTo(object obj)
        {
            
            return 1;
        }
    }
}
