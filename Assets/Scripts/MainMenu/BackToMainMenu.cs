using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackToMainMenu : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private SceneLoader loadingScreen;
    private void Start()
    {
        mainMenuButton.onClick.AddListener(BackToMainMenue);
    }

    private void BackToMainMenue()
    {
        loadingScreen.LoadScene(0);
    }
}
