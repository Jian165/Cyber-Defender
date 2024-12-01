using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VirusPopupManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> popUpVirusList;
    [SerializeField] private Computer parentComputer;

    [SerializeField] private Tips tipsUI;
    [SerializeField] private Notes notesUI;

    [SerializeField] private TipsSO tips;
    [SerializeField] private NotesSO notes;

    [SerializeField] private List<GameObject>AppsToTurnOn;
    [SerializeField] private List<GameObject>AppsToTurnOff;

    public List<GameObject> popUpViruseActiveList;
    
    private float popUpMaxTime = 2f;
    private float popUpMinTime;

    private bool OnProgressScanner = false;
    public static VirusPopupManager instance { get; private set; }

    private void Start()
    {
        foreach (GameObject appsIcon in AppsToTurnOff)
        {
            appsIcon.SetActive(false);
        }

        foreach (GameObject appsIcon in AppsToTurnOn)
        {
            appsIcon.SetActive(true);
        }

        instance = this;
        popUpViruseActiveList =  new List<GameObject>();
        tipsUI.gameObject.SetActive(true);
        tipsUI.GetTips(tips);
        notesUI.gameObject.SetActive(true);
        notesUI.GetNote(notes);
    }
    private void Update()
    {

       popUpMinTime -= Time.deltaTime;

        if (popUpMinTime <= 0f)
        { 
            popUpMinTime = popUpMaxTime;
            if (popUpViruseActiveList.Count < 10 && OnProgressScanner == false)
            {
                while (true)
                {
                    GameObject selectedPopup = popUpVirusList[UnityEngine.Random.Range(0, popUpVirusList.Count)];

                    if (!popUpViruseActiveList.Contains(selectedPopup))
                    {
                        selectedPopup.SetActive(true);
                        popUpViruseActiveList.Add(selectedPopup);
                        break;
                    }
                }
            }

            if(OnProgressScanner)
            {
                VirusClearActivated(); 
            }
        }
    }

    private void VirusClearActivated()
    {
        if (popUpViruseActiveList.Count > 0)
        {
            GameObject activePopup = popUpViruseActiveList[popUpViruseActiveList.Count-1];
            activePopup.SetActive(false);
            popUpViruseActiveList.Remove(activePopup);
        }
    }

    public int VirusPopupCount()
    {
        return popUpViruseActiveList.Count;
    }

    public void IsOnScanning(bool progressScanner)
    {
        OnProgressScanner = progressScanner;
    }

    public void VirusFinished()
    {
        foreach (GameObject appsIcon in AppsToTurnOff)
        {
            appsIcon.SetActive(true);
        }
        foreach (GameObject appsIcon in AppsToTurnOn)
        { 
            appsIcon.SetActive(false);
        }
        tipsUI.gameObject.SetActive(false);
        notesUI.gameObject.SetActive(false);
        DesktopController.instance.AddFinishedComputer(parentComputer);
        gameObject.SetActive(false);
    }
}
