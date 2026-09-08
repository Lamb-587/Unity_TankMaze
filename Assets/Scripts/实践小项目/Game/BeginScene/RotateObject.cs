using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotateSpeed = 1f;

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Rotate(transform.up, rotateSpeed*Time.deltaTime);
    }
}
