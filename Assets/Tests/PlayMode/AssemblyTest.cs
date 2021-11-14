using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AssemblyTest
{

    [UnityTest]
    public IEnumerator TestSceneSwitching()
    {
        string assemblySceneName = "Assembly";
         // set the test scene
        SceneManager.LoadScene(assemblySceneName);
        yield return new WaitForSeconds(0.2f); // delay to load

        // verify the test scene was loaded
        Scene currentScene = SceneManager.GetActiveScene();
        yield return new WaitForSeconds(0.2f); // delay to get active scene
        string sceneName = currentScene.name;
        Assert.AreEqual(assemblySceneName, sceneName);

        //make sure the satelite slots exist
        GameObject sateliteSlots = GameObject.Find("Satelite Slots");
        Assert.NotNull(sateliteSlots);
        //make sure the textpanel is not active
        GameObject textPanel = GameObject.Find("Panel");
        Assert.Null(textPanel);
        //itterate through the slots
        foreach(DropSlot slot in sateliteSlots.GetComponentsInChildren<DropSlot>()){
            GameObject slotObject = slot.gameObject;
            //move the matching object to the slot
            slot.slotMatch.transform.SetPositionAndRotation(slotObject.transform.position, slotObject.transform.rotation);
            yield return new WaitForSeconds(0.2f); // delay to let collision occur
            //check the slot shows as completed
            Assert.IsTrue(slot.IsCompleted());
            //check the text panel appears
            textPanel = GameObject.Find("Panel");
            Assert.NotNull(textPanel);
            //click the button
            textPanel.GetComponentInChildren<Button>().onClick.Invoke();
            yield return new WaitForSeconds(0.2f); // delay to let panel close
            //check to see if all the slots are completed (will show another text box on completion)
            if (sateliteSlots.GetComponent<Completion>().IsCompleted()){
                break;
            }
            //check that the textpanel was set to inactive after clicking the button
            textPanel = GameObject.Find("Panel");
            Assert.Null(textPanel);
        }
        //check that all the slots were in fact completed
        Assert.True(sateliteSlots.GetComponent<Completion>().IsCompleted());
        //make sure the completion panel is displayed
        textPanel = GameObject.Find("Panel");
        Assert.NotNull(textPanel);
        //click the button on the completion panel
        textPanel.GetComponentInChildren<Button>().onClick.Invoke();
        yield return new WaitForSeconds(0.2f); // delay to let scenes switch
        //check the scene has changed
        Scene assemblyScene = currentScene;
        currentScene = SceneManager.GetActiveScene();
        yield return new WaitForSeconds(0.2f); // delay to get active scene
        Assert.AreNotEqual(currentScene, assemblyScene);
    }
}
