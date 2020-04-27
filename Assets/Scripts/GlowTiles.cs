using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowTiles : MonoBehaviour
{
    public GameObject bigTile;
    private Renderer tileRender;
    private Renderer[] bigRender;

    void Start()
    {
        tileRender = GetComponent<Renderer>();
        bigRender = bigTile.GetComponentsInChildren<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            tileRender.material.SetColor("_Color", Color.green);

            foreach(Renderer rend in bigRender)
            {
                rend.material.SetColor("_Color", Color.green);
            }              
        }
    }

    private void OnTriggerExit(Collider other)
    {
        tileRender.material.SetColor("_Color", Color.white);
        tileRender.material.SetColor("_Emission", Color.black);

        foreach (Renderer rend in bigRender)
        {
            rend.material.SetColor("_Color", Color.white);
            rend.material.SetColor("_Emission", Color.black);
        }
    }
}
