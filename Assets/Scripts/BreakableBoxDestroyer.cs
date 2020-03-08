using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBoxDestroyer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Hurtbox"))
        {
            EventManager.TriggerEvent<BreakableBoxBreakEvent, Vector3>(this.gameObject.transform.position);
            Destroy(this.gameObject);
        }
    }
}
