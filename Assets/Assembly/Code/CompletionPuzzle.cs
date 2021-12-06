using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CompletionPuzzle : MonoBehaviour, Completion
{

     public TextAsset completionTextAsset;
    public UnityEvent nextScene;
    public bool IsCompleted(){
        foreach (Transform child in transform)
        {
            //Implement completion check
            if (child != null && child.GetComponent<DropSlot>() != null)
            {
                bool slotCompleted = child.GetComponent<DropSlot>().IsCompleted();
                // Debug.Log(child.name + " is " + slotCompleted);
                if (!slotCompleted)
                {
                    return false; //does nothing if one of the slots is incomplete
                }
            }
        }
        return true; 
    }


    public void OnCompletion(){
        nextScene.Invoke();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
