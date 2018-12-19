using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using Input = GoogleARCore.InstantPreviewInput;
#endif

public class Bow : MonoBehaviour
{
    public GameObject shootObjectPrefab;
    public Transform shootPos;
    public Transform arrowParent;
    public float shootPower = 10f;
    Pooler arrowPooler;
    Vector2 startPos, endPos;

    // Start is called before the first frame update
    void Start()
    {
        arrowPooler = new Pooler(100, shootObjectPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateShoot();
    }

    void UpdateShoot()
    {
        Touch touch;
        if (Input.touchCount < 1)
        {
            return;
        }

        touch = Input.GetTouch(0);
        Vector2 center = new Vector2(Screen.width / 2, Screen.height / 2);

        if (touch.phase == TouchPhase.Began)
        {
            startPos = touch.position;
        }
        if (touch.phase == TouchPhase.Ended)
        {
            endPos = touch.position;
            Vector3 diff = (endPos - startPos);
            float screenPercent = new Vector2(diff.x / Screen.width, diff.y / Screen.height).magnitude;

            Debug.Log("screenPercent " + screenPercent * 100);

            if (diff.x > 0 && diff.y < 0 && screenPercent > .2)
            {
                Vector3 force = shootPos.forward.normalized * screenPercent * shootPower;

                Debug.Log("Force: " + force);
                GameObject g = arrowPooler.getObject();
                g.transform.position = shootPos.position;
                g.transform.parent = arrowParent;
                g.transform.forward = shootPos.forward;
                g.GetComponent<Rigidbody>().AddForce(force, ForceMode.Force);
            }
        }
    }
}
