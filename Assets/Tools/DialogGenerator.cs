using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DialogMaker
{
    public class DialogGenerator : MonoBehaviour
    {
        [SerializeField]
        public DialogScriptableObject dialogContainer;


        private Canvas dialogCanvas;
        private Image dialogBackground;
        private Image dialogPortrait;
        private Image dialogPortraitBackground;
        private Text dialogUIText;

        private GameObject goCanvasContainer;
        private GameObject goBackgroundContainer;
        private GameObject goPortraitContainer;
        private GameObject goTextContainer;
        private GameObject goButtonContainer;
        private Button btnDialogButton;

        private DialogTyper dialogTyper;

        private int dialogEntryPosition = 0;

        private const string DIALOG_BACKGROUND_PATH = "Dialog";
        private const string DIALOG_PORTRAIT_BACKGROUND_PATH = "PortraitBack";
        [SerializeField]
        private int dialogFontSize = 36;
        [SerializeField]
        private float dialogTypingSpeed = 0.05f;

        // Start is called before the first frame update
        void Start()
        {
            // If we don't have a dialog container, we can't do anything.  Assert.
            if (dialogContainer == null)
            {
                //Debug.Assert(dialogContainer == null, "Warning: A DialogScriptableObject is desired but not found.");
                Debug.LogAssertion("Warning: A DialogScriptableObject is desired but not found.");
            }

            // Create the necessary components and get ready...
            goCanvasContainer = new GameObject();
            goCanvasContainer.name = "DMCanvas";
            goCanvasContainer.transform.SetParent(this.transform);
            goCanvasContainer.layer = 5;


            dialogCanvas = goCanvasContainer.gameObject.AddComponent<Canvas>();
            dialogCanvas.transform.SetParent(goCanvasContainer.transform);
            dialogCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            dialogCanvas.gameObject.AddComponent<CanvasScaler>();
            dialogCanvas.gameObject.AddComponent<GraphicRaycaster>();

            goBackgroundContainer = new GameObject();
            goBackgroundContainer.transform.SetParent(goCanvasContainer.transform);
            goBackgroundContainer.name = "DMBackground";
            goBackgroundContainer.AddComponent<CanvasRenderer>();

            dialogBackground = goBackgroundContainer.AddComponent<Image>();
            dialogBackground.transform.SetParent(goBackgroundContainer.transform);
            dialogBackground.rectTransform.anchorMax = new Vector2(0.5f, 0);
            dialogBackground.rectTransform.anchorMin = new Vector2(0.5f, 0);
            dialogBackground.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1158);
            dialogBackground.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 366);
            dialogBackground.rectTransform.anchoredPosition = new Vector2(0, 195.6f);
            dialogBackground.sprite = Resources.Load<Sprite>(DIALOG_BACKGROUND_PATH);
            dialogBackground.type = Image.Type.Sliced;
            btnDialogButton = goBackgroundContainer.gameObject.AddComponent<Button>();
            btnDialogButton.transition = Selectable.Transition.None;
            btnDialogButton.onClick.AddListener(() => BeginPlayingDialog());


            goPortraitContainer = new GameObject();
            goPortraitContainer.transform.SetParent(goBackgroundContainer.transform);
            goPortraitContainer.name = "DMPortraitBackground";
            goPortraitContainer.AddComponent<CanvasRenderer>();

            dialogPortraitBackground = goPortraitContainer.AddComponent<Image>();
            dialogPortraitBackground.transform.SetParent(goBackgroundContainer.transform);
            dialogPortraitBackground.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            dialogPortraitBackground.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            dialogPortraitBackground.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 209);
            dialogPortraitBackground.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 326);
            dialogPortraitBackground.rectTransform.anchoredPosition = new Vector2(390.0f, 0);
            dialogPortraitBackground.sprite = Resources.Load<Sprite>(DIALOG_PORTRAIT_BACKGROUND_PATH);

            goTextContainer = new GameObject();
            goTextContainer.transform.SetParent(goBackgroundContainer.transform);
            goTextContainer.name = "DMText";
            goTextContainer.AddComponent<CanvasRenderer>();

            dialogUIText = goTextContainer.AddComponent<Text>();
            dialogUIText.transform.SetParent(goTextContainer.transform);
            dialogUIText.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            dialogUIText.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            dialogUIText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 736);
            dialogUIText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 270);
            dialogUIText.rectTransform.anchoredPosition = new Vector2(-114.0f, 0);
            dialogUIText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
            dialogUIText.fontSize = dialogFontSize;
            dialogUIText.color = new Color32(249, 160, 0, 255);

            goCanvasContainer.SetActive(false);

            dialogTyper = gameObject.AddComponent<DialogTyper>();


        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public bool BeginPlayingDialog()
        {
            if (dialogTyper.currentlyTyping)
            {
                dialogTyper.FinishTypingFaster();
            }
            else
            {
                if(dialogEntryPosition < dialogContainer.dialogs.Length)
                {
                    goCanvasContainer.SetActive(true);
                    dialogTyper.AddTyper(dialogUIText, dialogContainer.GetNextDialogMessage(dialogEntryPosition++).dialogText, dialogTypingSpeed);
                }
                else
                {
                    goCanvasContainer.SetActive(false);
                    Destroy(this);
                }
                
            }
            
            return true;
        }

        public int GetCurrentDialogPosition()
        {
            return dialogEntryPosition;
        }

        public bool AllDialogComplete()
        {
            if(dialogEntryPosition >= dialogContainer.dialogs.Length && dialogTyper.currentlyTyping == false)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }
}


