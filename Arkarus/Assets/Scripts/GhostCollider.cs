using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCollider : MonoBehaviour
{
    public Ghost ghostController;

    private void OnTriggerEnter(Collider other)
    {
        ghostController.OnTriggerCollide(other);
    }
}
