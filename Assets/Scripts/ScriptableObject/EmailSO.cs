using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu ]
public class EmailSO : ScriptableObject
{
    public string senderName;
    public Sprite senderProfile;
    public string senderSubject;

    [TextArea(10,100)]
    public string senderMessage;
    public bool isMalicious;
        
}
