using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpSingle : MonoBehaviour
{
    [SerializeField] Button closeButton;
    [SerializeField] Button advertButton;
    [SerializeField] private Computer parentComputer;

    private void Awake()
    {
        closeButton.onClick.AddListener(OnCloseAds);
        advertButton.onClick.AddListener(OnOpenAds);
    }

    private void OnOpenAds()
    {
        parentComputer.PlayerPenalized();
        TimeController.instance.SkipTimePenalty();
    }

    private void OnCloseAds()
    {
        PopupAdsManager.instance.addClosePupup(gameObject);
        gameObject.SetActive(false);
    }
}
