using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TipsSO: ScriptableObject
{
    [TextArea(4, 10)]
    public List <string> TipsContent;

}
