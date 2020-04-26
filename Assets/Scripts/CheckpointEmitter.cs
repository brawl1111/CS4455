using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointEmitter : MonoBehaviour
{
    public void EmitCheckpoint()
    {
    	EventManager.TriggerEvent<CheckpointSFXEvent, Vector3>(this.transform.position);
    }

}
