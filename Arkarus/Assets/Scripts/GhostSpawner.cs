using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    public GameObject ghostPrefab;
    public float spawnRange;
    public int maxGhostCount, ghostSpawnRate;
    Pooler ghostPooler;

    // Start is called before the first frame update
    void Start()
    {
        ghostPooler = new Pooler(maxGhostCount, ghostPrefab);
        InvokeRepeating("SpawnGhosts", 0f, ghostSpawnRate);
    }

    public void SpawnGhosts()
    {
        if (ghostPooler.active.Count < maxGhostCount)
        {
           GameObject g = ghostPooler.getObject();
            g.transform.position = Random.onUnitSphere * spawnRange;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
