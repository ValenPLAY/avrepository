using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _maxDistanceAttack = 3f;

    [SerializeField] private Weapon _weapon = null;

    [SerializeField] private List<GameObject> _enemies; // GameObject replace Enemy script

    [SerializeField] private Movement _movement = null;

    public static Player Instance = null;

    private GameObject _targetEnemy = null;

    public System.Action PewPew;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
            Instance = null;
        }

        Instance = this;

        Initialize();
    }

    public void SetNewEnemies(List<GameObject> enemies)
    {
        foreach (var enemy in enemies)
        {
            _enemies.Add(enemy);
        }

        Initialize();
    }

    private void Initialize()
    {
        foreach (var enemy in _enemies)
        {
            enemy.GetComponent<Enemy>().DieAction += UpdateEnemyCount;
        }
    }

    private void UpdateEnemyCount(GameObject remove)
    {
        foreach (var enemy in _enemies)
        {
            if (enemy == remove)
            {
                enemy.GetComponent<Enemy>().DieAction -= UpdateEnemyCount;
                _enemies.Remove(enemy);
                break;
            }
        }

        if (_enemies.Count > 0)
        {
            _targetEnemy = null;

            FindEnemy();
        }
        else
        {
            GameManager.Instance.GameStatus = EGameStatus.Win;
            GameManager.Instance.EndGame();
        }
    }

    private void Update()
    {
        if (_movement.MoveStatus == Movement.EMoveStatus.idle && _enemies.Count > 0)
        {
            FindEnemy();

            if (_targetEnemy != null && Vector2.Distance(
                    new Vector2(transform.position.x, transform.position.z),
                    new Vector2(_targetEnemy.transform.position.x, _targetEnemy.transform.position.z)) < _maxDistanceAttack
               )
            {
                Shoot();
            }
        }
        else if (_movement.MoveStatus == Movement.EMoveStatus.walk)
        {
            _weapon.SetDefaultRotation();
        }
    }

    private void Shoot()
    {
        PewPew?.Invoke();
    }

    private void FindEnemy()
    {
        float minDistance = float.MaxValue;

        GameObject targetEnemy = null;

        foreach (var i in _enemies)
        {
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z),
                    new Vector2(i.transform.position.x, i.transform.position.z)) < minDistance)
            {
                minDistance = Vector3.Distance(transform.position, i.transform.position);

                targetEnemy = i;
            }
        }

        SetTargetEnemy(targetEnemy);
    }

    private void SetTargetEnemy(GameObject targetEnemy)
    {
        if (_targetEnemy != null)
        {
            _targetEnemy.GetComponent<EnemyCircle>().ChangeActive();
        }

        _targetEnemy = targetEnemy;
        _targetEnemy.GetComponent<EnemyCircle>().ChangeActive();

        LookAt(_targetEnemy.transform.position);
    }

    private void LookAt(Vector3 position)
    {
        transform.rotation = CalculateDirectionLook(transform.position, position);

        _weapon.transform.LookAt(position);
    }

    private Quaternion CalculateDirectionLook(Vector3 a, Vector3 b)
    {
        Vector3 dir = b - a;
        dir.Normalize();

        float rot = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

        return Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, rot, 0),
            _movement.LookRotationSpeed * Time.deltaTime);
    }
}