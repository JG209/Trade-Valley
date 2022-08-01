using System;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TradeValley.Items;
using TradeValley.Buttons;

namespace TradeValley.UI
{
    public class CharacterPanel : MonoBehaviour
    {
        private static CharacterPanel instance;
        public static CharacterPanel MyInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = GameObject.FindObjectOfType<CharacterPanel>();
                }

                return instance;
            }
        }
        [SerializeField]private CanvasGroup canvasGroup;

        [SerializeField] private CharButton head, shoulders, chest, legs, feet;

        public CharButton MySelectedButton { get; set; }
        void Start()
        {
        
        }

        void Update()
        {
        
        }

        public void OpenClose()
        {
            if(canvasGroup.alpha <= 0)
            {
                canvasGroup.blocksRaycasts = true;
                canvasGroup.alpha = 1;
            }
            else
            {
                canvasGroup.blocksRaycasts = false;
                canvasGroup.alpha = 0;
            }
        }

        public void EquipArmor(Armor armor)
        {
            switch (armor.MyArmorType)
            {
                case ArmorType.Head:
                    head.EquipArmor(armor);
                    break;
                case ArmorType.Shoulders:
                    shoulders.EquipArmor(armor);
                    break;
                case ArmorType.Chest:
                    chest.EquipArmor(armor);
                    break;
                case ArmorType.Legs:
                    legs.EquipArmor(armor);
                    break;
                case ArmorType.Feet:
                    feet.EquipArmor(armor);
                    break;
            }
        }

    }
}
