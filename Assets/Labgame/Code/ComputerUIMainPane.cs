using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerUIMainPane : MonoBehaviour
{
    [SerializeField] private ShellTyper shellTyper;
    private Text mainText;
    private void Awake()
    {
        mainText = transform.Find("ComputerMainText").GetComponent<Text>();
    }

    private void Start()
    {
        mainText.text = "How dare you?";
        shellTyper.AddTyper(mainText, "How dare you do this to me?  What do you think this is a joke?  How could you?", 0.1f);

    }
}
