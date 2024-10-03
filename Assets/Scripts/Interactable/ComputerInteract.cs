using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerInteract : Interactable
{
    public GameObject uiPanel; 
    private bool isPlayerNearby = false;

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {

    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            Debug.Log("Player left the interaction zone.");
            uiPanel.SetActive(false);
        }
    }
    protected override void Interact()
    {
        uiPanel.SetActive(!uiPanel.activeSelf);
    }
}