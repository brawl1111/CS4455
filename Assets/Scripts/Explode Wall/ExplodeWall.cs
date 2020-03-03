using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeWall : MonoBehaviour
{

    Collider other;
    private bool triggered = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (triggered && !other)
        {
            Debug.Log("wall destroy in update");
            gameObject.SetActive(false);        // this kinda works
        }
    }

    private void OnTriggerStay(Collider other)
    {

        //Destroy(other.gameObject);

        if (other.gameObject.CompareTag("BOMB"))
        {
            if (other.gameObject.GetComponent<BombChase>().getExploded() == true)
            {
                //Debug.Log("wall destroy in triggerstay");

                //gameObject.SetActive(false);
            }
        }

        //Destroy(other.gameObject);      // this works, destroys the thing

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("BOMB"))
        {
            if (other.gameObject.GetComponent<BombChase>().getExploded() == true)
            {
                //Debug.Log("wall destroy in triggerexit");
                //gameObject.SetActive(false);
            }
            //gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("BOMB"))
        {
            triggered = true;
            this.other = other;
        }

    }

}
