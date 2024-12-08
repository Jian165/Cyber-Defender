using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class FireWallMinigame : MonoBehaviour
{
    [SerializeField] private FireWallIconSO imageResource;

    [SerializeField] private Image privateFirewallColorIndicator;
    [SerializeField] private Image privateFirewallImageIndicator;
    [SerializeField] private FireWallToggle privateFirewallToggle;

    [SerializeField] private Image publicFirewallColorIndicator;
    [SerializeField] private Image publicFirewallImageIndicator;
    [SerializeField] private FireWallToggle publicFirewallToggle;

    [SerializeField] private Button okeyButton;

    [SerializeField] private Computer parentComputer;

    [SerializeField] private Warning WarningUI;
    [SerializeField] private WarningMessageSO warningMessage;

    [SerializeField] private NotesSO notes;
    [SerializeField] private TipsSO tips;

    [SerializeField] private Tips tipsUI;
    [SerializeField] private Notes NotesUI;


    private bool privateFirewallState = false;
    private bool publicFirewallState = false;

    public void Start()
    {
        privateFirewallToggle.OnToggleChange += PrivateFirewallToggle_OnToggleChange;
        publicFirewallToggle.OnToggleChange += PublicFirewallToggle_OnToggleChange;
        
        WarningUI.OnWarningResponse += WarningUI_OnWarningResponse;

        okeyButton.onClick.AddListener(OnOkeyButton);
        tipsUI.gameObject.SetActive(true);
        NotesUI.gameObject.SetActive(true);

        tipsUI.GetTips(tips);
        NotesUI.GetNote(notes);
    }

    private void WarningUI_OnWarningResponse(object sender, Warning.OnWarningResponseArgs e)
    {
        if (e.isWarningIgnored)
        {
            TimeController.instance.SkipTimePenalty();
            parentComputer.PlayerPenalized();
            WarningUI.gameObject.SetActive(false);
            WarningUI.gameObject.SetActive(false);
        }
    }

    private void OnOkeyButton()
    {
        if (privateFirewallState && publicFirewallState)
        {
            tipsUI.gameObject.SetActive(false);
            NotesUI.gameObject.SetActive(false);
            Close();
        }
        else
        { 
            WarningUI.gameObject.SetActive(true);
            WarningUI.GetWarningMessage(warningMessage);
        }

    }

    private void PublicFirewallToggle_OnToggleChange(object sender, FireWallToggle.OnToggleChangeAgrs e)
    {
        if (e.isFirewallOnValue)
        {
            publicFirewallColorIndicator.color = Color.green;
            publicFirewallImageIndicator.GetComponent<Image>().sprite = imageResource.firewallOnSprite;
        }
        else
        {
            publicFirewallColorIndicator.color = Color.red;
            publicFirewallImageIndicator.GetComponent<Image>().sprite = imageResource.firewallOffSprite;

        }

        publicFirewallState = e.isFirewallOnValue;
    }

    private void PrivateFirewallToggle_OnToggleChange(object sender, FireWallToggle.OnToggleChangeAgrs e)
    {
        if (e.isFirewallOnValue)
        {
            privateFirewallColorIndicator.color = Color.green;
            privateFirewallImageIndicator.GetComponent<Image>().sprite = imageResource.firewallOnSprite;
        }
        else
        {
            privateFirewallColorIndicator.color = Color.red;
            privateFirewallImageIndicator.GetComponent<Image>().sprite = imageResource.firewallOffSprite;

        }

        privateFirewallState = e.isFirewallOnValue;

    }

    private void Close()
    { 
            gameObject.SetActive(false);
            DesktopController.instance.AddFinishedComputer(parentComputer);
    }
}
