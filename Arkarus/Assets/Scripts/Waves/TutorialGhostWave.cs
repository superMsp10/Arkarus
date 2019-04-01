using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGhostWave : Wave
{
    public override void StartWave(WaveUpdate upd)
    {
        gameObject.SetActive(true);
        base.StartWave(upd);
    }



    public override void EndWave()
    {
        gameObject.SetActive(false);
        base.EndWave();
    }
}
