using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public AudioClip treeCrack;
    public AudioClip treeHitGround;
    public AudioSource audioSource;
    public GameObject particles;

    private bool isFalling = false;

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
        // Gets the player object and checks if they are currently spinning
        // If so, we're gonna start the falling animation
        if (collider.CompareTag("Hurtbox") && !isFalling)
        {
            gameObject.GetComponent<Animation>().Play("Tree Fall");
            EventManager.TriggerEvent<TreeCrackEvent, Vector3>(this.gameObject.transform.position);
            Instantiate(particles, transform.position, Quaternion.identity);
            isFalling = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // We'll want to play
        // A sound to suggest that the tree has finished moving
        // In this case, Ground is eqivalent to layer 8
        // if (collision.gameObject.CompareTag("Hurtbox") && !isFalling)
        // {
        //     gameObject.GetComponent<Animation>().Play("Tree Fall");
        //     EventManager.TriggerEvent<TreeCrackEvent, Vector3>(this.gameObject.transform.position);
        //     Instantiate(particles, collision.GetContact(0).point, Quaternion.identity);
        //     isFalling = true;
        //     return;
        // }
        GameObject collider = collision.gameObject;
        if (collider.gameObject.layer == 10 && collider.gameObject.name != "Stump_3_3")
        {
            EventManager.TriggerEvent<TreeHitGroundEvent, Vector3>(this.gameObject.transform.position);
            StartCoroutine(WasteTimeCoroutine());
        }
    }

    IEnumerator WasteTimeCoroutine()
    {
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<Rigidbody>().constraints =
            gameObject.GetComponent<Rigidbody>().constraints | // We want to preserve the constrains already in place
            RigidbodyConstraints.FreezePositionX |
            RigidbodyConstraints.FreezePositionY |
            RigidbodyConstraints.FreezePositionZ |
            RigidbodyConstraints.FreezeRotationZ;
    }
}