using UnityEngine;
using System.Collections;

public interface Poolable
{
    GameObject pooledGameObject
    {
        get;
    }

    Pooler thisPooler
    {
        get;
        set;
    }
    void reset(bool on);

}
