using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TradeValley.Inventorys;
using TradeValley.UI;
using TradeValley.Buttons;

namespace TradeValley.Items
{

    public abstract class Item : ScriptableObject, IMoveable
    {
        [SerializeField] private Sprite icon;
        [SerializeField] private string title;

        [SerializeField] private int stackSize;
        [SerializeField] private int price;

        private Slot slot;
        private CharButton charButton;
        public Sprite MyIcon { get => icon; }
        public int MyStackSize { get => stackSize; }

        public Slot MySlot
        {
            get => slot;
            set
            {
                slot = value;
            }
        }
        public string MyTitle
        {
            get => title;
            set
            {
                title = value;
            }
        }
        public int MyPrice{ get => price; }

        public CharButton MyCharButton
        {
            get => charButton;

            set
            {
                // MySlot = null;
                charButton = value;
            }
        }

        public void Remove()
        {
            if(MySlot != null)
            {
                MySlot.RemoveItem(this);
                MySlot = null;
            }
        }
    }
}
