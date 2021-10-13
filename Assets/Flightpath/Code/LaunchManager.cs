using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaunchManager : MonoBehaviour
{
    public void OnAngleSliderChanged(float value) {
        GameObject satellite = GameObject.Find("Satellite");
        var launch = satellite.GetComponent<Launch>();
        launch.setAngle(value);
    }

    public void OnPowerSliderChanged(float value) {
        GameObject satellite = GameObject.Find("Satellite");
        var launch = satellite.GetComponent<Launch>();
        launch.setPower(value);
    }

    public void OnLaunchButtonClicked() {
        GameObject satellite = GameObject.Find("Satellite");
        var launch = satellite.GetComponent<Launch>();
        launch.doLaunch();
    }

}
