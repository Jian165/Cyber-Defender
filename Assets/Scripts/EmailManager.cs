using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EmailManager : Minigame
{
    [SerializeField] private List<EmailSO> emailSOList;

    public event EventHandler OnEmailSpawn; 

    private List<EmailSO> emailInbox;
    private float spawnEmailTimer;
    private float spawnEmailTiemerMax = 2f;
    [SerializeField] private int emailInboxMax;

    public static EmailManager Instance { get; private set; }

    private int totalEmailCounter;

    private void Awake()
    {
        emailInbox = new List<EmailSO>();
        Instance = this;
    }

    private void Update()
    {
        spawnEmailTimer -= Time.deltaTime;
        if (spawnEmailTimer <= 0f )
        {
            spawnEmailTimer = spawnEmailTiemerMax;

            if (emailInbox.Count < emailInboxMax && totalEmailCounter < emailInboxMax)
            {
                totalEmailCounter++;
                EmailSO email = emailSOList[UnityEngine.Random.Range(0, emailSOList.Count)];
                if (!emailInbox.Contains(email) || email.isMalicious == true)
                {
                    emailInbox.Add(email);
                    OnEmailSpawn?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }

    public void removeItem (EmailSO email)
    {
        emailInbox.Remove(email);
        OnEmailSpawn?.Invoke(this, EventArgs.Empty);
    }

    public List<EmailSO> GetEmailInbox()
    {
        return emailInbox;
    }
       
}
