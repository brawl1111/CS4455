using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainWallAnimator : MonoBehaviour
{

    public GameObject shockwaveParticles;
    public GameObject smokeParticles;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DropWall()
    {
        EventManager.TriggerEvent<MtnFallSFXEvent, Vector3>(transform.position);
        GetComponent<Animation>().Play("Mtn Wall Fall");
        StartCoroutine(GenerateShockwave());
    }

    IEnumerator GenerateShockwave()
    {
        yield return new WaitForSeconds(6.7f);
        Instantiate(shockwaveParticles, transform.GetChild(0).transform.position + new Vector3(0f, -4f, 0f), Quaternion.identity);
    }
}
