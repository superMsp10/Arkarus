using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wave : MonoBehaviour
{
    public delegate void Update(float progressPercent);
    public Update OnUpdate;
    public int currentProgress;
    public int totalProgress;
    public Wave nextWave;
    public string WaveName;

    public virtual void WaveStart(Update upd)
    {
        OnUpdate = upd;
    }

    public virtual void WaveUpdate()
    {
        currentProgress++;
        OnUpdate(((float)currentProgress) / totalProgress);
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
