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

    public override void WaveStart(Update upd)
    {
        gameObject.SetActive(true);
        base.WaveStart(upd);
    }


    public override void WaveUpdate()
    {
        base.WaveUpdate();
    }


    public override void WaveEnd()
    {
        gameObject.SetActive(false);
        base.WaveEnd();
    }
}
