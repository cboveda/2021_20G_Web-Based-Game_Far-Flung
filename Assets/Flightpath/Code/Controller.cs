using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flightpath
{
    public class Controller : MonoBehaviour
    {


        private GameObject _dynamicObjects;
        [SerializeField]
        private FlightpathPathBuilder _pathBuilder;
        [SerializeField]
        private FlightpathObjectFactory _objectFactory;
        [SerializeField]
        private PathScriptableObject _earthPath;
        [SerializeField]
        private PathScriptableObject _marsPath;
        [SerializeField]
        private PathScriptableObject _asteroidPath;
        [SerializeField]
        private GameObject _eventSystem;
        [SerializeField]
        private GameObject _arrow;
        [SerializeField]
        private GameObject _satellitePathDrawing;
        private GameObject _sun;
        private GameObject _earth;
        private GameObject _mars;
        private GameObject _asteroid;
        private GameObject _satellite;

        // Start is called before the first frame update
        void Start()
        {

            // Setup Dynamic Objects Container
            _dynamicObjects = new GameObject();
            _dynamicObjects.name = "Dynamic Objects";

            // Sun
            _sun = _objectFactory.GetFlightpathObject(FlightpathObjectFactory.BuildableObjects.Sun, _dynamicObjects.transform);

            // Earth
            _earth = _objectFactory.GetFlightpathObject(FlightpathObjectFactory.BuildableObjects.Earth, _dynamicObjects.transform);
            _earth.GetComponent<PathFollower>().Path = _pathBuilder.GetPath(_earthPath, _dynamicObjects.transform);

            // Mars
            _mars = _objectFactory.GetFlightpathObject(FlightpathObjectFactory.BuildableObjects.Mars, _dynamicObjects.transform);
            _mars.GetComponent<PathFollower>().Path = _pathBuilder.GetPath(_marsPath, _dynamicObjects.transform);

            // Asteroid
            _asteroid = _objectFactory.GetFlightpathObject(FlightpathObjectFactory.BuildableObjects.Asteroid, _dynamicObjects.transform);
            _asteroid.GetComponent<PathFollower>().Path = _pathBuilder.GetPath(_asteroidPath, _dynamicObjects.transform);

            // Satellite
            _satellite = _objectFactory.GetFlightpathObject(FlightpathObjectFactory.BuildableObjects.Satellite, _dynamicObjects.transform);
            //      TODO STILL, IMPLEMENT SCRIPTABLEOBJECTS TO DECOUPLE ALL OF THIS:
            _satellite.GetComponent<SatelliteCollision>().EventSystem = _eventSystem;
            _eventSystem.GetComponent<LaunchManager>().Satellite = _satellite;
            _arrow.GetComponent<Trajectory>().Satellite = _satellite.GetComponent<Launch>();
            _satellitePathDrawing.GetComponent<SatellitePathDrawing>().Satellite = _satellite;
        }
    }
}