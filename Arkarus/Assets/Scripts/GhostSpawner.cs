using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    public GameObject[] ghostPrefabs;
    public float spawnRangeMin, spawnRangeMax, frustrumDestOffest = 0.5f;
    public int maxGhostCount, ghostSpawnRate;
    public Transform ghostsTransform;
    Pooler ghostPooler;
    Camera fpCam ;

    // Start is called before the first frame update
    void Start()
    {
        fpCam = GameManager.Instance.FirstPersonCamera;
        ghostPooler = new GhostPooler(maxGhostCount, ghostPrefabs);
        InvokeRepeating("SpawnGhosts", 0f, ghostSpawnRate);
    }

    public void SpawnGhosts()
    {
        if (ghostPooler.active.Count < maxGhostCount)
        {
            GameObject g = ghostPooler.getObject();
            //Camera frustrum has 4 lines projecting out of the camera
            //      0
            //   A --- B
            // 3 |     | 1
            //   C --- D
            //      2
            //Chose random point on a side to start from
            //Get direction using viewport point to world raycast
            //Frustrum destination = direction * random depth
            //Offset Direction: Difference between camera center position and frustrum destination normalized
            // Final position = frustrum destination + offset direciton * randomMagnitude
            float r_depth = Random.Range(spawnRangeMin, spawnRangeMax);
            Vector2 pointOnViewportEdge = Vector2.zero;
            int r_side = Random.Range(0, 4);
            float r_sideMagnitude = Random.Range(0.0f, 1f);

            switch (r_side)
            {
                case 0:
                    pointOnViewportEdge = new Vector2(r_sideMagnitude, 0);
                    break;
                case 1:
                    pointOnViewportEdge = new Vector2(1, r_sideMagnitude);
                    break;
                case 2:
                    pointOnViewportEdge = new Vector2(r_sideMagnitude, 1);
                    break;
                case 3:
                    pointOnViewportEdge = new Vector2(0, r_sideMagnitude);
                    break;
                default:
                    pointOnViewportEdge = Vector2.zero;
                    break;
            }


            Vector3 frustrumDest = fpCam.ViewportPointToRay(pointOnViewportEdge).direction * r_depth;
            Vector3 offset = (frustrumDest - fpCam.transform.forward * r_depth).normalized * frustrumDestOffest;
            g.transform.position = frustrumDest + offset;
            g.transform.parent = ghostsTransform;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
