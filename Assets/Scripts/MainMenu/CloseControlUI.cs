using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseControlUI : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    private void Start()
    {
        closeButton.onClick.AddListener(OnCloseControles); 
    }

    private void OnCloseControles()
    {
        gameObject.SetActive(false);
    }
}
