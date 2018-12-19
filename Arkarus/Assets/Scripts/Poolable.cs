using UnityEngine;
using System.Collections;

public interface Poolable
{
    GameObject gameobject
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
