using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CompletionParent : MonoBehaviour, Completion
{
    public UnityEvent next;
        public bool IsCompleted(){

        if(GetComponentInChildren<Completion>()==null){
            Debug.Log("No Completion found");
        }
        foreach(Completion completion in GetComponentsInChildren<Completion>()){
            if((object) completion == this) continue;

            if(!completion.IsCompleted()) return false;
        }
        return true;
    }

    public void OnCompletion(){
        next.Invoke();
    }
   
}
