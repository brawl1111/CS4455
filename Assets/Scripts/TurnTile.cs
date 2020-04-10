using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTile : MonoBehaviour
{

    public GameObject bigTile;

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
        if (other.gameObject.CompareTag("Hurtbox"))
        {
            if (Vector3.Distance(transform.position, other.transform.position) < 2)
            {
                transform.Rotate(0, 90, 0);
                bigTile.transform.Rotate(0, 90, 0);
                //bigTile.GetComponent<Animator>().Play("RotateTile90");
                Debug.Log("TurnTile.cs: tile rotated");
            }
        }
    }

}
