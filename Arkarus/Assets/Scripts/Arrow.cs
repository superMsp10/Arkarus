using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour, Poolable
{

    public Rigidbody r;

    public Pooler thisPooler { get; set; }
    public float resetTime = 5f;

    GameObject Poolable.pooledGameObject
    {
        get
        {
            return gameObject;
        }
    }

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

    public void reset(bool on)
    {
        gameObject.SetActive(on);
        if (on)
        {
            Invoke("arrowRecycle", 10f);
            transform.rotation = Quaternion.identity;
            r.velocity = Vector3.zero;
        }
    }

    void arrowRecycle()
    {
        thisPooler.disposeObject(this);
    }

}
