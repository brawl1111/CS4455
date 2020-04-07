using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSFXEmitter : MonoBehaviour
{
    public void EmitSpikeExtendSFX()
    {
    	EventManager.TriggerEvent<SpikeExtendSFXEvent, Vector3>(this.transform.position);
    }
}
