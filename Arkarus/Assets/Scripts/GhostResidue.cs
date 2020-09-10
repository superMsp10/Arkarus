using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostResidue : MonoBehaviour, Poolable
{
    public float clearTime = 2f;
    float startTime = 0f;
    public new ParticleSystem particleSystem;
    public ParticleSystem soulParticleSystem;

    public SpriteRenderer[] faceElements;
    SoulJar jar;
    //bool animateSoul;


    public GameObject pooledGameObject
    {
        get
        {
            return gameObject;
        }
    }

    public Pooler thisPooler { get; set; }

    public void reset(bool on)
    {
        pooledGameObject.SetActive(on);
        if (on)
        {
            if (jar == null)
            {
                jar = GameManager.Instance.waves.jar;
            }
            startTime = Time.time;
            particleSystem.Play();
            //if (animateSoul)
            //{
            //    soulParticleSystem.Play();
            //}
            //else
            //{
            //    soulParticleSystem.Stop();
            //}

            Invoke("ClearResidue", clearTime);
        }
        else
        {
            particleSystem.Stop();
            soulParticleSystem.Stop();
            CancelInvoke();
            soulParticleSystem.transform.localPosition = Vector3.zero;
        }
    }

    private void Update()
    {
        //Vector3 x = Vector3.zero;
        //if (animateSoul)
        //    soulParticleSystem.transform.position = Vector3.SmoothDamp(soulParticleSystem.transform.position, jar.transform.position, ref x, Time.deltaTime * clearTime);
        foreach (var item in faceElements)
        {
            item.color = Color.Lerp(Color.white, Color.clear, (Time.time - startTime) / clearTime);
        }
    }

    public void CopyfromGhost(Ghost dead, bool animateSoul)
    {
        transform.position = dead.mesh.position;
        transform.rotation = dead.mesh.rotation;
        particleSystem.GetComponentInChildren<ParticleSystemRenderer>().material = dead.renderer.material;
        soulParticleSystem.GetComponentInChildren<ParticleSystemRenderer>().material.color = dead.renderer.material.color;
        faceElements[0].sprite = dead.eyeL.sprite;
        faceElements[1].sprite = dead.eyeR.sprite;
        faceElements[2].sprite = dead.mouth.sprite;
        faceElements[0].transform.position = dead.eyeL.transform.position;
        faceElements[1].transform.position = dead.eyeR.transform.position;
        faceElements[2].transform.position = dead.mouth.transform.position;
        //this.animateSoul = animateSoul;
    }

    public void ClearResidue()
    {
        thisPooler.DisposeObject(this);
    }
}
