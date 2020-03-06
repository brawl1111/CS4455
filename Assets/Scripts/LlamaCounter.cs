using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Singleton design for tracking how many llamas the player has collected
*/
public class LlamaCounter : MonoBehaviour
{
	private int llamaCount = 0;
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

        
    }

    // Start is called before the first frame update
    void Start()
    {
        //ResetLlamaCount();   
    }

    public void IncrementLlamaCount()
    {
    	llamaCount++;
        Debug.Log("you now have " + llamaCount + " llamas");
        llamaPopulator.AddLlamaIcon();
    }

    public void ThreexLlamaCount()
    {
        IncrementLlamaCount();
        IncrementLlamaCount();
        IncrementLlamaCount();
    }

    public void ResetLlamaCount()
    {
    	llamaCount = 0;
        //Debug.Log(llamaPopulator);
        llamaPopulator.RemoveLlamaIcons();
    }

    public int GetLlamaCount()
    {
    	return llamaCount;
    }
}
