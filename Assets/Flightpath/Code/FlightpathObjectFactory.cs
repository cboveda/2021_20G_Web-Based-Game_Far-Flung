using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Flightpath
{
    /*
        Factory class for the instantiation of prefabs.
        Author: Chris Boveda
    */
    public class FlightpathObjectFactory : MonoBehaviour
    {
        [SerializeField]
        private GameObject _sunPrefab;
        [SerializeField]
        private GameObject _earthPrefab;
        [SerializeField]
        private GameObject _marsPrefab;
        [SerializeField]
        private GameObject _asteroidPrefab;
        [SerializeField]
        private GameObject _satellitePrefab;

        public enum BuildableObjects
        {
            Sun, Earth, Mars, Asteroid, Satellite
        }

        public GameObject GetFlightpathObject(BuildableObjects target, Transform parent)
        {
            GameObject output;
            switch (target)
            {
                case BuildableObjects.Sun:
                    output = Object.Instantiate(_sunPrefab, parent);
                    output.name = "Sun";
                    break;
                case BuildableObjects.Earth:
                    output = Object.Instantiate(_earthPrefab, parent);
                    output.name = "Earth";
                    break;
                case BuildableObjects.Mars:
                    output = Object.Instantiate(_marsPrefab, parent);
                    output.name = "Mars";
                    break;
                case BuildableObjects.Asteroid:
                    output = Object.Instantiate(_asteroidPrefab, parent);
                    output.name = "Asteroid";
                    break;
                case BuildableObjects.Satellite:
                    output = Object.Instantiate(_satellitePrefab, parent);
                    output.name = "Satellite";
                    break;
                default:
                    output = null;
                    break;
            }
            return output;
        }
    }
}