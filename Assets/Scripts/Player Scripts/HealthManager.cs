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

	void Awake()
    {
        Debug.Log("Am I here?");
    	if (s_Instance == null)
    		s_Instance = this;
    	else if (s_Instance != this)
    		throw new UnityException("There cannot be more than one HealthManager script. The instances are " + s_Instance.name + " and " + name + ".");
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
        health -= i;
        GameObject.Find("HeartContainer").GetComponent<HeartPopulator>().RemoveHeartIcon();
    }
}
