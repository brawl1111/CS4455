using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitSwordSlashSFX : MonoBehaviour
{
    public void EmitSwordSlash()
    {
    	EventManager.TriggerEvent<SwordSwing, Vector3>(this.transform.position);
    }
}
