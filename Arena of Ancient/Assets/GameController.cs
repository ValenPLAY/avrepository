using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    private enum gameState
    {
        inGame,
        paused,
        betweenWave,
    }

    gameState currentGameState = gameState.inGame;

    [Header("Player Information")]
    public Hero selectedHero;
    public Vector3 playerWorldMousePos;
    public Camera mainCamera;

    [Header("Hero Movement")]
    public Vector3 inputVector;

    //[Header("Global Variables")]


    [Header("Wave Variables")]
    public int currentWave = 1;
    public float difficulty = 1.0f;
    public List<Unit> enemiesOnMap = new List<Unit>();
    //public List<Unit> enemiesToSpawn = new List<Unit>();

    public Unit waveEnemy;
    public int enemiesToSpawnNumber;
    public int enemiesPerWave = 1;
    public int enemiesStartCount = 1;

    public float inBetweenWaveTimer = 5.0f;
    private float inBetweenWaveTimerCurrent;

    [Header("Enemy Spawn Variables")]
    public float inBetweenEnemyTimer = 3.0f;
    private float inBetweenEnemyTimerCurrent;
    private Vector3 spawnPoint;

    [Header("Selection Variables")]
    public Unit lookingAtUnit;
    public Unit targetUnit;

    [Header("Prefab Variables")]
    public Arena arena;
    public InfoPanel infoPanelPrefab;
    private InfoPanel currentInfoPanel;


    // Start is called before the first frame update
    void Awake()
    {
        mainCamera = Camera.main;

        if (selectedHero == null)
        {
            selectedHero = FindObjectOfType<Hero>();
        }

        if (arena == null)
        {
            GameObject.FindGameObjectWithTag("Arena");
        }

        enemiesToSpawnNumber = enemiesStartCount;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerCameraUpdate();
        PlayerInput();


        if (currentGameState == gameState.inGame)
        {
            if (enemiesToSpawnNumber > 0)
            {
                if (inBetweenEnemyTimerCurrent <= 0)
                {
                    EnemySpawnController.Instance.SpawnEnemy(waveEnemy);
                    inBetweenEnemyTimerCurrent = inBetweenEnemyTimer;
                    enemiesToSpawnNumber--;
                }
                else
                {
                    inBetweenEnemyTimerCurrent -= Time.deltaTime;
                }

            }



            if (enemiesOnMap.Count + enemiesToSpawnNumber == 0)
            {
                EndWave();
            }

        }

        // Next Wave Check
        if (currentGameState == gameState.betweenWave)
        {
            inBetweenWaveTimerCurrent -= Time.deltaTime;
            if (inBetweenWaveTimerCurrent <= 0) NextWave();
        }

        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 0.4f);
    }

    //Wave Specifics
    void EndWave()
    {
        Debug.Log("Wave " + currentWave + " ended!");
        currentGameState = gameState.betweenWave;
        inBetweenWaveTimerCurrent = inBetweenWaveTimer;
        selectedHero.upgradePoints++;

    }

    void NextWave()
    {
        currentGameState = gameState.inGame;
        currentWave++;
        enemiesToSpawnNumber = enemiesStartCount + (currentWave * enemiesPerWave);
        Debug.Log("Wave " + currentWave + " began!");

    }

    public void SetWave(int specificWave)
    {
        currentWave = specificWave;
    }

    // Player Input and Camera
    private void PlayerCameraUpdate()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            lookingAtUnit = hit.collider.gameObject.GetComponent<Unit>();

            if (lookingAtUnit != null)
            {
                if (targetUnit != lookingAtUnit)
                {
                    targetUnit = lookingAtUnit;
                    playerWorldMousePos = targetUnit.transform.position;
                    //currentInfoPanel = SpawnInfoPanel(targetUnit);
                    Debug.Log("Looking at: " + targetUnit.gameObject.name);
                }
                else
                {
                    playerWorldMousePos = targetUnit.transform.position;
                }
            }

            if (lookingAtUnit == null)
            {
                playerWorldMousePos = hit.point;
                targetUnit = null;

                if (currentInfoPanel != null)
                {
                    Destroy(currentInfoPanel.gameObject);
                }
            }

        }
    }

    private void PlayerInput()
    {
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.z = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Fire1"))
        {
            selectedHero.OrderAttack();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //EnemySpawnController.Instance.SpawnEnemy(waveEnemy);
            selectedHero.TakeDamage(1);
        }
    }

    // Projectile Spawn
    public void SpawnProjectile(Vector3 startPosition, Vector3 endPosition, Unit owner, Projectile projectilePrefab, float damage)
    {
        Projectile firedProjectile = Instantiate(projectilePrefab, startPosition, Quaternion.identity);
        firedProjectile.transform.LookAt(endPosition);
        firedProjectile.projectileOwner = owner;
        firedProjectile.projectileDamage = damage;
        firedProjectile.gameObject.SetActive(true);
    }

    public void SpawnProjectile(Vector3 position, Unit owner, Projectile projectilePrefab, float damage)
    {
        Projectile firedProjectile = Instantiate(projectilePrefab, position, Quaternion.identity);
        firedProjectile.projectileOwner = owner;
        firedProjectile.projectileDamage = damage;
        firedProjectile.gameObject.SetActive(true);
    }

    // Info
    private InfoPanel SpawnInfoPanel(Unit incomingUnit)
    {
        if (infoPanelPrefab != null)
        {
            if (currentInfoPanel != null)
            {
                Destroy(currentInfoPanel.gameObject);
            }

            InfoPanel createdInfoPanel = Instantiate(infoPanelPrefab, incomingUnit.transform);
            createdInfoPanel.transform.position = incomingUnit.transform.position;
            createdInfoPanel.transform.parent = incomingUnit.transform;
            return createdInfoPanel;
        }
        else
        {
            return null;
        }
    }


    // Unit Related
    static void HealUnitPercentage(Unit healedUnit, float percentage)
    {
        healedUnit.HealPercentage(percentage);
    }

    static void HealUnit(Unit healedUnit)
    {
        HealUnitPercentage(healedUnit, 1.0f);
    }
}
