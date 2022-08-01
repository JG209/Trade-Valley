using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TradeValley.UI;
using TradeValley.Character;
using TradeValley.Items;
using TradeValley.Inventorys;

namespace TradeValley
{
    public class SellItemButton : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            if(HandScript.MyInstance.MyMoveable != null)
            {
                Item item = (Item)HandScript.MyInstance.MyMoveable;
                Player.MyInstance.MyMoney += item.MyPrice * Inventory.MyInstance.FromSlot.MyCount;
                HandScript.MyInstance.DeleteItem();
            }
        }
    }
}
