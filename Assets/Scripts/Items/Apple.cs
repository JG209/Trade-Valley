using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TradeValley.Character;

namespace TradeValley.Items
{
    [CreateAssetMenu(fileName ="Apple",menuName ="Items/Apple", order =1)]
    public class Apple : Item, IUseable
    {
        //Only to be a sell item

        public void Use()
        {
            
        }
    }
}
