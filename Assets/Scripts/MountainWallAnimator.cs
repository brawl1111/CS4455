using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainWallAnimator : MonoBehaviour
{
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
    }
}
