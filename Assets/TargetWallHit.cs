using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetWallHit : MonoBehaviour
{
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
        //Debug.Log(other.gameObject.tag);
        if (other.CompareTag("Bullet"))
        {
            //Debug.Log("target hit");
            gameObject.transform.parent.gameObject.SetActive(false);
        }
    }
}
