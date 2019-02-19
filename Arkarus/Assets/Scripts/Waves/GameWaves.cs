using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWaves : Wave
{
    public override void WaveStart(Update upd)
    {
        GameManager.Instance.spawner.StartSpawning();
        base.WaveStart(upd);
    }
}
