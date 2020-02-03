using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAttack : MonoBehaviour
{

	private Animator anim;
	private bool isSpinning;
	private Transform hurtbox;
	//private bool doneSpinning;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        if (anim == null)
            Debug.Log("Animator could not be found");

        hurtbox = transform.GetChild(0);
        if (hurtbox == null)
            Debug.Log("BoxCollider could not be found");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p") && !isSpinning)
        {
        	anim.SetBool("doneSpinning", false);
        	anim.SetTrigger("spinTrigger");
        	isSpinning = true;
        	hurtbox.gameObject.SetActive(true);
        	StartCoroutine(disableBoxCollider());
        }
    }

    public bool getIsSpinning()
    {
    	return isSpinning;
    }

    public void startCooldown() {
    	StartCoroutine(Cooldown());

    }

    public void setDoneSpinning() {
    	anim.SetBool("doneSpinning", true);
    }

    IEnumerator disableBoxCollider() {
    	yield return new WaitForSeconds(0.7f);
    	hurtbox.gameObject.SetActive(false);
    }

    IEnumerator Cooldown() {
    	yield return new WaitForSeconds(1.5f);
    	isSpinning = false;
    }
}
