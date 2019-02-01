using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialShootingWave : Wave
{
    public Bow bow;
    public override void WaveStart(Update upd)
    {
        gameObject.SetActive(true);
        base.WaveStart(upd);
        bow.OnShoot += OnPlayerBowShoot;
    }

    void OnPlayerBowShoot(float percentage)
    {
        currentProgress += percentage;
        base.WaveUpdate();
    }

    public override void WaveEnd()
    {
        gameObject.SetActive(false);
        base.WaveEnd();
        bow.OnShoot -= OnPlayerBowShoot;
    }
}
