using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWaves : Wave
{
    public GhostSpawner spawner;
    public int iteration;

    private void Start()
    {
        spawner.GhostDeath += OnGhostDeath;
    }

    public override void WaveStart(Update upd)
    {
        iteration++;
        WaveName = "Game Wave " + iteration;
        spawner.StartSpawning();
        spawner.ghostSpawnRate += iteration;
        spawner.spawnRangeMax += iteration;
        spawner.spawnRangeMin += iteration;

        base.WaveStart(upd);
        spawner.StartSpawning();
    }

    public void OnGhostDeath(float count)
    {
        currentProgress += count;
    }
}
