using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TradeValley.Inventorys;
using TradeValley.Items;
using TradeValley.Character;

namespace TradeValley.UI
{
    public class HandScript : MonoBehaviour
    {
        /// <summary>
        /// Singleton instance of the handscript
        /// </summary>
        private static HandScript instance;

        public static HandScript MyInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<HandScript>();
                }

                return instance;
            }
        }

        /// <summary>
        /// The current moveable
        /// </summary>
        public IMoveable MyMoveable { get; set; }

        /// <summary>
        /// The icon of the item, that we acre moving around atm.
        /// </summary>
        private Image icon;

        /// <summary>
        /// An offset to move the icon away from the mouse
        /// </summary>
        [SerializeField]
        private Vector3 offset;

        // Use this for initialization
        void Start ()
        {
            //Creates a reference to the image on the hand
            icon = GetComponent<Image>();	
        }
        
        // Update is called once per frame
        void Update ()
        {
            //Makes sure that the icon follows the hand
            icon.transform.position = Input.mousePosition+offset;

            if (Input.GetMouseButton(0) &&  MyInstance.MyMoveable != null)
            {
                if(!EventSystem.current.IsPointerOverGameObject())
                    DeleteItem();
                
                // Debug.Log("AAAA");
            }

        
        }
        

        /// <summary>
        /// Take a moveable in the hand, so that we can move it around
        /// </summary>
        /// <param name="moveable">The moveable to pick up</param>
        public void TakeMoveable(IMoveable moveable)
        {
            this.MyMoveable = moveable;
            icon.sprite = moveable.MyIcon;
            icon.enabled = true;
        }

        public IMoveable Put()
        {
            IMoveable tmp = MyMoveable;
            MyMoveable = null;
            icon.enabled = false;
            return tmp;
        }

        public void Drop()
        {
            MyMoveable = null;
            icon.enabled = false;
            Inventory.MyInstance.FromSlot = null;
        }

        /// <summary>
        /// Deletes an item from the inventory
        /// </summary>
        public void DeleteItem()
        {
            if (MyMoveable is Item)
            {
                Item item = (Item)MyMoveable;
                if (item.MySlot != null)
                {
                    item.MySlot.Clear();
                }
                else if (item.MyCharButton != null)
                {
                    item.MyCharButton.DequipArmor();
                }
            }

            Drop();

            Inventory.MyInstance.FromSlot = null;
        }
    }
}
