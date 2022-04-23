using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    [SerializeField] private Portal _portal = null;
    
    [SerializeField] private Transform _playerSpawn = null;
    [SerializeField] private List<Transform> _enemiesSpawn = new List<Transform>();

    public Portal Portal
    {
        get => _portal;
    }
    
    public Transform PlayerSpawn
    {
        get => _playerSpawn;
    }

    public List<Transform> EnemiesSpawn
    {
        get => _enemiesSpawn;
    }
}
