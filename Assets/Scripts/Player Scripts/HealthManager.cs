using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
	private int health = 3;
	public static HealthManager Instance
	{
		get {return s_Instance;}
	}
	private static HealthManager s_Instance;

    private WaitForSeconds invincibilityFramesDuration;
    private bool inInvincibilityFrames = false;
    private bool isAlive = true;

	void Awake()
    {
        //Debug.Log("Am I here?");
    	if (s_Instance == null)
    		s_Instance = this;
    	else if (s_Instance != this)
    		throw new UnityException("There cannot be more than one HealthManager script. The instances are " + s_Instance.name + " and " + name + ".");

        invincibilityFramesDuration = new WaitForSeconds(2f);
    }

    public int GetHealth()
    {
    	return health;
    }


    public void AddHealth(int i)
    {
        health += i;
        GameObject.Find("HeartContainer").GetComponent<HeartPopulator>().AddHeartIcon();
    }

    public void SubtractHealth(int i)
    {
        if (!inInvincibilityFrames && isAlive)
        {    
            health -= i;
            GameObject.Find("HeartContainer").GetComponent<HeartPopulator>().RemoveHeartIcon();
            Debug.Log("health: " + health);
            inInvincibilityFrames = true;
            if (health == 0) {
                isAlive = false;
            	GameObject player = GameObject.FindWithTag("Player");
            	player.GetComponent<Animator>().SetTrigger("hasDied");
            	player.GetComponent<Rigidbody>().isKinematic = true;
                player.GetComponent<CharacterMovement>().enabled = false;
            } else 
            {
                StartCoroutine(InvincibilityFrames());
            }
        }
    }


    IEnumerator InvincibilityFrames()
    {
        yield return invincibilityFramesDuration;
        inInvincibilityFrames = false;
    }


    public void Update()
    {
        //Kill player or restore/remove health for testing purposes
        if (Input.GetKeyDown("p")) KillPlayer();
        if (Input.GetKeyDown("o")) RestoreAllHealth();
        if (Input.GetKeyDown("i")) RemoveOneHealth();
        if (Input.GetKeyDown("u")) RestoreOneHealth();
    }

    public void KillPlayer()
    {
        SubtractHealth(health);
    }

    public void RestoreAllHealth()
    {
        AddHealth(3 - health);
    }

    public void RemoveOneHealth()
    {
        SubtractHealth(1);
    }
    public void RestoreOneHealth()
    {
        AddHealth(1);
    }
}
