using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using DialogMaker;


public class DialogMakerTest
{
    
    Dialog line1 = new Dialog("Line1 is not too long of a line but needs to be long enough", RobotCharacter.Serious);
    Dialog line2 = new Dialog("Line2 is fairly short", RobotCharacter.Serious);
    Dialog line3 = new Dialog("Line3 is a much longer line of text meant to cover multiple lines and go on and on" +
        " and on, never stopping in one incredibly unbroken sentences moving from topic to topic so that one one " +
        "has a change to interrupt it is really quite hypnotic.", RobotCharacter.Serious);

    
    [UnityTest]
    public IEnumerator Test_DialogCreation()
    {
        Dialog[] dialogArray = {
            line1,
            line2,
            line3
        };

        GameObject myObject = new GameObject();
        DialogGenerator myDialogGenerator = myObject.gameObject.AddComponent<DialogGenerator>();

        DialogScriptableObject myDialogContainer = ScriptableObject.CreateInstance<DialogScriptableObject>();
        myDialogGenerator.dialogContainer = myDialogContainer;

        myDialogContainer.dialogs = dialogArray;
        Assert.AreEqual(myDialogContainer.dialogs.Length, 3);
        
        //Let start() run for a moment to make the object.
        yield return new WaitForSeconds(0.1f);

        //We should be at zero position first...
        Assert.AreEqual(0, myDialogGenerator.GetCurrentDialogPosition());
        
        //This will increment our position.
        myDialogGenerator.BeginPlayingDialog();
        yield return new WaitForSeconds(0.1f);

        //Should now be at 1.
        Assert.AreEqual(1, myDialogGenerator.GetCurrentDialogPosition());

        //Run again, but we haven't let time elapse, so all we're doing is making it type faster.
        myDialogGenerator.BeginPlayingDialog();

        //Now we'll wait for a second, which should be enough time for the typer to finish.
        yield return new WaitForSeconds(1.0f);

        //Tutorial is still up and hasn't been passed yet, so we haven't incremented.  Let's check.
        Assert.AreEqual(1, myDialogGenerator.GetCurrentDialogPosition());

        //Now, let's force us to pass the tutorial, which should increment our position.
        myDialogGenerator.BeginPlayingDialog();
        yield return new WaitForSeconds(0.1f);
        Assert.AreEqual(2, myDialogGenerator.GetCurrentDialogPosition());

        //Now let's incrememnt two more times, one to speed up the current text, wait a sec, and then once to dispose of the tutorial by destroying it.
        yield return new WaitForSeconds(0.1f);
        myDialogGenerator.BeginPlayingDialog();
        yield return new WaitForSeconds(1.0f);
        myDialogGenerator.BeginPlayingDialog();
        Assert.AreEqual(3, myDialogGenerator.GetCurrentDialogPosition());
        yield return new WaitForSeconds(1.0f);
        myDialogGenerator.BeginPlayingDialog();
        yield return new WaitForSeconds(1.0f);
        myDialogGenerator.BeginPlayingDialog();
        yield return new WaitForSeconds(1.0f);



    }

    [UnityTest]
    public IEnumerator Test_DialogCreationFail()
    {
        GameObject myObject = new GameObject();
        DialogGenerator myDialogGenerator2 = myObject.gameObject.AddComponent<DialogGenerator>();
        LogAssert.Expect(LogType.Assert, "Warning: A DialogScriptableObject is desired but not found.");
        yield return new WaitForSeconds(0.1f);
        
        

        
    }

}
