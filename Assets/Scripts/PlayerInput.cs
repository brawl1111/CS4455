using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This class handles all of the player input commands, such as movement, jumping, or spinning.
This way, all of the handling isn't stuffed into CharacterMovement.cs, which is meant to actually process
the inputs into on-screen changes.
*/
public class PlayerInput : MonoBehaviour
{
<<<<<<< HEAD
=======
	public static PlayerInput Instance
	{
		get {return s_Instance; }
	}

	protected static PlayerInput s_Instance;
>>>>>>> level_design
	public Vector2 InputVector
    {
        get
        {
            return movement;
        }
    }
<<<<<<< HEAD
    public bool JumpInput
=======
    public bool Jump
>>>>>>> level_design
    {
        get;
        private set;
    }
    public bool Spin
    {
        get;
        private set;
    }

<<<<<<< HEAD
    protected Vector2 movement;
=======

    protected Vector2 movement;
    protected bool isSpinning;

    private WaitForSeconds spinCooldownWait;
    Coroutine attackCooldownCoroutine;
    const float spinCooldownTime = 1.5f;

    void Awake()
    {
    	spinCooldownWait = new WaitForSeconds(spinCooldownTime);
    	if (s_Instance == null)
    		s_Instance = this;
    	else if (s_Instance != this)
    		throw new UnityException("There cannot be more than one PlayerInput script.");
    }
>>>>>>> level_design

    void Update()
    {
        movement.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
<<<<<<< HEAD
        //Debug.Log(InputVector);
        JumpInput = Input.GetButton("Jump");
        //if (JumpInput) Debug.Log(JumpInput);
=======
        Jump = Input.GetButtonDown("Jump");
        // if (Input.GetButtonDown("Fire1") && !isSpinning)
        // {
        // 	isSpinning = true;
        // 	//set hurtbox active

        // 	if(attackCooldownCoroutine != null) StopCoroutine(attackCooldownCoroutine);

        // 	attackCooldownCoroutine = StartCoroutine(SpinCooldown());

        // }
    }

    IEnumerator SpinCooldown()
    {
    	isSpinning = true;
    	yield return spinCooldownWait;
    	isSpinning = false;
>>>>>>> level_design
    }
}
