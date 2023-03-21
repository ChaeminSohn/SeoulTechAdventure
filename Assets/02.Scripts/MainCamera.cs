using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform targetTr;
    public Vector3 offset;

    void Update()
    {
        transform.position = targetTr.position + offset;
    }
}
