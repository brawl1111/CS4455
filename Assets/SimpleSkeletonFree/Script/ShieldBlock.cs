using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBlock : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Hurtbox"))
        {
            Debug.Log("Front Hit");
        }
    }
}
