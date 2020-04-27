using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LlamaScript : MonoBehaviour
{
    // Which section of the game this llama is in
    //public int section;
    public GameObject starParticles;
    private bool collected = false;

    void OnCollisionEnter(Collision collision)
    {
        GameObject collider = collision.gameObject;

        if (collider.gameObject.CompareTag("Player") && !collected)
        {
            //collider.gameObject.GetComponent<CharacterMovement>().IncrementLlamaCount(section);
            //Debug.Log(collider.gameObject.GetComponent<CharacterMovement>().GetLlamaCount(section));
            collected = true;
            EventManager.TriggerEvent<LlamaPickupSFXEvent, Vector3>(this.transform.position);
            Debug.Log("detected llama collision");
            Instantiate(starParticles, transform.position, Quaternion.identity);
            this.gameObject.SetActive(false);
            LlamaCounter.Instance.IncrementLlamaCount();
        }
    }
}
