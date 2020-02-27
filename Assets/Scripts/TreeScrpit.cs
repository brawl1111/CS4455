using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScrpit : MonoBehaviour
{
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
        // Gets the player object and checks if they are currently spinning
        // If so, we're gonna start the falling animation
        GameObject collider = collision.gameObject;
        if (collider.gameObject == GameObject.Find("PlayerContainer"))
        {
            GameObject player = collider.gameObject.transform.GetChild(1).gameObject;
            if (player == GameObject.Find("Player") && player.GetComponent<SpinAttack>().getIsSpinning())
            {
                gameObject.GetComponent<Animation>().Play("Tree Fall");
            }
        }
    }
}
