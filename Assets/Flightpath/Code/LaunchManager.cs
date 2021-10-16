using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaunchManager : MonoBehaviour
{
    public GameObject Satellite;
    public Slider AngleSlider;
    public Slider PowerSlider;
    

    public void Start()
    {
        Satellite.GetComponent<Launch>().SetAngle(AngleSlider.GetComponent<Slider>().minValue);
        Satellite.GetComponent<Launch>().SetPower(PowerSlider.GetComponent<Slider>().minValue);
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
}
