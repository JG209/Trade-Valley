using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TradeValley.UI;

namespace TradeValley.Items
{
    enum ArmorType {Head, Shoulders, Chest, Legs ,Feet }

    [CreateAssetMenu(fileName = "Armor", menuName = "Items/Armor", order = 2)]
    public class Armor : Item
    {
        [SerializeField]
        private ArmorType armorType;

        [SerializeField]
        private AnimationClip[] animationClips;

        [SerializeField]
        private Sprite visual;

        internal ArmorType MyArmorType
        {
            get
            {
                return armorType;
            }
        }

        public AnimationClip[] MyAnimationClips
        {
            get
            {
                return animationClips;
            }
        }

        public Sprite Visual { get => visual; set => visual = value; }

        public void Equip()
        {
            CharacterPanel.MyInstance.EquipArmor(this);
        }
    }
}
