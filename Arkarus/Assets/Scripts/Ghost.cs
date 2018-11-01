using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{

    Vector3 finalPos;
    public float moveTime = 1f, moveDistance = 2f;
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
            finalPos = Vector3.MoveTowards(transform.position, Random.onUnitSphere, 1f);
            yield return new WaitForSeconds(moveTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(this, transform.position, transform.rotation);
        Instantiate(this, transform.position, transform.rotation);
        Destroy(gameObject);
    }


    private void FixedUpdate()
    {
        Vector3 x = Vector3.zero;
        transform.position = Vector3.SmoothDamp(transform.position, finalPos, ref x, moveTime / 2);
    }
}
