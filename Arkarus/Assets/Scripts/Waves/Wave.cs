using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wave : MonoBehaviour
{
    public delegate void WaveUpdate(float progressPercent);
    public WaveUpdate OnUpdate;
    public bool active = true;

    public float totalProgress, _currentProgress, startProgress = 5;
    [SerializeField] protected string _waveName = "Wave",
        _startDescription = "<color=white>Be Aware!</color>",
        _endDescription = "<color=red>You destroyed innocent bedsheets!</color>",
        _resetDescription = "<color=black>Progress reached xero!</color>";



    public virtual string waveName
    {
        get { return _waveName; }
    }

    public string startDescription
    {
        get { return _startDescription; }
    }

    public string endDescription
    {
        get { return _endDescription; }
    }

    public string resetDescription
    {
        get { return _resetDescription; }
    }


    public float currentProgress
    {
        get
        {
            return _currentProgress;
        }
        set
        {
            _currentProgress = value;
            UpdateWave();
        }
    }

    public Wave nextWave;

    public virtual void BeforeWaveStart()
    {

    }

    public virtual void StartWave( WaveUpdate upd)
    {
        OnUpdate = upd;
        active = true;
        ResetWave();
    }

    public virtual void ResetWave()
    {
        currentProgress = startProgress;
        //Automatically updates wave through currentProgress param
    }

    public virtual void RestartWave()
    {
        GameManager.Instance.waves.WaveRestartAnimation();
        ResetWave();
    }

    public virtual void UpdateWave()
    {
        if (!active) return;
        float progress = (currentProgress) / totalProgress;

        OnUpdate(progress);
        if (progress >= 1)
        {
            EndWave();

        }
        if (progress < 0)
        {
            RestartWave();
        }
    }

    public virtual void EndWave()
    {
        active = false;
        if (nextWave == null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            GameManager.Instance.waves.TransitionToNewWave(nextWave);
        }
    }
}
