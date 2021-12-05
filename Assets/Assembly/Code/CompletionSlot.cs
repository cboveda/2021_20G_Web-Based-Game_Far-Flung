using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CompletionSlot : MonoBehaviour, Completion
{

    public bool IsCompleted(){
        foreach(Completion completetion in GetComponentsInChildren<Completion>()){
            if(!completetion.IsCompleted()) return false;
        }
        return true;
    }

    public void OnCompletion(){
    }
   
}
