using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Events;


public interface Completion
{

    public bool IsCompleted();

    public void OnCompletion();
}
