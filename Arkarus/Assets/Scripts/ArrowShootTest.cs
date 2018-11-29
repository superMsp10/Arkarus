using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrowShootTest : MonoBehaviour
{

    public float timeScale;
    public Vector3 force;

    // Use this for initialization
    void Start()
    {

        Debug.Log("Press R to reset");
        applyProperties();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach (var item in transform.GetComponentsInChildren<Rigidbody>())
            {
                item.velocity = Vector3.zero;
                item.transform.position = Vector3.zero;
                item.transform.forward = Vector3.forward;
            }

            applyProperties();

        }
    }

    void applyProperties()
    {
        Time.timeScale = timeScale;
        foreach (var item in transform.GetComponentsInChildren<Rigidbody>())
        {
            item.AddForce(force, ForceMode.Force);
        }
    }
}
