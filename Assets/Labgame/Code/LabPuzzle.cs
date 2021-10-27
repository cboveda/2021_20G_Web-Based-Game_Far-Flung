using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class LabPuzzle : MonoBehaviour
{
    //private string name;
  

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public abstract bool CheckSolution();
}
