using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TradeValley.Items;
using TradeValley.Buttons;

namespace TradeValley.Inventorys
{
    public class Inventory : MonoBehaviour
    {
        private static Inventory instance;

        public static Inventory MyInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<Inventory>();
                }

                return instance;
            }
        }

        /// <summary>
        /// The slot where the item that is selected is from
        /// </summary>
        private Slot fromSlot;

        private List<Bag> bags = new List<Bag>();

        [SerializeField]private BagButton[] bagButtons;

        [SerializeField] private Item[] items;

        public bool canAddBag{ get => bags.Count < 5; }

        public Slot FromSlot
        {
            get => fromSlot;
            set
            {
                fromSlot = value;
                if(value != null)
                {
                    fromSlot.MyIcon.color =  Color.grey;
                }
            }
        }

        void Awake()
        {
            for (int i = 0; i < 4; i++)
            {
                Bag bag = (Bag)Instantiate(items[0]);
                bag.Initialize(20);
                bag.Use();
            }
            OpenClose();
        }

        void Update()
        {
            //---------DEBUG
            if(Input.GetKeyDown(KeyCode.J))
            {
                Bag bag = (Bag)Instantiate(items[0]);
                bag.Initialize(20);
                bag.Use();
            }
            if(Input.GetKeyDown(KeyCode.K))
            {
                Bag bag = (Bag)Instantiate(items[0]);
                bag.Initialize(20);
                AddItem(bag);
            }
            if(Input.GetKeyDown(KeyCode.L))
            {
                AddItem((Potion)Instantiate(items[1]));
                AddItem((Apple)Instantiate(items[8]));
            }
            if(Input.GetKeyDown(KeyCode.H))
            {            
                AddItem((Armor)Instantiate(items[2]));
                AddItem((Armor)Instantiate(items[3]));
                AddItem((Armor)Instantiate(items[4]));
                AddItem((Armor)Instantiate(items[5]));
                AddItem((Armor)Instantiate(items[7]));
            }
        }
        public void AddBag(Bag bag)
        {
            foreach (BagButton bagButton in bagButtons)
            {
                if(bagButton.MyBag == null)
                {
                    bagButton.MyBag = bag;
                    bags.Add(bag);
                    break;
                }
            }
        }

        public bool AddItem(Item item)
        {
            if(item.MyStackSize > 0)
            {
                if(PlaceInStack(item))
                {
                    return true;
                }
            }

            return PlaceInEmpty(item);
        }

        /// <summary>
        /// Places an item on an empty slot in the game
        /// </summary>
        /// <param name="item">Item we are trying to add</param>
        private bool PlaceInEmpty(Item item)
        {
            foreach (Bag bag in bags)//Checks all bags
            {
                if (bag.MyBagScript.AddItem(item)) //Tries to add the item
                {
                    return true; //It was possible to add the item
                }
            }

            return false;
        }

        /// <summary>
        /// Tries to stack an item on anothe
        /// </summary>
        /// <param name="item">Item we try to stack</param>
        /// <returns></returns>
        private bool PlaceInStack(Item item)
        {
            foreach (Bag bag in bags)//Checks all bags
            {
                foreach (Slot slots in bag.MyBagScript.MySlots) //Checks all the slots on the current bag
                {
                    if (slots.StackItem(item)) //Tries to stack the item
                    {
                        return true; //It was possible to stack the item
                    }
                }
            }

        return false; //It wasn't possible to stack the item
    }
        public void OpenClose()
        {
            bool closedBag = bags.Find(x => !x.MyBagScript.IsOpen);
            //If all bags are closed, then open all the bags
            //If all bags are open, then close all the bags
            foreach (Bag bag in bags)
            {
                if(bag.MyBagScript.IsOpen != closedBag)
                {
                    bag.MyBagScript.OpenClose();
                }
            }
        }
    }
}
