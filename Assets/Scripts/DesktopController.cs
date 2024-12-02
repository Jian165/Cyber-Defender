using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Build;
using UnityEngine;

public class DesktopController : MonoBehaviour
{
    [SerializeField] List<Computer> computers;

    [SerializeField] TimeController timeController;

    [SerializeField] GameObject Win;
    [SerializeField] GameObject Loose;
    [SerializeField] int computerErrorCount = 5;

    private GameObject WinOrLoosObject;

    private List<Computer> computersWithError;
    private List<Computer> computerSuccessd;

    public EventHandler OnDayChange;
    private bool NightTimeCurrentState;
    private bool JustStarted = true;
    

    public static DesktopController instance { get; private set; }
    
    private void Start()
    {
        computersWithError = new List<Computer>();
        computerSuccessd = new List<Computer>();

        timeController.OnNightTime += OnNightTime_EnableAttack;
        instance = this;

    }

    private void Update()
    {
        if (computerSuccessd.Count >= computerErrorCount )
        {
            Debug.Log("game successes");
            if (WinOrLoosObject == null)
            {
                GameInput.instance.ForceExitInteract();
                Player.instance.isPayerWinOrLose();
                Win.SetActive(true);
                WinOrLoosObject = Win;
            }
        }

        if (computerSuccessd.Count != computerErrorCount && NightTimeCurrentState == false)
        {
            if (!JustStarted)
            {
                Debug.Log("Game Over!");

                if (WinOrLoosObject == null)
                {
                    GameInput.instance.ForceExitInteract();
                    Player.instance.isPayerWinOrLose();
                    Loose.SetActive(true);
                    WinOrLoosObject = Loose;
                }

            }

            JustStarted = false;
        }

    }

    private void OnNightTime_EnableAttack(object sender, TimeController.OnNightTimeEventArgs e)
    {

        if (e.isNightTime == true)
        {
            if (NightTimeCurrentState != e.isNightTime)
            {
                ComputerPicker();
                ActivateErrors(e.isNightTime);
                NightTimeCurrentState = e.isNightTime;
            }
        }
        else
        {
            if (NightTimeCurrentState != e.isNightTime)
            {
                ActivateErrors(e.isNightTime);
                NightTimeCurrentState = e.isNightTime;
            }

        }

    }
  
    private void ComputerPicker()
    {
        computersWithError = new List<Computer>();
        while (computersWithError.Count != computerErrorCount)
        {
            int selectedIndex = UnityEngine.Random.Range(0,computers.Count);
            if (!computersWithError.Contains(computers[selectedIndex]))
            {
                computersWithError.Add(computers[selectedIndex]);
            }
        }

    }
    private void ActivateErrors(bool isActive)
    {
        if (isActive)
        {
            foreach (Computer computerError in computersWithError)
            {
                computerError.EnableAttack();
            }
        }
        else
        {
            if (computersWithError != null)
            {
                foreach (Computer computerError in computersWithError)
                {
                    computerError.DisableAttack();
                }
                
                computersWithError.Clear();
            }

        }

    }

    public void AddFinishedComputer(Computer computer)
    {
        if (computersWithError.Contains(computer))
        {
            computer.DisableAttack();
            computersWithError.Remove(computer);
            computerSuccessd.Add(computer);
        }
    }
}
