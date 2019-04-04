using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWaves : Wave
{
    public GhostSpawner spawner;
    public int iteration;


    new public string waveName
    {
        get { return _waveName + " " + iteration; }
    }

    private void Start()
    {
        spawner.GhostDeath += OnGhostDeath;
    }

    public override void StartWave( WaveUpdate upd)
    {
        iteration++;
        _waveName = "Game Wave " + iteration;
        spawner.ghostSpawnRate += iteration;
        spawner.spawnRangeMax += iteration;
        spawner.spawnRangeMin += iteration;

        base.StartWave(upd);
        spawner.StartSpawning();
    }

    public void OnGhostDeath(float count)
    {
        currentProgress += count;
    }
}
