using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public Transform start;
    public Transform startDir;
    public Transform end;
    public Transform endDir;
    public Vector2 position;

    private void OnDrawGizmos() {
        for (float t = 0; t <= 1; t += .05f) {
            position = Mathf.Pow(1-t, 3) * start.position +
                3 * Mathf.Pow(1-t, 2) * t * startDir.position +
                3 * (1-t) * Mathf.Pow(t, 2) * endDir.position +
                Mathf.Pow(t, 3) * end.position;

            Gizmos.DrawSphere(position, 0.1f);
        }

        Gizmos.DrawLine(new Vector2(start.position.x, start.position.y), 
            new Vector2(startDir.position.x, startDir.position.y));

        Gizmos.DrawLine(new Vector2(end.position.x, end.position.y), 
            new Vector2(endDir.position.x, endDir.position.y));
    }
}
