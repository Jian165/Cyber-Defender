using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstallerAntiVirusUI : MonoBehaviour
{
    [SerializeField] private Toggle acceptPolicy;
    [SerializeField] private Toggle acceptTerms;
    [SerializeField] private Button installButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] public LoadingInstallation loadingBar;

    private bool isPolicyAccepted = false;
    private bool isTermsAccepted = false;
    private bool startInstalation;
    public InstallerAntiVirusUI instance { get; private set; }

    private void Start()
    {
        if (loadingBar != null)
        {
            loadingBar.OnLoadingComplete += Loading_OnLoadingCompleted;
        }
        instance = this;
        acceptPolicy.onValueChanged.AddListener(OnChangeTogglePolicy);
        acceptTerms.onValueChanged.AddListener(OnChangeToggleTerms);

        installButton.onClick.AddListener(OnInstallClick);

        cancelButton.onClick.AddListener(OnCloseInstallation);
        closeButton.onClick.AddListener(OnCloseInstallation);
    }

    private void Loading_OnLoadingCompleted(object sender, EventArgs e)
    {
        gameObject.SetActive(false);
    }

    private void OnCloseInstallation()
    {
        gameObject.SetActive(false);
    }

  
    private void OnInstallClick()
    {
        if (isPolicyAccepted && isTermsAccepted)
        {
            loadingBar.gameObject.SetActive(true); 
        }
    }

    private void OnChangeToggleTerms(bool arg0)
    {
        isTermsAccepted = arg0;
    }
    private void OnChangeTogglePolicy(bool arg0)
    {
        isPolicyAccepted = arg0;
    }

    
}
