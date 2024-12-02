using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpSingle : MonoBehaviour
{
    [SerializeField] Button closeButton;
    [SerializeField] Button advertButton;

    private void Awake()
    {
        closeButton.onClick.AddListener(OnCloseAds);
        advertButton.onClick.AddListener(OnOpenAds);
    }

    private void OnOpenAds()
    {
        TimeController.instance.SkipTimePenalty();
    }

    private void OnCloseAds()
    {
        PopupAdsManager.instance.addClosePupup(gameObject);
        gameObject.SetActive(false);
    }
}
