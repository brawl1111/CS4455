using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChange : MonoBehaviour
{
    public AudioClip sectionAMusic;
    public AudioClip sectionBMusic;
    private AudioClip currMusic;
    public AudioClip wind;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider c)
    {

    }

    public void VolumeDown()
    {
        GetComponent<Animation>().Play();
    }
}
