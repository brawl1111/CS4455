using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmarterCamControl : MonoBehaviour
{

	public float camMoveSpeed = 120.0f;
	public GameObject camFollowObj;
	public Vector3 followPOS;
	public float topClampAngle = 60.0f;
	public float bottomClampAngle = 40.0f;
    public float mouseSensitivity = 20.0f;
	public float inputSensitivity = 150.0f;
	public float joystickSensitivity = 2.5f;
	public GameObject camera;
	public GameObject player;
	public float camDistanceXToPlayer;
	public float camDistanceYToPlayer;
	public float camDistanceZToPlayer;
	public float smoothX;
	public float smoothY;
	private float mouseX;
	private float mouseY;
    private float joystickX;
    private float joystickY;
	private float finalInputX;
	private float finalInputZ;
	private float rotY = 0.0f;
	private float rotX = 0.0f; 


    enum CamControlState
    {
        mouseControl,
        joystickControl
    };
    CamControlState state;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        state = CamControlState.joystickControl;
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case CamControlState.mouseControl:
                mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
                mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
                rotY += mouseX * inputSensitivity * Time.deltaTime;
                rotX += mouseY * inputSensitivity * Time.deltaTime;

                rotX = Mathf.Clamp(rotX, -bottomClampAngle, topClampAngle);

                Quaternion localRot1 = Quaternion.Euler(rotX, rotY, 0.0f);
                transform.rotation = localRot1;
                if (Input.GetMouseButtonUp(1))
                {
                    joystickX = mouseX;
                    joystickY = mouseY;
                    state = CamControlState.joystickControl;
                }
            break;

            case CamControlState.joystickControl:
                joystickX = Input.GetAxis("Joystick X") * joystickSensitivity;
                joystickY = Input.GetAxis("Joystick Y") * joystickSensitivity;
                rotY += joystickX * inputSensitivity * Time.deltaTime;
                rotX += joystickY * inputSensitivity * Time.deltaTime;

                rotX = Mathf.Clamp(rotX, -bottomClampAngle, topClampAngle);
                //Debug.Log("joystickx: " + joystickX + " joystickY: " + joystickY + " roty: " + rotY + " rotx: " + rotX);

                Quaternion localRot = Quaternion.Euler(rotX, rotY, 0.0f);
                transform.rotation = localRot;
                if (Input.GetMouseButton(1))
                {
                    mouseX = joystickX;
                    mouseY = joystickY;
                    state = CamControlState.mouseControl;
                }
            break;

        }



        /*joystickX = Input.GetAxis("Joystick X") * joystickSensitivity;
        joystickY = Input.GetAxis("Joystick Y") * joystickSensitivity;
        if (Input.GetMouseButton(1))
        {
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");
        }
        finalInputX = joystickX + mouseX;
        finalInputZ = joystickY + mouseY;

        rotY += finalInputX * inputSensitivity * Time.deltaTime;
        rotX += finalInputZ * inputSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -bottomClampAngle, topClampAngle);

        Quaternion localRot = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRot;*/
    }

    void LateUpdate()
    {
    	CameraUpdate();
    }

    void CameraUpdate()
    {
    	//set target object to follow
    	Transform target = camFollowObj.transform;

    	//move towards target
    	float step = camMoveSpeed * Time.deltaTime;
    	transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

    public void changeMouseSensitivity(float f)
    {
        mouseSensitivity = f;
    }
}
