using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWaves : Wave
{
    [SerializeField] GhostSpawner spawner;
    [SerializeField] int iteration, arrowCost;
    [SerializeField] Bow bow;

    public override string waveName
    {
        get
        {
            return _waveName + " " + iteration;
        }
    }

    void OnPlayerBowShoot(float percentage)
    {
        currentProgress -= arrowCost;
        UpdateWave();
    }

    public override void BeforeWaveStart()
    {
        spawner.GhostDeath += OnGhostDeath;
        bow.OnShoot += OnPlayerBowShoot;
        IncreaseDifficulty();
    }

    void IncreaseDifficulty()
    {
        iteration++;
        totalProgress += iteration;
        spawner.ghostSpawnRate += iteration;
    }

    public override void StartWave(WaveUpdate upd)
    {
        base.StartWave(upd);

        Debug.Log((int)(totalProgress - currentProgress));
        spawner.StartSpawning((int)(totalProgress - currentProgress));
    }

    public void OnGhostDeath(float count)
    {
        currentProgress += count;
    }

    public override void RestartWave()
    {
        spawner.ResetSpawns();
        base.RestartWave();
    }

    public override void EndWave()
    {
        spawner.GhostDeath -= OnGhostDeath;
        bow.OnShoot -= OnPlayerBowShoot;
        spawner.ResetSpawns();
        base.EndWave();
    }


}
