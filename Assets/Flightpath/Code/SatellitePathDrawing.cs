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
        public LinkedList<GameObject> PathHistory;

        public void Start()
        {
            _timer = 0;
            PathHistory = new LinkedList<GameObject>();
        }

        void Update()
        {
            if (Active)
            {
                _timer += Time.deltaTime;
                if (_timer >= TimeInterval && PathPrefab != null)
                {
                    PathHistory.AddLast(Instantiate(PathPrefab,
                        new Vector3(Satellite.transform.position.x, Satellite.transform.position.y, 0),
                        Quaternion.identity,
                        transform.gameObject.transform));
                    _timer = 0;
                }
            }
        }

        public void ClearHistory()
        {
            while (PathHistory.First != null)
            {
                GameObject node = PathHistory.First.Value;
                node.GetComponent<SatellitePathFader>().BeginFade(1);
                PathHistory.RemoveFirst();
            }
        }
    }
}
