using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulJar : MonoBehaviour
{
    public float maxDisplay = 0.48f;

    public void SetSouls(float percentage)
    {
        transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(0, maxDisplay, percentage), transform.localScale.z);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localScale.y, transform.localPosition.z);
    }
}
