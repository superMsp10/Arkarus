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
    public Transform arrowModel;
    public float shootPower = 10f, drawBackDefaultZ, drawBackMaxZ, reloadDefaultY, reloadDownY, reloadTime;
    float reloadStartTime;
    Pooler arrowPooler;
    Vector2 startPos, endPos;
    bool reloading = false;

    // Start is called before the first frame update
    void Start()
    {
        arrowPooler = new Pooler(100, shootObjectPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        if (reloading)
            Reload();
        UpdateShoot();
    }

    void Reload()
    {
        float p = (Time.time - reloadStartTime) / reloadTime;

        Debug.Log(p);
        arrowModel.localPosition = new Vector3(arrowModel.localPosition.x, Mathf.Lerp(reloadDownY, reloadDefaultY, p), arrowModel.localPosition.z);
        if (p >= 1)
        {
            arrowModel.localPosition = new Vector3(arrowModel.localPosition.x, reloadDefaultY, arrowModel.localPosition.z);
            reloading = false;
        }
    }

    void UpdateShoot()
    {
        Touch touch;
        if (Input.touchCount < 1 || reloading)
        {
            return;
        }

        touch = Input.GetTouch(0);
        Vector2 center = new Vector2(Screen.width / 2, Screen.height / 2);

        if (touch.phase == TouchPhase.Began)
        {
            startPos = touch.position;
        }
        else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
        {
            Vector3 diff = (touch.position - startPos);
            float screenPercent = new Vector2(diff.x / Screen.width, diff.y / Screen.height).magnitude;
            arrowModel.localPosition = new Vector3(arrowModel.localPosition.x, arrowModel.localPosition.y, Mathf.Lerp(drawBackDefaultZ, drawBackMaxZ, screenPercent));
        }
        else
        {
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
                    reloading = true;
                    reloadStartTime = Time.time;
                }
            }
            arrowModel.localPosition = new Vector3(arrowModel.localPosition.x, arrowModel.localPosition.y, drawBackDefaultZ);
        }

    }
}
