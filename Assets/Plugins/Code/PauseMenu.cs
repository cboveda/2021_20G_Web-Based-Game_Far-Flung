using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/*
    Pause menu functinality for all scenes via a plug-and-play component.
*/
public class PauseMenu : MonoBehaviour
{
    private const int ASSY = 0;
    private const int FLIGHT = 1;
    private const int SCAN = 2;
    private const int COM = 3;
    private const int LAB = 4;
    [SerializeField] private GameObject[] instructionSet;
    [SerializeField] private Button openMenuButton;
    [SerializeField] private Button closeMenuButton;
    [SerializeField] private Button returnToHubButton;
    [SerializeField] private GameObject pauseMenuCanvas;
    [SerializeField] private GameObject pauseMenuButtonCanvas;

    void Start()
    {
        pauseMenuCanvas.SetActive(false);
        pauseMenuButtonCanvas.SetActive(true);
        foreach (GameObject g in instructionSet)
        {
            g.SetActive(false);
        }
        int target = getActiveInstructionSet();
        if (0 <= target && target < instructionSet.Length)
        {
            instructionSet[target].SetActive(true);
        }
    }

    private static int getActiveInstructionSet()
    {
        int target;
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentIndex == 1) //Assembly
        {
            target = ASSY;
        }
        else if (2 <= currentIndex && currentIndex <= 4) //Flight
        {
            target = FLIGHT;
        }
        else if (5 <= currentIndex && currentIndex <= 7) //Scanning
        {
            target = SCAN;
        }
        else if (8 <= currentIndex && currentIndex <= 13) //Com
        {
            target = COM;
        }
        else if (currentIndex == 14) //Lab
        {
            target = LAB;
        }
        else
        {
            target = -1;
        }
        return target;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            toggleMenu();
        }
    }

    public void toggleMenu()
    {
        pauseMenuCanvas.SetActive(!pauseMenuCanvas.activeInHierarchy);
    }

    public void returnToHub()
    {
        SceneManager.LoadScene("Hub");
    }
}
