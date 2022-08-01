using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TradeValley.Inventorys;
using TradeValley.Items;
using TradeValley.Character;
using TradeValley.UI;
using TMPro;

namespace TradeValley
{
    public class AppleTree : MonoBehaviour
    {
        private CanvasGroup canvasGroup;
        private bool interacting = false;

        private float timer = 2;
        private bool timerOn = false;

        /// <summary>
        /// Energy to lost when collect a apple
        /// </summary>
        [SerializeField] private float energyTolost = 2f;

        [SerializeField]private TMP_Text timerTxt;

        void Start()
        {
            canvasGroup = UIManager.MyInstance.appleCollectedCanvasGroup;
            timerTxt.text = "";
        }

        void Update()
        {
            if(!timerOn) return;

            if(timer > 0)
            {
                // string tmp = Math.Round(timer,2).ToString();
                timerTxt.text = String.Format("{0:0.00}", timer);
                timer -= Time.deltaTime;
            }
            else
            {
                timerTxt.text = "";
                timerOn = false;
            }
        }
        
        void OnTriggerEnter2D(Collider2D other)
        {
            
        }

        void OnTriggerStay2D(Collider2D other)
        {
            if(other.CompareTag("Player") 
            && Input.GetKey(KeyCode.Space) 
            && !interacting 
            && Player.MyInstance.EnergyValue >= energyTolost)
            {
                interacting = true;
                StartCoroutine(nameof(Collect));

            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if(other.CompareTag("Player"))
            {
                interacting = false;
                CloseCollectWindow();
                StopAllCoroutines();
                timerOn = false;
                timerTxt.text = "";
            }
        }

        IEnumerator Collect()
        {
            timer = 2f;
            timerOn = true;
            yield return new WaitWhile(() => timerOn);

            Player.MyInstance.EnergyValue -= energyTolost;

            OpenCollectWindow();

            Inventory.MyInstance.AddItem((Apple)Instantiate(Inventory.MyInstance.items[8]));
            
            yield return new WaitForSeconds(1f);

            CloseCollectWindow();

            interacting = false;//allow to interact again
        }

        public void OpenCollectWindow()
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
        }
        public void CloseCollectWindow()
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
        }
    }
}
