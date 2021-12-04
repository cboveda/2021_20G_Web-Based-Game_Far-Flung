using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flightpath
{
    [CreateAssetMenu(fileName = "MessageScriptableObject", menuName = "ScriptableObjects/Flightpath/MessageScriptableObject", order = 0)]
    public class MessageScriptableObject : ScriptableObject
    {
        public string[] Messages;
    }
}