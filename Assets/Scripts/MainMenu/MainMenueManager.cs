using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenueManager : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button controles;
    [SerializeField] private Button quit;
    [SerializeField] private SceneLoader loading;


    [SerializeField] GameObject controlesUI;

    private AudioSource menuMusic;
    [SerializeField] private AudioClip mainMenuSound;
    

    private void Start()
    {
        menuMusic = GetComponent<AudioSource>();
        playButton.onClick.AddListener(PlayeNight1);
        controles.onClick.AddListener(OpenControlsUI);
        quit.onClick.AddListener(quitGame);

        menuMusic.clip =  mainMenuSound;
        menuMusic.Play();
    }

    private void quitGame()
    {
        Application.Quit();
    }

    private void OpenControlsUI()
    {
        controlesUI.SetActive(true);
    }

    public void PlayeNight1()
    {
        loading.LoadScene(1);
    }
}
