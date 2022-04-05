using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    [Header("Starting level")]
    public GameObject startingCell;

    [Header("Generated level lists")]
    public List<GameObject> levelPrefabs = new List<GameObject>();
    public List<GameObject> rareLevelPrefabs = new List<GameObject>();
    public List<GameObject> monsterLevelPrefabs = new List<GameObject>();

    [Header("Generation Settings")]
    public int safeLevels = 10;
    public float rareLevelChance = 0.2f;
    public float monsterLevelChance = 0.1f;
    public float monsterChancePerLevelMultiplier = 1.02f;

    public int preloadedLevelsAmount = 2;


    [Header("Current levels")]
    public List<GameObject> levels = new List<GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        levels.Add(startingCell);
        for (int x = 1; x < preloadedLevelsAmount; x++)
        {
            FloorCreate(x, 0);
        }
    }

    public void FloorCheck(int currentLevel)
    {
        if (currentLevel + preloadedLevelsAmount >= levels.Count)
        {
            levels.Add(new GameObject());
            FloorCreate(levels.Count - 1, currentLevel);
        }

        int TopFloorID = currentLevel - preloadedLevelsAmount;
        int BotFloorID = currentLevel + preloadedLevelsAmount + 1;
        if (TopFloorID >= 0 && levels[TopFloorID] != null)
        {
            FloorDestroy(TopFloorID);
        }


        if (BotFloorID < levels.Count)
        {
            if (levels[BotFloorID] != null)
            {
                FloorDestroy(BotFloorID);
            }
        }

        if (levels[BotFloorID - 1] == null)
        {
            FloorCreate(BotFloorID - 1, currentLevel);
        }

        if (TopFloorID >= -1 && levels[TopFloorID + 1] == null)
        {
            FloorCreate(TopFloorID + 1, currentLevel);
        }
    }

    public void FloorCreate(int createdLevelID, int currentLevel)
    {
        //Debug.Log("Count: " + levels.Count);
        GameObject newLevel = Instantiate(createdFloor());
        //levels.Add(newLevel);
        if (createdLevelID > levels.Count - 1)
        {
            levels.Add(newLevel);
        }
        else
        {
            Destroy(levels[createdLevelID]);
            levels[createdLevelID] = newLevel;
        }

        Floor level = levels[createdLevelID].GetComponent<Floor>();
        if (currentLevel < createdLevelID)
        {
            level.transform.position = levels[createdLevelID - 1].GetComponent<Floor>().botPoint.transform.position;
            level.transform.position -= level.topPoint.transform.localPosition;
        }
        else
        {
            level.transform.position = levels[createdLevelID + 1].GetComponent<Floor>().topPoint.transform.position;
            level.transform.position -= level.botPoint.transform.localPosition;
        }
        level.floorNumber.text = createdLevelID.ToString();
        level.levelNumber = createdLevelID;
    }

    public void FloorDestroy(int destroyedLevelID)
    {
        Destroy(levels[destroyedLevelID]);
        //Debug.Log(levels[destroyedLevelID]);
    }

    GameObject createdFloor()
    {
        int selectedPrefab;
        GameObject generatedFloor;

        float randomRoomChance = Random.Range(0.0f, 1.0f);
        if (rareLevelPrefabs.Count > 0 && randomRoomChance <= rareLevelChance && levels.Count > safeLevels)
        {
            selectedPrefab = Random.Range(0, rareLevelPrefabs.Count);
            generatedFloor = rareLevelPrefabs[selectedPrefab];
            Debug.Log("Rare Level Generated!");
            return generatedFloor;
        }

        randomRoomChance = Random.Range(0.0f, 1.0f);
        if (monsterLevelPrefabs.Count > 0 && randomRoomChance <= monsterLevelChance * (levels.Count * monsterChancePerLevelMultiplier) && levels.Count > safeLevels)
        {
            selectedPrefab = Random.Range(0, monsterLevelPrefabs.Count);
            generatedFloor = monsterLevelPrefabs[selectedPrefab];
            Debug.Log("Monster Level Generated!");
            return generatedFloor;
        }

        selectedPrefab = Random.Range(0, levelPrefabs.Count);
        generatedFloor = levelPrefabs[selectedPrefab];
        return generatedFloor;

    }
}
