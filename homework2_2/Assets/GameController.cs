using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Required Components")]
    public Terrain mainTerrain;
    private Vector3 terrainBounds;

    [Header("Random Objects Spawner")]
    public int numberOfRandomObjects;
    public List<GameObject> spawnedObjects = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        terrainBounds = mainTerrain.terrainData.size;
        if (numberOfRandomObjects > 0 && spawnedObjects.Count > 0)
        {
            for (int x = 0; x < numberOfRandomObjects; x++)
            {
                Vector3 spawnPosition = new Vector3();
                spawnPosition.x = Random.Range(mainTerrain.transform.position.x, mainTerrain.transform.position.x + terrainBounds.x);
                spawnPosition.z = Random.Range(mainTerrain.transform.position.z, mainTerrain.transform.position.z + terrainBounds.z);
                spawnPosition.y = mainTerrain.SampleHeight(spawnPosition);

                GameObject createdObject = Instantiate(spawnedObjects[Random.Range(0, spawnedObjects.Count)], spawnPosition, Quaternion.identity);
            }
        }



    }
}
