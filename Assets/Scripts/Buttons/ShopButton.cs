using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TradeValley.Character;
using TradeValley.Inventorys;
using TradeValley.UI;

namespace TradeValley.Buttons
{
    public class ShopButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
    [SerializeField] private Image icon;

    [SerializeField] private TMP_Text title;

    [SerializeField] private TMP_Text price;

    [SerializeField] private TMP_Text quantity;

    private ShopItem shopItem;

    public void AddItem(ShopItem shopItem)
    {
        this.shopItem = shopItem;

        if (shopItem.MyQuantity > 0 ||(shopItem.MyQuantity == 0 && shopItem.Unlimited))
        {

            icon.sprite = shopItem.MyItem.MyIcon;
            title.text = shopItem.MyItem.MyTitle;

            if (!shopItem.Unlimited)
            {
                quantity.text = shopItem.MyQuantity.ToString();
            }
            else
            {
                quantity.text = string.Empty;
            }

            if (shopItem.MyItem.MyPrice > 0)
            {
                price.text = "Price: " + shopItem.MyItem.MyPrice.ToString();
            }
            else
            {
                price.text = string.Empty;
            }

            gameObject.SetActive(true);
        }

    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if ((Player.MyInstance.MyMoney >= shopItem.MyItem.MyPrice) && Inventory.MyInstance.AddItem(Instantiate(shopItem.MyItem)))
        {
            SellItem();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // DO SOMETHING
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // DO SOMETHING
    }

    private void SellItem()
    {
        Player.MyInstance.MyMoney -= shopItem.MyItem.MyPrice;

        if (!shopItem.Unlimited)
        {
            shopItem.MyQuantity--;
            quantity.text = shopItem.MyQuantity.ToString();

            if (shopItem.MyQuantity == 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
    }
}
