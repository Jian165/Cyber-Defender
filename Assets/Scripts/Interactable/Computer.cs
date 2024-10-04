using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : Interactable
{
    [SerializeField] private GameObject computerUI;
    private bool isPlayerNearby = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            Debug.Log("Player left the interaction zone.");
            computerUI.SetActive(isPlayerNearby);
        }
    }
    protected override void Interact()
    {
        computerUI.SetActive(!computerUI.activeSelf);
        Debug.Log("Interacted With " + gameObject.name);
    }
}
