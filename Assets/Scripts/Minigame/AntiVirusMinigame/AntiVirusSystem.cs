using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AntiVirusSystem : MonoBehaviour
{
    [SerializeField] private Button cancelButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button ScannAndClear;
    [SerializeField] private LoadingScann loadingScann;

    private bool CanCancel =  true;

    private void Start()
    {
        loadingScann.OnScannInProgress += loadingScann_OnProgress;
        cancelButton.onClick.AddListener(OnCloseAntiVirusSystem);
        closeButton.onClick.AddListener(OnCloseAntiVirusSystem);
        ScannAndClear.onClick.AddListener(OnStartScannAndClear);
    }

    private void loadingScann_OnProgress(object sender, LoadingScann.OnScannInProgressEventArgs e)
    {
        CanCancel = !e.isScannOnProgress;

        if (e.isScannOnProgress == false)
        {
            gameObject.SetActive(false); 
        }
    }

    private void OnStartScannAndClear()
    {
        loadingScann.isStartScann(true);
    }

    private void OnCloseAntiVirusSystem()
    {
        if (CanCancel)
        {
            gameObject.SetActive(false);
        }
    }
}
