using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject player;
    private PlayerCharacter playerController;
    public GameObject startingCell;
    public List<GameObject> levelPrefabs = new List<GameObject>();
    public List<GameObject> levels = new List<GameObject>();

    public int preloadedLevelsAmount = 2;

    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerCharacter>();
        levels.Add(startingCell);
        for (int x = 1; x < preloadedLevelsAmount; x++)
        {
            FloorCreate(x);
            //Debug.Log(level.floorSize);
            // - levels[x].GetComponent<Floor>().topPoint.transform.position;
            //Debug.Log(level.botPoint.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            var destroyedFloor = levels[1];
            levels.RemoveAt(1);
            Destroy(destroyedFloor);
            //DestroyImmediate(destroyedFloor,true);
        }

        if(playerController.currentLevel+preloadedLevelsAmount>=levels.Count)
        {
            //levels.Add(new GameObject());

            levels.Add(new GameObject());
            FloorCreate(levels.Count-1);
        }

        int TopFloorID = playerController.currentLevel - preloadedLevelsAmount;
        int BotFloorID = playerController.currentLevel + preloadedLevelsAmount + 1;
        if (TopFloorID >= 0 && levels[TopFloorID] != null)
        {
            FloorDestroy(TopFloorID);
        }


        if (BotFloorID<levels.Count) { 
        if (levels[BotFloorID] != null)
        {
            FloorDestroy(BotFloorID);
        }
        }

        if (levels[BotFloorID - 1] == null)
        {
            FloorCreate(BotFloorID-1);
        }

        if (TopFloorID>=-1 && levels[TopFloorID+1]==null)
        {
            FloorCreate(TopFloorID+1);
        }
    }

    public void FloorCreate(int createdLevelID)
    {
        //Debug.Log("Count: " + levels.Count);
        int selectedPrefab = Random.Range(0, levelPrefabs.Count);
        GameObject newLevel = Instantiate(levelPrefabs[selectedPrefab]);
        //levels.Add(newLevel);
        if (createdLevelID > levels.Count-1)
        {
            levels.Add(newLevel);
        } else
        {
            Destroy(levels[createdLevelID]);
            levels[createdLevelID] = newLevel;
        }
        
        Floor level = levels[createdLevelID].GetComponent<Floor>();
        if (playerController.currentLevel<createdLevelID)
        {
            level.transform.position = levels[createdLevelID - 1].GetComponent<Floor>().botPoint.transform.position;
            level.transform.position -= level.topPoint.transform.localPosition;
        } else
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
}
