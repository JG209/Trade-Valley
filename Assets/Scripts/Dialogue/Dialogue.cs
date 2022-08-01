using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TradeValley.DialogueSystem
{
    [System.Serializable]
    public class Dialogue
    {
        public string name;
        [TextArea(3, 10)]public string[] sentences;

        public UnityEvent ON_END_DIALOGUE;
    }
}
