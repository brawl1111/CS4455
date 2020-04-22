using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
Singleton design for tracking how many llamas the player has collected
*/
public class LlamaCounter : MonoBehaviour
{
    private LlamaPopulator llamaPopulator;
	public static LlamaCounter Instance
	{
		get {return s_Instance;}
	}
	private static LlamaCounter s_Instance;

	void Awake()
    {
    	if (s_Instance == null)
    		s_Instance = this;
    	else if (s_Instance != this)
    		throw new UnityException("There cannot be more than one LlamaCounter script. The instances are " + s_Instance.name + " and " + name + ".");
        llamaPopulator = GameObject.Find("LlamaContainer").GetComponent<LlamaPopulator>();
        if (llamaPopulator == null) Debug.Log("LlamaContainer could not be found");
        if (PlayerPrefs.HasKey("LlamaCount") && SceneManager.GetActiveScene().name == "Section1&2")
        {
            PlayerPrefs.DeleteKey("LlamaCount");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // If we're starting in the forest, basically
        if (!PlayerPrefs.HasKey("LlamaCount"))
        {
            PlayerPrefs.SetInt("LlamaCount", 0);
        }
        // If we're starting in the mountain, basically
        else
        {
            llamaPopulator.AddLlamaCount(GetLlamaCount());
        }
    }

    public void IncrementLlamaCount()
    {
        int llamaCount = GetLlamaCount();
    	PlayerPrefs.SetInt("LlamaCount", ++llamaCount);
        Debug.Log("you now have " + llamaCount + " llamas");
        llamaPopulator.AddLlamaCount(llamaCount);
    }

    public void ThreexLlamaCount()
    {
        IncrementLlamaCount();
        IncrementLlamaCount();
        IncrementLlamaCount();
    }

    public void ResetLlamaCount()
    {
        llamaPopulator.RemoveLlamaCount(GetLlamaCount());
        PlayerPrefs.SetInt("LlamaCount", 0);
    }

    public int GetLlamaCount()
    {
    	return PlayerPrefs.GetInt("LlamaCount");
    }
}
