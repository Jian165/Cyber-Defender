using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tips : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tipsPrompt;

    private float timeCycle;
    private float maxTimeCycle = 2;
    private TipsSO tips;

    private int listPointer = 0;

  
    // Update is called once per frame
    void Update()
    {
        timeCycle -= Time.deltaTime;

        if (timeCycle <= 0f)
        {
            timeCycle = maxTimeCycle;
            if (tips != null && listPointer < tips.TipsContent.Count)
            {
                tipsPrompt.text = tips.TipsContent[listPointer];
                listPointer++;
            }
            else
            { 
                listPointer = 0;
            }
        }
      
    }

    public void GetTips(TipsSO tips)
    {
        this.tips = tips;
    }
    
    
}
