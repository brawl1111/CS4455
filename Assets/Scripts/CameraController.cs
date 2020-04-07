using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //public GameObject player;
    private Vector3 offset;
    public float rotationSpeed = 1f;
    public float clampAngle = 80.0f;
    public Transform target, player;
    float joystickX, joystickY;
    float mouseX, mouseY;
    enum CamControlState
    {
        mouseControl,
        joystickControl
    };
    CamControlState state;

    void Start()
    {
        //offset = target.transform.position - transform.position;
        state = CamControlState.joystickControl;
    }

    void LateUpdate()
    {
        //transform.position = player.transform.position + offset;
        //CamControl();
        // float desiredAngle = target.transform.eulerAngles.y;
        // Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        // transform.position = target.transform.position - (rotation * offset);
        // transform.LookAt(target.transform);
        // Debug.Log(state);
        switch(state)
        {

            case CamControlState.mouseControl:
                mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
                mouseY += Input.GetAxis("Mouse Y") * rotationSpeed;
                mouseY = Mathf.Clamp(mouseY, -35, 60);
                transform.LookAt(target);
                target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
                if (Input.GetMouseButtonUp(1))
                {
                    joystickX = mouseX;
                    joystickY = mouseY;
                    state = CamControlState.joystickControl;
                }
                break;

            case CamControlState.joystickControl:
                joystickX += Input.GetAxis("Joystick X") * rotationSpeed;
                joystickY -= Input.GetAxis("Joystick Y") * rotationSpeed;
                //Debug.Log("joystickX: " + joystickX + "joystickY" + joystickY);
                joystickY = Mathf.Clamp(joystickY, -35, 60);
                transform.LookAt(target);
                target.rotation = Quaternion.Euler(joystickY, joystickX, 0);
                if (Input.GetMouseButton(1))
                {
                    mouseX = joystickX;
                    mouseY = joystickY;
                    state = CamControlState.mouseControl;
                }
                break;
        }

    }

    void CamControl()
    {
        if (Input.GetMouseButton(1))
        {
            if (!Mathf.Approximately(joystickX, 0f)) mouseX = joystickX;
            if (!Mathf.Approximately(joystickY, 0f)) mouseY = joystickY;

            mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
            mouseY += Input.GetAxis("Mouse Y") * rotationSpeed;
            mouseY = Mathf.Clamp(mouseY, -35, 60);
            transform.LookAt(target);
            target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        } else
        {

            if (!Mathf.Approximately(mouseX, 0f)) joystickX = mouseX;
            if (!Mathf.Approximately(mouseY, 0f)) joystickY = mouseY;

            joystickX += Input.GetAxis("Joystick X") * rotationSpeed;
            joystickY -= Input.GetAxis("Joystick Y") * rotationSpeed;
            //Debug.Log("joystickX: " + joystickX + "joystickY" + joystickY);
            joystickY = Mathf.Clamp(joystickY, -35, 60);
            transform.LookAt(target);
            target.rotation = Quaternion.Euler(joystickY, joystickX, 0);
        }
        // joystickX += Input.GetAxis("Joystick X") * rotationSpeed;
        // joystickY -= Input.GetAxis("Joystick Y") * rotationSpeed;
        // Debug.Log("joystickX: " + joystickX + "joystickY" + joystickY);
        // joystickY = Mathf.Clamp(joystickY, -35, 60);
        // transform.LookAt(target);
        // target.rotation = Quaternion.Euler(joystickY, joystickX, 0);

        //Debug.Log(Input.GetAxis("Mouse X"));
        // mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        // mouseY += Input.GetAxis("Mouse Y") * rotationSpeed;

        // mouseX += joystickX;
        // mouseY += joystickY;
        // mouseY = Mathf.Clamp(mouseY, -35, 60);

        // transform.LookAt(target);

        // target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        //Player.rotation = Quaternion.Euler(0, moveX, 0);
    }
}
