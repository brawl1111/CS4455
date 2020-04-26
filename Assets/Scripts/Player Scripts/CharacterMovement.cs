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
    public Transform camTransform;

    //variables for jumping/ground check
    private bool isGrounded = true;
    //private float verticalSpeed = 0f;
    public float gravityScale = 2f;
    public float jumpSpeed = 1f;

    public float distanceToGround;
	public LayerMask ground;
	private Transform groundCheck;
	public bool isGroundedCheck;

	private bool canDoubleJump;

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

    //bunch of variables to support spinning
    private bool isSpinningCooldownOver = true;
    private bool isSpinning = false;
    private WaitForSeconds spinCooldownWait;
    public float speedWhileSpinning = 2.5f;
    public float spinJumpBoost = 8f;

    //information on llamas for each section, where each index is a section
    private int[] llamas = new int[] {0, 0, 0, 0};

    public ParticleSystem doubleJumpParticles;


    protected bool IsMoveInput
     {
        get { return !Mathf.Approximately(m_Input.InputVector.sqrMagnitude, 0f); }
     }

    private int moveState = Animator.StringToHash("Movement");
    private int speedFloat = Animator.StringToHash("forward");
    private int isGroundedState = Animator.StringToHash("isGrounded");
    private int spinState = Animator.StringToHash("spinTrigger");
    private int doneSpinning = Animator.StringToHash("doneSpinning");

    private Transform hurtbox;

    void Awake()
    {
        spinCooldownWait = new WaitForSeconds(1f);

    	anim = GetComponent<Animator>();
    	if (anim == null) Debug.Log("Animator could not be found");

    	rb = GetComponent<Rigidbody>();
    	if (rb == null) Debug.Log("rigidbody could not be found");

    	m_Input = GetComponent<PlayerInput>();
    	if (m_Input == null) Debug.Log("PlayerInput script could not be found");

    	charCtrl = GetComponent<CharacterController>();
    	if (charCtrl == null) Debug.Log("CharacterController could not be found");
        //Debug.Log(charCtrl);

        //Disable CharacterController's collider's interference with the model's capsule collider.
        Physics.IgnoreCollision(charCtrl, GetComponent<CapsuleCollider>());
        extraJumps = maxExtraJumps;

        groundCheck = transform.GetChild(0);
        hurtbox = transform.GetChild(1);
    }

    void Update()
    {
    	Vector2 input = m_Input.InputVector;
    	if (input.x != 0f || input.y != 0f)
    	{
    		RotateModel(input.x, input.y);
    	}
        //Debug.Log(isGrounded);

        //Spin if player pushes LControl or any of the trigger buttons
        if ((Input.GetAxis("XBOX_360_LTrigger") != 0 || Input.GetButtonDown("Fire1")) && isSpinningCooldownOver)
        {
            if (!(isGroundedCheck || isGrounded))
            {
                rb.velocity = Vector3.up * spinJumpBoost;
            }
            anim.SetBool(doneSpinning, false);
            anim.SetTrigger(spinState);
            isSpinningCooldownOver = false;
            isSpinning = true;
            EventManager.TriggerEvent<SpinSFXEvent, Vector3>(this.transform.position);
            //hurtbox.gameObject.SetActive(true);

        }
    	CalculateForwardMovement();
    	CalculateVerticalMovement();

    }

    public void SetAnimDoneSpinning() {
        anim.SetBool(doneSpinning, true);
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

    //to fix the respawn glitch where you're stuck in "spin" mode without it showing
    public void resetSpin()
    {
        isSpinning = false;
        isSpinningCooldownOver = true;
        anim.SetBool(doneSpinning, true);
    }

    public bool GetIsSpinning()
    {
        return isSpinning;
    }

    public void StartSpinCooldown()
    {
        isSpinning = false;
        StartCoroutine(SpinCooldown());
    }

    IEnumerator SpinCooldown()
    {
        yield return spinCooldownWait;
        //Debug.Log("cooldown coroutine over");
        isSpinningCooldownOver = true;
    }

    public void IncrementLlamaCount(int section)
    {
        llamas[section] = llamas[section] + 1;
    }

    public int GetLlamaCount(int section)
    {
        return llamas[section];
    }

    void FixedUpdate()
    {
    	isGroundedCheck = Physics.CheckSphere(groundCheck.position, distanceToGround, ground, QueryTriggerInteraction.Ignore);
    	isGrounded = charCtrl.isGrounded;
        //Debug.Log(isGroundedCheck);
        //anim.SetBool(isGroundedState, isGroundedCheck);

    	if (isGroundedCheck || isGrounded) extraJumps = maxExtraJumps;
    }


    void CalculateForwardMovement()
    {
        if (!isSpinning)
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
        else
        {
            //Debug.Log("moving while spinning");
            anim.SetFloat(speedFloat, 0.0f);
            Vector3 inputs = Vector3.zero;
            inputs.x = Input.GetAxis("Horizontal");
            inputs.z = Input.GetAxis("Vertical");
            inputs = camTransform.TransformDirection(inputs);
            inputs.Set(inputs.x, 0f, inputs.z);
            // if (inputs != Vector3.zero)
            // {
            //     transform.forward = inputs;
            // }
            //Debug.Log(inputs);
            rb.MovePosition(rb.position + inputs * speedWhileSpinning * Time.deltaTime);
        }
    }

    void CalculateVerticalMovement()
    {
    	if (m_Input.Jump && (isGrounded || isGroundedCheck))
    	{
    		rb.velocity = Vector3.up * jumpSpeed;
            //Debug.Log(rb.velocity);
            EventManager.TriggerEvent<JumpSFXEvent, Vector3>(this.transform.position);
    	} else if (m_Input.Jump && extraJumps > 0)
    	{
            //double jump
            //doubleJumpParticles.Emit(20);
    		rb.velocity = Vector3.up * jumpSpeed;
            EventManager.TriggerEvent<JumpSFXEvent, Vector3>(this.transform.position);
    		extraJumps--;
    	}
    }

    void RotateModel(float h, float v)
    {
    	Vector3 targetDir = new Vector3(h, 0f, v);
    	//Debug.Log(targetDir);
        targetDir = camTransform.TransformDirection(targetDir);
        targetDir.Set(targetDir.x, 0f, targetDir.z);
    	Quaternion targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);
    	Quaternion newRotation = Quaternion.Lerp(rb.rotation, targetRotation, turnSmoothing * Time.deltaTime);
    	transform.rotation = newRotation;
    }

    /*void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.gameObject);
    }*/
}
