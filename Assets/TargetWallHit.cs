using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetWallHit : MonoBehaviour
{
    public GameObject particle;
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
            EventManager.TriggerEvent<TargetHitSFXEvent, Vector3>(this.transform.position);
            Instantiate(particle, transform.position, Quaternion.identity);
            gameObject.transform.parent.gameObject.SetActive(false);
        }
    }
}
