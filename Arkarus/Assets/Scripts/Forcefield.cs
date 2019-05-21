using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forcefield : MonoBehaviour
{
    [SerializeField] Material fieldMaterial;
    [SerializeField] LayerMask hitLayer;
    [SerializeField] float exterminateRadius, zapSoulCost = 1;
    [SerializeField] GameObject zapObject;
    GhostPooler ghosts;
    ComputeBuffer buff;
    Pooler zapPooler;
    SoulJar jar;


    // Start is called before the first frame update
    void Start()
    {
        ghosts = GameManager.Instance.spawner.GetGhostPooler();
        jar = GameManager.Instance.waves.jar;
        zapPooler = new Pooler(5, zapObject);
    }

    // Update is called once per frame
    void Update()
    {
        Vector4[] positions = new Vector4[25];
        Color[] colors = new Color[25];

        for (int i = 0; i < ghosts.active.Count; i++)
        {
            Vector3 p = ghosts.active[i].transform.position;
            Ghost g = ghosts.active[i].GetComponent<Ghost>();
            RaycastHit[] hits = Physics.RaycastAll(p, (transform.position - p), 500f, hitLayer);
            if (hits.Length < 1)
                continue;
            if (hits.Length < 2)
            {
                positions[i] = hits[0].point;
            }
            else
            {
                positions[i] = ((hits[0].point.sqrMagnitude) < (hits[1].point.sqrMagnitude)) ? hits[0].point : hits[1].point;
            }
            positions[i].w = Vector3.Distance(transform.position, p);
            if(positions[i].w < exterminateRadius)
            {
                ZapGhost(g);
            }
            colors[i] = g.renderer.sharedMaterial.color;
        }
        fieldMaterial.SetVectorArray("ghostsPos", positions);
        fieldMaterial.SetColorArray("ghostsColors", colors);

        fieldMaterial.SetInt("ghostCount", ghosts.active.Count);
    }

    void ZapGhost(Ghost g)
    {
        GameManager.Instance.waves.currentWave.currentProgress -= zapSoulCost;
        g.Die(false);
        GameObject z = zapPooler.getObject();
        z.transform.parent = transform;
        z.transform.position = jar.transform.position;
        z.GetComponent<ForcefieldZap>().SetTarget(g.transform.position);
    }
}
