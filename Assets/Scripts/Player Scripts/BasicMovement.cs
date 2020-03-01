using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
	public float speed = 5f;
	public float jumpHeight = 2f;
	public float distanceToGround;
	public LayerMask ground;

	private Rigidbody rb;
	private Vector3 inputs = Vector3.zero;
	private bool isGrounded = true;
	private Transform groundCheck;

    public int maxExtraJumps;
    private int extraJumps;


    void Start()
    {
        extraJumps = maxExtraJumps;
        rb = GetComponent<Rigidbody>();
        
        groundCheck = transform.GetChild(0);
        /*
		GroundCheck gets the first child of the capsule. In this case, it's an empty child
		that's located at the bottom of the capsule. This is for checking if the capsule is "grounded." We use
		a function called Physics.CheckSphere which draws an imaginary sphere centered around
		groundCheck and determines if anything is intersecting that sphere.
        */
    }
 
    // Update is called once per frame
    void Update()
    {
        if (isGrounded) extraJumps = maxExtraJumps; //if player is on ground, reset double jump count
        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //Debug.Log("normal jump");
        	//rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            //rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            rb.velocity = Vector3.up * jumpHeight;
        }
        else if (Input.GetButtonDown("Jump") && extraJumps > 0)
        {
            //Debug.Log("double jump");
            //rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            //rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            rb.velocity = Vector3.up * jumpHeight;
            extraJumps--;
        }
    }

    void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, distanceToGround, ground, QueryTriggerInteraction.Ignore);
        //Debug.Log(isGrounded);
        inputs = Vector3.zero;
        inputs.x = Input.GetAxis("Horizontal");
        inputs.z = Input.GetAxis("Vertical");
        if (inputs != Vector3.zero)
        {
            transform.forward = inputs;
        }
    	rb.MovePosition(rb.position + inputs * speed * Time.fixedDeltaTime);
    }
}