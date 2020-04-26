using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombFootstepEmitter : MonoBehaviour
{
    public void EmitFootstep()
    {
    	EventManager.TriggerEvent<BombFootstepSFXEvent, Vector3>(this.transform.position);
    }
}
