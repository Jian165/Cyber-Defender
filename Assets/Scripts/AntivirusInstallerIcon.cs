using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AntivirusINstallerIcon : MonoBehaviour
{
    [SerializeField] private GameObject installerWindow;
    [SerializeField] private Button iconButton;
    [SerializeField] private LoadingInstallation loadingInsallation;

    private bool isAlreadyInstalled = false;

    private void Start()
    {
        loadingInsallation.OnLoadingComplete += loadingInstallation_OnComplete;
        iconButton.onClick.AddListener(OnIconClick);
    }

    private void loadingInstallation_OnComplete(object sender, EventArgs e)
    {
        isAlreadyInstalled = true;
    }

    private void OnIconClick()
    {
        if (!isAlreadyInstalled)
        {
            installerWindow.SetActive(true);
        }
    }
}
