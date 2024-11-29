using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Notes : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private Button okeyButton;
    [SerializeField] private TextMeshProUGUI notePrompt;

    private NotesSO note;
    private void Start()
    {
        if (note != null)
        {
            notePrompt.text = note.NotesContent;
        }

        closeButton.onClick.AddListener(OnCloseNotes);
        okeyButton.onClick.AddListener(OnCloseNotes);

    }

    private void OnCloseNotes()
    {
        gameObject.SetActive(false);
    }

    public void GetNote(NotesSO promt)
    {
        note = promt; 
    }
}
