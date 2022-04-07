using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LabMainTest
{
    [UnityTest]
    public IEnumerator Test_LabFlow()
    {

        SceneManager.LoadScene("scene5");
        yield return new WaitForSeconds(0.5f);

        GameObject goLabMain = GameObject.Find("Puzzle 1");
        yield return new WaitForSeconds(0.5f);

        LabMain labMain = goLabMain.GetComponent<LabMain>();
        EventSystem eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();


        GameObject goAmplitudeInc = GameObject.Find("AmplitudeInc");
        GameObject goAmplitudeDec = GameObject.Find("AmplitudeDec");
        GameObject goFrequencyInc = GameObject.Find("FrequencyInc");
        GameObject goFrequencyDec = GameObject.Find("FrequencyDec");

        Button ampIncButton = goAmplitudeInc.GetComponent<Button>();
        Button ampDecButton = goAmplitudeDec.GetComponent<Button>();
        Button frqIncButton = goFrequencyInc.GetComponent<Button>();
        Button frqDecButton = goFrequencyDec.GetComponent<Button>();


        GameObject goPuzzleControls = GameObject.Find("UIPuzzleControls");
        //PuzzleManipulators puzzleManipulators = goPuzzleControls.GetComponent<PuzzleManipulators>();

        RadioPuzzle radioPuzzle1 = labMain.GetCurrentRadioPuzzle();

        //Warm the system up with a few clicks...
        eventSystem.SetSelectedGameObject(ampIncButton.gameObject);
        yield return new WaitForSeconds(0.1f);
        ampIncButton.onClick.Invoke();
        yield return new WaitForSeconds(0.1f);

        eventSystem.SetSelectedGameObject(ampDecButton.gameObject);
        yield return new WaitForSeconds(0.1f);
        ampDecButton.onClick.Invoke();
        yield return new WaitForSeconds(0.1f);

        eventSystem.SetSelectedGameObject(frqIncButton.gameObject);
        yield return new WaitForSeconds(0.1f);
        frqIncButton.onClick.Invoke();
        yield return new WaitForSeconds(0.1f);

        eventSystem.SetSelectedGameObject(frqDecButton.gameObject);
        yield return new WaitForSeconds(0.1f);
        frqDecButton.onClick.Invoke();
        yield return new WaitForSeconds(0.1f);

        while (radioPuzzle1.amplitudeCurrentGuess < radioPuzzle1.amplitudeAnswer)
        {
            //labMain.IncrementAmplitude();
            eventSystem.SetSelectedGameObject(ampIncButton.gameObject);
            yield return new WaitForSeconds(0.1f);
            ampIncButton.onClick.Invoke();
            yield return new WaitForSeconds(0.1f);
        }
        while (radioPuzzle1.amplitudeCurrentGuess > radioPuzzle1.amplitudeAnswer)
        {
            //labMain.DecrementAmplitude();
            eventSystem.SetSelectedGameObject(ampDecButton.gameObject);
            yield return new WaitForSeconds(0.1f);
            ampDecButton.onClick.Invoke();
            yield return new WaitForSeconds(0.1f);
        }
        while (radioPuzzle1.frequencyCurrentGuess < radioPuzzle1.frequencyAnswer)
        {
            //labMain.IncrementFrequency();
            eventSystem.SetSelectedGameObject(frqIncButton.gameObject);
            yield return new WaitForSeconds(0.1f);
            frqIncButton.onClick.Invoke();
            yield return new WaitForSeconds(0.1f);
        }
        while (radioPuzzle1.frequencyCurrentGuess > radioPuzzle1.frequencyAnswer)
        {
            //labMain.DecrementFrequency();
            eventSystem.SetSelectedGameObject(frqDecButton.gameObject);
            yield return new WaitForSeconds(0.1f);
            frqDecButton.onClick.Invoke();
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.5f);

        

        RadioPuzzle radioPuzzle2 = labMain.GetCurrentRadioPuzzle();
        while (radioPuzzle2.amplitudeCurrentGuess < radioPuzzle2.amplitudeAnswer)
        {
            labMain.IncrementAmplitude();
        }
        while (radioPuzzle2.amplitudeCurrentGuess > radioPuzzle2.amplitudeAnswer)
        {
            labMain.DecrementAmplitude();
        }
        while (radioPuzzle2.frequencyCurrentGuess < radioPuzzle2.frequencyAnswer)
        {
            labMain.IncrementFrequency();
        }
        while (radioPuzzle2.frequencyCurrentGuess > radioPuzzle2.frequencyAnswer)
        {
            labMain.DecrementFrequency();
        }

        yield return new WaitForSeconds(0.5f);

        GameObject goElementNext = GameObject.Find("ElementNext");
        GameObject goElementPrev = GameObject.Find("ElementPrev");
        GameObject goElementAdd = GameObject.Find("ElementAdd");
        GameObject goElementCheck = GameObject.Find("ElementCheck");

        Button eleNextButton = goElementNext.GetComponent<Button>();
        Button elePrevButton = goElementPrev.GetComponent<Button>();
        Button eleAddButton = goElementAdd.GetComponent<Button>();
        Button eleCheckButton = goElementCheck.GetComponent<Button>();

        eventSystem.SetSelectedGameObject(elePrevButton.gameObject);
        yield return new WaitForSeconds(0.1f);
        elePrevButton.onClick.Invoke();
        yield return new WaitForSeconds(0.1f);
        eventSystem.SetSelectedGameObject(elePrevButton.gameObject);
        yield return new WaitForSeconds(0.1f);
        elePrevButton.onClick.Invoke();
        yield return new WaitForSeconds(0.1f);
        eventSystem.SetSelectedGameObject(elePrevButton.gameObject);
        yield return new WaitForSeconds(0.1f);
        elePrevButton.onClick.Invoke();
        yield return new WaitForSeconds(0.1f);
        eventSystem.SetSelectedGameObject(eleAddButton.gameObject);
        yield return new WaitForSeconds(0.1f);
        eleAddButton.onClick.Invoke();
        yield return new WaitForSeconds(0.1f);
        eventSystem.SetSelectedGameObject(eleNextButton.gameObject);
        yield return new WaitForSeconds(0.1f);
        eleNextButton.onClick.Invoke();
        yield return new WaitForSeconds(0.1f);
        eventSystem.SetSelectedGameObject(eleNextButton.gameObject);
        yield return new WaitForSeconds(0.1f);
        eleNextButton.onClick.Invoke();
        yield return new WaitForSeconds(0.1f);
        eventSystem.SetSelectedGameObject(eleNextButton.gameObject);
        yield return new WaitForSeconds(0.1f);
        eleNextButton.onClick.Invoke();
        yield return new WaitForSeconds(0.1f);
        eventSystem.SetSelectedGameObject(eleAddButton.gameObject);
        yield return new WaitForSeconds(0.1f);
        eleAddButton.onClick.Invoke();
        yield return new WaitForSeconds(0.1f);
        eventSystem.SetSelectedGameObject(eleNextButton.gameObject);
        yield return new WaitForSeconds(0.1f);
        eleNextButton.onClick.Invoke();
        yield return new WaitForSeconds(0.1f);
        eventSystem.SetSelectedGameObject(eleAddButton.gameObject);
        yield return new WaitForSeconds(0.1f);
        eleAddButton.onClick.Invoke();
        yield return new WaitForSeconds(0.1f);
        eventSystem.SetSelectedGameObject(eleCheckButton.gameObject);
        yield return new WaitForSeconds(0.1f);
        eleCheckButton.onClick.Invoke();
        yield return new WaitForSeconds(0.1f);


        labMain.InsertSpectra(SpectraPuzzle.nickel);
        yield return new WaitForSeconds(0.1f);
        labMain.InsertSpectra(SpectraPuzzle.aluminum);
        yield return new WaitForSeconds(0.1f);
        labMain.InsertSpectra(SpectraPuzzle.gold);
        yield return new WaitForSeconds(0.1f);


        SceneManager.LoadScene("StartScene.Scanning");

        yield return new WaitForSeconds(0.1f);

    }

}
