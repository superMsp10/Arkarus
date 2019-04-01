using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStartWave : Wave
{
    public GameObject tutStartPanel;
    public Wave gameStartWave;

    public override void StartWave(WaveUpdate upd)
    {
        gameObject.SetActive(true);
        base.StartWave(upd);
    }

    public void SkipToGameWave()
    {
        nextWave = gameStartWave;
        EndWave();
    }

    public override void EndWave()
    {
        gameObject.SetActive(false);
        base.EndWave();
    }

}
