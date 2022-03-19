using UnityEngine;

public class CompletionSlot : MonoBehaviour, Completion
{

    public bool IsCompleted() {
        foreach(Completion completetion in GetComponentsInChildren<Completion>()){
            if(!completetion.IsCompleted()) return false;
        }
        return true;
    }

    public void OnCompletion() {}
   
}


/* COMMON MERGE - DELETE FILE */