using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [Header("Enemy prefabs")] [SerializeField]
    private List<GameObject> _enemes = new List<GameObject>();

    [Header("Levels")] [SerializeField] private List<LevelData> _levels = new List<LevelData>();

    [Header("Loading setting")] [SerializeField]
    private float _loadingDuration = 2f;

    [SerializeField] private Animator _loadingAnim = null;

    [HideInInspector] public EGameStatus GameStatus = EGameStatus.Loading;

    public System.Action WinGame;

    public static GameManager Instance = null;

    private Coroutine _loadingCoroutine = null;

    private LevelData _activeLevel = null;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
            Instance = null;
        }

        Instance = this;

        GameStatus = EGameStatus.Loading;
    }

    private void Start()
    {
        CreateNewLevel();
        GameStatus = EGameStatus.InGame;
    }

    public void EndGame()
    {
        switch (GameStatus)
        {
            case EGameStatus.Win:
                WinGame?.Invoke();
                break;
        }
    }

    public void StartLoading()
    {
        GameStatus = EGameStatus.Loading;
        _loadingCoroutine = StartCoroutine(Loading());
    }

    private IEnumerator Loading()
    {
        _loadingAnim.SetTrigger("StartLoading");
        
        yield return new WaitForSecondsRealtime(1f); // duration animation
        CreateNewLevel();
        
        yield return new WaitForSecondsRealtime(_loadingDuration);
        _loadingAnim.SetTrigger("StartLoading");

        yield return new WaitForSecondsRealtime(_loadingDuration / 2);

        GameStatus = EGameStatus.InGame;

        StopCoroutine(_loadingCoroutine);
        _loadingCoroutine = null;
    }

    private void CreateNewLevel()
    {
        if(_activeLevel != null)
        {
            Destroy(_activeLevel.gameObject);
        }
        
        _activeLevel = Instantiate(_levels[Random.Range(0, _levels.Count)]);

        Player.Instance.transform.position = _activeLevel.PlayerSpawn.position;

        List<GameObject> enemies = new List<GameObject>();
        foreach (var i in _activeLevel.EnemiesSpawn)
        {
            var enemy = Instantiate(_enemes[Random.Range(0, _enemes.Count)], i.position, Quaternion.identity);
            
            enemies.Add(enemy);
        }
        
        Player.Instance.SetNewEnemies(enemies);
    }

    public void DestroyAny(GameObject toDestroy, float time)
    {
        Destroy(toDestroy, time);
    }
}

public enum EGameStatus
{
    Loading,
    Win,
    GameOver,
    InGame,
    Pause
}