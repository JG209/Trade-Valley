using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TradeValley.DialogueSystem
{
    public class DialogueStarter : MonoBehaviour
    {
        [SerializeField]private Dialogue dialogue;
        public void InitiateDialogue()
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
    }
}
