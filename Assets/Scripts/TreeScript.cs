using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public AudioClip treeCrack;
    public AudioClip treeHitGround;
    public AudioSource audioSource;

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
            isFalling = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // We'll want to play
        // A sound to suggest that the tree has finished moving
        // In this case, Ground is eqivalent to layer 8
        GameObject collider = collision.gameObject;
        if (collider.gameObject.layer == 8 && collider.gameObject.name != "Stump_3_3")
        {
            EventManager.TriggerEvent<TreeHitGroundEvent, Vector3>(this.gameObject.transform.position);
        }
    }
}


/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public AudioClip treeCrack;
    public AudioClip treeHitGround;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collision)
    {
        // Gets the player object and checks if they are currently spinning
        // If so, we're gonna start the falling animation
        GameObject collider = collision.gameObject;
        //Debug.Log(collider.tag);
        if (collider.gameObject.CompareTag("Hurtbox"))
        {
            // GameObject player = collider.gameObject.transform.GetChild(1).gameObject;
            // if (player == GameObject.Find("Player") && player.GetComponent<SpinningAttackPrincess>().getIsSpinning())
            // {
                gameObject.GetComponent<Animation>().Play("Tree Fall");
                EventManager.TriggerEvent<TreeCrackEvent, Vector3>(this.gameObject.transform.position);
            // }
        }
        // // But wait, what about when we hit the ground? We'll want to play
        // // A sound to suggest that the tree has finished moving
        // // In this case, Ground is eqivalent to layer 8
        // else if (collider.gameObject.layer == 8)
        // {
        //     EventManager.TriggerEvent<TreeHitGroundEvent, Vector3>(this.gameObject.transform.position);
        // }
    }

    void onCollisionStay(Collision col)
    {
        // But wait, what about when we hit the ground? We'll want to play
        // A sound to suggest that the tree has finished moving
        // In this case, Ground is eqivalent to layer 8
        Debug.Log(col);
        if (col.gameObject.layer == 8)
        {
            EventManager.TriggerEvent<TreeHitGroundEvent, Vector3>(this.gameObject.transform.position);
        }
    }
}

*/