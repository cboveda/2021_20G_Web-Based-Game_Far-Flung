using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ComGameTests
{

    [Test]
    public void TestSceneSwitching()
    {
        Dictionary<string, string> testScenes = new Dictionary<string, string>()
           {
              {"comGame", "comGameWin"},
              {"comGameWin", "comUnscrambleIntro"},
              {"comUnscrambleIntro", "comUnscramble"}
           };

        GameObject ob = new GameObject();
        ob.AddComponent<SceneActions>();
        SceneActions sceneObject = GameObject.FindObjectOfType<SceneActions>();

        foreach (KeyValuePair<string, string> testScene in testScenes)
        {
            //Debug.Log(testScene.Key);
            //Debug.Log(testScene.Value);

            // set the test scene
            SceneManager.LoadScene(testScene.Key);

            // switch to the next scene
            sceneObject.StartGameScene();

            // get the scene that has been switched to
            Scene scenes = SceneManager.GetActiveScene();
            string sceneName = scenes.name;
            Debug.Log(sceneName);

            // verify next scene is correct
            Assert.AreEqual(testScene.Value, sceneName);
        }
    }
}
