using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forcefield : MonoBehaviour
{
    [SerializeField] Material fieldMaterial;
    [SerializeField] LayerMask hitLayer;
    GhostPooler ghosts;
    ComputeBuffer buff;

    // Start is called before the first frame update
    void Start()
    {
        ghosts = GameManager.Instance.spawner.GetGhostPooler();
    }

    // Update is called once per frame
    void Update()
    {
        Vector4[] positions = new Vector4[25];
        Color[] colors = new Color[25];

        for (int i = 0; i < ghosts.active.Count; i++)
        {
            Vector3 p = ghosts.active[i].transform.position;
            RaycastHit[] hits = Physics.RaycastAll(p, (transform.position - p), 500f, hitLayer);
            if (hits.Length < 1)
                continue;
            if (hits.Length < 2)
            {
                positions[i] = hits[0].point;
            }
            else
            {
                positions[i] = ((hits[0].point.sqrMagnitude) < (hits[1].point.sqrMagnitude)) ? hits[0].point : hits[1]. point;
            }
            positions[i].w = Vector3.Distance(transform.position, p);
            colors[i] = ghosts.active[i].GetComponent<Ghost>().renderer.sharedMaterial.color;
        }
        fieldMaterial.SetVectorArray("ghostsPos", positions);
        fieldMaterial.SetColorArray("ghostsColors", colors);

        fieldMaterial.SetInt("ghostCount", ghosts.active.Count);
    }
}
