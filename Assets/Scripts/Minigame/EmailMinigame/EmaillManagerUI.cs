using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmaillManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform emailTemplate;


    private void Awake()
    {
        emailTemplate.gameObject.SetActive(false);
    }
    private void Start()
    {
        EmailManager.Instance.OnEmailSpawn += EmailManager_SpawEmail;
        UpdateVisual();
        
    }

    private void EmailManager_SpawEmail(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if (child == emailTemplate) continue;
            Destroy(child.gameObject);
        
        }

        foreach (EmailSO email in EmailManager.Instance.GetEmailInbox())
        {
            Transform emailTransform = Instantiate(emailTemplate, container);
            emailTransform.gameObject.SetActive(true);
            emailTransform.GetComponent<EmailManagerSingel>().setEmailSO(email);

        }
        
    }
}
