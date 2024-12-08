using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    private Animator animator;
    private const string IS_OPEN = "IsOpen";
    private bool doorState = false;
    private AudioSource doorAudio;
    [SerializeField] private AudioClip doorOpeningClip;
    [SerializeField] private AudioClip doorCloseClip;

    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Start()
    {
        doorAudio = GetComponent<AudioSource>();
        CustomPormt();
    }

    public override void Interact(Player player)
    {
        if (!doorState)
        {
            doorAudio.clip = doorOpeningClip;
            doorAudio.Play();
        }
        else
        { 
            doorAudio.clip = doorCloseClip;
            doorAudio.Play();
        
        }

        doorState = !doorState;
        animator.SetBool(IS_OPEN, doorState);
        CustomPormt();
    }

    public void CustomPormt()
    {
        if (doorState)
        {
            SetCustomPrompt = "[E] To Close ";
        }
        else
        {
            SetCustomPrompt = "[E] To Open";
        }
    }



}
