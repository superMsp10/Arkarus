using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wave : MonoBehaviour
{
    public delegate void Update(float progressPercent);
    public Update OnUpdate;
    public float totalProgress, _currentProgress;

    public float currentProgress
    {
        get
        {
            return _currentProgress;
        }
        set
        {
            _currentProgress = value;
            WaveUpdate();
        }
    }

    public Wave nextWave;
    public string WaveName;

    public virtual void WaveStart(Update upd)
    {
        OnUpdate = upd;
        WaveReset();
    }

    public virtual void WaveReset()
    {
        currentProgress = 0;
        WaveUpdate();
    }

    public virtual void WaveUpdate()
    {
        float progress = (currentProgress) / totalProgress;
        OnUpdate(progress);
        if (progress >= 1)
            WaveEnd();
        if (progress < 0)
            WaveReset();
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
