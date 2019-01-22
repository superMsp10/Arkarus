﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour, Poolable
{

    Vector3 finalPos;
    Quaternion finalRot;
    Camera playerCam;

    public Renderer renderer;
    public SpriteRenderer eyeR, eyeL, mouth;
    //0 is left, 1 is right
    public Sprite[] eyeOpenSprite = new Sprite[2], eyeClosedSprite = new Sprite[2];
    public Transform mesh;

    public float moveRefreshTime = 1f,
        moveSpeed = 2f,
        rotateSpeed = 1f,
        openFaceTimeMin = 5f,
        openFaceTimeMax = 10f,
        closedFaceTimeMin = .2f,
        closedFaceTimeMax = 1f;

    GameObject Poolable.pooledGameObject
    {
        get
        {
            return gameObject;
        }
    }

    public Pooler thisPooler { get; set; }

    void Start()
    {
    }

    private void Awake()
    {
        playerCam = GameManager.Instance.FirstPersonCamera;
    }

    void Update()
    {

    }

    IEnumerator FaceExpressionChange()
    {
        bool normalface = true;
        while (true)
        {
            if (normalface)
            {
                int expr = Random.Range(0, 3);
                switch (expr)
                {
                    case 0:
                        eyeL.sprite = eyeClosedSprite[0];
                        eyeR.sprite = eyeClosedSprite[1];
                        break;
                    case 1:
                        eyeL.sprite = eyeClosedSprite[0];
                        eyeR.sprite = eyeOpenSprite[1];
                        break;
                    case 2:
                        eyeL.sprite = eyeOpenSprite[0];
                        eyeR.sprite = eyeClosedSprite[1];
                        break;
                }
                yield return new WaitForSeconds(Random.Range(closedFaceTimeMin, closedFaceTimeMax));

            }
            else
            {
                eyeL.sprite = eyeOpenSprite[0];
                eyeR.sprite = eyeOpenSprite[1];
                yield return new WaitForSeconds(Random.Range(openFaceTimeMin, openFaceTimeMax));
            }
            normalface = !normalface;
        }
    }

    IEnumerator Move()
    {
        while (true)
        {
            Vector3 rand = Random.onUnitSphere;
            Vector3 deltaPlayer = playerCam.transform.position - transform.position;
            finalPos = Vector3.MoveTowards(transform.position, rand + deltaPlayer, 1f);
            finalRot = Quaternion.LookRotation((playerCam.transform.position - transform.position) + rand);
            yield return new WaitForSeconds(moveRefreshTime);
        }
    }

    public void OnTriggerCollide(Collider other)
    {
        GameManager.Instance.spawner.SpawnGhostResidue(this);
        thisPooler.disposeObject(this);
    }


    private void FixedUpdate()
    {
        Vector3 x = Vector3.zero;
        transform.position = Vector3.SmoothDamp(transform.position, finalPos, ref x, 1/moveSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, finalRot, rotateSpeed * Time.deltaTime);
    }

    void Poolable.reset(bool on)
    {
        gameObject.SetActive(on);
        if (on)
        {
            StartCoroutine(Move());
            StartCoroutine(FaceExpressionChange());
        }
        else
        {
            StopAllCoroutines();
        }

    }
}
