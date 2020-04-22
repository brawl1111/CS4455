using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
	// private int health = 3;
    private HeartPopulator heartPopulator;
	public static HealthManager Instance
	{
		get {return s_Instance;}
	}
	private static HealthManager s_Instance;
    private GameOverMenuToggle gameOverToggle;
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
        gameOverToggle = GameObject.Find("GameOverCanvas").GetComponent<GameOverMenuToggle>();
        heartPopulator = GameObject.Find("HeartContainer").GetComponent<HeartPopulator>();
        if (PlayerPrefs.HasKey("HealthCount") && SceneManager.GetActiveScene().name == "Section1&2")
        {
            PlayerPrefs.DeleteKey("HealthCount");
        }
    }

    void Start()
    {
        // If we're starting in the forest, basically
        if (!PlayerPrefs.HasKey("HealthCount"))
        {
            PlayerPrefs.SetInt("HealthCount", 3);
        }
        heartPopulator.AddHeartCount(GetHealth());
    }

    public int GetHealth()
    {
    	return PlayerPrefs.GetInt("HealthCount");
    }


    public void AddHealth(int i)
    {
        for (int j = 0; j < i; j++)
        {
            PlayerPrefs.SetInt("HealthCount", GetHealth() + 1);
            heartPopulator.AddHeartCount(GetHealth());
        }
        // A little bit of reviving action
        if (!isAlive)
        {
            isAlive = true;
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<Animator>().SetTrigger("hasRevived");
            CharacterMovement cm = player.GetComponent<CharacterMovement>();
            cm.enabled = true;
            gameOverToggle.GameOverMenuOff();
        }
    }

    public void SubtractHealth(int i)
    {
        //Debug.Log("isalive: " + isAlive + " InvincibilityFrames: " + inInvincibilityFrames);
        if (!inInvincibilityFrames && isAlive)
        {
            int health = GetHealth();
            health -= i;
            PlayerPrefs.SetInt("HealthCount", health);
            heartPopulator.RemoveHeartCount(health);
            Debug.Log("health: " + health);
            inInvincibilityFrames = true;
            EventManager.TriggerEvent<PlayerHurtSFXEvent, Vector3>(this.transform.position);
            if (health == 0) {
                HandleDeath();
            }
            else
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
        SubtractHealth(GetHealth());
    }

    public void KillPlayerByFall()
    {
        SubtractHealth(GetHealth());
        HandleDeath();
    }

    private void HandleDeath()
    {
        isAlive = false;
        inInvincibilityFrames = false;
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<Animator>().SetTrigger("hasDied");
        //player.GetComponent<Rigidbody>().isKinematic = true;
        player.GetComponent<CharacterMovement>().enabled = false;
        gameOverToggle.GameOverMenuOn();

    }

    public void RestoreAllHealth()
    {
        AddHealth(3 - GetHealth());
    }

    public void RemoveOneHealth()
    {
        SubtractHealth(1);
    }
    public void RestoreOneHealth()
    {
        AddHealth(1);
    }
    public void SetIsAlive(bool b)
    {
        isAlive = b;
    }
}
