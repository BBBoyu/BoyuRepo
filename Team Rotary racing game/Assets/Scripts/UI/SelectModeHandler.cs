using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectModeHandler : MonoBehaviour
{
    public static string mode;

    public void TimeAttack() {
        mode = "TimeAttack";
        SceneManager.LoadScene("Track Selection");
    }

    public void AIRacer() {
        mode = "AIRacer";
        SceneManager.LoadScene("Track Selection");
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }
}

