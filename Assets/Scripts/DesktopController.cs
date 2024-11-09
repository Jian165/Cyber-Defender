using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Build;
using UnityEngine;

public class DesktopController : MonoBehaviour
{
    [SerializeField]
    List<Computer> computers;

    [SerializeField]
    TimeController timeController;

    List<Computer> computersWithError;

    public EventHandler OnDayChange;
    private bool NightTimeCurrentState;
    


    private void Start()
    {
        timeController.OnNightTime += OnNightTime_EnableAttack;

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
        while (computersWithError.Count != 5)
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


}
