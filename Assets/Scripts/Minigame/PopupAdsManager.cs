using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupAdsManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> popUpList;
    [SerializeField] private Computer parentComputer;

    [SerializeField] private Tips tipsUI;
    [SerializeField] private Notes notesUI;

    [SerializeField] private TipsSO tips;
    [SerializeField] private NotesSO notes;
    
    private float popUpMaxTime = 1.5f;
    private float popUpMinTime;
    private int totalPupUpCount=0;
    private int totalnumberOfPopups = 10;

    private List<GameObject> closedPopupList;

    public static PopupAdsManager instance { get; private set; }

    private void Start()
    {
        instance = this; 
        closedPopupList = new List<GameObject>();
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
            if (totalPupUpCount < 10)
            {
                popUpList[totalPupUpCount].SetActive(true);
                totalPupUpCount++;
            }
        }
        if (closedPopupList.Count == totalnumberOfPopups)
        {
            DesktopController.instance.AddFinishedComputer(parentComputer);
            gameObject.SetActive(false);
            tipsUI.gameObject.SetActive(false);
            notesUI.gameObject.SetActive(false);
        }
    }

 

    public void addClosePupup(GameObject pupUp)
    { 
        closedPopupList.Add(pupUp);
    }

    

}
