using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPooler : Pooler
{
    public GameObject[] originals;
    public GhostPooler(int max, GameObject[] ghostsToInstantiate) : base(max, ghostsToInstantiate[0])
    {
        originals = ghostsToInstantiate;
    }

    public override GameObject InstantiateObject()
    {
        GameObject o = Object.Instantiate(originals[Random.Range(0, originals.Length)]);
        o.GetComponent<Ghost>().OnDeath = GameManager.Instance.spawner.GhostDeath;
        return o;
    }
}
