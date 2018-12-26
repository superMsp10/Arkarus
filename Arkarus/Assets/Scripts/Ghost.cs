using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour, Poolable
{

    Vector3 finalPos;
    Quaternion finalRot;
    public float moveTime = 1f, moveDistance = 2f, rotateSpeed = 1f;
    Camera playerCam;

    GameObject Poolable.gameobject
    {
        get
        {
            return gameObject;
        }
    }

    public Pooler thisPooler { get; set; }

    // Use this for initialization
    void Start()
    {
        playerCam = GameManager.gameManager.FirstPersonCamera;
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
            Vector3 rand = Random.onUnitSphere;
            Vector3 deltaPlayer = playerCam.transform.position - transform.position;
            finalPos = Vector3.MoveTowards(transform.position, rand + deltaPlayer, 1f);
            finalRot = Quaternion.LookRotation((playerCam.transform.position -transform.position) + rand);
            yield return new WaitForSeconds(moveTime);
        }
    }

    public void OnTriggerCollide(Collider other)
    {
        thisPooler.disposeObject(this);
    }


    private void FixedUpdate()
    {
        Vector3 x = Vector3.zero;
        transform.position = Vector3.SmoothDamp(transform.position, finalPos, ref x, moveTime / 2);
        transform.rotation = Quaternion.Slerp(transform.rotation, finalRot, rotateSpeed * Time.deltaTime);

    }

    void Poolable.reset(bool on)
    {
        gameObject.SetActive(on);
        if (on)
            transform.rotation = Quaternion.identity;
    }
}
