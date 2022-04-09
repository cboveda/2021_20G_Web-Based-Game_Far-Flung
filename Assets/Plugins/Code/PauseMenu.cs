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
    [SerializeField] private Button openMenuButton;
    [SerializeField] private Button closeMenuButton;
    [SerializeField] private Button returnToHubButton;
    [SerializeField] private GameObject pauseMenuCanvas;
    [SerializeField] private GameObject pauseMenuButtonCanvas;
    private bool active;

    void Start()
    {
        pauseMenuCanvas.SetActive(false);
        pauseMenuButtonCanvas.SetActive(true);
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
        pauseMenuCanvas.SetActive(!pauseMenuCanvas.active);
    }

    public void returnToHub()
    {
        SceneManager.LoadScene("Hub");
    }
}
