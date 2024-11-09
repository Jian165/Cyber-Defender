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

    private bool NightTimeCurrentState;

    bool screenLightStatus;
    public void Start()
    {
        timeController.OnNightTime += OnNightTime_ComputerLigthsOn;
        screenLight.enabled = screenLightStatus;
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

    private Coroutine warningCoroutine;

    public void EnableAttack()
    { 
        warningUI.SetActive(true);
        warningCoroutine = StartCoroutine(WarningLoopImage());
        screenLight.color = Color.red;
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
