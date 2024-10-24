using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTaskKiller : MonoBehaviour
{
    public GameObject taskKillerPannel;

    void Start()
    {
        taskKillerPannel.SetActive(false);
    }

    public void ToggleSubPanel()
    {
        taskKillerPannel.SetActive(!taskKillerPannel.activeSelf);
    }
}
