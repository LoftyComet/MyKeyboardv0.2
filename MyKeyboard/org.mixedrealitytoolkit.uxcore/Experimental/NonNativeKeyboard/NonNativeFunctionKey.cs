// Copyright (c) Mixed Reality Toolkit Contributors
// Licensed under the BSD 3-Clause

using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace MixedReality.Toolkit.UX.Experimental
{
    /// <summary>
    /// Class representing a function key in the non native keyboard
    /// </summary>
    /// <remarks>
    /// This is an experimental feature. This class is early in the cycle, it has 
    /// been labeled as experimental to indicate that it is still evolving, and 
    /// subject to change over time. Parts of the MRTK, such as this class, appear 
    /// to have a lot of value even if the details haven’t fully been fleshed out. 
    /// For these types of features, we want the community to see them and get 
    /// value out of them early enough so to provide feedback. 
    /// </remarks>
    public class NonNativeFunctionKey : NonNativeKey
    {
        /// <summary>
        /// Possible functionalities for a function key.
        /// </summary>
        public enum Function
        {
            /// <summary>
            /// No valid function key has been set.
            /// </summary>
            Undefined = 0,
            /// <summary>
            /// If SubmitOnEnter is enabled, this function key closes the keyboard. Otherwise, adds a new line. 
            /// </summary>
            Enter = 1,
            /// <summary>
            /// Adds a tab.
            /// </summary>
            Tab = 2,
            /// <summary>
            /// Switches from the symbol key section to the alpha key section.
            /// </summary>
            Alpha = 3,
            /// <summary>
            /// Switches from the alpha key section to the symbol key section.
            /// </summary>
            Symbol = 4,
            /// <summary>
            /// Moves the carat back one index.
            /// </summary>
            Previous = 5,
            /// <summary>
            /// Moves the carat forward one index.
            /// </summary>
            Next = 6,
            /// <summary>
            /// Shifts all of the NonNativeValueKeys until the next character is typed.
            /// </summary>
            Shift = 7,
            /// <summary>
            /// Shifts all of the NonNativeValueKeys until CapsLock is disabled.
            /// </summary>
            CapsLock = 8,
            /// <summary>
            /// Adds a space.
            /// </summary>
            Space = 9,
            /// <summary>
            /// Deletes the previous character, or the selected characters.
            /// </summary>
            Backspace = 10,
            /// <summary>
            /// Closes the keyboard. 
            /// </summary>
            Close = 11,
            /// <summary>
            /// Starts and ends dictation.
            /// </summary>
            Dictate = 12,
        }

        private TMP_Text textMeshProText;
        private GameObject PressableButtonPrefab;
        private GameObject PressableButton;
        private TMP_Text PressableButtonText;
        private SpriteRenderer PressableButtonIcon;
        private Vector3 PositionBias;

        /// <inheritdoc/>
        protected override void Awake()
        {
            base.Awake();
            if (textMeshProText == null)
            {
                textMeshProText = gameObject?.GetComponentInChildren<TMP_Text>();
            }

            PressableButtonPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/MyKeyboard/PressableButton.prefab");
        }

        /// <summary>
        /// A Unity event function that is called on the frame when a script is enabled just before any of the update methods are called the first time.
        /// </summary> 
        private void Start()
        {
            gameObject.GetComponent<Button>().interactable = false;
            gameObject.GetComponent<Graphic>().GetComponent<Image>().enabled=false;

            PositionBias=new Vector3(0.1f,-0.1f,-0.05f);
            PressableButton=Instantiate(PressableButtonPrefab,transform.position+PositionBias,transform.rotation);
            PressableButton.transform.localScale=new Vector3(5,4,4);

            switch(KeyFunction)
            {
                case Function.Enter:PressableButton.transform.localScale=new Vector3(10,4,4);PositionBias.x+=0.1f;break;
                // case Function.Tab:break;
                // case Function.Alpha:break;
                // case Function.Symbol:break;
                // case Function.Previous:break;
                // case Function.Next:break;
                // case Function.Shift:break;
                // case Function.CapsLock:break;
                case Function.Space:PressableButton.transform.localScale=new Vector3(50,4,4);PositionBias.x+=0.9f;break;
                // case Function.Backspace:break;
                // case Function.Close:break;
                // case Function.Dictate:break;
            }

            PressableButton.GetComponent<PressableButton>().OnClicked.AddListener(FireKey);
            PressableButtonText=PressableButton?.GetComponentInChildren<TMP_Text>();
            PressableButtonIcon=PressableButton?.GetComponentInChildren<SpriteRenderer>();
            if(textMeshProText!=null)PressableButtonText.text=textMeshProText.text;
            else
            {
                Sprite icon=null;
                foreach(Transform child in transform)
                {
                    icon=child?.GetComponent<Graphic>()?.GetComponent<Image>()?.sprite;
                    if(icon!=null)break;
                }
                PressableButtonIcon.sprite=icon;
                PressableButtonText.text="";
            } 
            
        }

        private void Update() 
        {
            PressableButton.transform.position=transform.position+PositionBias;
            PressableButton.transform.rotation = transform.rotation;
            PressableButton.transform.localScale = transform.localScale;
            if (textMeshProText!=null)textMeshProText.fontSize=0;
            if(textMeshProText!=null)PressableButtonText.text=textMeshProText.text;
            else
            {
                PressableButtonText.text="";
            } 
        }

        private void OnEnable() 
        {
            if(PressableButton!=null)PressableButton?.SetActive(true);
        }
        private void OnDisable()
        {
            if(PressableButton!=null)PressableButton?.SetActive(false);
        }

        /// <summary>
        /// The function of this key.
        /// </summary>
        [field: SerializeField, Tooltip("The function of this key.")]
        public Function KeyFunction { get; set; } = Function.Undefined;

        /// <inheritdoc/>
        protected override void FireKey()
        {
            NonNativeKeyboard.Instance.ProcessFunctionKeyPress(this);
        }
    }
}
