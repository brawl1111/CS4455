using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //public GameObject player;
    private Vector3 offset;
    public float rotationSpeed = 1f;
    public Transform target, player;
    float moveX, moveY;

    void Start()
    {
        //offset = target.transform.position - transform.position;
    }


    void LateUpdate()
    {
        //transform.position = player.transform.position + offset;
        CamControl();
        // float desiredAngle = target.transform.eulerAngles.y;
        // Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        // transform.position = target.transform.position - (rotation * offset);
        // transform.LookAt(target.transform);
    }

    void CamControl()
    {
        moveX += Input.GetAxis("Mouse X") * rotationSpeed;
        moveY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        moveY = Mathf.Clamp(moveY, -35, 60);

        transform.LookAt(target);

        target.rotation = Quaternion.Euler(moveY, moveX, 0);
        //Player.rotation = Quaternion.Euler(0, moveX, 0);
    }
}
