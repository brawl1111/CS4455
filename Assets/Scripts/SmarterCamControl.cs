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
	private float finalInputX;
	private float finalInputZ;
	private float rotY = 0.0f;
	private float rotX = 0.0f; 

    // Start is called before the first frame update
    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Joystick X") * joystickSensitivity;
        float inputY = Input.GetAxis("Joystick Y") * joystickSensitivity;
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        finalInputX = inputX + mouseX;
        finalInputZ = inputY + mouseY;

        rotY += finalInputX * inputSensitivity * Time.deltaTime;
        rotX += finalInputZ * inputSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -bottomClampAngle, topClampAngle);

        Quaternion localRot = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRot;
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
}
