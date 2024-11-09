using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ErrorPicker : MonoBehaviour
{
    [SerializeField]
    List<Computer> computers;
    System.Random random = new System.Random();
    List<Computer> computersWithError;

    public EventHandler OnDayChange;


    private void Awake()
    {
        ComputerPicker();
        ActivateErrors();

    }

    public void ComputerPicker()
    {
        computersWithError = new List<Computer>();
        while (computersWithError.Count != 5)
        {
            int selectedIndex = random.Next(0, computers.Count - 1);
            if (!computersWithError.Contains(computers[selectedIndex]))
            {
                computersWithError.Add(computers[selectedIndex]);
            }
        }

    }
    public void ActivateErrors()
    {
        foreach (Computer computerError in computersWithError)
        {
            computerError.hasError();
        }
    }

 
}
