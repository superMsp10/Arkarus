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
    public string startPreface = "Starting";
    public string endPreface = "Ending";

    public ParticleSystem textEffect;

    private void Start()
    {
        currentWave = startWave;
        StartWave();
    }

    public void StartWave()
    {
        StartWaveAnimation();
        Invoke("_StartWave", 2f);
    }

    void _StartWave()
    {
        currentWave.StartWave(OnWaveUpdate);
    }

    void StartWaveAnimation()
    {
        // Preface, -Title-, Ending
        Debug.Log("Starting wave " + currentWave.waveName);
        ParticleSystem.ShapeModule s = textEffect.shape;
        s.mesh = waveText.mesh;
        waveText.text = string.Format("<size=100><color=#00ff00ff>{0}</color></size=100>\n-{1}-\n<size=50>{2}</size=50>", startPreface, currentWave.waveName, currentWave.startDescription);
    }

     void WaveEndAnimation()
    {
        waveText.text = string.Format("<size=100><color=#00ff00ff>{0}</color></size=100>\n-{1}-\n<size=50>{2}</size=50>", endPreface, currentWave.waveName, currentWave.endDescription);
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
