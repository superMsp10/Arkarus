using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public Wave startWave;
    public SoulJar jar;

    private void Start()
    {
        StartWave();
    }

    public void StartWave()
    {
        startWave.WaveStart(OnWaveUpdate);
    }

    public void OnWaveUpdate(float updatePercent)
    {
        Debug.Log("Update Wave " + updatePercent);
        jar.SetSouls(updatePercent);
    }

    public void OnWaveEnd(int nextWave)
    {

    }
}
