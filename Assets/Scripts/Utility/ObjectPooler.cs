using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    #region Singleton
    public static ObjectPooler Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public List<Pool> pools;
    public Dictionary<string, List<GameObject>> activePoolDictionary;

    private Dictionary<string, Queue<GameObject>> deactivePoolDictionary;

    // Start is called before the first frame update
    void Start()
    {
        activePoolDictionary = new Dictionary<string, List<GameObject>>();
        deactivePoolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            deactivePoolDictionary.Add(pool.tag, objectPool);
            activePoolDictionary.Add(pool.tag, new List<GameObject>());

        }

    } // start

    public GameObject SpawnFromPool(string tag, Vector3 position)
    {

        if (deactivePoolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("pool with tag " + tag + "doesnt exist");
            return null;
        }

        GameObject objectToSpawn = deactivePoolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;

        activePoolDictionary[tag].Add(objectToSpawn);

        return objectToSpawn;
    } // SpawnFromPool

}
