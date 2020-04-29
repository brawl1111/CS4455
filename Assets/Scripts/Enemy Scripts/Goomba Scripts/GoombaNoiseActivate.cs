using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaNoiseActivate : MonoBehaviour
{

    public GameObject zoneOfCroaking;
    private bool playerInCroakZone = false;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Croak", Random.Range(0.2f, 1f), Random.Range(5f, 8f));
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Croak()
    {
        EventManager.TriggerEvent<CroakSFXEvent, Vector3>(transform.position);
    }
}
