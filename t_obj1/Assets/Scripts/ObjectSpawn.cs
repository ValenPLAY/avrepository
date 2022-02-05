using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] arrayPrefabs;
    private GameObject createdObject;
    private GameObject selectedObject;
    private float randomRotation;

    static public float tolerant = 0.1f;
    static public bool removeOnSpawn = false;
    static public List<GameObject> spawnedObjects = new List<GameObject>();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            spawnObject();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            removeObject();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            removeAll();
        }
    }

    public void spawnObject() {
        if (removeOnSpawn == true) {
            removeAll();
        }
        var selectedPrefab = Random.Range(0, arrayPrefabs.Length);
        if (arrayPrefabs[selectedPrefab] != null)
        {
            randomRotation = Random.Range(0.0f, 360.0f);
            var instanceRotation = Quaternion.Euler(0.0f, randomRotation, 0.0f);
            var instancePosition = new Vector3(1.0f, 1.0f, 1.0f);
            createdObject = Instantiate(arrayPrefabs[selectedPrefab], instancePosition, instanceRotation);
            spawnedObjects.Add(createdObject);
        }
        else
        {
            Debug.Log("Spawned prefab was a null.");
        }
    }

    public void removeObject()
    {
        if (spawnedObjects.Count > 0)
        {
            Destroy(spawnedObjects[spawnedObjects.Count - 1]);
            spawnedObjects.RemoveAt(spawnedObjects.Count - 1);
        }
        else
        {
            Debug.Log("List is empty");
        }
    }

    private GameObject test;
    public void removeAll() {
        var objectAmount = spawnedObjects.Count;
        for (int counter = 0; counter < objectAmount; counter++) {
            Destroy(spawnedObjects[counter]);
            
        }
    }
}
