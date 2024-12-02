using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EmailManagerSingel : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI senderNameUI;
    [SerializeField] private TextMeshProUGUI messageSubjectUI;
    [SerializeField] private Transform senderProfileUI;
    [SerializeField] private Button markAsReadButton;
    [SerializeField] private Button removeMessageButton;

    [Header("EmailManager")]
    [SerializeField] private EmailManager EmailManager;

    [Header("EmailContent")]
    [SerializeField] private EmailContent emailContent;


    private EmailSO email;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnMessageRead);
        markAsReadButton.onClick.AddListener(OnMessageRead);
        removeMessageButton.onClick.AddListener(OnMessageRemove);

        markAsReadButton.onClick.AddListener(OnMessageMarkRead);

       
    }

    private void OnMessageRemove()
    {
        EmailManager.Instance.removeItem(email);

        if (!email.isMalicious)
        {
            TimeController.instance.SkipTimePenalty();
        }

        if (EmailContent.GetCurrentReadEmailSO() == email)
        {
            emailContent.setReadContent(null);
        }
    }

    private void OnMessageMarkRead()
    {
        EmailManager.Instance.removeItem(email);

        if (email.isMalicious)
        {
            TimeController.instance.SkipTimePenalty();
        }
        if (EmailContent.GetCurrentReadEmailSO() == email)
        {
            emailContent.setReadContent(null);
        }

    }

    private void OnMessageRead()
    {
        emailContent.setReadContent(email);
    }

    public void setEmailSO(EmailSO emailSO)
    {
        senderNameUI.text = emailSO.senderName;
        messageSubjectUI.text = emailSO.senderSubject;
        senderProfileUI.GetComponent<Image>().sprite = emailSO.senderProfile;
        email = emailSO;
    }

    







}
