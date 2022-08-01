using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TradeValley.Character;
using TradeValley.UI;
using TradeValley.DialogueSystem;

namespace TradeValley
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private ShopItem[] items;

        public ShopItem[] MyItems { get => items; }

        [SerializeField] private ShopWindown shopWindown;
        [SerializeField] private DialogueStarter dialogueStarter;

        private bool isPlayerInsedeShopArea = false;
        
        public void Interact()
        {
            if(!isPlayerInsedeShopArea) return;
            
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
                dialogueStarter.InitiateDialogue();
                isPlayerInsedeShopArea = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if(other.gameObject.GetComponent<Player>() != null)
            {
                isPlayerInsedeShopArea = false;
                StopInteract();
            }
        }
    }
}
