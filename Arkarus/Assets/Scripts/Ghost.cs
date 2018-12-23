using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour, Poolable
{

    Vector3 finalPos;
    public float moveTime = 1f, moveDistance = 2f;

    GameObject Poolable.gameobject
    {
        get
        {
            return gameObject;
        }
    }

    public Pooler thisPooler { get ; set ; }

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Move());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Move()
    {
        while (true)
        {
            Vector3 deltaPlayer = GameManager.gameManager.FirstPersonCamera.transform.position - transform.position;
            finalPos = Vector3.MoveTowards(transform.position, Random.onUnitSphere + deltaPlayer, 1f);
            yield return new WaitForSeconds(moveTime);
        }
    }

    public void OnTriggerCollide(Collider other)
    {

        Debug.Log("hi");
        thisPooler.disposeObject(this);
    }


    private void FixedUpdate()
    {
        Vector3 x = Vector3.zero;
        transform.position = Vector3.SmoothDamp(transform.position, finalPos, ref x, moveTime / 2);
    }

    void Poolable.reset(bool on)
    {
        gameObject.SetActive(on);
        if (on)
            transform.rotation = Quaternion.identity;
    }
}
