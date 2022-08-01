using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace TradeValley.DialogueSystem
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text nameTxt;
        [SerializeField] private TMP_Text dialogueTxt;

        [SerializeField] private Animator animator;

        private Dialogue actualDialogue;
        
        private Queue<string> sentences = new Queue<string>();
        public void StartDialogue(Dialogue dialogue)
        {
            actualDialogue = dialogue;
            animator.SetBool("isOpen", true);
            nameTxt.text = actualDialogue.name;
            
            sentences.Clear();

            foreach (string sentence in actualDialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }

            GoToNextSentence();
        }

        public void GoToNextSentence()
        {
            if(sentences.Count == 0)
            {
                EndDialogue(actualDialogue.ON_END_DIALOGUE);
                return;
            }

            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }

        IEnumerator TypeSentence(string sentence)
        {
            dialogueTxt.text = "";
            foreach (char letter in sentence.ToCharArray())
            {
                dialogueTxt.text += letter;
                yield return new WaitForSeconds(0.02f);
            }
        }

        void EndDialogue()
        {
            animator.SetBool("isOpen", false);
            actualDialogue = null;
        }
        void EndDialogue(UnityEvent endEvent)
        {
            endEvent.Invoke();
            animator.SetBool("isOpen", false);
            actualDialogue = null;
        }

    }
}
