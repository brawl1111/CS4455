using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnBullet : MonoBehaviour
{

    public int duration;

    // Start is called before the first frame update
    void Start()
    {
        duration = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine(Despawn());
    }

    private void OnDisable()
    {
        
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSecondsRealtime(duration);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.tag);
        if (!other.CompareTag("Platform"))
        {
            gameObject.SetActive(false);
        }
    }

}
