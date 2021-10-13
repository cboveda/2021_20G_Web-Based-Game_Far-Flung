using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public Transform Start;
    public Transform StartDirection;
    public Transform End;
    public Transform EndDirection;
    public float LineSize;

    private void OnDrawGizmos() {
        for (float t = 0; t <= 1; t += .05f) {
            Vector2 Position = Mathf.Pow(1-t, 3) * Start.position +
                3 * Mathf.Pow(1-t, 2) * t * StartDirection.position +
                3 * (1-t) * Mathf.Pow(t, 2) * EndDirection.position +
                Mathf.Pow(t, 3) * End.position;

            Gizmos.DrawSphere(Position, LineSize);
        }

        Gizmos.DrawLine(new Vector2(Start.position.x, Start.position.y), 
            new Vector2(StartDirection.position.x, StartDirection.position.y));

        Gizmos.DrawLine(new Vector2(End.position.x, End.position.y), 
            new Vector2(EndDirection.position.x, EndDirection.position.y));
    }
}
