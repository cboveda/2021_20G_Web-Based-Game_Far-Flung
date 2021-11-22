using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flightpath
{
    public class SatellitePathDrawing : MonoBehaviour
    {

        public GameObject PathPrefab;
        public GameObject Satellite;
        public bool Active;
        public float TimeInterval;
        private float _timer;
        private LinkedList<GameObject> _pathHistory;

        public void Start()
        {
            _timer = 0;
            _pathHistory = new LinkedList<GameObject>();
        }

        void Update()
        {
            if (Active)
            {
                _timer += Time.deltaTime;
                if (_timer >= TimeInterval && PathPrefab != null)
                {
                    _pathHistory.AddLast(Instantiate(PathPrefab,
                        new Vector3(Satellite.transform.position.x, Satellite.transform.position.y, Satellite.transform.position.z - 199),
                        Quaternion.identity,
                        transform.gameObject.transform));
                    _timer = 0;
                }
            }
        }

        public void ClearHistory()
        {
            while (_pathHistory.First != null)
            {
                GameObject node = _pathHistory.First.Value;
                node.GetComponent<SatellitePathFader>().BeginFade(1);
                _pathHistory.RemoveFirst();
            }
        }
    }
}
