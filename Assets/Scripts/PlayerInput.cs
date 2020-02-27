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
        get;
        private set;
    }


    protected Vector2 movement;
    protected bool isSpinning;

    private WaitForSeconds spinCooldownWait;
    Coroutine attackCooldownCoroutine;
    const float spinCooldownTime = 1.5f;

    void Awake()
    {
    	spinCooldownWait = new WaitForSeconds(spinCooldownTime);
    }

    void Update()
    {
        movement.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
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
    }
}
