using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanLift : MonoBehaviour
{

    public float fanLiftForce = 10f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hurtbox"))
        {
            GameObject princess = GameObject.FindGameObjectWithTag("Player");
            princess.GetComponent<Rigidbody>().AddForce(0, fanLiftForce * 100, 0, ForceMode.Impulse);

            Animator fanBladesAnim = GetComponentInChildren<Animator>();
            fanBladesAnim.Play("FanSpin");

            Debug.Log("fan lift");
        }
    }

}
