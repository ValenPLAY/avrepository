using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingHouse : MonoBehaviour
{
    enum states
    {
        Begining,
        InGame,
        AfterGame,
        GameFinished,
    }

    states currentState;

    [Header("Basic Setup")]

    public Camera mainCamera;
    public GameObject blockPrefab;
    public GameObject housePrefab;
    private GameObject buildingHouseParent;
    private HousePrefab currentHousePrefab;
    private Vector3 startingCameraPosition;
    private Vector3 startingCameraRotation;
    public Transform houseCameraTransform;
    private GameObject currentBlock;

    [Header("Decoration Setup")]

    public GameObject smokePrefab;
    public GameObject cutSide;
    public Material houseMaterial;

    [Header("Score Setup")]
    public int score = 0;
    private Text currentScoreText;

    [Header("In-game Variables")]
    public List<GameObject> blocks = new List<GameObject>();
    public int movingDirection;
    public float speed = 2.0f;
    public float spawnDistance = 5.0f;
    public Vector3 movingVector;
    private Vector3 newBlockScale;
    private Vector3 decreasedScale;
    private float gameCameraMoveDelay;

    // Start is called before the first frame update
    void Start()
    {
        GameStart();
        CreateBlock();
    }

    // Update is called once per frame

    void GameStart()
    {
        if (blockPrefab == null)
        {
            //blockPrefab = EmptyBlockGenerator.generateCube();
            //blockPrefab.GetComponent<Renderer>().material = houseMaterial;
            //cutSide = blockPrefab;
        }

        if (buildingHouseParent == null)
        {
            buildingHouseParent = Instantiate(housePrefab, transform.position + new Vector3(0, 0, 0), Quaternion.identity);
            //buildingHouseParent.transform.parent = gameObject.transform;
            currentHousePrefab = buildingHouseParent.GetComponent<HousePrefab>();
            //houseCameraTransform = currentHousePrefab.cameraPosition;
        }

        if (cutSide == null) cutSide = blockPrefab;

        currentState = states.Begining;
        mainCamera = FindObjectOfType<Camera>();
        var currentScoreGameObject = GameObject.FindGameObjectWithTag("ScoreCounter");
        currentScoreText = currentScoreGameObject.GetComponent<Text>();
        startingCameraPosition = mainCamera.transform.position;
        startingCameraRotation = mainCamera.transform.rotation.eulerAngles;

    }

    void PreparationPhase()
    {
        if (gameCameraMoveDelay <= 1.0f)
        {
            gameCameraMoveDelay += Time.deltaTime * 0.5f;
            mainCamera.transform.position = Vector3.Lerp(startingCameraPosition, houseCameraTransform.position, gameCameraMoveDelay);
            mainCamera.transform.rotation = Quaternion.Euler(Vector3.Lerp(startingCameraRotation, houseCameraTransform.rotation.eulerAngles, gameCameraMoveDelay));

            //Debug.Log(gameCameraMoveDelay);
        }
        else
        {
            currentState = states.InGame;
        }

    }

    void Update()
    {
        if (currentState == states.Begining) PreparationPhase();


        if (currentState == states.InGame)
        {
            CameraAutoMove();
            if (Input.GetButtonDown("Jump") || (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)) CreateBlock();
            BlockMove();
        }

        if (currentState == states.AfterGame) AfterGame();


    }

    void AfterGame()
    {
        if (gameCameraMoveDelay <= 1)
        {
            gameCameraMoveDelay += Time.deltaTime * 0.3f;
            mainCamera.transform.position = Vector3.Lerp(startingCameraPosition, houseCameraTransform.position, gameCameraMoveDelay);

            mainCamera.transform.LookAt(blocks[blocks.Count - 1].transform);
        }
        else
        {
            currentState = states.GameFinished;
            currentHousePrefab.levels = score;
            Destroy(gameObject);
        }

    }

    void CameraAutoMove()
    {
        //float cameraHeight = blockPrefab.transform.localScale.y * (blocks.Count);
        float cameraHeight = blocks[blocks.Count - 1].transform.position.y + blocks[blocks.Count - 1].transform.localScale.y;
        if (mainCamera.transform.position.y < cameraHeight)
        {
            mainCamera.transform.position += (Vector3.up * Time.deltaTime);
        }
    }

    void CreateBlock()
    {

        if (currentBlock != null && blocks.Count >= 1)
        {
            if (blocks.Count > 1)
            {
                Vector3 decreaseSize = currentBlock.transform.position - blocks[blocks.Count - 2].transform.position;

                decreaseSize.y = 0.0f;
                decreasedScale = currentBlock.transform.localScale;
                decreasedScale.x -= Mathf.Abs(decreaseSize.x);
                decreasedScale.z -= Mathf.Abs(decreaseSize.z);

                if ((decreasedScale.x < 0 || decreasedScale.z < 0) && blocks.Count > 1)
                {
                    GameOver();
                    return;
                }

                ScoreUpdate();

                currentBlock.transform.localScale = decreasedScale;
                currentBlock.transform.localPosition -= decreaseSize / 2;

                if (Mathf.Abs(decreaseSize.x) > 0 || Mathf.Abs(decreaseSize.z) > 0) CreateCutSide(decreaseSize, true);

                GameObject smoke = Instantiate(smokePrefab, currentBlock.transform.position, Quaternion.identity);
                smoke.transform.localScale = currentBlock.transform.localScale;
                Destroy(smoke, 3);
            }
            newBlockScale = currentBlock.transform.localScale;
            Vector3 newBlockPosition = blocks[blocks.Count - 1].transform.position;
            newBlockPosition.y = blocks[0].transform.position.y + newBlockScale.y * blocks.Count;
            SpawnBlock(newBlockPosition);


            movingDirection = Random.Range(0, 2);
            if (movingDirection == 0) movingVector = new Vector3(-1, 0, 0);
            if (movingDirection == 1) movingVector = new Vector3(0, 0, -1);

            //newBlockScale.y = blockPrefab.transform.localScale.y;
            currentBlock.transform.localScale = newBlockScale;

            speed = Random.Range(0.8f, 1.4f + (blocks.Count / 10));
            currentBlock.transform.position += -movingVector * spawnDistance * speed;
        }
        else
        {
            SpawnBlock(buildingHouseParent.transform.position);
            newBlockScale = gameObject.transform.localScale;
            currentBlock.transform.localScale = newBlockScale;

            CreateBlock();
        }

    }

    void SpawnBlock(Vector3 spawnPosition)
    {
        GameObject createdBlock;
        if (blockPrefab != null)
        {
            createdBlock = Instantiate(blockPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            createdBlock = EmptyBlockGenerator.generateCube();
            createdBlock.transform.position = spawnPosition;
            createdBlock.GetComponent<Renderer>().material = houseMaterial;
        }
        createdBlock.transform.parent = buildingHouseParent.transform;
        blocks.Add(createdBlock);
        currentBlock = createdBlock;
    }

    void BlockMove()
    {
        if (currentBlock != null)
        {
            currentBlock.transform.Translate(movingVector * speed * Time.deltaTime);

            Vector3 blockOutOfBoundsCalculation = currentBlock.transform.position + currentBlock.transform.localScale - blocks[blocks.Count - 2].transform.position;
            if (blockOutOfBoundsCalculation.x < 0 || blockOutOfBoundsCalculation.z < 0)
            {
                GameOver();
            }

        }
    }

    void GameOver()
    {
        currentState = states.AfterGame;
        if (currentBlock != null)
        {
            blocks.Remove(blocks[blocks.Count - 1]);
            CreateCutSide(currentBlock.transform.localScale, false);
            Destroy(currentBlock.gameObject);

        }
        gameCameraMoveDelay = 0.0f;
        startingCameraPosition = mainCamera.transform.position;
        startingCameraRotation = mainCamera.transform.rotation.eulerAngles;
    }

    void ScoreUpdate()
    {
        score++;
        if (currentScoreText != null) currentScoreText.text = score.ToString();
    }

    void CreateCutSide(Vector3 decreaseSize, bool isChangePosition)
    {
        GameObject createdCutSide;
        if (cutSide != null)
        {
            createdCutSide = Instantiate(cutSide, currentBlock.transform.position, Quaternion.identity);
        }
        else
        {
            createdCutSide = EmptyBlockGenerator.generateCube();
            createdCutSide.transform.position = currentBlock.transform.position;
            createdCutSide.GetComponent<Renderer>().material = houseMaterial;
        }

        if (createdCutSide.GetComponent<Rigidbody>() == null) createdCutSide.AddComponent<Rigidbody>();
        createdCutSide.transform.localScale = newBlockScale;
        Vector3 cutSizeScale = createdCutSide.transform.localScale;
        if (Mathf.Abs(decreaseSize.x) > 0) cutSizeScale.x = Mathf.Abs(decreaseSize.x);
        if (Mathf.Abs(decreaseSize.z) > 0) cutSizeScale.z = Mathf.Abs(decreaseSize.z);

        if (isChangePosition) createdCutSide.transform.position += decreaseSize;

        createdCutSide.transform.localScale = cutSizeScale;
        Destroy(createdCutSide, 3);

    }
}
