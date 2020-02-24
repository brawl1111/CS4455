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
    public bool JumpInput
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

    void Update()
    {
        movement.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //Debug.Log(InputVector);
        JumpInput = Input.GetButton("Jump");
        //if (JumpInput) Debug.Log(JumpInput);
    }
}
