using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialProgressWave : Wave
{
    public void IncreaseProgress()
    {
        currentProgress++;
    }

    public void DecreaseProgress()
    {
        currentProgress--;
    }

    public override void StartWave(WaveUpdate upd)
    {
        gameObject.SetActive(true);
        base.StartWave(upd);
    }


    public override void UpdateWave()
    {
        base.UpdateWave();
    }


    public override void EndWave()
    {
        gameObject.SetActive(false);
        base.EndWave();
    }
}
