using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAttack : MonoBehaviour
{

	private Animator anim;
	private bool isSpinning;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        if (anim == null)
            Debug.Log("Animator could not be found");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p") && !isSpinning)
        {
        	anim.SetTrigger("spinTrigger");
        	isSpinning = true;
        }
    }

    public void startCooldown() {
    	StartCoroutine(Cooldown());
    	//anim.SetTrigger("spinTrigger");
    	//Debug.Log("coolddown over");
    }

    IEnumerator Cooldown() {
    	yield return new WaitForSeconds(1.5f);
    	//Debug.Log("hello");
    	isSpinning = false;
    	//anim.SetTrigger("spinTrigger");
    }
}
