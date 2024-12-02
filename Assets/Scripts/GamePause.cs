using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePause : MonoBehaviour
{
    [SerializeField] private Button backToMainMenu;
    [SerializeField] private Button cancel;
    [SerializeField] private Button close;
    [SerializeField] private SceneLoader sceneLoader;

    void Start()
    {
        backToMainMenu.onClick.AddListener(OnbackOnMainMenu);
        cancel.onClick.AddListener(OnResume);
        close.onClick.AddListener(OnResume);
    }

    private void OnResume()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnbackOnMainMenu()
    {
        sceneLoader.LoadScene(0);
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }

    public void Pause()
    { 
        Time.timeScale = 0;
    }
}
