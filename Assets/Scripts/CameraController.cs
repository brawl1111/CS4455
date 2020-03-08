using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    public float rotationSpeed = 1f;
    public Transform Target, Player;
    float moveX, moveY;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }


    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
        //CamControl();
    }

    void CamControl()
    {
        moveX += Input.GetAxis("Mouse X") * rotationSpeed;
        moveY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        moveY = Mathf.Clamp(moveY, -35, 60);

        transform.LookAt(Target);

        Target.rotation = Quaternion.Euler(moveY, moveX, 0);
        //Player.rotation = Quaternion.Euler(0, moveX, 0);
    }
}
