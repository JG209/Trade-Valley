using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TradeValley.Inventorys;

namespace TradeValley.UI
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager instance;

        public static UIManager MyInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<UIManager>();
                }

                return instance;
            }
        }

        
        [System.Serializable]
        public class InputManager
        {
            public bool one;
            public bool two;
            public bool three;
            public bool openClose;
            

            public void UpdateInputs()
            {
                one = Input.GetKeyDown(KeyCode.Alpha1);
                two = Input.GetKeyDown(KeyCode.Alpha2);
                three = Input.GetKeyDown(KeyCode.Alpha3);
                openClose = Input.GetKeyDown(KeyCode.I);
            }
        }
        [SerializeField] private InputManager inputs = new InputManager();

        [SerializeField] private Button[] actionButtons;
        [SerializeField] private CharacterPanel charPanel;
        public TMP_Text moneyTxt;
        
        
        void Update()
        {
            inputs.UpdateInputs();


            if(inputs.openClose)
            {
                charPanel.OpenClose();// avoid to open when is closing the inventory
                Inventory.MyInstance.OpenClose();
            }

            //NOT BEING USED
            // if(inputs.one)
            //     ActionButtonOnCLick(0);

            // if(inputs.two)
            //     ActionButtonOnCLick(1);

            // if(inputs.three)
            //     ActionButtonOnCLick(2);
        }

        // public void OpenClose(CanvasGroup canvasGroup)
        // {
        //     canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1;
        //     canvasGroup.blocksRaycasts = canvasGroup.blocksRaycasts == true ? false : true;
        // }

        private void ActionButtonOnCLick(int index)
        {
            actionButtons[index].onClick.Invoke();
        }
        public void UpdateStackSize(IClickable clickable)
        {
            if(clickable.MyCount > 1)
            {
                clickable.MyStackText.text = clickable.MyCount.ToString();
                clickable.MyStackText.enabled = true;
                clickable.MyIcon.enabled = true;
            }
            else
            {
                clickable.MyStackText.enabled = false;
                clickable.MyIcon.enabled = true;
                clickable.MyIcon.color = Color.white;
            }

            if(clickable.MyCount == 0)
            {
                clickable.MyStackText.enabled = false;
                clickable.MyIcon.enabled = false;
            }
        }
    }
}
