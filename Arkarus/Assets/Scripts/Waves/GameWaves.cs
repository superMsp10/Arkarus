using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameWaves : Wave
{
    [SerializeField] GhostSpawner spawner;
    [SerializeField] int iteration, arrowCost, coins;
    [SerializeField] Bow bow;

    public TextMeshProUGUI text;

    public override string waveName
    {
        get
        {
            return _waveName + " " + iteration;
        }
    }

    private void Awake()
    {
        coins = PlayerPrefs.GetInt("score");
        text.text = coins.ToString();
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("score", coins);
        PlayerPrefs.Save();
    }

    void OnPlayerBowShoot(float percentage)
    {
        //currentProgress -= arrowCost;
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
        coins += (int)totalProgress;
        text.text = coins.ToString();
        totalProgress++;
        spawner.ghostSpawnRate += iteration;
    }

    public override void StartWave(WaveUpdate upd)
    {
        base.StartWave(upd);

        Debug.Log((int)(totalProgress));
        spawner.StartSpawning((int)(totalProgress));
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
