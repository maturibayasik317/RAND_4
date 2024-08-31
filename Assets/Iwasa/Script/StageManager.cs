using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public List<StageData> stages;

    public void UnlockNextStage(int currentStageIndex)
    {
        if (currentStageIndex < stages.Count - 1)
        {
            stages[currentStageIndex + 1].isUnlocked = true;
            Debug.Log(stages[currentStageIndex + 1].stageName + "‚ª‰ð‹Ö‚³‚ê‚Ü‚µ‚½I");
        }
    }

    public bool IsStageUnlocked(int stageIndex)
    {
        return stages[stageIndex].isUnlocked;
    }
}
