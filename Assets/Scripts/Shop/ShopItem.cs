using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TradeValley.Items;

namespace TradeValley
{
    [System.Serializable]
    public class ShopItem
    {
        [SerializeField] private Item item;

        [SerializeField] private int quantity;

        [SerializeField] private bool unlimited;

        public Item MyItem { get => item; }

        public int MyQuantity 
        {
            get => quantity;

            set { quantity = value;}
        }

        public bool Unlimited { get => unlimited; }
    }
}
