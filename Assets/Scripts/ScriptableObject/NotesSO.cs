using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class NotesSO: ScriptableObject
{
    [TextArea(4, 10)]
    public string NotesContent;

}
