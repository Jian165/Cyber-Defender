using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class LoadingScann : MonoBehaviour
{

    [SerializeField] private Image loadingBar;
    [SerializeField] private float loadingSpeed = 0.05f;
    [SerializeField] private VirusPopupManager virusPopupManager;

    private float fastLoadingSpeed = 0.5f;

    private float currentProgress = 0f;

    public bool isLoadingComplte = false;

    private bool isStart;
    public event EventHandler<OnScannInProgressEventArgs> OnScannInProgress;

    private bool locanOnScannProgress; 

    public LoadingScann instance { get; private set;}

    public class OnScannInProgressEventArgs : EventArgs
    {
        public bool isScannOnProgress;
    }

    private void Start()
    {
        instance = this;
    }
    private void Update()
    {
        if (virusPopupManager.VirusPopupCount() <= 0)
        {
            loadingSpeed = fastLoadingSpeed;
            
        }

        if (!isLoadingComplte && isStart==true)
        {
            currentProgress += Time.deltaTime * loadingSpeed;
            loadingBar.fillAmount = currentProgress;

            if (currentProgress >= 1f)
            {
                OnScannInProgress?.Invoke(this, new OnScannInProgressEventArgs
                {
                    isScannOnProgress = false
                    
                    
                });
                virusPopupManager.VirusFinished();
            }
            OnScannInProgress?.Invoke(this, new OnScannInProgressEventArgs
            {
                isScannOnProgress = true
               
            });
            locanOnScannProgress = true;
        }

        virusPopupManager.IsOnScanning(locanOnScannProgress);
    }

    public void isStartScann(bool isStartScann) 
    {
       isStart =  isStartScann;
    }
    
    


}
