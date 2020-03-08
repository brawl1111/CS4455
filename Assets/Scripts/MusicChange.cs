using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChange : MonoBehaviour
{
    public AudioClip sectionAMusic;
    public AudioClip sectionBMusic;
    private AudioClip currMusic;

    // Start is called before the first frame update
    void Start()
    {
        currMusic = sectionAMusic;
        gameObject.GetComponent<AudioSource>().clip = currMusic;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider c)
    {

    }
}
