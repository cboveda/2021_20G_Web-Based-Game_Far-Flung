using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugMenu : MonoBehaviour
{

    public Canvas debugCanvas;
    public RectTransform menuPosition;
    public GameObject buttonPrefab;
    private const string BUTTON_PREFAB_PATH = "Prefabs/DebugButtonPF";
    private float canvasHeight;
    private const float widthOffset = 80.0f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        buttonPrefab = Resources.Load<GameObject>(BUTTON_PREFAB_PATH);
        
        debugCanvas = gameObject.AddComponent<Canvas>();
        debugCanvas.name = "DebugMenu";
        debugCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasHeight = debugCanvas.pixelRect.height;
        GetNewUIButton("Assembly", 1).transform.parent = this.transform;
        //menuPosition = debugCanvas.gameObject.AddComponent<RectTransform>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject GetNewUIButton(string displayName, int buttonNumber)
    {
        GameObject goButton = Instantiate<GameObject>(buttonPrefab);
        goButton.name = displayName;
        goButton.GetComponentInChildren<Text>().text = displayName;
        Vector3 buttonLocation = new Vector3(widthOffset - 50.0f, canvasHeight - 30.0f * buttonNumber, 0);
        goButton.GetComponent<RectTransform>().SetPositionAndRotation(buttonLocation, Quaternion.identity);
        

        
        

        return goButton;
    }
}
