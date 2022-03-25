using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public GameObject topPoint;
    public GameObject botPoint;
    public TextMeshPro floorNumber;
    public float floorSize;
    public int levelNumber;

    public List<GameObject> randomPOI = new List<GameObject>();
    public List<GameObject> randomPrefabs = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        floorSize = topPoint.transform.position.y - botPoint.transform.position.y;
        //Debug.Log("Size "+floorSize);
        if (randomPOI.Count > 0 && randomPrefabs.Count > 0)
        {
            for (int selectedPOI = 0; selectedPOI < randomPOI.Count; selectedPOI++) { 
            int selectedPrefab = Random.Range(-5,randomPrefabs.Count);
            if (selectedPrefab>=0)
            {
                    GameObject spawnedPOI = Instantiate(randomPrefabs[selectedPrefab], randomPOI[selectedPOI].transform);
                    spawnedPOI.transform.rotation = Quaternion.Euler(0,Random.Range(0,360),0);
                    spawnedPOI.transform.localScale *= Random.Range(0.8f,1.2f);
            }
            
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
