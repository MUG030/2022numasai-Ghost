using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    public void SCTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void SCTutorial()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void SCMain()
    {
        SceneManager.LoadScene("MainScene 2");
    }

    public void SCClear()
    {
        SceneManager.LoadScene("ClearScene");
    }

    public void SCCredit()
    {
        SceneManager.LoadScene("CreditRoll");
    }
}
