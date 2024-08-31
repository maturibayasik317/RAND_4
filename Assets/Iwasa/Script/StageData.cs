using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "ScriptableObjects/StageData", order = 1)]
public class StageData : ScriptableObject
{
    public string stageName;
    public bool isUnlocked;
}
