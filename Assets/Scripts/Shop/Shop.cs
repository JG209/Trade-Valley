using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TradeValley.Character;
using TradeValley.UI;

namespace TradeValley
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private ShopItem[] items;

        public ShopItem[] MyItems { get => items; }

        [SerializeField] private ShopWindown shopWindown;
        
        public void Interact()
        {
            shopWindown.CreatePage(items);
            shopWindown.Open();
        }

        public void StopInteract()
        {
            shopWindown.Close();
        }


        void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.GetComponent<Player>() != null)
            {
                Interact();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if(other.gameObject.GetComponent<Player>() != null)
            {
                StopInteract();
            }
        }
    }
}
