using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EmailContent : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI senderName;
    [SerializeField] private TextMeshProUGUI senderSubject;
    [SerializeField] private Transform senderProfile;
    [SerializeField] private TextMeshProUGUI senderMessage;

    private static EmailSO currentReadEmailSO;

    public void setReadContent(EmailSO emailcontent)
    {
        if (emailcontent != null)
        {
            currentReadEmailSO = emailcontent;
            senderName.text = currentReadEmailSO.senderName;
            senderSubject.text = currentReadEmailSO.senderSubject;
            senderProfile.GetComponent<Image>().sprite = currentReadEmailSO.senderProfile;
            senderMessage.text = currentReadEmailSO.senderMessage;

        }
        else 
        { 
            currentReadEmailSO = null;
            senderName.text = "";
            senderSubject.text = "";
            senderProfile.GetComponent<Image>().sprite = null;
            senderMessage.text = "";
        }
    }

    public static EmailSO GetCurrentReadEmailSO()
    {
        return currentReadEmailSO;
    }




}
