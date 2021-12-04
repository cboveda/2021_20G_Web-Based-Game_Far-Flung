using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogMaker
{
    [System.Serializable]
    public enum RobotCharacter
    {
        Serious,
        HighEnergy,
        LowEnergy,
        None
    }

    [System.Serializable]
    public class Dialog
    {
        [SerializeField]
        public string dialogText;
        [SerializeField]
        public RobotCharacter robotVoice;
    }
}



