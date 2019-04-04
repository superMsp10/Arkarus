using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public Wave startWave;
    public Wave currentWave;
    public SoulJar jar;
    public TextMeshProUGUI waveText;
    public ParticleSystem effectParticles;
    public Camera effectCamera;
    public string startPreface = "Starting";
    public string endPreface = "Ending";
    ParticleSystem.MainModule effectParticlesMain;



    private void Start()
    {
        currentWave = startWave;
        effectParticlesMain = effectParticles.main;
        EndAnimation();
        StartWave();
    }

    public void StartWave()
    {
        WaveStartAnimation();
        Invoke("_StartWave", 2f);
    }

    void _StartWave()
    {
        Debug.Log("Starting wave " + currentWave.waveName);
        currentWave.StartWave(OnWaveUpdate);
    }

    void WaveStartAnimation()
    {
        // Preface, -Title-, Ending
        waveText.text = string.Format("<size=100><color=#00ff00ff>{0}</color></size=100>\n-{1}-\n<size=50>{2}</size=50>", startPreface, currentWave.waveName, currentWave.startDescription);
        StartAnimation();
    }

    void WaveEndAnimation()
    {
        waveText.text = string.Format("<size=100><color=#00ff00ff>{0}</color></size=100>\n-{1}-\n<size=50>{2}</size=50>", endPreface, currentWave.waveName, currentWave.endDescription);
        StartAnimation();
    }

    void StartAnimation()
    {
        waveText.gameObject.SetActive(true);
        effectCamera.enabled = true;
        effectParticlesMain.loop = true;
        effectParticles.Play();
        Invoke("EndAnimation", 2f);
    }

    void EndAnimation()
    {
        waveText.gameObject.SetActive(false);
        effectParticlesMain.loop = false;
        effectCamera.enabled = false;
    }

    public void TransitionToNewWave(Wave n)
    {
        WaveEndAnimation();
        currentWave = n;
        Invoke("StartWave", 2f);
    }

    public void OnWaveUpdate(float updatePercent)
    {
        Debug.Log("Update Wave " + updatePercent);
        jar.SetSouls(updatePercent);
    }
}
