using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerInput))]
public class CharacterMovement : MonoBehaviour
{
	//Player's various components
    private Animator anim;
    private Rigidbody rb;
    private PlayerInput m_Input;
    public CharacterController charCtrl;

    //variables for jumping/ground check
    private bool isGrounded = true;
    private float verticalSpeed = 0f;
    public float gravityScale = 2f;
    public float jumpSpeed = 5f;

    public float distanceToGround;
	public LayerMask ground;
	private Transform groundCheck;
	public bool isGroundedCheck;

    //smoothing vars for horizontal movements
    public float turnSmoothing = 15f;
    public float speedDampTime = 0.1f;

    public float maxSpeed = 3f;
    public float groundAcceleration = 5f;
    public float groundDeceleration = 4;
    private float desiredSpeed;
    private float currSpeed;

    //variables for double jumping
    public int maxExtraJumps;
    private int extraJumps;
    private bool canJump;
    

    protected bool IsMoveInput
     {
        get { return !Mathf.Approximately(m_Input.InputVector.sqrMagnitude, 0f); }
     }

    private int moveState = Animator.StringToHash("Movement");
    private int speedFloat = Animator.StringToHash("forward");
    private int isGroundedState = Animator.StringToHash("isGrounded");

    void Awake()
    {
    	anim = GetComponent<Animator>();
    	if (anim == null) Debug.Log("Animator could not be found");

    	rb = GetComponent<Rigidbody>();
    	if (rb == null) Debug.Log("rigidbody could not be found");

    	m_Input = GetComponent<PlayerInput>();
    	if (m_Input == null) Debug.Log("PlayerInput script could not be found");

    	charCtrl = GetComponent<CharacterController>();
    	if (charCtrl == null) Debug.Log("CharacterController could not be found");

        //Disable CharacterController's collider's interference with the model's capsule collider.
        Physics.IgnoreCollision(charCtrl, GetComponent<CapsuleCollider>());
        extraJumps = maxExtraJumps;

        groundCheck = transform.GetChild(0);
    }

    void Update()
    {
    	// CalculateVerticalMovement();
    	// CalculateForwardMovement();
    }

    void FixedUpdate()
    {
    	isGroundedCheck = Physics.CheckSphere(groundCheck.position, distanceToGround, ground, QueryTriggerInteraction.Ignore);
    	
    	if (isGroundedCheck) extraJumps = maxExtraJumps;
    	Vector2 input = m_Input.InputVector;
    	if (input.x != 0f || input.y != 0f)
    	{
    		RotateModel(input.x, input.y);
    	}	
    	CalculateForwardMovement();
    	CalculateVerticalMovement();
    	
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

    void CalculateVerticalMovement()
    {
    	if (!m_Input.Jump && (isGrounded || isGroundedCheck))
    		canJump = true;

    	if (isGroundedCheck)
    	{
    		verticalSpeed = Physics.gravity.y * 0.3f;
    		//Debug.Log("IsGrounded");
    		if (m_Input.Jump && canJump)
    		{
    			Debug.Log("detected jump");
    			verticalSpeed = jumpSpeed;
    			isGroundedCheck = false;
    		}
    	} else
    	{
    		//Debug.Log("not grounded");
    		verticalSpeed += Physics.gravity.y * gravityScale * Time.deltaTime;
    		if (m_Input.Jump && extraJumps > 0 && canJump)
    		{
    			Debug.Log("detected double jump");
    			verticalSpeed = jumpSpeed;
    			extraJumps--;
    			canJump = false;
    		}
    		//Debug.Log("not grounded verticalSpeed: " + verticalSpeed);
    	}
    	/*verticalSpeed += Physics.gravity.y * gravityScale * Time.deltaTime;
    	if (m_Input.Jump && isGrounded)
    	{
    		Debug.Log("detected jump");
    		verticalSpeed = jumpSpeed;
    		isGrounded = false;
    	} else if (m_Input.Jump && extraJumps > 0)
    	{
    		Debug.Log("detected double jump");
    		verticalSpeed = jumpSpeed;
    		extraJumps--;
    		canJump = false;
    	}*/
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

    void OnAnimatorMove()
    {
    	Vector3 movement;

    	movement = currSpeed * transform.forward * Time.deltaTime;
    	movement.y = verticalSpeed * Time.deltaTime;
    	//Debug.Log(movement.y);
    	//Debug.Log("movement: " + movement + " transform: " + transform.position);
    	charCtrl.Move(movement);
    	//isGrounded = charCtrl.isGrounded;
    	if (isGroundedCheck) anim.SetBool(isGroundedState, true);
    	else anim.SetBool(isGroundedState, false);
    	
    }
}
