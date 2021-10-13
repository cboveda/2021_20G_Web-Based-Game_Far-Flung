using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaunchManager : MonoBehaviour
{
    private Launch _satelliteLaunch;
    private Slider _angleSlider;
    private Slider _powerSlider;

    void Start()
    {
        _satelliteLaunch = GameObject.Find("Satellite").GetComponent<Launch>();
        _angleSlider = GameObject.Find("LaunchAngleSlider").GetComponent<Slider>();
        _powerSlider = GameObject.Find("LaunchPowerSlider").GetComponent<Slider>();
        _satelliteLaunch.SetAngle(_angleSlider.minValue);
        _satelliteLaunch.SetPower(_powerSlider.minValue);
    }

    public void OnAngleSliderChanged(float value)
    {
        _satelliteLaunch.SetAngle(value);
    }

    public void OnPowerSliderChanged(float value)
    {
        _satelliteLaunch.SetPower(value);
    }

    public void OnLaunchButtonClicked()
    {
        _satelliteLaunch.DoLaunch();
        PathFollower[] pathFollowers = FindObjectsOfType<PathFollower>();
        foreach (PathFollower p in pathFollowers)
        {
            p.BeginMovement();
        }
    }
}
