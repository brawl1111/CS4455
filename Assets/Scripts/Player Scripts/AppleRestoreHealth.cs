using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleRestoreHealth : MonoBehaviour
{
	public int healthRestored = 1;
	public GameObject heartParticles;

	void Start()
	{
		gameObject.SetActive(true);
	}
    void OnTriggerEnter(Collider col)
    {
    	if (col.gameObject.CompareTag("Player"))
    	{
            HealthManager.Instance.AddHealth(healthRestored);
            Instantiate(heartParticles, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            EventManager.TriggerEvent<EatAppleSFXEvent, Vector3>(this.transform.position);

        }
    }
}
