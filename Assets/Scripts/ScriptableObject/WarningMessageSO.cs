using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WarningMessageSO : ScriptableObject
{
    [TextArea(10,20)]
    public string warningText;
}
   