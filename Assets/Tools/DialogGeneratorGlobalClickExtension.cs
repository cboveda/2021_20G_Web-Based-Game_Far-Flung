using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DialogMaker
{
    public class DialogGeneratorGlobalClickExtension : MonoBehaviour
    {       
        private DialogGenerator _dg;
        private void Start() {
            _dg = GetComponent<DialogGenerator>();
            _dg.BtnDialogButton.onClick.RemoveAllListeners();
        } 
        private void Update() {
            if (Input.GetMouseButtonDown(0))
            {
                if (_dg)
                {
                    _dg.BeginPlayingDialog();
                }
            }
        }
    }
}