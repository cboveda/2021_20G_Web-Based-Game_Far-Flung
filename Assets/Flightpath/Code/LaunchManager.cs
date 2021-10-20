using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaunchManager : MonoBehaviour
{
    public GameObject Satellite;
    public Slider AngleSlider;
    public Slider PowerSlider;

    // Placeholder members for win/lose text
    public Text WinText;
    public Text LoseText;


    public void Start()
    {
        Satellite.GetComponent<Launch>().SetAngle(AngleSlider.GetComponent<Slider>().minValue);
        Satellite.GetComponent<Launch>().SetPower(PowerSlider.GetComponent<Slider>().minValue);

        // todo
        ResetPlaceholderText();
    }

    public void OnAngleSliderChanged(float value)
    {
        Satellite.GetComponent<Launch>().SetAngle(value);
    }

    public void OnPowerSliderChanged(float value)
    {
        Satellite.GetComponent<Launch>().SetPower(value);
    }

    public void OnLaunchButtonClicked()
    {
        Satellite.GetComponent<Launch>().DoLaunch();
        PathFollower[] pathFollowers = FindObjectsOfType<PathFollower>();
        foreach (PathFollower p in pathFollowers)
        {
            p.BeginMovement();
        }
    }

    public void OnResetButtonClicked()
    {
        Satellite.GetComponent<Launch>().ResetLaunch();
        PathFollower[] pathFollowers = FindObjectsOfType<PathFollower>();
        foreach (PathFollower p in pathFollowers)
        {
            p.ResetPosition();
        }
        ResetPlaceholderText();
    }

    public void OnAsteroidCollisionDetected()
    {
        Satellite.GetComponent<Launch>().StopLaunch();
        PathFollower[] pathFollowers = FindObjectsOfType<PathFollower>();
        foreach (PathFollower p in pathFollowers)
        {
            p.StopPosition();
        }

        // todo
        EnablePlaceholderWinText();
    }

    public void OnSatelliteLeaveWindow()
    {
        Satellite.GetComponent<Launch>().StopLaunch();
        PathFollower[] pathFollowers = FindObjectsOfType<PathFollower>();
        foreach (PathFollower p in pathFollowers)
        {
            p.StopPosition();
        }

        // todo
        LoseText.enabled = true;
    }

    private void ResetPlaceholderText()
    {
        if (WinText)
        {
            WinText.enabled = false;
        }
        if (LoseText)
        {
            LoseText.enabled = false;
        }
    }

    // Temporary methods for placeholder win/lose text
    private void EnablePlaceholderWinText()
    {
        if (WinText)
        {
            WinText.enabled = true;
        }
    }

    private void EnablePlaceholderLoseText()
    {
        if (LoseText)
        {
            LoseText.enabled = true;
        }
    }
}
