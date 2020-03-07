using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepEmitter : MonoBehaviour
{
	private CharacterMovement charMove;

	void Awake()
	{
		charMove = GetComponent<CharacterMovement>();
	}
    public void EmitFootstep()
    {
    	if (charMove.isGroundedCheck)
    	{
    		EventManager.TriggerEvent<FootstepSFXEvent, Vector3>(this.transform.position);
    	}
    }
}
