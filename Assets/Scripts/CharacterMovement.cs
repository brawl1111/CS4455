using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerInput))]
public class CharacterMovement : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rb;
    private PlayerInput m_Input;

    public float turnSmoothing = 15f;
    public float speedDampTime = 0.1f;

    public float maxSpeed = 3f;
    public float groundAcceleration = 5f;
    public float groundDeceleration = 4;
    private float desiredSpeed;
    private float currSpeed;

    protected bool IsMoveInput
     {
        get { return !Mathf.Approximately(m_Input.InputVector.sqrMagnitude, 0f); }
     }

    private int moveState = Animator.StringToHash("Movement");
    private int speedFloat = Animator.StringToHash("forward");


    void Awake()
    {
    	anim = GetComponent<Animator>();
    	if (anim == null) Debug.Log("Animator could not be found");

    	rb = GetComponent<Rigidbody>();
    	if (rb == null) Debug.Log("rigidbody could not be found");

    	m_Input = GetComponent<PlayerInput>();
    	if (m_Input == null) Debug.Log("PlayerInput script could not be found");
    }

    void Update()
    {
    	Vector2 input = m_Input.InputVector;
    	//Debug.Log(Input.GetAxis("Horizontal"));
    	if (input.x != 0f || input.y != 0f)
    	{
    		//Debug.Log("rotating");
    		RotateModel(input.x, input.y);
    	}	
    	CalculateForwardMovement();
    	
    }


    void CalculateForwardMovement()
    {
    	Vector2 input = m_Input.InputVector;
        if (input.sqrMagnitude > 1f)
            input.Normalize();

        // Calculate the speed intended by input.
        desiredSpeed = input.magnitude * maxSpeed;

        // Determine change to speed based on whether there is currently any move input.
        float acceleration = IsMoveInput ? groundAcceleration : groundDeceleration;

        // Adjust the forward speed towards the desired speed.
        currSpeed = Mathf.MoveTowards(currSpeed, desiredSpeed, acceleration * Time.deltaTime);

        // Set the animator parameter to control what animation is being played.
        anim.SetFloat(speedFloat, currSpeed);
    }

    void RotateModel(float h, float v)
    {
    	Vector3 targetDir = new Vector3(h, 0f, v);
    	//Debug.Log(targetDir);
    	Quaternion targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);
    	Quaternion newRotation = Quaternion.Lerp(rb.rotation, targetRotation, turnSmoothing * Time.deltaTime);
    	//rb.MoveRotation(newRotation);
    	transform.rotation = newRotation;
    }
}
