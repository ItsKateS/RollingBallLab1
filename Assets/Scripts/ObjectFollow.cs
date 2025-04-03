using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollow : MonoBehaviour
{
    public GameObject objectFollow;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - objectFollow.transform.position;
    }

    void LateUpdate()
    {
        transform.position = objectFollow.transform.position + offset;
    }
}
