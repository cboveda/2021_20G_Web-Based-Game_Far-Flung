using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogMaker
{
    [CreateAssetMenu(fileName = "DialogScriptableObject", menuName = "ScriptableObjects/DialogScriptableObject", order = 1)]
    public class DialogScriptableObject : ScriptableObject
    {

        public Dialog[] dialogs;


        public Dialog GetNextDialogMessage(int slot)
        {

            return dialogs[slot];
        }

    }
}


