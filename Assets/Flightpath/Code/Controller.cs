using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Flightpath
{
    /*
        Controller class that handles in initialization of all object relationships at run time.
        Author: Chris Boveda
    */
    public class Controller : MonoBehaviour
    {
        public GameObject dynamicObjects;
        [SerializeField]
        public FlightpathPathBuilder pathBuilder;
        [SerializeField]
        public FlightpathObjectFactory objectFactory;
        [SerializeField]
        public PathScriptableObject earthPath;
        [SerializeField]
        public PathScriptableObject marsPath;
        [SerializeField]
        public PathScriptableObject asteroidPath;
        [SerializeField]
        public GameObject eventSystem;
        [SerializeField]
        public GameObject arrow;
        [SerializeField]
        public GameObject satellitePathDrawing;

        public GameObject sun;
        public GameObject earth;
        public GameObject mars;
        public GameObject asteroid;
        public GameObject satellite;

        void Awake()
        {
            Time.timeScale = 1;
            // Setup Dynamic Objects Container
            dynamicObjects = new GameObject();
            dynamicObjects.name = "Dynamic Objects";

            // Sun
            sun = objectFactory.GetFlightpathObject(FlightpathObjectFactory.BuildableObjects.Sun, dynamicObjects.transform);

            // Earth
            earth = objectFactory.GetFlightpathObject(FlightpathObjectFactory.BuildableObjects.Earth, dynamicObjects.transform);
            earth.GetComponent<PathFollower>().Path = pathBuilder.GetPath(earthPath, dynamicObjects.transform);

            // Mars
            mars = objectFactory.GetFlightpathObject(FlightpathObjectFactory.BuildableObjects.Mars, dynamicObjects.transform);
            mars.GetComponent<PathFollower>().Path = pathBuilder.GetPath(marsPath, dynamicObjects.transform);

            // Asteroid
            asteroid = objectFactory.GetFlightpathObject(FlightpathObjectFactory.BuildableObjects.Asteroid, dynamicObjects.transform);
            asteroid.GetComponent<PathFollower>().Path = pathBuilder.GetPath(asteroidPath, dynamicObjects.transform);

            // Satellite
            satellite = objectFactory.GetFlightpathObject(FlightpathObjectFactory.BuildableObjects.Satellite, dynamicObjects.transform);
            satellite.GetComponent<SatelliteCollision>().launchManager = eventSystem.GetComponent<LaunchManager>();
            eventSystem.GetComponent<LaunchManager>().Satellite = satellite;
            arrow.GetComponent<Trajectory>().Satellite = satellite.GetComponent<Launch>();
            satellitePathDrawing.GetComponent<SatellitePathDrawing>().Satellite = satellite;


        }
    }
}