using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockLifetime : MonoBehaviour
{
	private MeshRenderer m;
	private WaitForSeconds blinkWait, fasterBlinkWait;
	private float blinkWaitSeconds = 0.2f;
	private float fasterBlinkWaitSeconds = 0.15f;
    // Start is called before the first frame update
    void Start()
    {
        m = GetComponent<MeshRenderer>();
        if (m == null) Debug.Log("couldn't get mesh renderer");
        blinkWait = new WaitForSeconds(blinkWaitSeconds);
        fasterBlinkWait = new WaitForSeconds(fasterBlinkWaitSeconds);
        StartCoroutine(Disappear());
    }

    void OnCollisionEnter(Collision col)
    {
    	if (col.gameObject.CompareTag("terrain"))
    	{
    		EventManager.TriggerEvent<PlayerHurtSFXEvent, Vector3>(this.transform.position);
    	}
    }

    IEnumerator Disappear()
    {
    	yield return new WaitForSeconds(5f);
    	m.enabled = false;
    	yield return blinkWait;
    	m.enabled = true;
    	yield return blinkWait;
    	m.enabled = false;
    	yield return blinkWait;
    	m.enabled = true;
    	yield return blinkWait;
    	m.enabled = false;
    	yield return blinkWait;
    	m.enabled = true;
    	yield return blinkWait;
    	m.enabled = false;
    	yield return blinkWait;
    	m.enabled = true;
    	yield return blinkWait;
    	m.enabled = false;
    	yield return blinkWait;
    	m.enabled = true;
    	yield return blinkWait;
    	m.enabled = false;
    	yield return fasterBlinkWait;
    	m.enabled = true;
    	yield return fasterBlinkWait;
    	m.enabled = false;
    	yield return fasterBlinkWait;
    	m.enabled = true;
    	yield return fasterBlinkWait;
    	m.enabled = false;
    	yield return fasterBlinkWait;
    	m.enabled = true;
    	yield return fasterBlinkWait;
    	m.enabled = false;
    	yield return fasterBlinkWait;
    	m.enabled = true;
    	yield return fasterBlinkWait;
    	m.enabled = false;
    	yield return fasterBlinkWait;
    	m.enabled = true;
    	yield return fasterBlinkWait;
    	m.enabled = false;
    	yield return fasterBlinkWait;
    	m.enabled = true;
    	yield return fasterBlinkWait;
    	m.enabled = false;
    	yield return fasterBlinkWait;

    	Destroy(gameObject);
    }
}
