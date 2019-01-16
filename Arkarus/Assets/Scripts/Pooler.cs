using UnityEngine;
using System.Collections.Generic;

public class Pooler
{
    public List<GameObject> active = new List<GameObject>();
    public List<GameObject> useable = new List<GameObject>();
    public GameObject original;
    protected int max;


    public Pooler(int max, GameObject objToInstantiate)
    {
        original = objToInstantiate;
        this.max = max;
    }

    public virtual GameObject getObject()
    {
        GameObject ret = null;
        if (active.Count >= max)
        {
            ret = active[0];
            active.Remove(ret);
            Poolable p = ret.GetComponent<Poolable>();
            if (p == null)
            {
                Debug.LogError(original.name + " is not poolable");
            }
            else
            {
                p.reset(false);
            }
            useable.Add(ret);

        }
        else if ((useable.Count - 1) < 0)
        {
            //						Debug.Log ("Useable" + useable.Count);
            Poolable p;
            ret = InstantiateObject();
            useable.Add(ret);
            ret.name = "ObjectPooled: " + (useable.Count + active.Count).ToString();
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
        }
        else
        {
            ret = useable[useable.Count - 1];
        }

        useable.Remove(ret);
        active.Add(ret);
        if (ret == null)
        {
            Debug.LogError("Poolable object has been destroyed externally; Effects will not work properly");

        }
        else
            ret.GetComponent<Poolable>().reset(true);
        return ret;

    }

    public virtual GameObject InstantiateObject()
    {
        return Object.Instantiate(original);
    }

    public virtual void disposeObject(Poolable p)
    {
        active.Remove(p.pooledGameObject);
        useable.Add(p.pooledGameObject);
        p.reset(false);
    }

    public void OnDestroy()
    {
        foreach (var item in active)
        {
            if (item != null)
            {
                Object.Destroy(item);
            }
        }
        foreach (var item in useable)
        {
            if (item != null)
            {
                Object.Destroy(item);
            }
        }
    }
}
