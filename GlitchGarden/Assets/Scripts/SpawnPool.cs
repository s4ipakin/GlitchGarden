using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPool : MonoBehaviour
{
    [SerializeField]
    GameObject generatedPrefab;
    GameObject[] objrctsArray;
    [SerializeField]
    int poolSize = 5;
    public Queue<Transform> objectQueue = new Queue<Transform>();
    void Start()
    {
        objrctsArray = new GameObject[poolSize];
        for (int i=0; i<poolSize; i++)
        {
            objrctsArray[i] = Instantiate(generatedPrefab, Vector2.zero, Quaternion.identity) as GameObject;
            Transform trObject = objrctsArray[i].GetComponent<Transform>();
            trObject.parent = transform;
            objectQueue.Enqueue(trObject);
            objrctsArray[i].SetActive(false);
        }
    }

    
    public Transform GenerateFromPool(Vector2 position, Quaternion rotation)
    {
        Transform GetObject = objectQueue.Dequeue();
        GetObject.position = position;
        GetObject.rotation = rotation;
        GetObject.gameObject.SetActive(true);
        objectQueue.Enqueue(GetObject);
        return GetObject;
    }

    
}
