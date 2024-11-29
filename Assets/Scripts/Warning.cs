using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Warning : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private Button okeyButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private TextMeshProUGUI warningText;


    public event EventHandler<OnWarningResponseArgs> OnWarningResponse;

    public class OnWarningResponseArgs : EventArgs
    {
        public bool isWarningIgnored;        
    }

    private void Start()
    {
        closeButton.onClick.AddListener(OnCancelWarning);
        cancelButton.onClick.AddListener(OnCancelWarning);

        okeyButton.onClick.AddListener(OnIgnoreWarning);
    }

    private void OnIgnoreWarning()
    {
        OnWarningResponse?.Invoke(this, new OnWarningResponseArgs
        {
            isWarningIgnored = true
        });
    }

    private void OnCancelWarning()
    {
        OnWarningResponse?.Invoke(this, new OnWarningResponseArgs
        {
            isWarningIgnored = false
        });
    }

    public void GetWarningMessage(WarningMessageSO warningMessage)
    {
        warningText.text = warningMessage.warningText;
    }

}
