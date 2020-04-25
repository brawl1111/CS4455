using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour
{
    public GameObject flag;
    private Animation flagRaise;

    // Start is called before the first frame update
    void Start()
    {
        flagRaise = flag.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            flagRaise.Play();
        }
    }
}
