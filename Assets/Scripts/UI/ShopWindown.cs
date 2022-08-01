using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TradeValley.Buttons;

namespace TradeValley.UI
{
    public class ShopWindown : MonoBehaviour
    {
        private CanvasGroup canvasGroup;

        [SerializeField] ShopButton[] shopButtons;

        void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        public void CreatePage(ShopItem[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                shopButtons[i].AddItem(items[i]);
            }
        }
        public void Open()
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
        }
        public void Close()
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
        }
    }
}
