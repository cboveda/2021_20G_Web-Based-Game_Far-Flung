using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerUIMainPane : MonoBehaviour
{
    [SerializeField] private ShellTyper shellTyper;
    private Text mainText;
    private const float TYPE_SPEED = 0.05f;
    private void Awake()
    {
        mainText = transform.Find("ComputerMainText").GetComponent<Text>();
    }

    private void Start()
    {
        shellTyper.AddTyper(mainText, "Welcome Scientist! \nPlease adjust the frequency and amplitude buttons to match the green wave to the gray one.", TYPE_SPEED);

    }

    public void DisplayComputerText(string textToDisplay)
    {
        shellTyper.AddTyper(mainText, textToDisplay, TYPE_SPEED);
    }
}
