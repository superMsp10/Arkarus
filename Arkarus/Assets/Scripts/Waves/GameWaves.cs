using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWaves : Wave
{
    public GhostSpawner spawner;
    public override void WaveStart(Update upd)
    {
        GameManager.Instance.spawner.StartSpawning();
        base.WaveStart(upd);
        spawner.StartSpawning();
    }
}
