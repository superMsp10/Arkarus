using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using Input = GoogleARCore.InstantPreviewInput;
#endif

public class Bow : MonoBehaviour
{
    public GameObject shootObjectPrefab;
    public Transform shootPos, arrowsParent, arrowModel;
    public float shootPower = 10f, drawBackDefaultZ, drawBackZ, reloadDefaultY, reloadDownY, reloadTime, defaultRotationY, maxShakeAmplitude, maxShakePeriods, maxShakeTime;
    float reloadStartTime, screenPercent, shakeDirection = 1;
    Pooler arrowPooler;
    Vector2 startPos;
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
        float r = (Time.time - reloadStartTime) / maxShakeTime;


        Debug.LogError("Reload time is less than shake time, undesired effects will arise");

        Debug.Log(r);
        if (r >= 1)
        {
            transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, defaultRotationY, transform.localRotation.eulerAngles.z);
        }
        else
        {
            float rotateY = defaultRotationY + Mathf.Sin(2 * Mathf.PI * r * maxShakePeriods * screenPercent) * (1 - r) * screenPercent * maxShakeAmplitude * shakeDirection;
            transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, rotateY, transform.localRotation.eulerAngles.z);
        }

        if (p >= 1)
        {
            arrowModel.localPosition = new Vector3(arrowModel.localPosition.x, reloadDefaultY, arrowModel.localPosition.z);
            reloading = false;
        }
        else
        {
            arrowModel.localPosition = new Vector3(arrowModel.localPosition.x, Mathf.Lerp(reloadDownY, reloadDefaultY, p), arrowModel.localPosition.z);
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
        Vector3 diff = (touch.position - startPos);
        screenPercent = new Vector2(diff.x / Screen.width, diff.y / Screen.height).magnitude;

        if (touch.phase == TouchPhase.Began)
        {
            startPos = touch.position;
        }
        else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
        {
            arrowModel.localPosition = new Vector3(arrowModel.localPosition.x, arrowModel.localPosition.y, Mathf.Lerp(drawBackDefaultZ, drawBackZ, screenPercent));
        }
        else
        {
            if (touch.phase == TouchPhase.Ended)
            {
                Debug.Log("screenPercent " + screenPercent * 100);

                if (diff.x > 0 && diff.y < 0 && screenPercent > .2)
                {
                    Vector3 force = shootPos.forward.normalized * screenPercent * shootPower;

                    Debug.Log("Force: " + force);
                    GameObject g = arrowPooler.getObject();
                    g.transform.position = shootPos.position;
                    g.transform.parent = arrowsParent;
                    g.transform.forward = shootPos.forward;
                    g.GetComponent<Rigidbody>().AddForce(force, ForceMode.Force);
                    reloading = true;
                    reloadStartTime = Time.time;
                    shakeDirection = Random.Range(-1,1) * 2 + 1;
                }
            }
            arrowModel.localPosition = new Vector3(arrowModel.localPosition.x, arrowModel.localPosition.y, drawBackDefaultZ);
        }

    }
}
