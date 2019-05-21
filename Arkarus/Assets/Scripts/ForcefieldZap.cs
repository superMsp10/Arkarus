using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcefieldZap : MonoBehaviour, Poolable
{
    public GameObject pooledGameObject
    {
        get
        {
            return gameObject;
        }
    }

    public Pooler thisPooler { get; set; }

    [SerializeField] ParticleSystem soulParticleSystem;
    [SerializeField] float clearTime = 1f;
    Vector3 target;


    public void SetTarget(Vector3 t)
    {
        target = t;
    }

    void Poolable.reset(bool on)
    {
        pooledGameObject.SetActive(on);
        if (on)
        {
            soulParticleSystem.Play();
        }
        else
        {
            soulParticleSystem.Stop();
            CancelInvoke();
            Invoke("ClearEffect", clearTime);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 x = Vector3.zero;
        if (target != null)
            soulParticleSystem.transform.position = Vector3.SmoothDamp(soulParticleSystem.transform.position, target, ref x, Time.deltaTime * 10 * clearTime);

    }

    public void ClearEffect()
    {
        thisPooler.disposeObject(this);
    }
}
