using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    public Rigidbody r;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (r.velocity.sqrMagnitude > 0)
            transform.rotation = Quaternion.LookRotation(r.velocity);
    }
}
