using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeesawCreak : MonoBehaviour
{
	private bool negativeZ, positiveZ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider c)
    {
    	//Debug.Log("collision detected with " + c.gameObject.CompareTag("Player"));
    	if (c.gameObject.CompareTag("Player") && Mathf.Abs(this.transform.rotation.eulerAngles.z) > 20.0f)
    	{
    		EventManager.TriggerEvent<Seesaw1SFX, Vector3>(this.transform.position);
    	}
    }
}
