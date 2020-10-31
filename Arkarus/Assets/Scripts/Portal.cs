using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public ParticleSystem orbParticles;
    ParticleSystem.MainModule mainParticles;
    public Color color1, color2;

    // Start is called before the first frame update
    void Start()
    {
        mainParticles = orbParticles.main;
    }

    // Update is called once per frame
    void Update()
    {
        Color flashColor = Color.Lerp(color1, color2, Mathf.Abs(Mathf.Sin(Time.time / 2)));
        mainParticles.startColor = flashColor;
    }
}
