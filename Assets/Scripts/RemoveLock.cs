using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveLock : MonoBehaviour
{
    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
