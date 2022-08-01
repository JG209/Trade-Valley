using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TradeValley.Inventorys;

namespace TradeValley.Items
{
    [CreateAssetMenu(fileName = "Bag", menuName = "Items/Bag", order = 1)]
    public class Bag : Item , IUseable
    {
        private int slots;
        
        [SerializeField]protected GameObject bagPrefab;
        public BagScript MyBagScript {get; set;}

        public int Slots{ get => slots; }

        public void Initialize(int slotsValue)
        {
            slots = slotsValue;
        }

        public void Use()
        {
            if(Inventory.MyInstance.canAddBag)
            {
                Remove();
                MyBagScript = Instantiate(bagPrefab,Inventory.MyInstance.transform).GetComponent<BagScript>();
                MyBagScript.AddSlots(slots);

                Inventory.MyInstance.AddBag(this);
            }

        }
    }
}
