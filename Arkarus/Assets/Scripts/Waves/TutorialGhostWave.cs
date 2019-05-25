using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGhostWave : Wave
{
    public GameObject ghostIns;

    public override void StartWave(WaveUpdate upd)
    {
        gameObject.SetActive(true);
        base.StartWave(upd);
        Camera player = GameManager.Instance.FirstPersonCamera;
        Vector3 frustrumDest = player.ViewportPointToRay(Vector3.one/2).direction * 2;

        GameObject o = Instantiate(ghostIns, frustrumDest * 2f, Quaternion.identity, GameManager.Instance.spawner.ghostsTransform);
        Ghost g = o.GetComponent<Ghost>();
        g.OnDeath = SoulCount =>{ currentProgress+=1; };
        g.reset(true);

    }





    public override void EndWave()
    {
        gameObject.SetActive(false);
        base.EndWave();
    }
}
