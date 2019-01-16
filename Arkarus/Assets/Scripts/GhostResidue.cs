using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostResidue : MonoBehaviour, Poolable
{
    public float clearTime = 2f;
    public Renderer renderer;

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
            Invoke("clearResidue", clearTime);
        }
    }

    public void CopyfromGhost(Ghost dead)
    {
        //TODO: Copy face expressions sprite and exact positions
        renderer.material.color = dead.renderer.material.color;
    }

    public void ClearResidue()
    {
        thisPooler.disposeObject(this); 
    }
}
