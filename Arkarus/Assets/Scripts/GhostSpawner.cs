using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    public GameObject ghostPrefab;
    public float spawnRangeMin, spawnRangeMax, frustrumDestOffest = 0.5f;
    public int maxGhostCount, ghostSpawnRate;
    Pooler ghostPooler;
    Camera fpCam ;

    // Start is called before the first frame update
    void Start()
    {
        fpCam = GameManager.gameManager.FirstPersonCamera;
        ghostPooler = new Pooler(maxGhostCount, ghostPrefab);
        InvokeRepeating("SpawnGhosts", 0f, ghostSpawnRate);
    }

    public void SpawnGhosts()
    {
        if (ghostPooler.active.Count < maxGhostCount)
        {
            GameObject g = ghostPooler.getObject();
            //Camera frustrum has 4 lines projecting out of the camera
            // 1 --- 2
            // |     |
            // 3 --- 4
            //Chose random line to start from
            //Get direction using viewport point to world raycast
            //Frustrum destination = direction * random depth
            //Offset Direction: Difference between camera center position and frustrum destination normalized
            // Final position = frustrum destination + offset direciton * randomMagnitude
            float r_depth = Random.Range(spawnRangeMin, spawnRangeMax);
            Vector2 r_corner = new Vector2(Random.Range(0, 2), Random.Range(0, 2));
            Vector3 frustrumDest = fpCam.ViewportPointToRay(r_corner).direction * r_depth;
            Vector3 offset = (frustrumDest - fpCam.transform.forward * r_depth).normalized * frustrumDestOffest;
            g.transform.position = frustrumDest + offset;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
