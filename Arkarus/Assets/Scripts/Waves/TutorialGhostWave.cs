using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGhostWave : Wave
{
    public override void WaveStart(Update upd)
    {
        gameObject.SetActive(true);
        base.WaveStart(upd);
    }

 

    public override void WaveEnd()
    {
        gameObject.SetActive(false);
        base.WaveEnd();
    }
}
