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

    [UnityTest]
    public IEnumerator TestSceneSwitching()
    {
        // Verify scenes are switching correctly.

        Dictionary<string, string> testScenes = new Dictionary<string, string>()
        {
              {"comGame", "comGameWin"},
              {"comGameWin", "comUnscrambleIntro"},
              {"comUnscrambleIntro", "comUnscramble"}
        };

        GameObject ob = new GameObject();
        ob.AddComponent<SceneActions>();
        SceneActions sceneObject = GameObject.FindObjectOfType<SceneActions>();

        string sceneName = "";
        string nextScene = "";
        Scene scene;
        foreach (KeyValuePair<string, string> testScene in testScenes)
        {
            // set the test scene
            SceneManager.LoadScene(testScene.Key);
            yield return new WaitForSeconds(0.2f); // delay to load

            // verify the test scene was loaded
            scene = SceneManager.GetActiveScene();
            yield return new WaitForSeconds(0.2f); // delay to get active scene
            sceneName = scene.name;
            Assert.AreEqual(testScene.Key, sceneName);

            // switch to the next scene
            sceneObject.StartGameScene();
            yield return new WaitForSeconds(0.2f); // delay to switch scene

            // verify the next scene was loaded
            scene = SceneManager.GetActiveScene();
            yield return new WaitForSeconds(0.2f); // delay to get active scene
            nextScene = scene.name;
            Assert.AreEqual(testScene.Value, nextScene);

        }
    }

    [UnityTest]
    public IEnumerator TestSuccessConditions()
    {
        // Verify tiles start in the correct positions and solve puzzle results in a success.

        // load scene with the scriptable tile objects
        SceneManager.LoadScene("comGame");
        yield return new WaitForSeconds(0.2f); // delay to load scene

        // get a scriptable tile object to use
        GameObject scriptObject = GameObject.Find("11");

        // confirm default tile positions is not a success
        VerifySuccess(scriptObject, false);

        GameObject mode = new GameObject();
        mode.AddComponent<ComGameModes>();
        ComGameModes modeObject = GameObject.FindObjectOfType<ComGameModes>();

        // solve puzzle
        modeObject.SolvePuzzle();
        yield return new WaitForSeconds(0.2f); // delay to solve
        
        VerifySuccess(scriptObject, true);

        GameObject main = new GameObject();
        main.AddComponent<ComGameMain>();
        ComGameMain mainObject = GameObject.FindObjectOfType<ComGameMain>();

        // set the tiles back to start positions 
        mainObject.SetTileStartPositions();
        yield return new WaitForSeconds(0.2f); // delay to reset pieces

        VerifySuccess(scriptObject, false);
    }

    public IEnumerator VerifySuccess(GameObject scriptObject, bool condition)
    {
        bool success = scriptObject.GetComponent<TileActions>().checkSuccess();
        yield return new WaitForSeconds(0.2f); // delay to check
        Assert.AreEqual(success, condition);
    }
}
