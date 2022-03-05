using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flightpath
{
    /*
        Recipe for a bezier curve path.
        Author: Chris Boveda
    */
    [CreateAssetMenu(fileName = "PathScriptableObject", menuName = "ScriptableObjects/Flightpath/PathScriptableObject", order = 0)]
    public class PathScriptableObject : ScriptableObject
    {
        public Vector2 StartPosition;
        public Vector2 StartDirection;
        public Vector2 EndPosition;
        public Vector2 EndDirection;
    }
}