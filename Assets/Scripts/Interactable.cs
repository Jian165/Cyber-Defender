using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string promtMessage;

    public virtual string SetCustomPrompt
    {
        set{ promtMessage = value;}
    }

    public virtual void Interact(Player player)
    { 
        
    }

    public virtual void AlternativeInteract(Player player)
    { 
        
    }

    public virtual void GetCurrentTime(string currentTime)
    { 
        
    }
 }
