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
    [SerializeField] private Computer parentComputer;

    [SerializeField] private Notes notesUI;
    [SerializeField] private Tips tipsUI;

    [SerializeField] private TipsSO tips;
    [SerializeField] private NotesSO notes;

 
    public static EmailManager Instance { get; private set;}

    private int totalEmailCounter;

    private void Awake()
    {
        emailInbox = new List<EmailSO>();
        Instance = this;
    }

    private void Start()
    {
        notesUI.gameObject.SetActive(true);
        notesUI.GetComponent<Notes>().GetNote(notes);
        tipsUI.gameObject.SetActive(true);
        tipsUI.GetTips(tips);
    }

    private void Update()
    {
        spawnEmailTimer -= Time.deltaTime;
        if (spawnEmailTimer <= 0f)
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

        if (emailInbox.Count <= 0 && totalEmailCounter >=emailInboxMax)
        {
            DesktopController.instance.AddFinishedComputer(parentComputer);
            gameObject.SetActive(false);
            notesUI.gameObject.SetActive(false);
            tipsUI.gameObject.SetActive(false);
        }
    }

    public List<EmailSO> GetEmailInbox()
    {
        return emailInbox;
    }
       
}
