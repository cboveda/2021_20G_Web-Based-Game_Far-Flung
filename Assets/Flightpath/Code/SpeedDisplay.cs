using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedDisplay : MonoBehaviour
{
    private Rigidbody satellite;
    [SerializeField]
    private Text output;

    // Start is called before the first frame update
    void Start()
    {
        satellite = GameObject.FindWithTag("FlightpathSatellite").GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        output.text = "Subject: Psyche\n" + "Speed: " + Mathf.Round(satellite.velocity.magnitude / 150 * 48923)  + " mph\n" + "Zoom: 500,000x";
    }
}
