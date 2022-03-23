using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogMaker;

public class AssemblyFlowControl : MonoBehaviour
{
    public DialogGenerator introDiag;

    public MouseLook mouseLook;
    // Start is called before the first frame update
    void Start()
    {
        if(introDiag == null){
            try{
                introDiag = GameObject.Find("AssemblyIntro").GetComponent<DialogGenerator>();
            } catch {
                Debug.Log("Failed to find Assembly Intro Dialog Generator");
            }
        }

        if( mouseLook == null){
            try{
                mouseLook = GameObject.Find("Assembly Camera").GetComponent<MouseLook>();
            } catch {
                Debug.Log("Failed to find Assembly Camrera mouse look");
            }
        }
        
        mouseLook.PauseAssembly();
        introDiag.BeginPlayingDialog();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
