using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaunchManager : MonoBehaviour
{
    public GameObject Satellite;
    public Slider AngleSlider;
    public Slider PowerSlider;
    public Trajectory TrajectoryArrow;

    // Placeholder members for win/lose text
    public Text WinText;
    public Text LoseText;
    private bool _sceneAdvanceStart;


    public void Start()
    {
        Satellite.GetComponent<Launch>().SetAngle(AngleSlider.GetComponent<Slider>().minValue);
        Satellite.GetComponent<Launch>().SetPower(PowerSlider.GetComponent<Slider>().minValue);
        TrajectoryArrow.GetComponent<SpriteRenderer>().enabled = true;
        TrajectoryArrow.SetPowerRange(PowerSlider.GetComponent<Slider>().minValue, PowerSlider.GetComponent<Slider>().maxValue);
        _sceneAdvanceStart = false;
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
        TrajectoryArrow.GetComponent<SpriteRenderer>().enabled = false;
        Satellite.GetComponent<Launch>().DoLaunch();
        PathFollower[] pathFollowers = FindObjectsOfType<PathFollower>();
        foreach (PathFollower p in pathFollowers)
        {
            p.BeginMovement();
            p.StartOrbitter();
        }
    }

    public void OnResetButtonClicked()
    {
        Satellite.GetComponent<Launch>().ResetLaunch();
        TrajectoryArrow.GetComponent<SpriteRenderer>().enabled = true;
        PathFollower[] pathFollowers = FindObjectsOfType<PathFollower>();
        foreach (PathFollower p in pathFollowers)
        {
            p.ResetPosition();
            p.StopOrbitter();
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
            p.StopOrbitter();
        }

        if (!_sceneAdvanceStart)
        {
            StartCoroutine("DelayedSceneAdvance");
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
            p.StopOrbitter();
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

    private IEnumerator DelayedSceneAdvance()
    {
        _sceneAdvanceStart = true;
        yield return new WaitForSeconds(3.0f);
        GetComponent<SceneControls>().Next();
    }

}
