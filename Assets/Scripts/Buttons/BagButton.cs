using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TradeValley.Items;

namespace TradeValley.Buttons
{
    public class BagButton : MonoBehaviour, IPointerClickHandler
    {
        private Bag bag;
        [SerializeField] private Sprite full, empty;

        public Bag MyBag
        {
            get => bag;
            set
            {
                if(value != null)
                    GetComponent<Image>().sprite = full;
                else
                    GetComponent<Image>().sprite = empty;

                bag = value;

            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(bag == null) return;

            bag.MyBagScript.OpenClose();
        }
    }
}
