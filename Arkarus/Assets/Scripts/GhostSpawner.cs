using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    public GameObject[] ghostPrefabs;
    public float spawnDistance = 5f, frustrumDestOffest = 0.5f;
    public int maxGhostCount, ghostSpawnRate;
    public Transform ghostsTransform;
    Pooler ghostPooler;
    Camera fpCam;

    public GameObject ghostResiduePrefab;
    public Ghost.GhostUpdate GhostDeath;
    Pooler ghostResiduePooler;

    public Transform portal;

    // Start is called before the first frame update
    void Start()
    {
        fpCam = GameManager.Instance.FirstPersonCamera;
        ghostPooler = new GhostPooler(maxGhostCount, ghostPrefabs);
        ghostResiduePooler = new Pooler(Mathf.Max(maxGhostCount / 4, 1), ghostResiduePrefab);
    }

    public void StartSpawning(int spawnAmount)
    {
        //InvokeRepeating("SpawnGhosts", 0f, ghostSpawnRate);
        portal.transform.position = Camera.main.transform.forward * spawnDistance;
        portal.gameObject.SetActive(true);
        StartCoroutine(SpawnGhosts(spawnAmount));
    }

    public void ResetSpawns()
    {
        ghostPooler.DeactivateAll();
    }

    public GhostPooler GetGhostPooler()
    {
        return (GhostPooler)ghostPooler;
    }

    public void SpawnGhostResidue(Ghost dead, bool animateSoul)
    {
        GameObject g = ghostResiduePooler.getObject();
        g.transform.parent = ghostsTransform;
        g.transform.position = dead.transform.position;
        g.transform.rotation = dead.transform.rotation;
        g.GetComponent<GhostResidue>().CopyfromGhost(dead, animateSoul);
    }

    public IEnumerator SpawnGhosts(int spawnAmount)
    {
        for (int i = 0; i < spawnAmount; i++)
        {

            Debug.Log("Spawn " + i);
            GameObject g = ghostPooler.getObject();
            Debug.Log(g);
            g.SetActive(true);
            g.transform.position = portal.transform.position;
            g.transform.parent = ghostsTransform;
            yield return new WaitForSeconds(1f);
        }
        portal.gameObject.SetActive(false);
    }
}
