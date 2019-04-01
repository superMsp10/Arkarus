using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialShootingWave : Wave
{
    public Bow bow;
    public override void StartWave(WaveUpdate upd)
    {
        gameObject.SetActive(true);
        base.StartWave(upd);
        bow.OnShoot += OnPlayerBowShoot;
    }

    void OnPlayerBowShoot(float percentage)
    {
        currentProgress += percentage;
        base.UpdateWave();
    }

    public override void EndWave()
    {
        gameObject.SetActive(false);
        base.EndWave();
        bow.OnShoot -= OnPlayerBowShoot;
    }
}
