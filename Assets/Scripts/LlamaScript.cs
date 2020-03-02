using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LlamaScript : MonoBehaviour
{
    // Which section of the game this llama is in
    public int section;

    private bool collected = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject collider = collision.gameObject;

        if (collider.gameObject == GameObject.Find("Princess") && !collected)
        {
            collider.gameObject.GetComponent<CharacterMovement>().IncrementLlamaCount(section);
            Debug.Log(collider.gameObject.GetComponent<CharacterMovement>().GetLlamaCount(section));
            collected = true;
            this.gameObject.SetActive(false);
        }
    }
}
