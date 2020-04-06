using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleRotate : MonoBehaviour
{
	public float rotateSpeed = 20f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, rotateSpeed);
    }
}
