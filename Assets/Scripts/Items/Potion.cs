using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TradeValley.Character;

namespace TradeValley.Items
{
    [CreateAssetMenu(fileName ="Potion",menuName ="Items/Potion", order =1)]
    public class Potion : Item, IUseable
    {
        [SerializeField] private float energy;
        public void Use()
        {
            if(Player.MyInstance.EnergyValue < Player.MyInstance.MyMaxEnergyValue)
            {
                Remove();
                Player.MyInstance.EnergyValue += energy;

            }
        }
    }
}
