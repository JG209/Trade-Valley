using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TradeValley.Items;

namespace TradeValley.Inventorys
{
    public class BagScript : MonoBehaviour
    {
        [SerializeField] private GameObject slotPrefab;
        private CanvasGroup canvasGroup;

        private List<Slot> slots = new List<Slot>();

        public bool IsOpen
        {
            get => canvasGroup.alpha > 0;
        }

        public List<Slot> MySlots { get => slots;}

        void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void AddSlots(int slotCount)
        {
            for (int i = 0; i < slotCount; i++)
            {
                Slot slot = Instantiate(slotPrefab, transform).GetComponent<Slot>();
                slots.Add(slot);
            }
        }

        public bool AddItem(Item item)
        {
            foreach (Slot slot in slots)
            {
                if(slot.IsEmpty)
                {
                    slot.AddItem(item);
                    return true;
                }
            }
            return false;
        }

        public void OpenClose()
        {
            canvasGroup.alpha = (canvasGroup.alpha > 0) ? 0 : 1;
            canvasGroup.blocksRaycasts = (canvasGroup.blocksRaycasts == true) ? false : true;
        }

        
    }
}
