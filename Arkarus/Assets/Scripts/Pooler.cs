using UnityEngine;
using System.Collections.Generic;

public class Pooler
{
    public GameObject original;
    protected int max;


    public Pooler(int max, GameObject objToInstantiate)
    {
        original = objToInstantiate;
        this.max = max;
    }

    public void DeactivateAll()
    {
        //GameObject[] old = active.ToArray();
        //foreach (GameObject item in old)
        //{
        //    DisposeObject(item.GetComponent<Poolable>());
        //}
    }

    public virtual GameObject getObject()
    {
        GameObject ret = null;

        //						Debug.Log ("Useable" + useable.Count);
        Poolable p;
        ret = InstantiateObject();
        //ret.name = "ObjectPooled: " + (useable.Count + active.Count).ToString();
        p = ret.GetComponent<Poolable>();
        if (p == null)
        {
            Debug.LogError(original.name + " is not poolable");
        }
        else
        {
            p.thisPooler = this;
            p.reset(false);
        }
        ret.GetComponent<Poolable>().reset(true);
        return ret;
    }

    public virtual GameObject InstantiateObject()
    {
        return Object.Instantiate(original);
    }

    public virtual void DisposeObject(Poolable p)
    {
        //active.Remove(p.pooledGameObject);
        //useable.Add(p.pooledGameObject);
        //p.reset(false);
        GameObject.Destroy(p.pooledGameObject);
    }

    public void OnDestroy()
    {
        //foreach (var item in active)
        //{
        //    if (item != null)
        //    {
        //        Object.Destroy(item);
        //    }
        //}
        //foreach (var item in useable)
        //{
        //    if (item != null)
        //    {
        //        Object.Destroy(item);
        //    }
        //}
    }
}
