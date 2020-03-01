using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningAttackPrincess : MonoBehaviour
{
	private Transform hurtbox;

	void Start() {
		hurtbox = transform.GetChild(1);
        if (hurtbox == null)
            Debug.Log("Hurtbox could not be found");
	}

    void Update() {

    }

    public void EnableHurtbox()
    {
        hurtbox.gameObject.SetActive(true);
        //Debug.Log("hurtbox enabled " + (hurtbox.gameObject.active == true));

    }

    public void DisableHurtbox()
    {
        hurtbox.gameObject.SetActive(false);
       // Debug.Log(hurtbox.gameObject.active != false);

    }



}
