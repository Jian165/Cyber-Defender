using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Computer : Interactable
{
    [SerializeField] private GameObject computerUI;
    [SerializeField] private GameObject warningImage;
    [SerializeField] private GameObject warningUI;
    [SerializeField] private Light screenLight;
    [SerializeField] private TimeController timeController;

    bool screenLightStatus;
    public void Start()
    {
        timeController.OnDayChange += OnTimeController_Change;
        screenLight.enabled = screenLightStatus;
    }

    private void OnTimeController_Change(object sender, TimeController.OnDayChangeEventArgs e)
    {
        screenLight.enabled = e.isNightTime;
    }

    public override void Interact(Player player)
    {
        // activate computer ui
        computerUI.SetActive(!computerUI.activeSelf);

        Debug.Log("Interacted With " + gameObject.name);

        // remove the player ui if the player is in computer ui
        player.isPlayerInComputer(computerUI.activeSelf);
    }

    public IEnumerator WarningLoopImage()
    {
        while (true)
        {
            warningImage.SetActive(!warningImage.activeSelf);
            yield return new WaitForSeconds(1f);
        }
    }

    public void hasError()
    { 
        warningUI.SetActive(true);
        StartCoroutine(WarningLoopImage());
    }
}
