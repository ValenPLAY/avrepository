using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCircle : MonoBehaviour
{
    [SerializeField] private GameObject _circle = null;

    public void ChangeActive()
    {
        _circle.SetActive(!_circle.activeSelf);
    }
}
