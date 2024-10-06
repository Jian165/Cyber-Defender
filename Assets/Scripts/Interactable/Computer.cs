using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : Interactable
{
    [SerializeField] private GameObject computerUI;
    
    public override void Interact(Player player)
    {
        // activate computer ui
        computerUI.SetActive(!computerUI.activeSelf);

        Debug.Log("Interacted With " + gameObject.name);

        // remove the player ui if the player is in computer ui
        player.isPlayerInComputer(computerUI.activeSelf);
    }
}
