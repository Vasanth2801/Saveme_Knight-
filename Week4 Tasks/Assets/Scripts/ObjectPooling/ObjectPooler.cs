using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
            
    }

    [System.Serializable] 
    public class ObjectPoolItem  
    {
        public string objectTag; 
        public GameObject prefab;
        public int objectSize;    
    }

    public List<ObjectPoolItem> pools;
    public Dictionary<string, Queue<GameObject>> dictionaryofPools;  

    void Start()
    {
        dictionaryofPools = new Dictionary<string, Queue<GameObject>>();

        foreach (ObjectPoolItem item in pools)
        {
            Queue<GameObject> obj = new Queue<GameObject>();

            for (int i = 0; i < item.objectSize; i++)
            {
                GameObject objPool = Instantiate(item.prefab);
                objPool.SetActive(false);
                obj.Enqueue(objPool);
            }

            dictionaryofPools.Add(item.objectTag, obj);
        }
    }

    public GameObject SpawnObjects(string tag, Vector2 position, Quaternion rotation)  
    {
        GameObject objectToSpawn = dictionaryofPools[tag].Dequeue(); 
        objectToSpawn.SetActive(true);                             
        objectToSpawn.transform.position = position;                
        objectToSpawn.transform.rotation = rotation;                


        dictionaryofPools[tag].Enqueue(objectToSpawn);              

        return objectToSpawn;                                       
    }
}