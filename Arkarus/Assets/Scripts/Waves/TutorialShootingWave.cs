using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialShootingWave : Wave
{
    public Bow bow;
    public override void StartWave(WaveUpdate upd)
    {
        gameObject.SetActive(true);
        bow.OnShoot += OnPlayerBowShoot;
        base.StartWave(upd);
    }

    void OnPlayerBowShoot(float percentage)
    {
        currentProgress += percentage;
        UpdateWave();
    }

    public override void EndWave()
    {
        gameObject.SetActive(false);
        bow.OnShoot -= OnPlayerBowShoot;
        base.EndWave();
    }
}
