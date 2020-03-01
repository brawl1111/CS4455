﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This class handles all of the player input commands, such as movement, jumping, or spinning.
This way, all of the handling isn't stuffed into CharacterMovement.cs, which is meant to actually process
the inputs into on-screen changes.
*/
public class PlayerInput : MonoBehaviour
{
	public static PlayerInput Instance
	{
		get {return s_Instance; }
	}

	protected static PlayerInput s_Instance;
	public Vector2 InputVector
    {
        get
        {
            return movement;
        }
    }
    public bool Jump
    {
        get;
        private set;
    }
    public bool Spin
    {
        get {
        	return isSpinning;
        }
    }


    protected Vector2 movement;
    protected bool isSpinning = false;
    protected bool canSpin = true;

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

    void Update()
    {
        movement.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Jump = Input.GetButtonDown("Jump");
        if (Input.GetButtonDown("Fire1") && canSpin)
        {
        	isSpinning = true;
        	Debug.Log("spinning");
        	StartCoroutine(SpinCooldown());

        }
        // if (Input.GetButtonDown("Fire1") && !isSpinning)
        // {
        // 	isSpinning = true;
        // 	//set hurtbox active

        // 	if(attackCooldownCoroutine != null) StopCoroutine(attackCooldownCoroutine);

        // 	attackCooldownCoroutine = StartCoroutine(SpinCooldown());

        // }
    }

    public void SetIsSpinningFalse()
    {
        isSpinning = false;
    }


    IEnumerator SpinCooldown()
    {
    	canSpin = false;
    	yield return spinCooldownWait;
    	canSpin = true;
        isSpinning = false;
    }
}
