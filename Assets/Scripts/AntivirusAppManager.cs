using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AntivirusAppManager : MonoBehaviour
{
    [SerializeField] LoadingInstallation loadingInstallation;
    [SerializeField] GameObject antivirusIcon;
    [SerializeField] Button antivirusIconButton;
    [SerializeField] GameObject antivirusSystem;

    private void Start()
    {
        if (antivirusSystem != null)
        {
            loadingInstallation.OnLoadingComplete += loadingInstallation_OnComplete;
        }
        antivirusIconButton.onClick.AddListener(OnAntivirusIconClick);
    }

    private void OnAntivirusIconClick()
    {
        antivirusSystem.SetActive(true);
    }

    private void loadingInstallation_OnComplete(object sender, EventArgs e)
    {
        antivirusIcon.gameObject.SetActive(true);
    }
}
