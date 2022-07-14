using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject OptionsMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GameIsPaused)
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        //Resume all audio sources
        AudioSource[] audioSources = Object.FindObjectsOfType(typeof(AudioSource)) as AudioSource[];

        foreach (AudioSource a in audioSources)
        {
            a.Play();
        }
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        //Pause all audio sources
        AudioSource[] audioSources = Object.FindObjectsOfType(typeof(AudioSource)) as AudioSource[];

        foreach (AudioSource a in audioSources)
        {
            a.Pause();
        }
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene("Menu");

        //Resume all audio sources
        AudioSource[] audioSources = Object.FindObjectsOfType(typeof(AudioSource)) as AudioSource[];

        foreach (AudioSource a in audioSources)
        {
            a.Play();
        }
    }

    public void LoadOptionsMenu()
    {
        pauseMenuUI.SetActive(false);
        OptionsMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
