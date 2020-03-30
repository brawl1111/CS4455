using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{

    public GameObject center;
    public Transform targetTransform;
    public float orbitDistance = 10.0f;
    public float orbitDegreesPerSec = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("script added");
    }

    private void LateUpdate()
    {
        Orbit();
    }

    private void Orbit()
    {
        targetTransform = center.transform;

        if (targetTransform != null)
        {
            // Keep us at orbitDistance from target
            transform.position = targetTransform.position + (transform.position - targetTransform.position).normalized * orbitDistance;
            transform.RotateAround(targetTransform.position, Vector3.up, orbitDegreesPerSec * Time.deltaTime);
        }
    }

}
