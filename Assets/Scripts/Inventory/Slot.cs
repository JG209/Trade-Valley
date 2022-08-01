using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using TradeValley.Items;
using TradeValley.UI;

namespace TradeValley.Inventorys
{
    public class Slot : MonoBehaviour, IPointerClickHandler, IClickable
    {
        private ObservableStack<Item> items = new ObservableStack<Item>();
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text stackSize;

        public bool IsEmpty{ get => items.Count == 0;}

        public Item MyItem
        {
            get
            {
                if(!IsEmpty)
                {
                    return items.Peek();
                }

                return null;
            }
        }
        public Image MyIcon
        {
            get => icon;

            set{ icon = value; }
        }

        public int MyCount
        {
            get => items.Count;
        }
        public TMP_Text MyStackText
        {
            get => stackSize;
        }
        /// <summary>
        /// Indicates if the slot is full
        /// </summary>
        public bool IsFull
        {
            get
            {
                if (IsEmpty || MyCount < MyItem.MyStackSize)
                    return false;

                return true;
            }
        }

        private void Awake()
        {
            items.OnPop += new UpdateStackEvent(UpdateSlot);
            items.OnPush += new UpdateStackEvent(UpdateSlot);
            items.OnClear += new UpdateStackEvent(UpdateSlot);
        }

        /// <summary>
        /// Adds an item to the slot
        /// </summary>
        /// <param name="item">the item to add</param>
        /// <returns>returns true if the item was added</returns>
        public bool AddItem(Item item)
        {
            items.Push(item);
            icon.sprite = item.MyIcon;
            icon.color = Color.white;
            item.MySlot = this;
            return true;
        }

        /// <summary>
        /// Adds a stack of items to the slot
        /// </summary>
        /// <param name="newItems">stack to add</param>
        /// <returns></returns>
        public bool AddItems(ObservableStack<Item> newItems)
        {
            if (IsEmpty || newItems.Peek().GetType() == MyItem.GetType())
            {
                int count = newItems.Count;

                for (int i = 0; i < count; i++)
                {
                    if (IsFull)
                    {
                        return false;
                    }

                    AddItem(newItems.Pop());
                }

                return true;
            }

            return false;
        }

        public void RemoveItem(Item item)
        {
            if(!IsEmpty)
            {
                items.Pop();
            }
        }

        public void Clear()
        {
            int initCount = items.Count;
            if (initCount > 0)
            {
                items.Clear();
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(eventData.button == PointerEventData.InputButton.Left)
            {
                if(Inventory.MyInstance.FromSlot == null && !IsEmpty)
                {
                    HandScript.MyInstance.TakeMoveable(MyItem as IMoveable);
                    Inventory.MyInstance.FromSlot = this;
                }
                else if (Inventory.MyInstance.FromSlot == null && IsEmpty)
                {
                    if (HandScript.MyInstance.MyMoveable is Armor)
                    {
                        Armor armor = (Armor)HandScript.MyInstance.MyMoveable;
                        CharacterPanel.MyInstance.MySelectedButton.DequipArmor();
                        AddItem(armor);
                        HandScript.MyInstance.Drop();
                    }
                }
                else if(Inventory.MyInstance.FromSlot != null)
                {
                    if(PutItemBack() || MergeItems(Inventory.MyInstance.FromSlot) || SwapItems(Inventory.MyInstance.FromSlot) || AddItems(Inventory.MyInstance.FromSlot.items))
                    {
                        HandScript.MyInstance.Drop();
                        Inventory.MyInstance.FromSlot = null;
                    }
                }
            }
            
            

            if(eventData.button == PointerEventData.InputButton.Right && HandScript.MyInstance.MyMoveable == null)
            {
                UseItem();
            }
        }

        public void UseItem()
        {
            if(MyItem is IUseable)
                (MyItem as IUseable).Use();
            else if(MyItem is Armor)
                (MyItem as Armor).Equip();
            
        }
        public bool StackItem(Item item)
        {
            if (!IsEmpty && item.name == MyItem.name && items.Count < MyItem.MyStackSize)
            {
                items.Push(item);
                item.MySlot = this;
                return true;
            }

            return false;
        }
        private bool PutItemBack()
        {
            // MyCover.enabled = false;
            if (Inventory.MyInstance.FromSlot == this)
            {
                Inventory.MyInstance.FromSlot.MyIcon.color = Color.white;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Swaps two items in the inventory
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        private bool SwapItems(Slot from)
        {
            if (IsEmpty)
            {
                return false;
            }
            if (from.MyItem.GetType() != MyItem.GetType() || from.MyCount+MyCount > MyItem.MyStackSize)
            {
                //Copy all the items we need to swap from A
                ObservableStack<Item> tmpFrom = new ObservableStack<Item>(from.items);

                //Clear Slot a
                from.items.Clear();
                //All items from slot b and copy them into A
                from.AddItems(items);

                //Clear B
                items.Clear();
                //Move the items from ACopy to B
                AddItems(tmpFrom);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Merges two identical stacks of items
        /// </summary>
        /// <param name="from">Slot to merge from</param>
        /// <returns></returns>
        private bool MergeItems(Slot from)
        {
            if (IsEmpty)
            {
                return false;
            }
            if (from.MyItem.GetType() == MyItem.GetType() && !IsFull )
            {
                //How many free slots do we have in the stack
                int free = MyItem.MyStackSize - MyCount;

                for (int i = 0; i < free; i++)
                {
                    AddItem(from.items.Pop());
                }

                return true;
            }

            return false;
        }

        private void UpdateSlot()
        {
            UIManager.MyInstance.UpdateStackSize(this);
        }
        
    }
}
