using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStartWave : Wave
{
    public GameObject tutStartPanel;
    public Wave gameStartWave;

    public override void WaveStart(Update upd)
    {
        gameObject.SetActive(true);
        base.WaveStart(upd);
    }

    public void SkipToGameWave()
    {
        gameObject.SetActive(false);
        gameStartWave.WaveStart(OnUpdate);
    }

    public override void WaveEnd()
    {
        gameObject.SetActive(false);
        base.WaveEnd();
    }

}
