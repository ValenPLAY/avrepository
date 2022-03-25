using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public GameObject character;
    public LevelPrefab startingLevel;
    public List<LevelPrefab> levelRandomPrefabs = new List<LevelPrefab>();
    public List<LevelPrefab> createdLevels = new List<LevelPrefab>();
    private LevelPrefab createdPrefab;

    private Vector2 calculatedPosition;
    public int loadedRooms = 3;



    // Start is called before the first frame update
    void Start()
    {
        var generatedRoom = Instantiate(startingLevel, Vector3.zero, Quaternion.identity);
        createdLevels.Add(generatedRoom);
    }

    // Update is called once per frame
    void Update()
    {
        float distanceTillNext = Vector3.Distance(character.transform.position, createdLevels[createdLevels.Count - 1].bondaryRight.position);
        if (distanceTillNext < 15) CreateNext();

        if (Input.GetKeyDown(KeyCode.R))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void CreateNext()
    {
        createdPrefab = Instantiate(levelRandomPrefabs[Random.Range(0, levelRandomPrefabs.Count)]);
        calculatedPosition = createdPrefab.boundaryLeft.transform.position - createdLevels[createdLevels.Count - 1].bondaryRight.position;
        createdPrefab.transform.position -= new Vector3(calculatedPosition.x, calculatedPosition.y);
        createdLevels.Add(createdPrefab);


        if (createdLevels.Count > loadedRooms && createdLevels[createdLevels.Count - loadedRooms] != null)
        {
            Destroy(createdLevels[createdLevels.Count - loadedRooms - 1].gameObject);
            Debug.Log("Check");
        }
    }


}
