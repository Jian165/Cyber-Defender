using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Computer : Interactable
{
    [SerializeField] private GameObject computerUI;
    [SerializeField] private GameObject warningImage;
    [SerializeField] private GameObject warningUI;
    [SerializeField] private Light screenLight;
    [SerializeField] private TextMeshProUGUI computerTimer;
    [SerializeField] private TimeController timeController;

    [SerializeField] private int Night;

    [SerializeField] private List<GameObject> miniGameListNight1;
    [SerializeField] private List<GameObject> miniGameListNight2;
    [SerializeField] private List<GameObject> miniGameListNight3;


    private List<GameObject> miniGameList;


    private bool isComputerUnderAttack;
    private GameObject SelectedMinigame;


    private bool NightTimeCurrentState;
    
    public static Computer instance {  get; private set; } 

    public void Start()
    {
        instance = this;
        timeController.OnNightTime += OnNightTime_ComputerLigthsOn;
        if (Night == 1)
        {
            miniGameList = miniGameListNight1;
        }
        else if (Night == 2)
        {
            miniGameList = miniGameListNight2;
        }
        else
        {
            miniGameList = miniGameListNight3;
        }


    }

    private void OnNightTime_ComputerLigthsOn(object sender, TimeController.OnNightTimeEventArgs e)
    {
        if (e.isNightTime == true)
        {
            if (NightTimeCurrentState != e.isNightTime)
            {
                screenLight.enabled = e.isNightTime;
                NightTimeCurrentState = e.isNightTime;
            }
        }
        else
        {
            if (NightTimeCurrentState != e.isNightTime)
            {
                screenLight.enabled = e.isNightTime;
                NightTimeCurrentState = e.isNightTime;
            }
        }
    }

    public override void GetCurrentTime(string currentime)
    {
        computerTimer.text = "Current Time: "+currentime;
    }

  
    public override void AlternativeInteract(Player player)
    {
        computerUI.SetActive(false);
        player.isPlayerInComputer(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public override void Interact(Player player)
    {
        computerUI.SetActive(true);
        player.isPlayerInComputer(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public IEnumerator WarningLoopImage()
    {
        while (true)
        {
            warningImage.SetActive(!warningImage.activeSelf);
            yield return new WaitForSeconds(1f);
        }
    }

    private Coroutine warningCoroutine;

    public void EnableAttack()
    { 
        warningUI.SetActive(true);
        warningCoroutine = StartCoroutine(WarningLoopImage());
        screenLight.color = Color.red;

        if (SelectedMinigame == null)
        {
            //Select Minigame
            GameObject randomMinigame = miniGameList[UnityEngine.Random.Range(0,miniGameList.Count)];
            SelectedMinigame = randomMinigame;
            //activate Minigame
            randomMinigame.SetActive(true);
        }
    }

    public void DisableAttack()
    {
        if (warningCoroutine != null)
        {
            warningUI.SetActive(false);
            StopCoroutine(warningCoroutine);
            screenLight.color = new Color(0.741f, 0.961f, 0.933f);
        }

    }

  }
