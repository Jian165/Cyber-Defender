using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoadingInstallation : MonoBehaviour
{

    [SerializeField] private Image loadingBar;
    [SerializeField] private float loadingSpeed = 0.5f;
    
    private float currentProgress = 0f;
    private bool isLoadingComplte=false;


    public event EventHandler OnLoadingComplete;
     private void Update()
    {
        if (!isLoadingComplte)
        {
            currentProgress += Time.deltaTime * loadingSpeed;
            loadingBar.fillAmount = currentProgress;

            if (currentProgress >= 1f)
            { 
                OnLoadingComplete?.Invoke(this, EventArgs.Empty);
            }
        }
    }


}
