using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TradeValley.UI
{
    public class Stat : MonoBehaviour
    {
        private Image content;
        [SerializeField]private TMP_Text statValueTXT;
        [SerializeField] float lerpSpeed = 0.5f;
        private float currentFill;
        public float MyMaxValue {get; set;}

        private float currentValue;

        public float MyCurrentValue
        {
            get => currentValue;
            set
            {
                // if(value > MyMaxValue)
                // {
                //     currentValue = MyMaxValue;
                // }
                // else if(value < 0)
                // {
                //     currentValue = 0;
                // }
                // else
                //     currentValue = value;

                currentValue = value;
                
                currentFill = currentValue/MyMaxValue;
                
                statValueTXT.text = $"{currentValue}/{MyMaxValue}";
            }

        }

        void Start()
        {
            content = GetComponent<Image>();
        }

        void Update()
        {
            if(currentFill != content.fillAmount)
            {
                content.fillAmount = Mathf.Lerp(content.fillAmount, currentFill, Time.deltaTime * lerpSpeed);
            }
        }

        public void Initialize(float maxValue, float currentValue)
        {
            MyMaxValue = maxValue;
            MyCurrentValue = currentValue;
        }
    }
}