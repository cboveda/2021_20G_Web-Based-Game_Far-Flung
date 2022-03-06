using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Flightpath
{
    /*
        Factory class for bezier curve paths.
        Author: Chris Boveda
    */
    public class FlightpathPathBuilder : MonoBehaviour
    {
        [SerializeField]
        private GameObject _pathPrefab;
        private GameObject _output;

        void Start()
        {
            if (_output == null) 
            {
                ResetPath();
            }
        }

        public void ResetPath()  
        {
            _output = Object.Instantiate(_pathPrefab, transform);
        }

        public void SetStartPosition(Vector2 position) 
        {            
            _output.transform.GetChild(0).transform.position = position;
        }

        public void SetStartDirection(Vector2 position) 
        {
            _output.transform.GetChild(1).transform.position = position;
        }

        public void SetEndPosition(Vector2 position) 
        {
            _output.transform.GetChild(2).transform.position = position;
        }

        public void SetEndDirection(Vector2 position) 
        {
            _output.transform.GetChild(3).transform.position = position;
        }

        public GameObject GetPath(Transform parent)
        {
            if (_output == null) 
            {
                Start();
            }
            GameObject temp = _output;
            temp.transform.parent = parent;
            ResetPath();
            return temp;
        }

        public GameObject GetPath(PathScriptableObject target, Transform parent)
        {
            if (_output == null) 
            {
                Start();
            }
            SetStartPosition(target.StartPosition);
            SetStartDirection(target.StartDirection);
            SetEndPosition(target.EndPosition);
            SetEndDirection(target.EndDirection);
            return GetPath(parent);
        }
    }
}