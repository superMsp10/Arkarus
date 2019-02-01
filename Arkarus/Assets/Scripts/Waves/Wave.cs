using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wave : MonoBehaviour
{
    public delegate void Update(float progressPercent);
    public Update OnUpdate;
    public float currentProgress, totalProgress;
    public Wave nextWave;
    public string WaveName;

    public virtual void WaveStart(Update upd)
    {
        OnUpdate = upd;
    }

    public virtual void WaveUpdate()
    {
        float progress = (currentProgress) / totalProgress;
        OnUpdate(progress);
        if (progress >= 1)
            WaveEnd();
    }

    public virtual void WaveEnd()
    {
        if (nextWave == null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            nextWave.WaveStart(OnUpdate);
        }
    }
}
